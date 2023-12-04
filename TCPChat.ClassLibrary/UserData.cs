using System.Windows.Media;
using System;
using System.Collections.Generic;
namespace TCPChat.ClassLibrary
{
    [Serializable]
    public enum AccountStatus
    {
        Noob,
        Intermidate,
        Professional,
        Admin
    }
    [Serializable]
    public class UserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Color UserColor { get; set; }
        public AccountStatus UserStatus { get; set; }
        public List<Message> UserMessages { get; set; }
        public UserData(string Login, string Password, Color UserColor, AccountStatus UserStatus, List<Message> UserMessages)
        {
            this.Login = Login;
            this.Password = Password;
            this.UserColor = UserColor;
            this.UserStatus = UserStatus;
            this.UserMessages = UserMessages;
        }
    }
}
