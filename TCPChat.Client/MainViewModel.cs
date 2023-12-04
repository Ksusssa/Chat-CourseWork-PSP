using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Text.Json;
using TCPChat.ClassLibrary;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Microsoft.Win32;
namespace TCPChat.Client
{
    public class MainViewModel : ViewModelBase
    {
        public string IP { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 5050;
        public string Nick { get; set; } = "";

        public string Password { get; set; } = "";

        public ObservableCollection<ChatMessage> ChatMessagesListBox { get; set; }
        public string Chat 
        { 
            get => GetValue<string>(); 
            set => SetValue(value);
        }
        public string MessageField { get => GetValue<string>(); set => SetValue(value); }

        private TcpClient? _client;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        public ICommand OnLoginClicked { get; }


        private bool isConnected = false;
        private UserMessagesWindow userMessagesWindow;
        public MainViewModel()
        {
            ChatMessagesListBox = new ObservableCollection<ChatMessage>();
            OnLoginClicked = new RelayCommand(ExecuteOnLoginClicked, (p) => { return true; } );

        }
        private void InitClient()
        {
            if(_client != null && _client.Connected)
            {
                return;
            }

            Listener();
            _client = new TcpClient();
            _client.Connect(IP, Port);
            _reader = new StreamReader(_client.GetStream());
            _writer = new StreamWriter(_client.GetStream());
            
            _writer.AutoFlush = true;
        }
        private void Listener()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (_client?.Connected == true)
                        {
                            var line = _reader?.ReadLine();
                            if(line == null)
                            {
                                _client.Close();
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    ChatMessagesListBox.Add(new ChatMessage("SYSTEM",Color.FromArgb(255,0,175,0),"Соединение с сервером разорвано",false,null));
                                });
                                continue;
                            }
                            if (line.StartsWith("Success:"))
                            {
                                isConnected = true;
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    ChatMessagesListBox.Add(JsonSerializer.Deserialize<ChatMessage>(line.Remove(0, 8))!);
                                });
                            }
                            else if (line.StartsWith("Fail:"))
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    ChatMessagesListBox.Add(JsonSerializer.Deserialize<ChatMessage>(line.Remove(0, 5))!);
                                });
                            }
                            else if (line.StartsWith("MsgLIST:"))
                            {
                                ShowUserMessages(JsonSerializer.Deserialize<MessageListData>(line.Remove(0,8)));
                            }
                            else
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    ChatMessagesListBox.Add(JsonSerializer.Deserialize<ChatMessage>(line)!);
                                });
                            }
                        }
                        Task.Delay(10).Wait();
                    }
                    catch (Exception ex)
                    { 
                        Chat += ex.Message + "\n";
                    }
                }
            });
        }
        public AsyncCommand RegisterCommand
        {
            get
            {
                return new AsyncCommand(() =>
                {
                    return Task.Run(() =>
                    {
                        try
                        {
                            if (String.IsNullOrWhiteSpace(Nick) || String.IsNullOrWhiteSpace(Password))
                            {
                                MessageBox.Show("Поле логина и пароль не должны быть пустыми");
                                return;
                            }
                            InitClient();
                            Random rnd = new Random();
                            _writer!.WriteLine($"REGISTER{JsonSerializer.Serialize(new UserData(Nick, Password, UserColors.noobColors[rnd.Next(0,5)], AccountStatus.Noob, new List<Message>()))}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
                }, () => _client is null || _client?.Connected == false || isConnected == false);
            }
        }
        public AsyncCommand ConnectCommand
        {
            get
            {
                return new AsyncCommand(() =>
                {
                    return Task.Run(() =>
                    {
                        try
                        {
                            if(String.IsNullOrWhiteSpace(Nick) || String.IsNullOrWhiteSpace(Password))
                            {
                                MessageBox.Show("Поле логина и пароль не должны быть пустыми");
                                return;
                            }
                            InitClient();
                            _writer!.WriteLine($"LOGIN{JsonSerializer.Serialize(new UserData(Nick, Password , Color.FromArgb(255,55,100,55), AccountStatus.Noob ,new List<Message>()))}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    });
                }, () => _client is null || _client?.Connected == false || isConnected == false);
            }
        }
        public AsyncCommand AttachPhoto
        {
            get
            {
                return new AsyncCommand(() =>
                {
                    return Task.Run(() =>
                    {
                        OpenFileDialog ofd = new OpenFileDialog();
                        ofd.Title = "Open an Image File";
                        ofd.Filter = "Image Files (*.png, *.jpeg, *.jpg) | *.png;*.jpeg;*.jpg"; //Here you can filter which all files you wanted allow to open
                        bool? dr = ofd.ShowDialog();
                        if (dr == true)
                        {
                            byte[] imageBytes = File.ReadAllBytes(ofd.FileName);
                            if(imageBytes != null)
                            {
                                _writer?.WriteLine($"PHOTO{JsonSerializer.Serialize(imageBytes)}");
                            }
                            else
                            {
                                MessageBox.Show("Выбран неправильный файл");
                            }
                        }
                    });
                }, () => _client?.Connected == true && isConnected == true);
            }
        }
        public AsyncCommand SendCommand
        {
            get
            {
                return new AsyncCommand(() =>
                {
                    return Task.Run(() =>
                    {
                        if (String.IsNullOrEmpty(MessageField))
                        {
                            return;
                        }
                        _writer?.WriteLine($"MESSAGE{MessageField}");
                        MessageField = "";
                    });
                }, () => _client?.Connected == true && isConnected == true);
            }
        }
        private void ExecuteOnLoginClicked(object parameter)
        {
            if(_client == null || !_client.Connected)
            {
                MessageBox.Show("Подключенние с сервером не установлено");
                return;
            }
            if(isConnected == false)
            {
                MessageBox.Show("Чтобы просмотреть сообщения вы должны авторизироваться");
                return;
            }

            var message = (parameter as ChatMessage);
                    _writer?.WriteLine($"GET{message.Username}");
            /*var newWindow = new UserMessagesWindow(message.Username , new List<string> { message.Message });
            newWindow.Show();*/
        }
        private void ShowUserMessages(MessageListData messagesData)
        {
            if(userMessagesWindow != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    userMessagesWindow.Close();
                });
            }
            Application.Current.Dispatcher.Invoke(() =>
            {

                userMessagesWindow = new UserMessagesWindow(messagesData.Login, messagesData.Messages);
                userMessagesWindow.Show();
            });
        }
    }
}