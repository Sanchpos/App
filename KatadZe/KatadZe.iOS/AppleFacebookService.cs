using System.Threading.Tasks;
using Facebook.CoreKit;
using Facebook.LoginKit;
using Foundation;
using KatadZe.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using KatadZe.Services;
using KatadZe.Models;
using KatadZe.Helpers;

[assembly: Dependency(typeof(AppleFacebookService))]
namespace KatadZe.iOS
{
    public class AppleFacebookService: IFacebookService
    {
        readonly LoginManager loginManager = new LoginManager();
        readonly string[] permissions = { @"public_profile", @"email", @"user_about_me" };

        LoginResult loginResult;
        TaskCompletionSource<LoginResult> completionSource;

        public Task<LoginResult> Login()
        {
            completionSource = new TaskCompletionSource<LoginResult>();
            loginManager.LogInWithReadPermissions(permissions, Utils.GetCurrentViewController(), LoginManagerLoginHandler);
            return completionSource.Task;
        }

        public void Logout()
        {
            loginResult = null;
            completionSource = null;
            AppSettings.RestoreDefaultValues();
            loginManager.LogOut();
        }

        void LoginManagerLoginHandler(LoginManagerLoginResult result, NSError error)
        {
            if (result.IsCancelled)
                completionSource?.TrySetResult(new LoginResult {LoginState = LoginState.Canceled});
            else if (error != null)
                completionSource?.TrySetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = error.LocalizedDescription });
            else
            {
                loginResult = new LoginResult
                {
                    Token = result.Token.TokenString,
                    UserId = result.Token.UserID,
                    ExpireAt = result.Token.ExpirationDate.ToDateTime()
                };

                var request = new GraphRequest(@"me", new NSDictionary(@"fields", @"email, first_name, last_name, picture.width(1000).height(1000)"));
                request.Start(GetEmailRequestHandler);
            }
        }


        // didn't create separate method, cause it would be too bulky
        void GetEmailRequestHandler(GraphRequestConnection connection, NSObject result, NSError error)
        {
            if (error != null || loginResult == null)
                completionSource?.TrySetResult(new LoginResult { LoginState = LoginState.Failed, ErrorString = loginResult == null ? "Invalid login sequence": error?.LocalizedDescription });
            else
            {
                var dict = result as NSDictionary;

                if (dict != null)
                {
                    var key = new NSString(@"email");
                    if (dict.ContainsKey(key))
                    {
                        loginResult.Email = dict[key]?.ToString();
                        AppSettings.Email = dict[key]?.ToString();
                    }

                    key = new NSString(@"first_name");
                    if (dict.ContainsKey(key))
                    {
                        loginResult.FirstName = dict[key]?.ToString();
                        AppSettings.FirstName = dict[key]?.ToString();
                    }

                    key = new NSString(@"last_name");
                    if (dict.ContainsKey(key))
                    {
                        loginResult.LastName = dict[key]?.ToString();
                        AppSettings.LastName = dict[key]?.ToString();
                    }


                    key = new NSString(@"picture");
                    if (dict.ContainsKey(key))
                    {
                        loginResult.ImageUrl = dict[key]?.ValueForKeyPath(new NSString("data"))?.ValueForKey(new NSString("url"))?.ToString();
                        AppSettings.ImageURL = loginResult.ImageUrl;
                    }
                } 

                loginResult.LoginState = LoginState.Success;
                AppSettings.LoggedViaFacebook = true;
                completionSource?.TrySetResult(loginResult);
            }
        }
    }
}