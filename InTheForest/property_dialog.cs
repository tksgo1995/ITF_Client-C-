using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace InTheForest
{
    public partial class property_dialog : MetroForm
    {
        string name, exten, loca, create, write, type;

        private void Property_dialog_Load(object sender, EventArgs e)
        {

        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_confirm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        long size;
        public property_dialog()
        {
            InitializeComponent();
        }

        public void properties(string name, string exten, string loca, long size, string create, string write, string type)
        {

            this.name = name;
            this.exten = exten;
            this.loca = loca;
            this.size = size;
            this.create = create;
            this.write = write;
            this.type = type;

            this.Text = name + " 속성";
            txt_name.Text = name;
            lbl_prop.Text = exten;
            lbl_loc.Text = loca;
            lbl_size.Text = size.ToString() + " KB";
            lbl_crea.Text = create;
            lbl_write.Text = write;
            lbl_type.Text = type;
            this.ShowDialog();
        }
    }
}
