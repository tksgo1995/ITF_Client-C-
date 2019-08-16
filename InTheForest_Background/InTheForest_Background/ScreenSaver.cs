using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//ssl
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;
//ssl

using System.Threading;



namespace InTheForest_Background
{
    
    public  class full3//전역변수를 위한 public 클래스
    {
     
        public void copyfun(string ori)
        {
            keyval = ori;
        }
        public void outcopy(string outdata)
        {
            outdata = keyval;
        }
        public string Valout()
        {
            return keyval;
        }
        public  string keyval = "";
    }


    public partial class ScreenSaver : Form
    {
        full3 vipdata = new full3();
        static bool requestvalue = false;

        private int nScreenSaverFlag;
        public int NSFlag
        {
            get { return nScreenSaverFlag; }
            set { nScreenSaverFlag = value; }
        }
        private int nScreenSaver;
        public int NSSaver
        {
            get { return nScreenSaver; }
            set { nScreenSaver = value; }
        }
        public ScreenSaver()
        {
            InitializeComponent();
            nScreenSaver = 0;
        }

        //private
        public void ScreenSaver_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;

        

            nScreenSaverFlag = 1;
            nScreenSaver = 0;
        }


        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            SslTcpClient.RunClient("127.0.0.1", "DESKTOP-NHIE464\\kkh", textBox1.Text, textBox2.Text, requestvalue, vipdata);
            //접속 서버 IP, 인증서 유효성 검사를 위한 인증서의 발급자 정보, 서버로 전송할 ID값, PWS값, 서버에서 로그인 여부를 저장할 bool변수
            if (SslTcpClient.IsSuccess)
            {
                this.Close();
            }
        }



        private void Button_Shutdown_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/s /t 0");
        }

        private void ScreenSaver_FormClosing(object sender, FormClosingEventArgs e)
        {
            nScreenSaverFlag = 0;
        }
        /***********************************************************************************
        //SSL 통신 코드
        ************************************************************************************/
        public class SslTcpClient
        {
            private static Hashtable certificateErrors = new Hashtable();
            public static bool IsSuccess;

            // The following method is invoked by the RemoteCertificateValidationDelegate.
            public static bool ValidateServerCertificate(//서버로부터 전송받은 인증서를 검증하는 함수로 추정
                  object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                    return true;

                Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

                // Do not allow this client to communicate with unauthenticated servers.
                return false;
            }

            public static void RunClient(string machineName, string serverName, string inputID, string inputPWS, bool requestvalue1,full3 buf)
            {
                // Create a TCP/IP client socket.
                // machineName is the host running the server application.
                TcpClient client = new TcpClient(machineName, 9000);//ip와 포트를 입력하여 클라이언트 동작
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
                    sslStream.AuthenticateAsClient(serverName);
                }
                catch (AuthenticationException e)
                {
                    MessageBox.Show("Exception: {0}", e.Message);
                    if (e.InnerException != null)
                    {
                        MessageBox.Show("Inner exception: {0}", e.InnerException.Message);
                    }
                    MessageBox.Show("Authentication failed - closing the connection.");
                    client.Close();
                    return;
                }
                // Encode a test message into a byte array.
                // Signal the end of the message using the "<EOF>".
                //메세지 전송 파트******************************************************************
                byte[] messsage = Encoding.UTF8.GetBytes(inputID + "%%" + inputPWS + "$$");
                // Send hello message to the server. 
                sslStream.Write(messsage);
                sslStream.Flush();
                //데이터 전송을 위해 바이트 배열에 저장 여기서는 문자열 이므로 문자 인코딩방식과 보내려는 문자 + 구분자 %%는 문자와 문자의 구분자
                //$$는 문자의 끝을 나타냄
                //메세지 전송 파트*******************************************************************
                // Read message from the server.

                //메세지 수신 파트*************************************************************************
                string serverMessage = ReadMessage(sslStream);
                //ReadMessage 를 통하여 서버에서 전송해준 메세지를 변수에 저장하고 아래에서 로그인 여부를 체크
                //ReadMessage함수는 바이트 단위의 데이터를 받아 UTF8인코딩으로 저장한 후 Tostring으로 리턴하므로 최종적으로 string으로 저장 가능
                if (serverMessage == "a$")//실험을 위해 잠시 b$로 해놈 원래 a가 성공기고 나머지 모든 문자가 실패
                {
                    MessageBox.Show("로그인 성공");
                    MessageBox.Show(serverMessage);

                    //키받는곧에서 뭔가 문제가 생기고있음 이부분을 규진이랑 확인해야할듯

                    buf.copyfun(ReadMessage(sslStream));
                    MessageBox.Show(buf.keyval);
                    //MessageBox.Show("키값 받을꺼임");
                    // MessageBox.Show(full3.keyval);
                    //MessageBox.Show("키값 받음");
                    IsSuccess = true;
                }
                else
                {
                    MessageBox.Show(serverMessage);
                    MessageBox.Show("로그인 실패");
                    IsSuccess = false;
                }
                //메세지 수신 파트*************************************************************************
                // Close the client connection.
                client.Close();

               // MessageBox.Show("Client closed.");

                return;
            }

            static string ReadMessage(SslStream sslStream)
            {
                // Read the  message sent by the server.
                // The end of the message is signaled using the
                // "<EOF>" marker.
                byte[] buffer = new byte[2048];
                StringBuilder messageData = new StringBuilder();
                int bytes = -1;
                do
                {
                    bytes = sslStream.Read(buffer, 0, buffer.Length);

                    // Use Decoder class to convert from bytes to UTF8
                    // in case a character spans two buffers.
                    Decoder decoder = Encoding.UTF8.GetDecoder();
                    char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                    decoder.GetChars(buffer, 0, bytes, chars, 0);
                    messageData.Append(chars);
                    // Check for EOF.
                    if (messageData.ToString().IndexOf("$") != -1)//서버로부터 받을 데이터의 끝을 확인하기위한 구분자
                    {

                        break;
                    }
                } while (bytes != 0);

                MessageBox.Show("메세지 수신 완료");
                return messageData.ToString();

            }

        }
        /******************************************************************************************************
        //ssl 클래스 끝
        ******************************************************************************************************/



     

    }
}
