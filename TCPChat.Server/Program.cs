using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Media;
using TCPChat.ClassLibrary;
namespace TCPChat.Server
{
    internal class Program
    {
        //ChatMassage chatMassage;
        static TcpListener listener = new TcpListener(IPAddress.Any, 5050);
        static List<UserData> userDatas = new List<UserData>();
        static Dictionary<TcpClient, UserData> ipDict = new Dictionary<TcpClient, UserData>();
        static string pathToUserData = Environment.CurrentDirectory + @"\userDatas.json";
        static void UpdateUserDatas()
        {
            File.WriteAllText(pathToUserData, JsonSerializer.Serialize(userDatas, new JsonSerializerOptions() { WriteIndented = true }));
        }
        static ChatMessage SystemMessage(string text)
        {
            return new ChatMessage("SYSTEM", Color.FromArgb(255, 0, 175, 0), text, false, null);
        }
        static UserData UpdateUser(in string messageText,in byte[] ImageBytes, in TcpClient client)
        {
            if (ImageBytes == null)
            {
                ipDict[client].UserMessages.Add(new Message(false, messageText, null));
            }
            else
            {
                ipDict[client].UserMessages.Add(new Message(true,"",ImageBytes));
            }
            var currentUser = ipDict[client];
            if(ipDict[client].UserMessages.Count >= 10)
            {
                Random rnd = new Random();
                if (ipDict[client].UserMessages.Count >= 25 && ipDict[client].UserStatus == AccountStatus.Intermidate)
                {
                    ipDict[client].UserColor = UserColors.proColor;
                    ipDict[client].UserStatus = AccountStatus.Professional;
                }
                else if (ipDict[client].UserStatus == AccountStatus.Noob)
                {
                    ipDict[client].UserColor = UserColors.intermidateColors[rnd.Next(0, 3)];
                    ipDict[client].UserStatus = AccountStatus.Intermidate;
                }

            }
            userDatas.Find(user => user.Login == currentUser.Login).UserMessages = ipDict[client].UserMessages;
            userDatas.Find(user => user.Login == currentUser.Login).UserColor = ipDict[client].UserColor;
            userDatas.Find(user => user.Login == currentUser.Login).UserStatus = ipDict[client].UserStatus;
            UpdateUserDatas();

            return ipDict[client];
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine(pathToUserData);
            userDatas = JsonSerializer.Deserialize<List<UserData>>(File.ReadAllText(pathToUserData));
            listener.Start();
            while (true)
            {
                var client = listener.AcceptTcpClient();

                Task.Factory.StartNew(() =>
                {
                    var sr = new StreamReader(client.GetStream());
                    while (client.Connected)
                    {
                        ProcessStream(ref sr, ref client);
                    }
                });
            }
        }

        private static void SendToAllClients(string JSONedMessage)
        {
            Task.Run(() =>
            {
                foreach(var client in ipDict.Keys)
                {
                    try
                    {
                        if (client.Connected)
                        {
                            var sw = new StreamWriter(client.GetStream());
                            sw.AutoFlush = true;
                            sw.WriteLine(JSONedMessage);
                        }
                        else
                        {
                            ipDict.Remove(client);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
        }
        static void ProcessStream(ref StreamReader sr, ref TcpClient client)
        {
            var sw = new StreamWriter(client.GetStream());
            sw.AutoFlush = true;
            try
            {
                var line = sr.ReadLine();
                if (line.StartsWith("REGISTER"))
                {
                    UserData data = JsonSerializer.Deserialize<UserData>(line.Remove(0, 8));
                    if (userDatas.FirstOrDefault(user => user.Login == data.Login) is null)
                    {
                        Console.WriteLine($"Зарегистрирован новый пользователь {data.Login}");
                        userDatas.Add(data);
                        sw.WriteLine($"Success:{JsonSerializer.Serialize(SystemMessage("Регистрация прошла успешно"))}");
                        ipDict.Add(client, data);
                        UpdateUserDatas();
                    }
                    else
                    {
                        sw.WriteLine($"Fail:{JsonSerializer.Serialize(SystemMessage("Пользователь с таким логином уже существует"))}");
                    }
                    return;
                }
                if (line.StartsWith("LOGIN"))
                {
                    UserData data = JsonSerializer.Deserialize<UserData>(line.Remove(0, 5));
                    var findedData = userDatas.FirstOrDefault(user => user.Login == data.Login && user.Password == data.Password);
                    if (findedData is not null)
                    {
                        Console.WriteLine($"Пользователь {findedData.Login} выполнил вход");
                        ipDict.Add(client, findedData);
                        sw.WriteLine($"Success:{JsonSerializer.Serialize(SystemMessage("Вы успешно залогинились"))}");
                    }
                    else
                    {
                        sw.WriteLine($"Fail:{JsonSerializer.Serialize(SystemMessage("Неверный логин или пароль"))}");
                    }
                    return;
                }
                if (line.StartsWith("MESSAGE"))
                {
                    string messageText = line.Remove(0, 7);
                    var user = UpdateUser(in messageText,null, in client);
                    Console.WriteLine($"Пользователь {user.Login} отправил сообщение {messageText.Substring(0,Min(messageText.Length,10))}...");
                    SendToAllClients(JsonSerializer.Serialize(new ChatMessage(user.Login, user.UserColor, messageText, false, null)));
                    return;
                }
                if (line.StartsWith("GET"))
                {
                    string User = line.Remove(0, 3);

                    Console.WriteLine($"Пользователь {ipDict[client].Login} запросил историю сообщений пользователя {User}");
                    if (ipDict[client].Login == User || ipDict[client].UserStatus == AccountStatus.Admin)
                    {
                        if (User == "SYSTEM")
                            return;
                        sw.WriteLine($"MsgLIST:{JsonSerializer.Serialize(new MessageListData(User,userDatas.Find(user => user.Login == User).UserMessages))}");
                    }
                    else
                    {
                        sw.WriteLine($"MsgLIST:{JsonSerializer.Serialize(new MessageListData(User, new List<Message> {new Message(false, "У вас нет доступа для простмотра сообщений" , null)}))}");
                    }
                    return;
                }
                if (line.StartsWith("PHOTO"))
                {
                    var ImageBytes = JsonSerializer.Deserialize<byte[]>(line.Remove(0, 5));
                    var user = UpdateUser(null, ImageBytes, client);

                    Console.WriteLine($"Пользователь {user.Login} отправил фотографию");
                    SendToAllClients(JsonSerializer.Serialize(new ChatMessage(user.Login,user.UserColor,"",true , ImageBytes)));
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static int Min(int a, int b)
        {
            return a > b ? b : a;
        }
    }
}
