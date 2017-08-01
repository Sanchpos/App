using KatadZe.Models;
using KatadZe.Services;
using KatadZe.Views;
using KatadZe.Helpers;
using System;
using Xamarin.Forms;

namespace KatadZe
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
            Detail = new NavigationPage(new Main())
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };
			InitializeComponent();
            menu.MenuList.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
			var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType))
                {
                    BarBackgroundColor = Color.Black,
                    BarTextColor = Color.White
                };
                menu.MenuList.SelectedItem = null;
                IsPresented = false;
            }
        }

        async private void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            //var apiRequest = "https://www.facebook.com/logout.php?next=https://www.facebook.com/&access_token=" + Facebook.AccessToken;
            //var webView = new WebView
            //{
            //    Source = apiRequest,
            //    HeightRequest = 1
            //};
            //Facebook.content = webView;
            //Main.Content = webView;
            //Facebook.AccessToken = "";
            //var httpClient = new HttpClient();

            //var userJson = await httpClient.GetStringAsync(apiRequest);
            if (AppSettings.LoggedViaFacebook)
            {
                DependencyService.Get<IFacebookService>().Logout();
                AppSettings.LoggedViaFacebook = false;
            }

            if (AppSettings.LoggedViaVkontakte)
            {
                DependencyService.Get<IVkService>().Logout();
                AppSettings.LoggedViaVkontakte = false;
            }
            AppSettings.LoggedNormally = false;
            App.Current.MainPage = new NavigationPage(new Login())
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };

            //Navigation.InsertPageBefore(new Login(), this);
            //await Navigation.PopAsync();
        }

        

        //public ICommand LogoutCommand
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {
        //            var facebookServices = new FacebookServices();
        //            await facebookServices.GetFacebookProfileAsync("");
        //        });
        //    }
        //}
    } 
}
