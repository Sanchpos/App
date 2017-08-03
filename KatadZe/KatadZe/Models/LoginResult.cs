using System;
using KatadZe.Helpers;

namespace KatadZe.Models
{
    public class LoginResult
    {

        public string FirstName
        {
            get { return AppSettings.FirstName; }
            set { AppSettings.FirstName = value; }
        }
        public string LastName
        {
            get { return AppSettings.LastName; }
            set { AppSettings.LastName = value; }
        }
        public string Email
        {
            get { return AppSettings.Email; }
            set { AppSettings.Email = value; }
        }
        public string ImageUrl
        {
            get { return AppSettings.ImageURL; }
            set { AppSettings.ImageURL = value; }
        }
        public string UserId
        {
            get { return AppSettings.UserID; }
            set { AppSettings.UserID = value; }
        }
        public string Token
        {
            get { return AppSettings.Token; }
            set { AppSettings.Token = value; }
        }
        public DateTimeOffset ExpireAt
        {
            get { return AppSettings.TokenExpiteAt; }
            set { AppSettings.TokenExpiteAt = value.DateTime; }
        }

        public LoginState LoginState { get; set; }
        public string ErrorString { get; set; }
    }

    public enum LoginState
    {
        Failed,
        Canceled,
        Success
    }
}