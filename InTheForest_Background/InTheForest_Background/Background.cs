using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest_Background
{
    public partial class Background : Form
    {
        
        LocalSocket ls;
        public static ScreenSaver Ss { get; set; }
        public Background()
        {
            InitializeComponent();
        }
        // Form을 Background로 돌림
        public void SetForm(bool ShowInTaskbar, int Opacity, bool Visible) 
        {
            this.ShowInTaskbar = ShowInTaskbar;
            this.Opacity = Opacity;
            this.Visible = Visible;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetForm(false, 0, false);
            // 키보드 마우스 후킹
            SetHook();
            // ScreenSaver 타이머 설정, ScreenSaver 올리기
            timer_ScreenSaver.Start();
            Ss = new ScreenSaver();
            Ss.Show();
            //MessageBox.Show("aa");
            // 윈도우 탐색기에 키넘겨주는 TCP 소켓
            ls = new LocalSocket();
        }
        // MainForm 종료시
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnHook();
        }
        
        // 타이머 60000 = 1분, 1000 = 1초
        private void Timer_ScreenSaver_Tick(object sender, EventArgs e)
        {
            if(Ss.NSSaver < 600000 && Ss.NSFlag == 0) Ss.NSSaver++;
            if (Ss.NSSaver == 600000 && Ss.NSFlag == 0)
            {
                Ss = new ScreenSaver();
                Ss.Show();
            }
        }

        // 키보드 마우스 후킹
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc callback, IntPtr hInstance, uint threadId);
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        const int WH_KEYBOARD_LL = 13;
        const int WH_MOUSE_LL = 14;
        const int WM_KEYDOWN = 0x100;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_RBUTTONDOWN = 0x0204;
        const int WM_MOUSEMOVE = 0x0200;
        const int WM_MOUSEWHEEL = 0x020A;
        private LowLevelKeyboardProc _kproc = khookProc;
        private LowLevelMouseProc _mproc = mhookProc;
        private static IntPtr hkhook = IntPtr.Zero;
        private static IntPtr hmhook = IntPtr.Zero;
        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hkhook = SetWindowsHookEx(WH_KEYBOARD_LL, _kproc, hInstance, 0);
            hmhook = SetWindowsHookEx(WH_MOUSE_LL, _mproc, hInstance, 0);
        }
        public void UnHook()
        {
            UnhookWindowsHookEx(hkhook);
            UnhookWindowsHookEx(hmhook);
        }
        public static IntPtr khookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                // TODO: 키보드 작동시 코드 밑에 삽입
                Ss.NSSaver = 0;
            }
            return CallNextHookEx(hkhook, code, (int)wParam, lParam);
        }
        public static IntPtr mhookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if ((code >= 0 && wParam == (IntPtr)WM_RBUTTONDOWN)
                || (code >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
                || (code >= 0 && wParam == (IntPtr)WM_MOUSEMOVE)
                || (code >= 0 && wParam == (IntPtr)WM_MOUSEWHEEL))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                // TODO: 마우스 작동시 코드 밑에 삽입
                Ss.NSSaver = 0;
            }
            return CallNextHookEx(hkhook, code, (int)wParam, lParam);
        }

        
    }
}
