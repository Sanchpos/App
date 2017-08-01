using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using KatadZe.Droid;
using Org.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using Xamarin.Forms;
using KatadZe.Services;
using KatadZe.Models;
using KatadZe.Helpers;

[assembly: Dependency(typeof(AndroidFacebookService))]
namespace KatadZe.Droid
{
    public class AndroidFacebookService: Java.Lang.Object, IFacebookService, GraphRequest.IGraphJSONObjectCallback, GraphRequest.ICallback, IFacebookCallback
    {
        public static AndroidFacebookService Instance => DependencyService.Get<IFacebookService>() as AndroidFacebookService;

        readonly ICallbackManager callbackManager = CallbackManagerFactory.Create();
        readonly string[] permissions = { @"public_profile", @"email", @"user_about_me" };

        Models.LoginResult loginResult;
        TaskCompletionSource<Models.LoginResult> completionSource;

        public AndroidFacebookService()
        {
            LoginManager.Instance.RegisterCallback(callbackManager, this);
        }

        public Task<Models.LoginResult> Login()
        {
            completionSource = new TaskCompletionSource<Models.LoginResult>();
            LoginManager.Instance.LogInWithReadPermissions(Forms.Context as Activity, permissions);
            return completionSource.Task;
        }

        public void Logout()
        {
            LoginManager.Instance.LogOut();
            AppSettings.RestoreDefaultValues();
        }

        public void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            callbackManager?.OnActivityResult(requestCode, resultCode, data);
        }

        public void OnCompleted(JSONObject data, GraphResponse response)
        {
            OnCompleted(response);
        }

        public void OnCompleted(GraphResponse response)
        {
            if (response?.JSONObject == null)
                completionSource?.TrySetResult(new Models.LoginResult { LoginState = LoginState.Canceled});
            else
            {
                loginResult = new Models.LoginResult
                {
                    FirstName = Profile.CurrentProfile.FirstName,
                    LastName = Profile.CurrentProfile.LastName,
                    Email = response.JSONObject.Has("email") ? response.JSONObject.GetString("email") : string.Empty,
                    ImageUrl = response.JSONObject.GetJSONObject("picture")?.GetJSONObject("data")?.GetString("url"),
                    Token = AccessToken.CurrentAccessToken.Token,
                    UserId = AccessToken.CurrentAccessToken.UserId,
                    ExpireAt = Utils.FromMsDateTime(AccessToken.CurrentAccessToken?.Expires?.Time),
                    LoginState = LoginState.Success
                };

                completionSource?.TrySetResult(loginResult);
                SetUserSettings(response);
            }
        }

        public void OnCancel()
        {
            completionSource?.TrySetResult(new Models.LoginResult { LoginState = LoginState.Canceled });
        }

        public void OnError(FacebookException exception)
        {
            completionSource?.TrySetResult(new Models.LoginResult
            {
                LoginState = LoginState.Failed,
                ErrorString = exception?.Message
            });
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var facebookLoginResult = result.JavaCast<Xamarin.Facebook.Login.LoginResult>();
            if (facebookLoginResult == null) return;

            var parameters = new Bundle();
            parameters.PutString("fields", "id,email,picture.width(1000).height(1000)");
            var request = GraphRequest.NewMeRequest(facebookLoginResult.AccessToken, this);
            request.Parameters = parameters;
            request.ExecuteAsync();
        }

        private void SetUserSettings(GraphResponse response)
        {
            AppSettings.FirstName = Profile.CurrentProfile.FirstName;
            AppSettings.LastName = Profile.CurrentProfile.LastName;
            AppSettings.Email = response.JSONObject.Has("email") ? response.JSONObject.GetString("email") : string.Empty;
            AppSettings.ImageURL = response.JSONObject.GetJSONObject("picture")?.GetJSONObject("data")?.GetString("url");
            AppSettings.LoggedViaFacebook = true;
        }
    }
}