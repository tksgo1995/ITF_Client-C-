﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace InTheForest
{
    public partial class Form1 : MetroForm
    {
        LocalSocket ls;
        USER_STAT StatUser;
        int Back_init;
        LinkedList<string> front_stack, back_stack;
        string key;
        AES k;
        private static EventWaitHandle waitforsinglesignal;
        private int mask;
        csNetDrive netDrive;
        public string select, fullPath;
        TreeNode fol;

        public Form1()
        {
            InitializeComponent();

            cboListViewMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboListViewMode.Items.Add("자세히");
            cboListViewMode.Items.Add("큰아이콘");
            cboListViewMode.Items.Add("리스트");
            cboListViewMode.Items.Add("타일");
            cboListViewMode.SelectedIndex = 0;
        }
        //버튼 활성화
        void SetButtonEnable()
        {
            if (back_stack.Count == 0) button_Back.Enabled = false;
            else button_Back.Enabled = true;

            if (front_stack.Count == 0) button_Front.Enabled = false;
            else button_Front.Enabled = true;
        }
        private bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return false;
        }
        void setDrive()
        {
            // 네트워크 드라이브 잡기  \\13.125.149.179\smbuser  smbuser  kit2019
            netDrive = new csNetDrive();
            string Folder, ID, Password, DriveAlphabet;
            int i = 0;
            StatUser.Folder.Add(StatUser.id, StatUser.KUser);
            foreach (KeyValuePair<string, string> item in StatUser.Folder)
            {
                if (item.Key == "All") Folder = @"\\15.164.170.79\" + StatUser.id;
                else Folder = @"\\15.164.170.79\" + item.Key;
                ID = StatUser.id;
                Password = StatUser.password;
                DriveAlphabet = (char)('Z' - i++) + ":";
                netDrive.setRemoteConnection(Folder, ID, Password, DriveAlphabet);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // 9000포트 로컬에서 키값 받아오기
            ls = new LocalSocket();
            StatUser = ls.us;

            Back_init = 1; // 시작할 때 Back 버튼 비활성화를 위한 마스크값
            front_stack = new LinkedList<string>();
            back_stack = new LinkedList<string>();
            SetButtonEnable();

            setDrive();
            /*if (result != 0)
            {
                //MessageBox.Show("네트워크 드라이드 연결 실패");
                //this.Close();
            }*/

            //드라이브 잡아서 트리뷰에 올리기
            string[] drives = Directory.GetLogicalDrives();

            foreach (string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);
                if (di.IsReady && di.DriveType == DriveType.Network)
                {
                    TreeNode node = treeView1.Nodes.Add(drive);
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;
                    node.Nodes.Add("\\");
                }
            }

            listView1.BeginUpdate();
            //ListView 속성을 위한 헤더추가
            listView1.Columns.Add("이름", listView1.Width / 4, HorizontalAlignment.Left);
            listView1.Columns.Add("수정한 날짜", listView1.Width / 4, HorizontalAlignment.Left);
            listView1.Columns.Add("유형", listView1.Width / 4, HorizontalAlignment.Left);
            listView1.Columns.Add("크기", listView1.Width / 4, HorizontalAlignment.Left);
            //행 단위 선택 가능
            listView1.FullRowSelect = true;
            listView1.EndUpdate();

            // 파일 암복호화 키 초기화
            k = new AES();
            waitforsinglesignal = new EventWaitHandle(false, EventResetMode.AutoReset);

            //관리자 권한으로 실행되었을 경우 제목 - 관리자 로 바꾸기
            if (IsAdministrator())
            {
                MessageBox.Show("관리자 권한으로 실행 시키면 안됩니다.");
                Close();
            }

            this.AllowDrop = true;
            listView1.AllowDrop = true;

            
        }
        private void SettingListView(string sFullPath)
        {
            try
            {
                listView1.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(sFullPath);
                int DirectCount = 0;

                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    // 숨김폴더인지 확인
                    if ((dirItem.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                    
                    ListViewItem lsvitem = new ListViewItem();

                    //아이콘 할당
                    if (dirItem.GetFiles().Length <= 0)
                        lsvitem.ImageIndex = 6;
                    else
                        lsvitem.ImageIndex = 10;

                    lsvitem.Text = dirItem.Name;
                    // 아이템을 ListView(listView1)에 추가
                    listView1.Items.Add(lsvitem);
                    listView1.Items[DirectCount].SubItems.Add(dirItem.CreationTime.ToString());
                    listView1.Items[DirectCount].SubItems.Add("폴더");
                    listView1.Items[DirectCount].SubItems.Add(dirItem.GetFiles().Length.ToString() +
                        " files");
                    DirectCount++;
                }

                // 디렉토리에 존재하는 파일목록 보여주기
                FileInfo[] files = dir.GetFiles();
                int Count = 0;
                foreach (FileInfo fileinfo in files)
                {
                    if ((fileinfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                    ListViewItem lsvitem = new ListViewItem();
                    if (fileinfo.Extension.Equals(".txt"))
                        lsvitem.ImageIndex = 0;
                    else if (fileinfo.Extension.Equals(".psd"))
                        lsvitem.ImageIndex = 3;
                    else if (fileinfo.Extension.Equals(".pdf"))
                        lsvitem.ImageIndex = 2;
                    else if (fileinfo.Extension.Equals(".doc"))
                        lsvitem.ImageIndex = 1;
                    else if (fileinfo.Extension.Equals(".jpg") || fileinfo.Extension.Equals(".png"))
                        lsvitem.ImageIndex = 7;
                    else if (fileinfo.Extension.Equals(".exe"))
                        lsvitem.ImageIndex = 4;
                    else if (fileinfo.Extension.Equals(".zip"))
                        lsvitem.ImageIndex = 9;
                    else if (fileinfo.Name.Contains(".docx.enc"))
                        lsvitem.ImageIndex = 12;
                    else if (fileinfo.Name.Contains(".pptx.enc"))
                        lsvitem.ImageIndex = 13;
                    else if (fileinfo.Name.Contains(".xlsx.enc"))
                        lsvitem.ImageIndex = 14;
                    else if (fileinfo.Name.Contains(".hwp.enc"))
                        lsvitem.ImageIndex = 15;
                    else if (fileinfo.Name.Contains(".txt.enc"))
                        lsvitem.ImageIndex = 16;
                    else if (fileinfo.Extension.Equals(".enc"))
                        lsvitem.ImageIndex = 11;
                    else
                        lsvitem.ImageIndex = 8;
                    lsvitem.Text = fileinfo.Name;

                    listView1.Items.Add(lsvitem);

                    if (fileinfo.LastWriteTime != null)
                    {
                        listView1.Items[Count].SubItems.Add(fileinfo.LastWriteTime.ToString());
                    }
                    else
                    {
                        listView1.Items[Count].SubItems.Add(fileinfo.CreationTime.ToString());
                    }
                    listView1.Items[Count].SubItems.Add(fileinfo.Attributes.ToString());
                    listView1.Items[Count].SubItems.Add(fileinfo.Length + " Bytes");
                    Count++;
                    
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("에러 발생 : " + ex.Message);
            }
        }
        private void TreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //SettingListView(e.Node.FullPath);
        }
        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //back = label1.Text;
                byte[] decbytes;
                ListViewItem item = listView1.SelectedItems[0];
                string file;

                file = (label_Path.Text + "\\" + item.Text).Replace("\\\\", "\\");

                FileAttributes attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    back_stack.AddLast(label_Path.Text);
                    SetButtonEnable();
                    label_Path.Text = file;
                    SettingListView(file);
                }
                else
                {
                    if (file.Contains(".enc"))
                    {
                        Thread t1 = new Thread(new ThreadStart(Run));
                        t1.Start();
                        waitforsinglesignal.WaitOne();
                        void Run()
                        {
                            try
                            {
                                string logPath;
                                logPath = label_Path.Text + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                                //MessageBox.Show(logPath);

                                key = GetDrivePassword(label_Path.Text.Substring(0, 1));
                                decbytes = k.AESDecrypto256(File.ReadAllBytes(file), key);
                                file = file.Replace(".enc", "");
                                file = label_Path.Text.Substring(0, label_Path.Text.IndexOf("\\")) + "\\.start\\" + file.Substring(file.LastIndexOf("\\") + 1);
                                FileStream fs = File.Create(file);
                                fs.Close();
                                File.WriteAllBytes(file, decbytes);
                                waitforsinglesignal.Set();
                                string filename = Path.GetFileName(file);
                                process_FileStart.StartInfo.FileName = filename;
                                process_FileStart.StartInfo.WorkingDirectory = label_Path.Text.Substring(0, label_Path.Text.IndexOf("\\")) + "\\.start\\";
                                process_FileStart.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                                FileInfo fi = new FileInfo(process_FileStart.StartInfo.WorkingDirectory + "\\" + process_FileStart.StartInfo.FileName);
                                process_FileStart.Start();
                                //MessageBox.Show(logPath);
                                SslTcpClient.LogRunClient(StatUser.id, logPath, StatUser.state[0], StatUser.outip());
                            }
                            catch (Exception e1)
                            {
                                MessageBox.Show(e1.Message);
                            }
                        }
                    }
                    SettingListView(label_Path.Text);
                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show("에러 : " + e1.Message);
            }
        }
        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if(e.Node.Nodes.Count ==1 && e.Node.Nodes[0].Text.Equals("\\"))
                {
                    e.Node.Nodes.Clear();
                    string path = e.Node.FullPath;
                    string[] directories = Directory.GetDirectories(path);

                    foreach(string directory in directories)
                    {
                        DirectoryInfo dirItem = new DirectoryInfo(directory);
                        if ((dirItem.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
                        TreeNode newnode = e.Node.Nodes.Add(directory.Substring(directory.LastIndexOf("\\") + 1));
                        newnode.Nodes.Add("\\");
                        newnode.ImageIndex = 6;
                        newnode.SelectedImageIndex = 6;
                    }
                }
            }
            catch(Exception ex) {// MessageBox.Show("error : " + ex); 
            }
        }
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = e.Node;
            string path = current.FullPath;

            if (!path.Equals(label_Path.Text) && Back_init == 0)
            {
                back_stack.AddLast(label_Path.Text);
            }
            if(Back_init == 1)
            {
                Back_init = 0;
            }

            SetButtonEnable();
            label_Path.Text = path.Replace("\\\\", "\\");
            SettingListView(label_Path.Text);
           
            try
            {
                if (label_Path.Text == "내 컴퓨터" || path == "Root")
                {
                    label_Path.Text = "C:\\";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error : " + ex);
            }
        }
        void Encrypt_Directory(string filename, string path, string copyfilepath)// filename: 디렉토리 이름, path: 파일경로, copyfilepath: 원본파일경로
        {
            // 1. 디렉토리 생성
            string directory = path + "\\" + filename;
            DirectoryInfo di = new DirectoryInfo(directory);
            di.Create();

            // 2. 파일을 모두 암호화함
            DirectoryInfo dir = new DirectoryInfo(copyfilepath);
            FileInfo[] files = dir.GetFiles();
            byte[] encbytes;
            key = GetDrivePassword(label_Path.Text.Substring(0, 1));
            foreach (FileInfo fileinfo in files)
            {
                string logPath;
                logPath = label_Path.Text + "\\" + fileinfo.Name;
                //MessageBox.Show("name: " + fileinfo.Name);
                encbytes = k.AESEncrypto256(File.ReadAllBytes(fileinfo.FullName), key);
                File.WriteAllBytes(directory + "\\" + fileinfo.Name + ".enc", encbytes);
                SslTcpClient.LogRunClient(StatUser.id, logPath, "1", StatUser.outip());
            }
            // 3. 디렉토리를 모두 찾아서 재귀함수돌림
            foreach (DirectoryInfo dirItem in dir.GetDirectories())
            {
                Encrypt_Directory(dirItem.Name, directory, dirItem.FullName);
                string logPath;
                logPath = label_Path.Text + "\\" + dirItem.Name + "\\";
                SslTcpClient.LogRunClient(StatUser.id, logPath, "1", StatUser.outip());
                /*MessageBox.Show(
                    "filename: " + dirItem.Name + 
                    "\npath: " + directory + 
                    "\ncopyfilepath: " + dirItem.FullName);*/
            }
        }
        string GetDrivePassword(string drive)
        {
            //MessageBox.Show(drive);
            char DriveAlphabet;
            int i = 0;
            string result = "";

            foreach (KeyValuePair<string, string> item in StatUser.Folder)
            {
                DriveAlphabet = (char)('Z' - i++);

                if (DriveAlphabet.ToString().Equals(drive))
                {
                    result = item.Value;
                }
                //MessageBox.Show("진짜키: " + item.Value);
            }
            //MessageBox.Show("return: " + result);

            return result;
        }
        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            string filename;
            byte[] encbytes;
            key = GetDrivePassword(label_Path.Text.Substring(0, 1));
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                FileAttributes attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    string logPath;
                    logPath = label_Path.Text + "\\" + file + "\\";
                    filename = file.Substring(file.LastIndexOf('\\') + 1);
                    Encrypt_Directory(filename, label_Path.Text, file);
                }
                else
                {
                    string logPath;
                    //MessageBox.Show(file);
                    logPath = label_Path.Text + "\\" + file.Replace("\\\\", "\\").Substring(file.LastIndexOf("\\") + 1);
                    filename = Path.GetFileName(file);
                    encbytes = k.AESEncrypto256(File.ReadAllBytes(file), key);
                    File.WriteAllBytes(label_Path.Text + "\\" + filename + ".enc", encbytes);
                    SslTcpClient.LogRunClient(StatUser.id, logPath, StatUser.state[1], StatUser.outip());
                }

            }
            SettingListView(label_Path.Text);
        }
        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            // 매니페스트 파일에 권한을 RequireAdministrator 을 하면 MIC 권한(?)이 달라서 드래그 앤 드록을 못함
            // 기본 값으로 하면 레지스트리 변경을 못함
            // 이 프로그램은 기본 설정으로하고 백엔드 프로그램을 관리자 권한으로 만들어서 따로둬야될듯
            // https://social.msdn.microsoft.com/Forums/ko-KR/02e2637b-0a8b-4dd9-80cc-d96b69597c9a/win10-5064049436-winform-440604815649884-drag-and-drop-505044610445716?forum=visualcsharpko
        }
        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    string file = label_Path.Text + "\\" + item.Text;
                    string logPath;
                    FileAttributes attr = File.GetAttributes(file);
                    // 디렉토리는 하위폴더까지 다지워줘야함
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        logPath = file + "\\";
                        DirectoryInfo di = new DirectoryInfo(file);
                        di.Delete(true);
                        SslTcpClient.LogRunClient(StatUser.id, logPath, "4", StatUser.outip());
                    }
                    File.Delete(file);
                    logPath = label_Path.Text + "\\" + item.Text;
                    SslTcpClient.LogRunClient(StatUser.id, logPath, "4", StatUser.outip());
                    SettingListView(label_Path.Text);
                }
                else if (e.KeyCode.Equals(Keys.F5))
                {
                    SettingListView(label_Path.Text);
                }
                else if (e.KeyData == Keys.F2)
                {
                    listView1.SelectedItems[0].BeginEdit();
                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show("error: " + e1);
            }
        }
        private void ListView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Right))
                {

                    ListViewItem item = listView1.SelectedItems[0];
                    string file = label_Path.Text + "\\" + item.Text;

                    FileAttributes attr = File.GetAttributes(file);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        cmsmenu_list1.Show(MousePosition.X, MousePosition.Y);
                        // fol = treeView1.GetNodeAt(e.X, e.Y);

                        lRename.Click += (senders, es) =>
                        {
                            listView1.SelectedItems[0].BeginEdit();
                        };

                        lDelete.Click += (senders, es) =>
                        {
                            DirectoryInfo di = new DirectoryInfo(file);
                            di.Delete(true);
                            SettingListView(label_Path.Text);
                        };
                    }
                    else
                    {
                        // TODO: 파일 마우스 오른쪽 버튼
                        string selected = listView1.GetItemAt(e.X, e.Y).Text;

                        cmsmenu_list2.Show(MousePosition.X, MousePosition.Y);

                        lfDelete.Click += (senders, es) =>
                        {
                            File.Delete(file);
                            SettingListView(label_Path.Text);
                        };

                        lfRename.Click += (senders, es) =>
                        {
                            listView1.SelectedItems[0].BeginEdit();
                        };     
                    
                    }
                }
            }
            catch (Exception e1)
            {

            }
        }
        private void LZip_Click(object sender, EventArgs e)
        {
            string sourcePath, zipPath;
            ListViewItem item = listView1.SelectedItems[0];
            sourcePath = label_Path.Text + "\\" + item.Text;
            zipPath = item.Text + ".zip";
            MessageBox.Show("sourcePath: " + sourcePath + "\nzipPath: " + zipPath);
            CompressZipByIO(sourcePath, zipPath);
            SettingListView(label_Path.Text);
        }

        private void LOpen_Click(object sender, EventArgs e)
        {
            //열기
            try
            {
                //back = label1.Text;
                byte[] decbytes;
                ListViewItem item = listView1.SelectedItems[0];
                string file = label_Path.Text + "\\" + item.Text;

                FileAttributes attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    back_stack.AddLast(label_Path.Text);
                    SetButtonEnable();
                    label_Path.Text = file;
                    SettingListView(file);
                }
                else
                {
                    if (file.Contains(".enc"))
                    {
                        Thread t1 = new Thread(new ThreadStart(Run));
                        t1.Start();
                        waitforsinglesignal.WaitOne();
                        void Run()
                        {
                            decbytes = k.AESDecrypto256(File.ReadAllBytes(file), key);
                            file = file.Replace(".enc", "");
                            File.WriteAllBytes(file, decbytes);
                            waitforsinglesignal.Set();
                        }

                        //Thread.Sleep(1000);
                    }
                    string filename = Path.GetFileName(file);
                    process_FileStart.StartInfo.FileName = filename;
                    process_FileStart.StartInfo.WorkingDirectory = label_Path.Text;
                    process_FileStart.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process_FileStart.Start();

                    SettingListView(label_Path.Text);
                }
            }
            catch (Exception e1)
            {
                //MessageBox.Show("에러 : " + e1.Message);
            }
        }

        private void LCut_Click(object sender, EventArgs e)
        {
            //잘라내기
            throw new NotImplementedException();
        }

        private void LCopy_Click(object sender, EventArgs e)
        {
            int indexnum = listView1.FocusedItem.Index;
            string name = listView1.Items[indexnum].SubItems[0].Text;
            if (name == @"Z:\")
                name = @"Z:\";
            else
                name = label_Path + "\\" + name;
            DirectoryCopy(name);
        }
        void DirectoryCopy(string source)
        {
            DirectoryInfo dir = new DirectoryInfo(source);
            if (!dir.Exists)
            {
                {
                    throw new DirectoryNotFoundException(
                        "폴더를 찾을 수 없거나 없는 폴더입니다."
                        + source);
                }
            }

        }
        private void LProp_Click(object sender, EventArgs e)
        {
            //속성
            Show_Property1();
        }

        private void LfProp_Click(object sender, EventArgs e)
        {
            Show_Property1();
        }
        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lvHit = listView1.HitTest(e.Location);

            if (e.Button == MouseButtons.Right)
            {
                if (lvHit.Location == ListViewHitTestLocations.None)
                {
                    // TODO: 빈공간 마우스 오른쪽 버튼
                    ContextMenuStrip m = new ContextMenuStrip();
                    ToolStripMenuItem New = new ToolStripMenuItem("새 폴더", null);
                    ToolStripMenuItem Sort = new ToolStripMenuItem("정렬", null);
                    ToolStripMenuItem Create = new ToolStripMenuItem("새로 만들기", null);
                    ToolStripMenuItem Property = new ToolStripMenuItem("속성", null);

                    ToolStripMenuItem Sort_Name = new ToolStripMenuItem("이름", null);
                    ToolStripMenuItem Sort_Date = new ToolStripMenuItem("수정한 날짜", null);
                    ToolStripMenuItem Sort_Type = new ToolStripMenuItem("유형", null);
                    ToolStripMenuItem Sort_Size = new ToolStripMenuItem("크기", null);

                    ToolStripMenuItem Create_Text = new ToolStripMenuItem("텍스트 문서", null);
                    ToolStripMenuItem Create_Link = new ToolStripMenuItem("바로가기", null);

                    m.Items.Add(New);
                    m.Items.Add(Sort);
                    m.Items.Add(Create);
                    m.Items.Add(Property);

                    Sort.DropDownItems.Add(Sort_Name);
                    Sort.DropDownItems.Add(Sort_Date);
                    Sort.DropDownItems.Add(Sort_Type);
                    Sort.DropDownItems.Add(Sort_Size);

                    Create.DropDownItems.Add(Create_Text);
                    Create.DropDownItems.Add(Create_Link);

                    m.Show(listView1, new Point(e.X, e.Y));
                }
            }
        }
        private void process_FileStart_Exited(object sender, EventArgs e)
        {
            string filename = label_Path.Text.Substring(0, label_Path.Text.IndexOf("\\")) + "\\.aa\\" + process_FileStart.StartInfo.FileName;
            try
            {
                File.Delete(filename); // 프로세스 종료시 파일삭제
            }
            catch(Exception e2)
            {
                //MessageBox.Show(e2 + "");
            }
            SettingListView(label_Path.Text);
        }
        private void Button_Back_Click(object sender, EventArgs e)
        {
            front_stack.AddLast(label_Path.Text);
            label_Path.Text = back_stack.Last.Value;
            SettingListView(back_stack.Last.Value);
            back_stack.RemoveLast();
            SetButtonEnable();
        }
        private void Button_Front_Click(object sender, EventArgs e)
        {
            back_stack.AddLast(label_Path.Text);
            label_Path.Text = front_stack.Last.Value;
            SettingListView(front_stack.Last.Value);
            front_stack.RemoveLast();
            SetButtonEnable();
        }
        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
        void CloseNetDrive()
        {
            string DriveAlphabet;
            int i = 0;
            foreach (KeyValuePair<string, string> item in StatUser.Folder)
            {
                DriveAlphabet = (char)('Z' - i++) + ":";
                netDrive.CencelRemoteServer(DriveAlphabet);
            }
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 네트워크 드라이브 해제
            CloseNetDrive();
        }
        private void CboListViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboListViewMode.Text)
            {
                case "자세히":
                    listView1.View = View.Details;
                    break;
                case "큰아이콘":
                    listView1.View = View.LargeIcon;
                    break;
                case "리스트":
                    listView1.View = View.List;
                    break;
                case "타일":
                    listView1.View = View.Tile;
                    break;
            }
        }
        /// <summary>
        /// 디렉토리내 파일 검색
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public static List<String> GetFileList(String rootPath, List<String> fileList)
        {
            if (fileList == null)
            {
                return null;
            }
            var attr = File.GetAttributes(rootPath);
            // 해당 path가 디렉토리이면
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var dirInfo = new DirectoryInfo(rootPath);
                // 하위 모든 디렉토리는
                foreach (var dir in dirInfo.GetDirectories())
                {
                    // 재귀로 통하여 list를 취득한다.
                    GetFileList(dir.FullName, fileList);
                }
                // 하위 모든 파일은
                foreach (var file in dirInfo.GetFiles())
                {
                    // 재귀를 통하여 list를 취득한다.
                    GetFileList(file.FullName, fileList);
                }
            }
            // 해당 path가 파일이면 (재귀를 통해 들어온 경로)
            else
            {
                var fileInfo = new FileInfo(rootPath);
                // 리스트에 full path를 저장한다.
                fileList.Add(fileInfo.FullName);
            }
            return fileList;
        }

        // 상위폴더로 이동하는 버튼
        private void Button_Parent_Click(object sender, EventArgs e)
        {
            label_Path.Text = label_Path.Text.Substring(0, label_Path.Text.LastIndexOf('\\'));
            SettingListView(label_Path.Text);
        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Button.Equals(MouseButtons.Right))
            {
                select = e.Node.FullPath;
                fullPath = select.Replace("Z:\\\\", "Z:\\");
                cmsTrayMenu.Show(MousePosition.X, MousePosition.Y);
                fol = treeView1.GetNodeAt(e.X, e.Y);

                Rename.Click += (senders, es) =>
                {
                    treeView1.LabelEdit = true;
                    if (!e.Node.IsEditing)
                    {
                        e.Node.BeginEdit();
                    }
                };
            }
        }

        private void Expand_Click(object sender, EventArgs e)
        {
            fol.Expand();
        }

        private void Open_Click_1(object sender, EventArgs e)
        {
            SettingListView(select);
        }

        private void Rename_Click_1(object sender, EventArgs e)
        {
            // 이름바꾸기
        }
        public long GetDirectorySize(string directoryPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            FileSystemInfo[] fileSystemInfoArray = directoryInfo.GetFileSystemInfos();
            long directorySize = 0L;
            for (int i = 0; i < fileSystemInfoArray.Length; i++)
            {
                FileInfo fileInfo = fileSystemInfoArray[i] as FileInfo;
                if (fileInfo != null)
                {
                    directorySize += fileInfo.Length;
                }
            }
            if (directorySize > 1024)
                return directorySize = directorySize / 1024;
            else
                return directorySize;
        }
        //트리뷰속성창
        private void Show_Property()
        {
            //속성창 띄우기
            property_dialog pd = new property_dialog();
            DirectoryInfo dri = new DirectoryInfo(label_Path.Text);
            string name = fol.Text;
            string exten = "파일 폴더";
            long size = GetDirectorySize(select);
            string crea = dri.CreationTime.ToString();
            string write = dri.LastWriteTime.ToString();
            string type = dri.Attributes.ToString();
            string loca = fullPath;
            pd.properties(name, exten, loca, size, crea, write, type);
        }
        //리스트뷰 속성창
        private void Show_Property1()
        {
            //속성창 띄우기
            property_dialog pd = new property_dialog();
            int indexnum = listView1.FocusedItem.Index;
            string name = listView1.Items[indexnum].SubItems[0].Text;
            string exten;
            long size;
            string crea;
            string write;
            string type;
            string loca;
            string item = "";
            if (label_Path.Text.Equals(@"Z:\"))
                item = @"Z:\" + name;
            else
                item = label_Path.Text + @"\" + name;

            FileInfo file = new FileInfo(item);
            DirectoryInfo dri = new DirectoryInfo(item);

            if (name.Contains("."))
            {
                exten = file.Extension;
                size = file.Length;
                crea = file.CreationTime.ToString();
                write = file.LastWriteTime.ToString();
                type = file.Attributes.ToString();
                loca = item;
                pd.properties(name, exten, loca, size, crea, write, type);
            }
            else
            {
                exten = "파일 폴더";
                size = GetDirectorySize(item);
                crea = dri.CreationTime.ToString();
                write = dri.LastWriteTime.ToString();
                type = dri.Attributes.ToString();
                loca = item;
                pd.properties(name, exten, loca, size, crea, write, type);
            }
        }

        private void Prop_Click_1(object sender, EventArgs e)
        {
            Show_Property();
        }
        private void Folder_Click(object sender, EventArgs e)
        {
            NewFolder nf = new NewFolder();
            nf.ShowDialog();
            if (nf.folderName == null || nf.folderName == "") return;

            select = select.Replace("\\\\", "\\");
            DirectoryInfo di = new DirectoryInfo(select + "\\" + nf.folderName);
            if(di.Exists)
            {
                MessageBox.Show("이미 존재하는 폴더입니다!");
            }
            else
            {
                di.Create();
                //treeView1.Nodes.Clear();
                TreeNode newnode = fol.Nodes.Add(nf.folderName);
                newnode.ImageIndex = 6;
                newnode.SelectedImageIndex = 6;

                SettingListView(label_Path.Text);
            }
        }
        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                if (e.Label == null)
                    return;
                else
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    string Name = label_Path.Text + "\\" + item.Text;
                    string newName = label_Path.Text + "\\" + e.Label;
                    Rename_(Name, newName);
                }
            }
            catch (Exception exc)
            {
            }
        }
        private void Rename_(string oldName, string newName)
        {
            FileAttributes attr = File.GetAttributes(oldName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                System.IO.Directory.Move(oldName, newName);
            }
            else
            {
                System.IO.File.Move(oldName, newName);
            }
        }

        private void ListView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                listView1.LabelEdit = true;
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        private void LfOpen_Click(object sender, EventArgs e)
        {

        }

        private void LDelete_Click(object sender, EventArgs e)
        {

        }

        private void TreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (e.Label == null) 
                    return;
                else
                {
                    string Name = fullPath;
                    string newName = "";
                    string[] nm = fullPath.Split('\\');
                    for(int i = 0; i<nm.Length-1; i++)
                    {
                        newName += nm[i];
                    }
                    newName += e.Label;
                    Rename_(Name, newName);
                }
            }
            catch (Exception exc)
            {
            }
        }

        /// <summary>
        /// 내부 라이브러리로 압축하기
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="zipPath"></param>
        public static void CompressZipByIO(string sourcePath, string zipPath)
        {
            var filelist = GetFileList(sourcePath, new List<String>());
            using (FileStream fileStream = new FileStream(zipPath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    foreach (string file in filelist)
                    {
                        string path = file.Substring(sourcePath.Length + 1);
                        zipArchive.CreateEntryFromFile(file, path);
                    }
                }
            }
        }
    }
}