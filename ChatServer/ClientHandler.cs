using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ChatLibrary;
using System.Text.Json;
using System.Windows.Forms;

namespace ChatServer
{
    public class ClientHandler
    {
        public TcpClient client;
        private FormServer formServer;

        public ClientHandler(TcpClient client, FormServer serverForm)
        {
            this.client = client;
            this.formServer = serverForm;
        }

        public void Process()
        {
            NetworkStream stream = client.GetStream();
            string message = Network.GetStringFromStream(stream);

            switch (message)
            {
                case "NEWMSG":
                    ProcessNewMessage(stream);
                    break;
                case "UPDATE":
                    ProcessUpdateMessages(stream);
                    break;
            }

            stream.Close();
            client.Close();
        }

        public void ProcessNewMessage(NetworkStream stream)
        {
            Network.SendStringToStream(stream, "OK");
            string message = Network.GetStringFromStream(stream);

            ChatMSG chatmsg = JsonSerializer.Deserialize<ChatMSG>(message);
            chatmsg.date = DateTime.Now;

            Network.SendStringToStream(stream, "OK");

            formServer.Invoke(new MethodInvoker(delegate { formServer.AddNewMsg(chatmsg); }));
        }

        public void ProcessUpdateMessages(NetworkStream stream)
        {
            Network.SendStringToStream(stream, "OK");
            string message = Network.GetStringFromStream(stream);

            DateTime date = JsonSerializer.Deserialize<DateTime>(message);

            List<ChatMSG> msgs = formServer.GetMsgs();
            message = JsonSerializer.Serialize(msgs);

            Network.SendStringToStream(stream, message);
        }
    }
}
