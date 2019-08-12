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

namespace InTheForest_Background
{
    public partial class ScreenSaver : Form
    {
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

        private void ScreenSaver_Load(object sender, EventArgs e)
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
            this.Close();
        }

        private void Button_Shutdown_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/s /t 0");
        }

        private void ScreenSaver_FormClosing(object sender, FormClosingEventArgs e)
        {
            nScreenSaverFlag = 0;
        }
    }
}
