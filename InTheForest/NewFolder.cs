using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest
{
    public partial class NewFolder : Form
    {
        public string folderName;

        public NewFolder()
        {
            InitializeComponent();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            folderName = textBox_FolderName.Text;
            if(folderName.Trim() == "")
            {
                MessageBox.Show("공백은 안됩니다!");
            }
            else Close();
        }
    }
}
