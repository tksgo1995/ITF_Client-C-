using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Collections;

namespace InTheForest
{
    public class USER_STAT
    {
        public string KUser { get; set; }
        public int FolderPolicyCount { get; set; }
        public Dictionary<string, string> Folder { get; set; }
        public string id { get; set; }
        public string password { get; set; }
        public string[] state;
        public string My_IP = string.Empty;

        public USER_STAT(string beforebuf)
        {
            //MessageBox.Show(beforebuf);
            My_IP = Get_MyIP();
            // MessageBox.Show(beforebuf);
            // MessageBox.Show(My_IP);
            Folder = new Dictionary<string, string>();
            char[] seq = { 'α' };
            string[] afterbuf = beforebuf.Split(seq);
            KUser = afterbuf[0];
            FolderPolicyCount = int.Parse(afterbuf[1]);
            id = afterbuf[2];
            password = afterbuf[3];
            for (int i = 4; i < FolderPolicyCount + 6; i = i + 2)
            {
                Folder.Add(afterbuf[i], afterbuf[i + 1]);
            }
            /*
            MessageBox.Show("\n유저키값: " + KUser +
                "\n정책개수: " + FolderPolicyCount + 
                "\nID: " + id + 
                "\nPW: " + password);
                */
            /*
        foreach (KeyValuePair<string, string> item in Folder)
        {
            MessageBox.Show("폴더이름: " + item.Key +
                "\n폴더키값: " + item.Value);
        }

            */

            state = new string[5];//읽기쓰기등의 정보를 저장할 변수
            state[0] = "1";
            state[1] = "2";
            state[2] = "3";
            state[3] = "4";
            state[4] = "5";
        }
        public string Get_MyIP()
        {
            IPHostEntry host = Dns.GetHostByName(Dns.GetHostName());
            string myip = host.AddressList[0].ToString();
            return myip;
            //ip잘 잡음 문제 : 와이파이 로 접속시 ip확인 불가 랜선을 통한 물리 네트워크 접속시에만 확인
        }
        public string outip()
        {
            return My_IP;
        }
    }
    public class SslTcpClient
    {
        private static Hashtable certificateErrors = new Hashtable();
        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(//서버로부터 전송받은 인증서를 검증하는 함수로 추정
              object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            // MessageBox.Show("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        public static void LogRunClient(string uid, string path, string state1, string mip)
        {
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            TcpClient client = new TcpClient("15.164.170.79", 9003);//ip와 포트를 입력하여 클라이언트 동작
            Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(//SSLStream 을 통해서 GetStream()함수 동작 
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null
                );
            // The server name must match the name on the server certificate.
            try
            {
                sslStream.AuthenticateAsClient("DESKTOP-NHIE464\\kkh");
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                return;
            }
            // Encode a test message into a byte array.
            // Signal the end of the message using the "<EOF>".
            //메세지 전송 파트******************************************************************
            byte[] messsage = Encoding.UTF8.GetBytes(uid + "α" + path + "α" + state1 + "α" + mip);
            // Send hello message to the server.
            sslStream.Write(messsage);
            sslStream.Flush();
            //데이터 전송을 위해 바이트 배열에 저장 여기서는 문자열 이므로 문자 인코딩방식과 보내려는 문자 + 구분자 %%는 문자와 문자의 구분자
            //$$는 문자의 끝을 나타냄
            //메세지 전송 파트*******************************************************************
            // Read message from the server.
            // Close the client connection.
            client.Close();
            return;
        }
    }//SSL 클래스 끝
}
