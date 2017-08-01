using KatadZe.Views;
using KatadZe.Helpers;

using Xamarin.Forms;

namespace KatadZe
{
	public partial class App : Application
	{
        public App()
        {
            InitializeComponent();        
        }

		protected override void OnStart ()
		{
            if (!AppSettings.Logged)
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
