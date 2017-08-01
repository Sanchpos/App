using System.Threading.Tasks;
using Foundation;
using KatadZe.iOS;
using UIKit;
using VKontakte;
using VKontakte.API;
using VKontakte.API.Methods;
using VKontakte.API.Models;
using VKontakte.Core;
using VKontakte.Views;
using Xamarin.Forms;
using KatadZe.Services;
using KatadZe.Models;
using KatadZe.Helpers;

[assembly: Dependency(typeof(AppleVkService))]
namespace KatadZe.iOS
{
    public class AppleVkService : NSObject, IVkService, IVKSdkDelegate, IVKSdkUIDelegate
    {
        readonly string[] permissions = {
            VKPermissions.Email,
            VKPermissions.Offline
        };

        LoginResult loginResult;
        TaskCompletionSource<LoginResult> completionSource;

        public AppleVkService()
        {
            VKSdk.Instance.RegisterDelegate(this);
            VKSdk.Instance.UiDelegate = this;
        }

        public Task<LoginResult> Login()
        {
            completionSource = new TaskCompletionSource<LoginResult>();
            VKSdk.Authorize(permissions);
            return completionSource.Task;
        }

        public void Logout()
        {
            loginResult = null;
            completionSource = null;
        }

        [Export("vkSdkTokenHasExpired:")]
        public void TokenHasExpired(VKAccessToken expiredToken)
        {
            VKSdk.Authorize(permissions);
        }

        public new void Dispose()
        {
            VKSdk.Instance.UnregisterDelegate(this);
            VKSdk.Instance.UiDelegate = null;
            SetCancelledResult();
        }

        public void AccessAuthorizationFinished(VKAuthorizationResult result)
        {
            if (result?.Token == null)
                SetErrorResult(result?.Error?.LocalizedDescription ?? @"VK authorization unknown error");
            else
            {
                loginResult = new LoginResult
                {
                    Token = result.Token.AccessToken,
                    UserId = result.Token.UserId,
                    Email = result.Token.Email,
                    ExpireAt = Utils.FromMsDateTime(result.Token.ExpiresIn),
                };
                Task.Run(GetUserInfo);
            }
        }

        async Task GetUserInfo()
        {
            var request = VKApi.Users.Get(NSDictionary.FromObjectAndKey((NSString)@"photo_400_orig", VKApiConst.Fields));
            var response = await request.ExecuteAsync();
            var users = response.ParsedModel as VKUsersArray;
            var account = users?.FirstObject as VKUser;
            if (account != null && loginResult != null)
            {
                loginResult.FirstName = account.first_name;
                loginResult.LastName = account.last_name;
                loginResult.ImageUrl = account.photo_400_orig;
                loginResult.LoginState = LoginState.Success;
                SetUserSettings(account);
                SetResult(loginResult);
            }
            else
                SetErrorResult(@"Unable to complete the request of user info");
        }

        public void UserAuthorizationFailed()
        {
            SetErrorResult(@"VK authorization unknown error");
        }

        public void ShouldPresentViewController(UIViewController controller)
        {
            Device.BeginInvokeOnMainThread(() => Utils.GetCurrentViewController().PresentViewController(controller, true, null));
        }

        public void NeedCaptchaEnter(VKError captchaError)
        {
            Device.BeginInvokeOnMainThread(() => VKCaptchaViewController.Create(captchaError).PresentIn(Utils.GetCurrentViewController()));
        }

        void SetCancelledResult()
        {
            SetResult(new LoginResult { LoginState = LoginState.Canceled });
        }

        void SetErrorResult(string errorString)
        {
            SetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = errorString });
        }

        void SetResult(LoginResult result)
        {
            completionSource?.TrySetResult(result);
            loginResult = null;
            completionSource = null;
        }

        private void SetUserSettings(VKUser account)
        {
            AppSettings.FirstName = account.first_name;
            AppSettings.LastName = account.last_name;
            AppSettings.Email = loginResult.Email;
            AppSettings.ImageURL = account.photo_400_orig;
        }
    }
}