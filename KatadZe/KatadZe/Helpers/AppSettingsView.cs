using System.ComponentModel;

namespace KatadZe.Helpers
{
    // Class was created in order to bind user's data in Menu.cs(and Menu.xaml.cs respectively)
    class AppSettingsView  : INotifyPropertyChanged
    {
        public string FirstName { get { return AppSettings.FirstName; } }

        public string LastName { get { return AppSettings.LastName; } }

        public string Email { get { return AppSettings.Email; } }

        public string ImageURL { get { return AppSettings.ImageURL; } }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
