using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatLibrary
{
    public class Network
    {
        public static string GetStringFromStream(NetworkStream stream)
        {
            byte[] data = new byte[1024];

            StringBuilder sb = new StringBuilder();
            int bytes = 0;

            do
            {
                bytes = stream.Read(data, 0, data.Length);
                sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (stream.DataAvailable);

            return sb.ToString();
        }

        public static void SendStringToStream(NetworkStream stream, string text)
        {
            byte[] data = Encoding.Unicode.GetBytes(text);
            stream.Write(data, 0, data.Length);
        }

    }
}
