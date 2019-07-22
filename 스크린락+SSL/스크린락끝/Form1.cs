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
using Microsoft.Win32;

//SSL을 위한 추가 using
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Net.Sockets;
using System.Net.Security;
using System.Net;
using System.Collections;


//SSL을 위한 추가 using


namespace test2
{
    public partial class Form1 : Form
    {
        ///////////////SSL통신을 위한 변수////////////////////////////
        static bool requestvalue = false;
        ///////////////SSL통신을 위한 변수////////////////////////////

        ///////////////////////SSL통신을 위해 추가한 클래스////////////////////////////
        public class SslTcpClient
        {
            private static Hashtable certificateErrors = new Hashtable();

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

            public static void RunClient(string machineName, string serverName, string inputID, string inputPWS, bool requestvalue1)
            {
                // Create a TCP/IP client socket.
                // machineName is the host running the server application.
                TcpClient client = new TcpClient(machineName, 9999);//ip와 포트를 입력하여 클라이언트 동작
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
                if (serverMessage == "성공$")//js랑 할때는 a$가 성공 메세지
                {
                    MessageBox.Show("로그인 성공");
                    MessageBox.Show(serverMessage);
                    full3.keyval = ReadMessage(sslStream);
                    MessageBox.Show(full3.keyval);

                    Application.Exit();//화면 종료
                }
                else
                {
                    MessageBox.Show(serverMessage);
                    MessageBox.Show("로그인 실패");
                }
                //메세지 수신 파트*************************************************************************
                Console.WriteLine("Server says: {0}", serverMessage);
                // Close the client connection.
                client.Close();
                Console.WriteLine("Client closed.");
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

                return messageData.ToString();
            }
            /*
            private static void DisplayUsage()
            {
                Console.WriteLine("To start the client specify:");
                Console.WriteLine("clientSync machineName [serverName]");
                Environment.Exit(1);
            }
            */

        }//SSL 클래스 끝

        public static class full3//전역변수 처럼 사용하기 위한 public 클래스
        {
            public static string keyval = "";
        }

        ///////////////////////SSL통신을 위해 추가한 클래스////////////////////////////

        OpenFileDialog ofdlg = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            WriteRegi();
            textBox1.Focus();
        }

        public static void WriteRegi()
        {
            RegistryKey reg = null;

            //activate screensaver
            reg = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            if (reg == null)
                reg = Registry.CurrentUser.CreateSubKey("Control Panel\\Desktop");
            reg.SetValue("ScreenSaveActive", 1, RegistryValueKind.String);
            reg.Close();

            //waiting time
            reg = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            reg.SetValue("ScreenSaveTimeOut", 1800, RegistryValueKind.String);
            reg.Close();

            //screensaver 등록
            String src = "C:\\Users\\heonji\\Desktop\\test2.scr";
            reg = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            reg.SetValue("SCRNSAVE.EXE", src, RegistryValueKind.String);
            reg.Close();

            //screen locker 등록
            /*src = "C:\\Users\\heonji\\Desktop\\test2.exe";
            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if(reg == null)
                reg = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            reg.SetValue("ScreenLock", src, RegistryValueKind.String);
            reg.Close();*/

            //screen locker 등록
            src = "C:\\Users\\heonji\\Desktop\\test2.exe";
            reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true);
            if (reg == null)
                reg = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce");
            reg.SetValue("ScreenLock", src, RegistryValueKind.String);
            reg.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new Form1();
            String id = textBox1.Text;
            String pwd = textBox2.Text;    
            String filename = "D:\\프로젝트\\window.exe";

            //////////////////////SSL통신을 위해 추가한 코드////////////////////////////

            SslTcpClient.RunClient("127.0.0.1", "DESKTOP-NHIE464\\kkh", id, pwd, requestvalue);
            //접속 서버 IP, 인증서 유효성 검사를 위한 인증서의 발급자 정보, 서버로 전송할 ID값, PWS값, 서버에서 로그인 여부를 저장할 bool변수
            //서버에서 전송해준 인증서의 발급자의 정보가 명시한 DESKTOP-NHIE464\\kkh가 맞다면 올바른 서버로 지정
            //192.168.0.21

            ///////////////////////SSL통신을 위해 추가한 코드////////////////////////////

            /* SSL을 위한 클래스로 이동됨
            if (id == "abc" && pwd == "1234")
            {
                System.Diagnostics.Process.Start(filename);
                //D:\\프로젝트\\window.exe 파일이 없어서 동작중 오류 발생 파일 자체를 알지 못하므로 id 체크 부분만 수정함
                Application.Exit();
            }
            */
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();//화면 종료
            //Process.Start("shutdown.exe", "-s"); // 기본적으로 30초 후 종료 (현지씨 코드 종료 = PC OFF 일단 주석처리)
        }
    }
}
