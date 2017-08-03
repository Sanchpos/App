using ImageCircle.Forms.Plugin.Abstractions;
using KatadZe.Models;
using KatadZe.Services;
using KatadZe.ViewModels;
using System;
using System.Collections.Generic;
using KatadZe.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KatadZe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        private readonly List<MasterPageItem> masterPageItems;
        public ListView MenuList { get { return menuList; } }

        public Menu()
        {
            BindingContext = new LoginResultViewModel();
            InitializeComponent();

            if (AppSettings.Logged && !AppSettings.LoggedAsGuest)
            {
                masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Главная",
                    Icon = "home.png",
                    TargetType = typeof(Views.Main)
                },
                new MasterPageItem
                {
                    Title = "История",
                    Icon = "history.png",
                    TargetType = typeof(Views.History)
                },
                new MasterPageItem
                {
                    Title = "Избранное",
                    Icon = "favorites.png",
                    TargetType = typeof(Views.Favorites)
                },
                new MasterPageItem
                {
                    Title = "Акции",
                    Icon = "sale.png",
                    TargetType = typeof(Views.Shares)
                },
                new MasterPageItem
                {
                    Title = "Настройки",
                    Icon = "settings.png",
                    TargetType = typeof(Views.Settings)
                },
                new MasterPageItem
                {
                    Title = "О нас",
                    Icon = "about.png",
                    TargetType = typeof(Views.About)
                }
            };
            }
            if (AppSettings.LoggedAsGuest)
            {
                masterPageItems = new List<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Главная",
                    Icon = "home.png",
                    TargetType = typeof(Views.Main)
                },
                    new MasterPageItem
                {
                    Title = "Акции",
                    Icon = "sale.png",
                    TargetType = typeof(Views.Shares)
                },
                     new MasterPageItem
                {
                    Title = "О нас",
                    Icon = "about.png",
                    TargetType = typeof(Views.About)
                }
            };
            } 
            menuList.ItemsSource = masterPageItems;


            //UserData userData = new UserData()
            //{
            //    Name = "Name",
            //    Surname = "Surname",
            //    Points = "999"
            //};
            //BindingContext = userData;
            //LoginResult userData = new LoginResult()
            //{ 
            //};
            //BindingContext = userData;
            
        }
    }
}
