using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Ionic.Zip;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using MetroFramework.Forms;

namespace InTheForest
{
    public partial class Compress : Form
    {
        public Compress()
        {
            InitializeComponent();
        }

        private void Bt_Folder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "경로를 선택하세요.";
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
            }
        }
        private void Bt_File_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "All Files |*.*", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = ofd.FileName;
                }
            }
        }
        private void Bt_CompFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFolder.Text))
            {
                MessageBox.Show("폴더를 선택해주세요.");
                txtFolder.Focus();
                return;
            }
            string path = txtFolder.Text;
            Thread thread = new Thread(t =>
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    zip.AddDirectory(path);
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                    zip.SaveProgress += Zip_SaveProgress;
                    zip.Save(string.Format("{0},{1}.zip", di.Parent.FullName, di.Name));
                }
            })
            { IsBackground = true };
            thread.Start();
        }

        private void Zip_SaveProgress(object sender, Ionic.Zip.SaveProgressEventArgs e)
        {
            if(e.EventType == Ionic.Zip.ZipProgressEventType.Saving_BeforeWriteEntry)
            {
                progressBar_Compress.Invoke(new MethodInvoker(delegate
                {
                    progressBar_Compress.Maximum = e.EntriesTotal;
                    progressBar_Compress.Value = e.EntriesSaved + 1;
                    progressBar_Compress.Update();
                }));
            }
        }
        private void Zip_SaveFileProgress(object sender, Ionic.Zip.SaveProgressEventArgs e)
        {
            if (e.EventType == Ionic.Zip.ZipProgressEventType.Saving_EntryBytesRead)
            {
                progressBar_Compress.Invoke(new MethodInvoker(delegate
                {
                    progressBar_Compress.Maximum = 100;
                    progressBar_Compress.Value = (int)((e.BytesTransferred * 100) / e.TotalBytesToTransfer);
                    progressBar_Compress.Update();
                }));
            }
        }
        private void Bt_CompFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFile.Text))
            {
                MessageBox.Show("파일을 선택해주세요.");
                txtFile.Focus();
                return;
            }
            string filename = txtFile.Text;
            Thread thread = new Thread(t =>
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                    FileInfo fi = new FileInfo(filename);
                    zip.AddFile(filename);
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(filename);
                    zip.SaveProgress += Zip_SaveFileProgress;
                    zip.Save(string.Format("{0},{1}.zip", di.Parent.FullName, di.Name));
                }
            })
            { IsBackground = true };
            thread.Start();
        }

        private void Compress_Load(object sender, EventArgs e)
        {

        }
    }
}
