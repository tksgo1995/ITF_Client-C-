using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest_Background
{
    public class LocalSocket
    {
        TcpListener server; // 서버
        TcpClient clientSocket; // 소켓
        static int counter = 0;
        
        public LocalSocket() {
            server = null;
            clientSocket = null;

            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
        }

        private void InitSocket()
        {
            // 서버 접속 IP, 포트
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9000);
            // 소켓 설정
            clientSocket = default(TcpClient);
            // 서버 시작
            server.Start();
            //MessageBox.Show("소켓시작");

            while(true)
            {
                
                try
                {
                    counter++;
                    clientSocket = server.AcceptTcpClient();

                    string strbuf = Form1.Ss.DATA.KUser + "%" +
                        Form1.Ss.DATA.FolderPolicyCount + "%";
                    foreach (KeyValuePair<string, string> item in Form1.Ss.DATA.Folder)
                    {
                        strbuf = strbuf.Replace(strbuf, strbuf + item.Key + "%" + item.Value + "%");
                        //MessageBox.Show(strbuf);
                    }

                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = Encoding.UTF8.GetBytes(strbuf);
                    stream.Write(buffer, 0, buffer.Length);
                }
                catch(Exception e)
                {
                    MessageBox.Show("socketerror: " + e.Message);
                }
            }
            clientSocket.Close();
            server.Stop();
        }
    }
}
