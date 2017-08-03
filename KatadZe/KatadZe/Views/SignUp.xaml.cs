using KatadZe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KatadZe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUp : ContentPage
	{
		public SignUp ()
		{
			InitializeComponent ();
		}

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new SignUpData()
            {
                //Username = usernameEntry.Text,
                FirstName = firstNameEntry.Text,
                LastName = lastNameEntry.Text,
                Email = emailEntry.Text,
                Phone = phoneEntry.Text,
                Password = passwordEntry.Text,
                RePassword = repasswordEntry.Text
            };

            // Sign up logic goes here

            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                 //  App.IsUserLoggedIn = true;
                    App.Current.MainPage = new MainPage();
                    ///Navigation.InsertPageBefore(new MainPage(), Navigation.NavigationStack.First());
                    //await Navigation.PopToRootAsync();
                }
            }
            else
            {
                messageLabel.IsVisible = true;
            }
        }

        bool AreDetailsValid(SignUpData user)
        {
            return (!string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName) && !string.IsNullOrWhiteSpace(user.Email) &&
                !string.IsNullOrWhiteSpace(user.Phone) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.RePassword) && user.Email.Contains("@"));
        }

        private void IsEntryFocused(object sender, FocusEventArgs e)
        {
            messageLabel.IsVisible = false;
        }
    }
}