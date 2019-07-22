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
        LinkedList<string> front_stack, back_stack;
        string key;
        AES k;
        private static EventWaitHandle waitforsinglesignal;
        private int mask;

        public Form1()
        {
            InitializeComponent();
        }

        void SetButtonEnable()
        {
            if (back_stack.Count == 0) button_Back.Enabled = false;
            else button_Back.Enabled = true;

            if (front_stack.Count == 0)button_Front.Enabled = false;
            else button_Front.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            front_stack = new LinkedList<string>();
            back_stack = new LinkedList<string>();
            SetButtonEnable(); // 앞으로 뒤로가기 버튼 초기화
            string[] drives = Directory.GetLogicalDrives();// 모든 논리적으로 구분되어 있는 드라이브들을 읽어들임

            foreach (string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);

                if (di.IsReady)// 드라이브가 준비되었는지 여부 판단. 이 조건으로 CD R/W 드라이브 등을 제외시킴.
                {
                    TreeNode node = treeView_Window.Nodes.Add(drive);
                    node.Nodes.Add("\\");    // 파일 경로 사이사이에 "\"를 추가함.
                }
            }
            k = new AES(); // AES 파일 암복호화에 사용할 키를 초기화
            waitforsinglesignal = new EventWaitHandle(false, EventResetMode.AutoReset); // waitforsingleobject()
        }

        private void SettingListView(string sFullPath) // 리스트뷰에 현위치에있는 리스트출력함수
        {
            try
            {
                // 기존의 파일 목록 제거
                listView_Window.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(sFullPath);

                int DirectCount = 0;
                // 하부 디렉토리 보여주기
                foreach (DirectoryInfo dirItem in dir.GetDirectories())
                {
                    // 하부 디렉토리가 존재할 경우 ListView에 추가
                    // ListViewItem 객체를 생성
                    ListViewItem lsvitem = new ListViewItem();

                    // 생성된 ListViewItem 객체에 똑같은 이미지를 할당
                    lsvitem.ImageIndex = 2;
                    lsvitem.Text = dirItem.Name;

                    // 아이템을 ListView(listView1)에 추가
                    listView_Window.Items.Add(lsvitem);

                    listView_Window.Items[DirectCount].SubItems.Add(dirItem.CreationTime.ToString());
                    listView_Window.Items[DirectCount].SubItems.Add("폴더");
                    listView_Window.Items[DirectCount].SubItems.Add(dirItem.GetFiles().Length.ToString() +
                        "files");
                    DirectCount++;
                }

                // 디렉토리에 존재하는 파일목록 보여주기
                FileInfo[] files = dir.GetFiles();
                int Count = 0;
                foreach (FileInfo fileinfo in files)
                {
                    ListViewItem lsvitem = new ListViewItem();
                    lsvitem.ImageIndex = 4;
                    lsvitem.Text = fileinfo.Name;
                    listView_Window.Items.Add(lsvitem);

                    if (fileinfo.LastWriteTime != null)
                    {
                        listView_Window.Items[Count].SubItems.Add(fileinfo.LastWriteTime.ToString());
                    }
                    else
                    {
                        listView_Window.Items[Count].SubItems.Add(fileinfo.CreationTime.ToString());
                    }
                    listView_Window.Items[Count].SubItems.Add(fileinfo.Attributes.ToString());
                    listView_Window.Items[Count].SubItems.Add(fileinfo.Length.ToString());
                    Count++;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("에러 발생 : " + ex.Message);
            }
        }

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

        private void treeView_Window_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //SettingListView(e.Node.FullPath);
        }

        private void listView_Window_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //back = label1.Text;
                byte[] decbytes;
                ListViewItem item = listView_Window.SelectedItems[0];
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

        private void treeView_Window_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text.Equals("\\"))
                {
                    e.Node.Nodes.Clear();

                    string path = e.Node.FullPath;
                    //string path = e.Node.FullPath.Substring(e.Node.FullPath.IndexOf("\\") + 1).Replace("\\\\", "\\");
                    //실제 절대 경로를 path에 저장합니다.
                    //MessageBox.Show(path);
                    string[] directories = Directory.GetDirectories(path);
                    // 현재 경로에서 존재하고 있는 폴더들을 하위 노드에 추가
                    //Directory.GetDirectories() 함수를 이용하여 인자에 입력된 경로상의 하위 디렉토리의 이름과 경로를 반환 시킵니다.
                    //하위 디렉토리가 여러개일 수 있으므로 string 배열로 저장 받습니다.
                    foreach (string directory in directories)//반복을 통해 트리 탐색창 의 노드에 추가시켜 화면에 하위 디렉토리를 보여주니다.
                    {
                        TreeNode newNode = e.Node.Nodes.Add(directory.Substring(directory.LastIndexOf("\\") + 1));
                        newNode.Nodes.Add("\\");
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error: " + ex);
            }
        }

        private void treeView_Window_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode current = e.Node;
            string path = current.FullPath;
            //MessageBox.Show(path);

            if(!path.Equals(label_Path.Text)) // 트리에서 같은 노드를 클릭한 경우엔 뒤로가기 할필요없음
            {
                back_stack.AddLast(label_Path.Text); // 뒤로가기 버튼 스택에 넣는 과정
            }
            SetButtonEnable();
            label_Path.Text = path.Replace("\\\\", "\\");
            SettingListView(label_Path.Text);
            try
            {
                if (label_Path.Text == "내 컴퓨터" || path == "Root")
                {
                    //webBr.Url = new Uri("C:\\");
                    label_Path.Text = "C:\\";
                }
                else
                {
                    //MessageBox.Show(path);
                    //webBr.Url = new Uri(TaxtPath.Text);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("error: " + ex);
            }
            
        }

        private void listView_Window_DragDrop(object sender, DragEventArgs e)
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

        private void listView_Window_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void listView_Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    ListViewItem item = listView_Window.SelectedItems[0];
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

        private void button_Back_Click(object sender, EventArgs e)
        {
            front_stack.AddLast(label_Path.Text);
            label_Path.Text = back_stack.Last.Value;
            SettingListView(back_stack.Last.Value);
            back_stack.RemoveLast();
            SetButtonEnable();
        }

        private void button_Front_Click(object sender, EventArgs e)
        {
            back_stack.AddLast(label_Path.Text);
            label_Path.Text = front_stack.Last.Value;
            SettingListView(front_stack.Last.Value);
            front_stack.RemoveLast();
            SetButtonEnable();
        }

        private void TreeView_Window_Click(object sender, EventArgs e)
        {
            //back_stack.AddLast(label_Path.Text);
        }

        private void TreeView_Window_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //SettingListView(e.Node.FullPath);
        }

        private void TreeView_Window_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SettingListView(e.Node.FullPath);
        }

        private void ListView_Window_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button.Equals(MouseButtons.Right))
                {

                    ListViewItem item = listView_Window.SelectedItems[0];
                    string file = label_Path.Text + "\\" + item.Text;

                    FileAttributes attr = File.GetAttributes(file);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        // TODO: 마우스 오른쪽 버튼 클릭시 디렉토리일 때
                        string selected = listView_Window.GetItemAt(e.X, e.Y).Text;
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

                        m.Show(listView_Window, new Point(e.X, e.Y));
                    }
                    else
                    {
                        // TODO: 마우스 오른쪽 버튼 클릭시 파일일 때
                        string selected = listView_Window.GetItemAt(e.X, e.Y).Text;
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

                        m.Show(listView_Window, new Point(e.X, e.Y));
                    }
                }
            }
            catch (Exception e1)
            {

            }
        }

        private void ListView_Window_MouseUp(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lvHit = listView_Window.HitTest(e.Location);

            if (e.Button == MouseButtons.Right)
            {
                if (lvHit.Location == ListViewHitTestLocations.None)
                {
                    // TODO: 마우스 오른쪽 버튼 클릭시 빈 공간일 때
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

                    m.Show(listView_Window, new Point(e.X, e.Y));
                }
            }
        }

        private void process_FileStart_Exited(object sender, EventArgs e)
        {
            string filename = label_Path.Text + process_FileStart.StartInfo.FileName;
            //MessageBox.Show(filename);
            File.Delete(filename); // 프로세스 종료시 파일삭제
            SettingListView(label_Path.Text);
        }
    }
}


// Hello World
// 진짜코드