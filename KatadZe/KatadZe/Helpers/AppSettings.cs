using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KatadZe.Helpers
{
    public static class AppSettings
    {
        private static ISettings Settings { get { return CrossSettings.Current; } }

        #region Setting Constants

        private const string FirstNameKey = "firstname_key";
        private static readonly string FirstNameDefault = string.Empty;

        private const string LastNameKey = "lastname_key";
        private static readonly string LastNameDefault = string.Empty;

        private const string EmailKey = "email_key";
        private static readonly string EmailDefault = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string PasswordDefault = string.Empty;

        private const string ImageURLKey = "imageurl_key";
        private static readonly string ImageURLDefault = string.Empty;

        private const string LoggedViaFacebookKey = "facebook_key";
        private static readonly bool LoggedViaFacebookDefault = false;

        private const string LoggedViaVkontakteKey = "vkontakte_key";
        private static readonly bool LoggedViaVkontakteDefault = false;

        private const string LoggedAsGuestKey = "guest_key";
        private static readonly bool LoggedAsGuestDefault = false;

        private const string LoggedNormallyKey = "loggednormally_key";
        private static readonly bool LoggedNormallyDefault = false;

        #endregion


        #region Public Properties

        public static string FirstName
        {
            get { return Settings.GetValueOrDefault(FirstNameKey, FirstNameDefault); }
            set { Settings.AddOrUpdateValue(FirstNameKey, value); }
        }

        public static string LastName
        {
            get { return Settings.GetValueOrDefault(LastNameKey, LastNameDefault); }
            set { Settings.AddOrUpdateValue(LastNameKey, value); }
        }

        public static string Email
        {
            get { return Settings.GetValueOrDefault(EmailKey, EmailDefault); }
            set { Settings.AddOrUpdateValue(EmailKey, value); }
        }

        public static string Password
        {
            get { return Settings.GetValueOrDefault(PasswordKey, PasswordDefault); }
            set { Settings.AddOrUpdateValue(PasswordKey, value); }
        }

        public static string ImageURL
        {
            get { return Settings.GetValueOrDefault(ImageURLKey, ImageURLDefault); }
            set { Settings.AddOrUpdateValue(ImageURLKey, value); }
        }

        public static bool LoggedViaFacebook
        {
            get { return Settings.GetValueOrDefault(LoggedViaFacebookKey, LoggedViaFacebookDefault); }
            set { Settings.AddOrUpdateValue(LoggedViaFacebookKey, value); }
        }

        public static bool LoggedViaVkontakte
        {
            get { return Settings.GetValueOrDefault(LoggedViaVkontakteKey, LoggedViaVkontakteDefault); }
            set { Settings.AddOrUpdateValue(LoggedViaVkontakteKey, value); }
        }

        public static bool LoggedAsGuest
        {
            get { return Settings.GetValueOrDefault(LoggedAsGuestKey, LoggedAsGuestDefault); }
            set { Settings.AddOrUpdateValue(LoggedAsGuestKey, value); }
        }

        public static bool LoggedNormally
        {
            get { return Settings.GetValueOrDefault(LoggedNormallyKey, LoggedNormallyDefault); }
            set { Settings.AddOrUpdateValue(LoggedNormallyKey, value); }
        }

        public static bool Logged
        { 
           get { return ( LoggedNormally || LoggedAsGuest || LoggedViaFacebook || LoggedViaVkontakte); }
        }

        #endregion


        public static void RestoreDefaultValues()
        {
            Settings.Clear();
        }
    }
}
