using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InTheForest
{
    public partial class Form1 : Form
    {
        int Back_init;
        LinkedList<string> front_stack, back_stack;
        string key;
        AES k;
        private static EventWaitHandle waitforsinglesignal;
        private int mask;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Back_init = 1; // 시작할 때 Back 버튼 비활성화를 위한 마스크값
            front_stack = new LinkedList<string>();
            back_stack = new LinkedList<string>();
            SetButtonEnable();
            string[] drives = Directory.GetLogicalDrives();

            foreach(string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);
                if (di.IsReady)
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

            k = new AES();
            waitforsinglesignal = new EventWaitHandle(false, EventResetMode.AutoReset);
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
                    long size = fileinfo.Length / 1024;
                    listView1.Items[Count].SubItems.Add(fileinfo.Attributes.ToString());
                    listView1.Items[Count].SubItems.Add(size.ToString() + " KB");
                    Count++;
                    
                label1.Text = Count + "개";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("에러 발생 : " + ex.Message);
            }
        }
        /*--------------------------------------
           키값 생성 및 레지스트리 등록
           -------------------------------------*/
        public bool GET_BIT(int x, int y) // 서버에서 시스템 정책 마스크값 가져오면 한비트씩 빼내기
        {
            if ((((x) >> (y)) & 0x01) == 1) return true;
            return false;
        }

        public void UpdateRegistry(string root, int value, string name) // 정책에 맞게 레지스트리 업데이트
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(root, true);
            if (reg == null)
            {
                // 해당이름으로 서브키 생성
                reg = Registry.CurrentUser.CreateSubKey(root);
            }
            reg.SetValue(name, value);
            reg.Close();
        }

        public void UpdatePolicy() // 마스크값에 따라 시스템 정책변경
        {
            int value;
            // 1. TaskMgr enable(0), disable(11)
            if (GET_BIT(mask, 0)) value = 1;
            else value = 0;
            UpdateRegistry("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", value, "DisableTaskMgr");

            // 2. regedit enable(0), disable(1)
            if (GET_BIT(mask, 1)) value = 1;
            else value = 0;
            UpdateRegistry("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", value, "DisableRegistryTools");

            // 3. cmd enable(0), disable(2)
            if (GET_BIT(mask, 2)) value = 2;
            else value = 0;
            UpdateRegistry("Software\\Policies\\Microsoft\\Windows\\System", value, "DisableCMD");

            // 4. snipping tools disable(1), enable(0)
            if (GET_BIT(mask, 3)) value = 1;
            else value = 0;
            UpdateRegistry("SOFTWARE\\Policies\\Microsoft\\TabletPC", value, "DisableSnippingTool");

            // 5. usb쓰기 disable(1), enable(0)
            if (GET_BIT(mask, 4)) value = 1;
            else value = 0;
            UpdateRegistry("SYSTEM\\CurrentControlSet\\Control\\StorageDevicepolicies", value, "WriteProtect");

            // 6. usb차단 disable(4) enable(3)
            if (GET_BIT(mask, 5)) value = 4;
            else value = 3;
            UpdateRegistry("SYSTEM\\CurrentControlSet\\Services\\USBSTOR", value, "Start");

            // 7. 디스크차단(C드라이브) disable(4) enable(0)
            if (GET_BIT(mask, 6)) value = 4;
            else value = 0;
            UpdateRegistry("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer", value, "NoViewOnDrive");
        }

        // 키값 생성 함수(그냥 랜덤)
        public string GetRandomPassword(int _totLen)
        {
            Random rand = new Random();
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, _totLen)
                .Select(x => input[rand.Next(0, input.Length)]);
            return new string(chars.ToArray());
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
                            MessageBox.Show(file);
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
                else
                {

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error : " + ex);
            }
        }
        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            string filename;
            byte[] encbytes;
            key = GetRandomPassword(32);
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                filename = Path.GetFileName(file);
                encbytes = k.AESEncrypto256(File.ReadAllBytes(file), key);
                File.WriteAllBytes(label_Path.Text + "\\" + filename + ".enc", encbytes);
                SettingListView(label_Path.Text);
            }
        }

        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    string file = label_Path.Text + "\\" + item.Text;
                    File.Delete(file);
                    SettingListView(label_Path.Text);
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
                        string selected = listView1.GetItemAt(e.X, e.Y).Text;
                        ContextMenuStrip m = new ContextMenuStrip();
                        ToolStripMenuItem Open = new ToolStripMenuItem();
                        ToolStripMenuItem Com = new ToolStripMenuItem();
                        ToolStripMenuItem Link = new ToolStripMenuItem();
                        ToolStripMenuItem Cut = new ToolStripMenuItem();
                        ToolStripMenuItem Copy = new ToolStripMenuItem();
                        ToolStripMenuItem Del = new ToolStripMenuItem();
                        ToolStripMenuItem Rename = new ToolStripMenuItem();
                        ToolStripMenuItem Prop = new ToolStripMenuItem();


                        Open.Text = "열기";
                        Com.Text = "압축하기";
                        Link.Text = "바로가기";
                        Cut.Text = "잘라내기";
                        Copy.Text = "복사";
                        Del.Text = "삭제";
                        Rename.Text = "이름바꾸기";
                        Prop.Text = "속성";

                        m.Items.Add(Open);
                        m.Items.Add(Com);
                        m.Items.Add(Link);
                        m.Items.Add(Cut);
                        m.Items.Add(Copy);
                        m.Items.Add(Del);
                        m.Items.Add(Rename);
                        m.Items.Add(Prop);

                        m.Show(listView1, new Point(e.X, e.Y));
                    }
                    else
                    {
                        string selected = listView1.GetItemAt(e.X, e.Y).Text;
                        ContextMenuStrip m = new ContextMenuStrip();
                        ToolStripMenuItem Open = new ToolStripMenuItem();
                        ToolStripMenuItem Edit = new ToolStripMenuItem();
                        ToolStripMenuItem Link = new ToolStripMenuItem();
                        ToolStripMenuItem Conn = new ToolStripMenuItem();
                        ToolStripMenuItem Cut = new ToolStripMenuItem();
                        ToolStripMenuItem Copy = new ToolStripMenuItem();
                        ToolStripMenuItem Del = new ToolStripMenuItem();
                        ToolStripMenuItem Rename = new ToolStripMenuItem();
                        ToolStripMenuItem Prop = new ToolStripMenuItem();

                        Open.Text = "열기";
                        Edit.Text = "편집";
                        Conn.Text = "연결 프로그램";
                        Link.Text = "바로가기";
                        Cut.Text = "잘라내기";
                        Copy.Text = "복사";
                        Del.Text = "삭제";
                        Rename.Text = "이름바꾸기";
                        Prop.Text = "속성";

                        m.Items.Add(Open);
                        m.Items.Add(Edit);
                        m.Items.Add(Conn);
                        m.Items.Add(Link);
                        m.Items.Add(Cut);
                        m.Items.Add(Copy);
                        m.Items.Add(Del);
                        m.Items.Add(Rename);
                        m.Items.Add(Prop);

                        m.Show(listView1, new Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception e1)
            {

            }
        }

        private void ListView1_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lvHit = listView1.HitTest(e.Location);

            if (e.Button == MouseButtons.Right)
            {
                if (lvHit.Location == ListViewHitTestLocations.None)
                {
                    ContextMenuStrip m = new ContextMenuStrip();
                    ToolStripMenuItem New = new ToolStripMenuItem();
                    ToolStripMenuItem See = new ToolStripMenuItem();
                    ToolStripMenuItem Sort = new ToolStripMenuItem();
                    ToolStripMenuItem Create = new ToolStripMenuItem();
                    ToolStripMenuItem Property = new ToolStripMenuItem();

                    ToolStripMenuItem Icon_Big = new ToolStripMenuItem();
                    ToolStripMenuItem Icon_Small = new ToolStripMenuItem();
                    ToolStripMenuItem Icon_Detail = new ToolStripMenuItem();
                    Icon_Big.Text = "큰 아이콘";
                    Icon_Small.Text = "작은 아이콘";
                    Icon_Detail.Text = "자세히";

                    ToolStripMenuItem Sort_Name = new ToolStripMenuItem();
                    ToolStripMenuItem Sort_Date = new ToolStripMenuItem();
                    ToolStripMenuItem Sort_Type = new ToolStripMenuItem();
                    ToolStripMenuItem Sort_Size = new ToolStripMenuItem();
                    Sort_Name.Text = "이름";
                    Sort_Date.Text = "수정한 날짜";
                    Sort_Type.Text = "유형";
                    Sort_Size.Text = "크기";

                    ToolStripMenuItem Create_Text = new ToolStripMenuItem();
                    ToolStripMenuItem Create_Link = new ToolStripMenuItem();
                    Create_Text.Text = "텍스트 문서";
                    Create_Link.Text = "바로가기";

                    New.Text = "새 폴더";
                    See.Text = "보기";
                    Sort.Text = "정렬";
                    Create.Text = "새로 만들기";
                    Property.Text = "속성";

                    See.DropDownItems.Add(Icon_Big);
                    See.DropDownItems.Add(Icon_Small);
                    See.DropDownItems.Add(Icon_Detail);

                    m.Items.Add(New);
                    m.Items.Add(See);
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
            string filename = label_Path.Text + process_FileStart.StartInfo.FileName;
            File.Delete(filename); // 프로세스 종료시 파일삭제
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

        
    }
}
