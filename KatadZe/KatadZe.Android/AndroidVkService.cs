using System.Threading.Tasks;
using Android.App;
using KatadZe.Droid;
using VKontakte;
using VKontakte.API;
using Xamarin.Forms;
using KatadZe.Services;
using KatadZe.Models;
using KatadZe.Helpers;
using Org.Json;

[assembly: Dependency(typeof(AndroidVkService))]
namespace KatadZe.Droid
{
    public class AndroidVkService : Java.Lang.Object, IVkService
    {
        public static AndroidVkService Instance => DependencyService.Get<IVkService>() as AndroidVkService;

        readonly string[] permissions = {
            VKScope.Email,
            VKScope.Offline
        };

        TaskCompletionSource<LoginResult> completionSource;
        LoginResult loginResult;

        public Task<LoginResult> Login()
        {
            completionSource = new TaskCompletionSource<LoginResult>();
            VKSdk.Login(Forms.Context as Activity, permissions);
            return completionSource.Task;
        }

        public void Logout()
        {
            loginResult = null;
            completionSource = null;
            AppSettings.RestoreDefaultValues();
            VKSdk.Logout();
        }

        public void SetUserToken(VKAccessToken token)
        {
            loginResult = new LoginResult
            {
                Email = token.Email,
                Token = token.AccessToken,
                UserId = token.UserId,
                ExpireAt = Utils.FromMsDateTime(token.ExpiresIn)
            };

            Task.Run(GetUserInfo);
        }

        async Task GetUserInfo()
        {
            var request = VKApi.Users.Get(VKParameters.From(VKApiConst.Fields, @"photo_400_orig,"));
            var response = await request.ExecuteAsync();
            var jsonArray = response.Json.OptJSONArray(@"response");
            var account = jsonArray?.GetJSONObject(0);
            if (account != null && loginResult != null)
            {
                loginResult.FirstName = account.OptString(@"first_name");
                loginResult.LastName = account.OptString(@"last_name");
                loginResult.ImageUrl = account.OptString(@"photo_400_orig");
                loginResult.LoginState = LoginState.Success;
                SetResult(loginResult);
            }
            else
                SetErrorResult(@"Unable to complete the request of user info");
        }

        public void SetErrorResult(string errorMessage)
        {
            SetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = errorMessage });
        }

        public void SetCanceledResult()
        {
            SetResult(new LoginResult { LoginState = LoginState.Canceled });
        }

        void SetResult(LoginResult result)
        {
            completionSource?.TrySetResult(result);
            loginResult = null;
            completionSource = null;
        }

        private void SetUsetSettings(JSONObject account)
        {
            AppSettings.FirstName = account.OptString(@"first_name");
            AppSettings.LastName = account.OptString(@"last_name");
            AppSettings.Email = loginResult.Email;
            AppSettings.ImageURL = account.OptString(@"photo_400_orig");
            AppSettings.LoggedViaVkontakte = true;
        }
    }
}