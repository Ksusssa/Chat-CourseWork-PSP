using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TCPChat.ClassLibrary;
namespace TCPChat.Client
{
    /// <summary>
    /// Логика взаимодействия для UserMessagesWindow.xaml
    /// </summary>

    public partial class UserMessagesWindow : Window
    {

        public List<Message> MessageList { get; set; }
        public UserMessagesWindow(string Login, List<Message> messages)
        {
            InitializeComponent();
            UserLoginLabel.Text = $"Messages of {Login}";
            MessageList = messages;
            DataContext = this;
        }
    }
}
