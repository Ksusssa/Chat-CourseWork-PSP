using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPChat.ClassLibrary
{
    [Serializable]
    public class MessageListData
    {
        public string Login { get; set; }
        public List<Message> Messages { get; set; }
        public MessageListData(string Login , List<Message> Messages)
        {
            this.Login = Login; ;
            this.Messages = Messages;
        }
    }
}
