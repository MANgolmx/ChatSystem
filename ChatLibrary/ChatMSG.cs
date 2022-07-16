using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChatLibrary
{
    public class ChatMSG
    {
        public string ip { get; set; }
        public string nickname { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }

        ChatMSG(string ip, string nickname, string text, DateTime date)
        {
            this.ip = ip;
            this.nickname = nickname;
            this.text = text;
            this.date = date;
        }

        public override string ToString()
        {
            return string.Format("[{0} ({1}) |{2}]{3}", nickname, ip, date.ToString(), text);
        }
    }
}
