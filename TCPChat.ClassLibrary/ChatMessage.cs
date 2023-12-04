using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
namespace TCPChat.ClassLibrary
{
    [Serializable]
    public class ChatMessage
    {
        public string Username { get; set; }
        public Color UsernameColor { get; set; }
        public string Message { get; set; }
        public bool IsImage { get; set; }
        public byte[] imageBytes { get; set; }
        public ChatMessage(string Username, Color UsernameColor, string Message, bool IsImage, byte[] imageBytes)
        {
            this.Username = Username;
            this.UsernameColor = UsernameColor;
            this.Message = Message;
            this.IsImage = IsImage;
            this.imageBytes = imageBytes;
            
        }
    }
}
