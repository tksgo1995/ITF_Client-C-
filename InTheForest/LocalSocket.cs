using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest
{
    class LocalSocket
    {
        TcpClient clientSocket;
        NetworkStream stream;
        public USER_STAT us { get; set; }
        public LocalSocket()
        {
            try
            {
                // 소켓
                clientSocket = new TcpClient();
                clientSocket.Connect("127.0.0.1", 9000);
                stream = clientSocket.GetStream();
                //MessageBox.Show("연결 성공");
                byte[] buffer = new byte[2048];
                stream.Read(buffer, 0, 2048);
                us = new USER_STAT(Encoding.UTF8.GetString(buffer));
            }
            catch (Exception e)
            {
                MessageBox.Show("socketerror: " + e.InnerException.Message);
            }
        }
    }
}
