using KatadZe.ViewModels;
using KatadZe.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace KatadZe
{
	public partial class App : Application
	{
        public static bool IsUserLoggedIn { get; set; }
        public static bool IsUserLoggedInByVk { get; set; }
        public static bool IsUserLoggedInByFacebook { get; set; }
        public App ()
		{
		    InitializeComponent();

            if (!IsUserLoggedIn/* || !IsUserLoggedInByVk || !IsUserLoggedInByFacebook || string.IsNullOrEmpty(LoginResultViewModel.loginResult.Token)*/)
            {
                MainPage = new NavigationPage(new Login())
                {
                    BarBackgroundColor = Color.Black,
                    BarTextColor = Color.White
                };
            }
            else
            {
                MainPage = new MainPage();
            }
    //MainPage = new MainPage();
}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
