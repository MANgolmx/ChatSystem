using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChatLibrary;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ChatServer
{
    public partial class FormServer : Form
    {
        private List<ChatMSG> msgs;
        private bool isServerOn;
        TcpListener listener;
        Thread threadListener;

        public FormServer()
        {
            InitializeComponent();
            msgs = new List<ChatMSG>();
            isServerOn = false;
            listener = null;
            threadListener = null;
        }

        public void AddNewMsg(ChatMSG chatMSG)
        {
            msgs.Add(chatMSG);

            textBoxChat.Text += chatMSG.ToString() + "\r\n";
        }

        public List<ChatMSG> GetMsgs()
        {
            return msgs;
        }

        private void buttonStartStop_Click_1(object sender, EventArgs e)
        {
            if (isServerOn)
            {
                isServerOn = false;
                labelStatus.Text = "off";

                listener.Stop();
            } else {
                isServerOn = true;
                labelStatus.Text = "on";

                listener = new TcpListener(IPAddress.Parse(textBoxIP.Text), Int32.Parse(textBoxPort.Text));
                listener.Start();
            }
        }
    }
}
