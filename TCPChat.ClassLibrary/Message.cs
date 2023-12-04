using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPChat.ClassLibrary
{
    [Serializable]
    public class Message
    {
        public bool IsImage { get; set; }
        public string Text { get; set; }
        public byte[] ByteArray { get; set; }
        public Message(bool IsImage, string Text , byte[] ByteArray)
        {
            this.IsImage = IsImage;
            this.Text = Text;
            this.ByteArray = ByteArray;
        }
    }
}
