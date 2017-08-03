//using FacebookLogin.Views;
using KatadZe.Models;
using KatadZe.Services;
using KatadZe.ViewModels;
using KatadZe.Helpers;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KatadZe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        public Login ()
		{
			InitializeComponent ();
		}
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new SignUpData
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            if (AreCredentialsCorrect(user))
            {
                AppSettings.LoggedNormally = true;
                App.Current.MainPage = new MainPage();
                //Navigation.InsertPageBefore(new MainPage(), this);
                //await Navigation.PopAsync();
            }
            else
            {
                messageLabel.IsVisible = true;
                passwordEntry.Text = string.Empty;
                forgetButton.IsVisible = true;
            }
        }

        bool AreCredentialsCorrect(SignUpData user)
        {
            return user.Email == LoginData.Email && user.Password == LoginData.Password;
        }

        async void OnForgetButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Recovery());
        }

        private void IsEntryFocused(object sender, FocusEventArgs e)
        {
            messageLabel.IsVisible = false;
        }

        async private void OnFacebookButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Facebook());

            LoginResultViewModel.loginResult = await DependencyService.Get<IFacebookService>().Login();

            switch (LoginResultViewModel.loginResult.LoginState)
            {
                case LoginState.Canceled:
                    // Обработать
                    break;
                case LoginState.Success:
                    AppSettings.LoggedViaFacebook = true;
                    App.Current.MainPage = new MainPage();
                    //var str = $"Hi {loginResult.FirstName}! Your email is {loginResult.Email}";
                    break;
                default:
                    // Обработать ошибки
                    break;
            }
        }

        async private void OnVKButtonClicked(object sender, EventArgs e)
        {
            LoginResultViewModel.loginResult = await DependencyService.Get<IVkService>().Login();

            switch (LoginResultViewModel.loginResult.LoginState)
            {
                case LoginState.Canceled:
                    // Обработать
                    break;
                case LoginState.Success:
                    AppSettings.LoggedViaVkontakte = true;
                    App.Current.MainPage = new MainPage();

                    //var str = $"Hi {loginResult.FirstName}! Your email is {loginResult.Email}";
                    break;
                default:
                    // Обработать ошибки
                    break;
            }
        }

        private void OnGuestButtonClicked(object sender, EventArgs e)
        {
            AppSettings.LoggedAsGuest = true;
            AppSettings.FirstName = "Гость";
            App.Current.MainPage = new MainPage();
        }
    }
}