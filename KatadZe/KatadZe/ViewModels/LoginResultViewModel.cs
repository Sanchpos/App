using KatadZe.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KatadZe.ViewModels
{
    class LoginResultViewModel : INotifyPropertyChanged
    {
        public static LoginResult loginResult;

        public LoginResult LoginResult
        {
            get { return loginResult; }
            set
            {
                loginResult = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
