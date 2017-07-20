using KatadZe.ViewModels;
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
	public partial class Settings : ContentPage
	{
		public Settings ()
		{
            BindingContext = new LoginResultViewModel();
			InitializeComponent ();
		}

        private void IsFirstNameFocused(object sender, FocusEventArgs e)
        {
            FirstNameLabel.IsVisible = false;
            FirstNameEntry.TextColor = Color.Black;
        }

        private void IsFirstNameUnfocused(object sender, FocusEventArgs e)
        {
            FirstNameEntry.TextColor = Color.FromHex("#a3a2a8");
            if (FirstNameEntry.Text == "")
            {
                FirstNameLabel.IsVisible = true;
                FirstNameEntry.Text = LoginResultViewModel.loginResult.FirstName;
            }
        }

        private void IsFirstNameCompleted(object sender, EventArgs e)
        {
            if (FirstNameEntry.Text == "")
            {
                FirstNameLabel.IsVisible = true;
                FirstNameEntry.Text = LoginResultViewModel.loginResult.FirstName;
            }
        }

        private void IsLastNameFocused(object sender, FocusEventArgs e)
        {
            LastNameLabel.IsVisible = false;
            LastNameEntry.TextColor = Color.Black;
        }

        private void IsLastNameUnfocused(object sender, FocusEventArgs e)
        {
            LastNameEntry.TextColor = Color.FromHex("#a3a2a8");
            if (LastNameEntry.Text == "")
            {
                LastNameLabel.IsVisible = true;
                LastNameEntry.Text = LoginResultViewModel.loginResult.LastName;
            }
        }

        private void IsLastNameCompleted(object sender, EventArgs e)
        {
            if (LastNameEntry.Text == "")
            {
                LastNameLabel.IsVisible = true;
                LastNameEntry.Text = LoginResultViewModel.loginResult.LastName;
            }
        }

        private void IsEmailFocused(object sender, FocusEventArgs e)
        {
            EmailLabel.IsVisible = false;
            EmailEntry.TextColor = Color.Black;
        }

        private void IsEmailUnfocused(object sender, FocusEventArgs e)
        {
            EmailEntry.TextColor = Color.FromHex("#a3a2a8");
            if (EmailEntry.Text == "")
            {
                EmailLabel.IsVisible = true;
                EmailEntry.Text = LoginResultViewModel.loginResult.Email;
            }
        }

        private void IsEmailCompleted(object sender, EventArgs e)
        {
            if (EmailEntry.Text == "")
            {
                EmailLabel.IsVisible = true;
                EmailEntry.Text = LoginResultViewModel.loginResult.Email;
            }
        }

        private void IsPhoneFocused(object sender, FocusEventArgs e)
        {
            PhoneLabel.IsVisible = false;
            PhoneEntry.TextColor = Color.Black;
        }

        private void IsPhoneUnfocused(object sender, FocusEventArgs e)
        {
            PhoneEntry.TextColor = Color.FromHex("#a3a2a8");
            if (PhoneEntry.Text == "")
            {
                PhoneLabel.IsVisible = true;
                //PhoneEntry.Text = LoginResultViewModel.loginResult.Phone;
            }
        }

        private void IsPhoneCompleted(object sender, EventArgs e)
        {
            if (PhoneEntry.Text == "")
            {
                PhoneLabel.IsVisible = true;
                //PhoneEntry.Text = LoginResultViewModel.loginResult.Phone;
            }
        }

        private void IsOldPassFocused(object sender, FocusEventArgs e)
        {
            OldPassLabel.IsVisible = false;
            OldPassEntry.TextColor = Color.Black;
        }

        private void IsOldPassUnfocused(object sender, FocusEventArgs e)
        {
            OldPassEntry.TextColor = Color.FromHex("#a3a2a8");
            //if (OldPassEntry.Text != пароль из базы){
            //    OldPassLabel.IsVisible = true;
            //    OldPassEntry.Text = "";
            //}
        }

        private void IsOldPassCompleted(object sender, EventArgs e)
        {
            //if (OldPassEntry.Text != пароль из базы){
            //    OldPassLabel.IsVisible = true;
            //    OldPassEntry.Text = "";
            //}
        }

        private void IsNewPassFocused(object sender, FocusEventArgs e)
        {
            ReNewPassLabel.IsVisible = false;
            NewPassEntry.TextColor = Color.Black;
        }

        private void IsNewPassUnfocused(object sender, FocusEventArgs e)
        {
            NewPassEntry.TextColor = Color.FromHex("#a3a2a8");
        }

        private void IsReNewPassFocused(object sender, FocusEventArgs e)
        {
            ReNewPassLabel.IsVisible = false;
            ReNewPassEntry.TextColor = Color.Black;
        }

        private void IsReNewPassUnfocused(object sender, FocusEventArgs e)
        {
            ReNewPassEntry.TextColor = Color.FromHex("#a3a2a8");
            if (NewPassEntry.Text != ReNewPassEntry.Text)
            {
                ReNewPassLabel.IsVisible = true;
                NewPassEntry.Text = "";
                ReNewPassEntry.Text = "";
            }
        }

        private void IsReNewPassCompleted(object sender, EventArgs e)
        {
            if (NewPassEntry.Text != ReNewPassEntry.Text)
            {
                ReNewPassLabel.IsVisible = true;
                NewPassEntry.Text = "";
                ReNewPassEntry.Text = "";
            }
        }
    }
}