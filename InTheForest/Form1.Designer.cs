﻿namespace InTheForest
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button_Parent = new System.Windows.Forms.Button();
            this.button_Front = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label_Path = new System.Windows.Forms.TextBox();
            this.cboListViewMode = new System.Windows.Forms.ComboBox();
            this.process_FileStart = new System.Diagnostics.Process();
            this.cmsTrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Expand = new System.Windows.Forms.ToolStripMenuItem();
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.New = new System.Windows.Forms.ToolStripMenuItem();
            this.Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.Prop = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmenu_list1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.lZip = new System.Windows.Forms.ToolStripMenuItem();
            this.lCut = new System.Windows.Forms.ToolStripMenuItem();
            this.lCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.lPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.lDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lRename = new System.Windows.Forms.ToolStripMenuItem();
            this.lProp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsmenu_list2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lfOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.lfCut = new System.Windows.Forms.ToolStripMenuItem();
            this.lfCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.lfDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.lfRename = new System.Windows.Forms.ToolStripMenuItem();
            this.lfProp = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsTrayMenu.SuspendLayout();
            this.cmsmenu_list1.SuspendLayout();
            this.cmsmenu_list2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(23, 75);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button_Parent);
            this.splitContainer1.Panel1.Controls.Add(this.button_Front);
            this.splitContainer1.Panel1.Controls.Add(this.button_Back);
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Panel2.Controls.Add(this.label_Path);
            this.splitContainer1.Panel2.Controls.Add(this.cboListViewMode);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(929, 612);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1_SplitterMoved);
            // 
            // button_Parent
            // 
            this.button_Parent.BackColor = System.Drawing.SystemColors.Menu;
            this.button_Parent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Parent.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Parent.Location = new System.Drawing.Point(130, 46);
            this.button_Parent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Parent.Name = "button_Parent";
            this.button_Parent.Size = new System.Drawing.Size(48, 35);
            this.button_Parent.TabIndex = 3;
            this.button_Parent.Text = "▲";
            this.button_Parent.UseVisualStyleBackColor = false;
            this.button_Parent.Click += new System.EventHandler(this.Button_Parent_Click);
            // 
            // button_Front
            // 
            this.button_Front.BackColor = System.Drawing.SystemColors.Menu;
            this.button_Front.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Front.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Front.Location = new System.Drawing.Point(75, 46);
            this.button_Front.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Front.Name = "button_Front";
            this.button_Front.Size = new System.Drawing.Size(48, 35);
            this.button_Front.TabIndex = 2;
            this.button_Front.Text = "▶▶";
            this.button_Front.UseVisualStyleBackColor = false;
            this.button_Front.Click += new System.EventHandler(this.Button_Front_Click);
            // 
            // button_Back
            // 
            this.button_Back.BackColor = System.Drawing.SystemColors.Menu;
            this.button_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Back.FlatAppearance.BorderColor = System.Drawing.SystemColors.Menu;
            this.button_Back.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Menu;
            this.button_Back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Menu;
            this.button_Back.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Back.Location = new System.Drawing.Point(21, 46);
            this.button_Back.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(48, 35);
            this.button_Back.TabIndex = 1;
            this.button_Back.Text = "◀◀";
            this.button_Back.UseVisualStyleBackColor = false;
            this.button_Back.Click += new System.EventHandler(this.Button_Back_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(11, 99);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(284, 502);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView1_AfterLabelEdit);
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "notepad.png");
            this.imageList1.Images.SetKeyName(1, "microsoft_office_word-512.png");
            this.imageList1.Images.SetKeyName(2, "833px-PDF_file_icon.svg.png");
            this.imageList1.Images.SetKeyName(3, "1200px-Adobe_Photoshop_CC_icon.svg.png");
            this.imageList1.Images.SetKeyName(4, "64.ico");
            this.imageList1.Images.SetKeyName(5, "320.ico");
            this.imageList1.Images.SetKeyName(6, "10.ico");
            this.imageList1.Images.SetKeyName(7, "103.ico");
            this.imageList1.Images.SetKeyName(8, "2.ico");
            this.imageList1.Images.SetKeyName(9, "1532.ico");
            this.imageList1.Images.SetKeyName(10, "1431.ico");
            this.imageList1.Images.SetKeyName(11, "itf.png");
            this.imageList1.Images.SetKeyName(12, "doc.png");
            this.imageList1.Images.SetKeyName(13, "ppt.png");
            this.imageList1.Images.SetKeyName(14, "xl.png");
            this.imageList1.Images.SetKeyName(15, "hwp.png");
            this.imageList1.Images.SetKeyName(16, "txt.png");
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.LargeImageList = this.imageList2;
            this.listView1.Location = new System.Drawing.Point(15, 99);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(587, 502);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listView1_AfterLabelEdit);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListView1_DragEnter);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView1_KeyDown);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListView1_KeyUp);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseClick);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseDoubleClick);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseUp);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "notepad.png");
            this.imageList2.Images.SetKeyName(1, "microsoft_office_word-512.png");
            this.imageList2.Images.SetKeyName(2, "833px-PDF_file_icon.svg.png");
            this.imageList2.Images.SetKeyName(3, "1200px-Adobe_Photoshop_CC_icon.svg.png");
            this.imageList2.Images.SetKeyName(4, "64.ico");
            this.imageList2.Images.SetKeyName(5, "320.ico");
            this.imageList2.Images.SetKeyName(6, "10.ico");
            this.imageList2.Images.SetKeyName(7, "103.ico");
            this.imageList2.Images.SetKeyName(8, "2.ico");
            this.imageList2.Images.SetKeyName(9, "1532.ico");
            this.imageList2.Images.SetKeyName(10, "1431.ico");
            this.imageList2.Images.SetKeyName(11, "itf.png");
            this.imageList2.Images.SetKeyName(12, "doc.png");
            this.imageList2.Images.SetKeyName(13, "ppt.png");
            this.imageList2.Images.SetKeyName(14, "xl.png");
            this.imageList2.Images.SetKeyName(15, "hwp.png");
            this.imageList2.Images.SetKeyName(16, "txt.png");
            // 
            // label_Path
            // 
            this.label_Path.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Path.Location = new System.Drawing.Point(15, 52);
            this.label_Path.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label_Path.Name = "label_Path";
            this.label_Path.Size = new System.Drawing.Size(404, 27);
            this.label_Path.TabIndex = 3;
            // 
            // cboListViewMode
            // 
            this.cboListViewMode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboListViewMode.FormattingEnabled = true;
            this.cboListViewMode.Location = new System.Drawing.Point(425, 52);
            this.cboListViewMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboListViewMode.Name = "cboListViewMode";
            this.cboListViewMode.Size = new System.Drawing.Size(177, 28);
            this.cboListViewMode.TabIndex = 2;
            this.cboListViewMode.SelectedIndexChanged += new System.EventHandler(this.CboListViewMode_SelectedIndexChanged);
            // 
            // process_FileStart
            // 
            this.process_FileStart.EnableRaisingEvents = true;
            this.process_FileStart.StartInfo.Domain = "";
            this.process_FileStart.StartInfo.LoadUserProfile = false;
            this.process_FileStart.StartInfo.Password = null;
            this.process_FileStart.StartInfo.StandardErrorEncoding = null;
            this.process_FileStart.StartInfo.StandardOutputEncoding = null;
            this.process_FileStart.StartInfo.UserName = "";
            this.process_FileStart.SynchronizingObject = this;
            this.process_FileStart.Exited += new System.EventHandler(this.process_FileStart_Exited);
            // 
            // cmsTrayMenu
            // 
            this.cmsTrayMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Expand,
            this.Open,
            this.Rename,
            this.New,
            this.Prop});
            this.cmsTrayMenu.Name = "cmsTrayMenu";
            this.cmsTrayMenu.Size = new System.Drawing.Size(154, 124);
            // 
            // Expand
            // 
            this.Expand.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Expand.Name = "Expand";
            this.Expand.Size = new System.Drawing.Size(153, 24);
            this.Expand.Text = "확장";
            this.Expand.Click += new System.EventHandler(this.Expand_Click);
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(153, 24);
            this.Open.Text = "열기";
            this.Open.Click += new System.EventHandler(this.Open_Click_1);
            // 
            // Rename
            // 
            this.Rename.Name = "Rename";
            this.Rename.Size = new System.Drawing.Size(153, 24);
            this.Rename.Text = "이름바꾸기";
            this.Rename.Click += new System.EventHandler(this.Rename_Click_1);
            // 
            // New
            // 
            this.New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Folder});
            this.New.Name = "New";
            this.New.Size = new System.Drawing.Size(153, 24);
            this.New.Text = "새로만들기";
            // 
            // Folder
            // 
            this.Folder.Name = "Folder";
            this.Folder.Size = new System.Drawing.Size(122, 26);
            this.Folder.Text = "폴더";
            this.Folder.Click += new System.EventHandler(this.Folder_Click);
            // 
            // Prop
            // 
            this.Prop.Name = "Prop";
            this.Prop.Size = new System.Drawing.Size(153, 24);
            this.Prop.Text = "속성";
            this.Prop.Click += new System.EventHandler(this.Prop_Click_1);
            // 
            // cmsmenu_list1
            // 
            this.cmsmenu_list1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsmenu_list1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lOpen,
            this.lZip,
            this.lCut,
            this.lCopy,
            this.lPaste,
            this.lDelete,
            this.lRename,
            this.lProp});
            this.cmsmenu_list1.Name = "cmsmenu_list1";
            this.cmsmenu_list1.Size = new System.Drawing.Size(154, 196);
            // 
            // lOpen
            // 
            this.lOpen.Name = "lOpen";
            this.lOpen.Size = new System.Drawing.Size(153, 24);
            this.lOpen.Text = "열기";
            this.lOpen.Click += new System.EventHandler(this.LOpen_Click);
            // 
            // lZip
            // 
            this.lZip.Name = "lZip";
            this.lZip.Size = new System.Drawing.Size(153, 24);
            this.lZip.Text = "압축하기";
            this.lZip.Click += new System.EventHandler(this.LZip_Click);
            // 
            // lCut
            // 
            this.lCut.Name = "lCut";
            this.lCut.Size = new System.Drawing.Size(153, 24);
            this.lCut.Text = "잘라내기";
            this.lCut.Click += new System.EventHandler(this.LCut_Click);
            // 
            // lCopy
            // 
            this.lCopy.Name = "lCopy";
            this.lCopy.Size = new System.Drawing.Size(153, 24);
            this.lCopy.Text = "복사";
            this.lCopy.Click += new System.EventHandler(this.LCopy_Click);
            // 
            // lPaste
            // 
            this.lPaste.Name = "lPaste";
            this.lPaste.Size = new System.Drawing.Size(153, 24);
            this.lPaste.Text = "붙여넣기";
            // 
            // lDelete
            // 
            this.lDelete.Name = "lDelete";
            this.lDelete.Size = new System.Drawing.Size(153, 24);
            this.lDelete.Text = "삭제";
            this.lDelete.Click += new System.EventHandler(this.LDelete_Click);
            // 
            // lRename
            // 
            this.lRename.Name = "lRename";
            this.lRename.Size = new System.Drawing.Size(153, 24);
            this.lRename.Text = "이름바꾸기";
            // 
            // lProp
            // 
            this.lProp.Name = "lProp";
            this.lProp.Size = new System.Drawing.Size(153, 24);
            this.lProp.Text = "속성";
            this.lProp.Click += new System.EventHandler(this.LProp_Click);
            // 
            // cmsmenu_list2
            // 
            this.cmsmenu_list2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsmenu_list2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lfOpen,
            this.lfCut,
            this.lfCopy,
            this.lfDelete,
            this.lfRename,
            this.lfProp});
            this.cmsmenu_list2.Name = "cmsmenu_list2";
            this.cmsmenu_list2.Size = new System.Drawing.Size(154, 148);
            // 
            // lfOpen
            // 
            this.lfOpen.Name = "lfOpen";
            this.lfOpen.Size = new System.Drawing.Size(153, 24);
            this.lfOpen.Text = "열기";
            this.lfOpen.Click += new System.EventHandler(this.LfOpen_Click);
            // 
            // lfCut
            // 
            this.lfCut.Name = "lfCut";
            this.lfCut.Size = new System.Drawing.Size(153, 24);
            this.lfCut.Text = "잘라내기";
            // 
            // lfCopy
            // 
            this.lfCopy.Name = "lfCopy";
            this.lfCopy.Size = new System.Drawing.Size(153, 24);
            this.lfCopy.Text = "복사";
            // 
            // lfDelete
            // 
            this.lfDelete.Name = "lfDelete";
            this.lfDelete.Size = new System.Drawing.Size(153, 24);
            this.lfDelete.Text = "삭제";
            // 
            // lfRename
            // 
            this.lfRename.Name = "lfRename";
            this.lfRename.Size = new System.Drawing.Size(153, 24);
            this.lfRename.Text = "이름바꾸기";
            // 
            // lfProp
            // 
            this.lfProp.Name = "lfProp";
            this.lfProp.Size = new System.Drawing.Size(153, 24);
            this.lfProp.Text = "속성";
            this.lfProp.Click += new System.EventHandler(this.LfProp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 712);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(23, 75, 23, 25);
            this.Text = "InTheForest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmsTrayMenu.ResumeLayout(false);
            this.cmsmenu_list1.ResumeLayout(false);
            this.cmsmenu_list2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Button button_Front;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.ComboBox cboListViewMode;
        private System.Diagnostics.Process process_FileStart;
        private System.Windows.Forms.TextBox label_Path;
        private System.Windows.Forms.Button button_Parent;
        private System.Windows.Forms.ContextMenuStrip cmsTrayMenu;
        private System.Windows.Forms.ToolStripMenuItem Expand;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem Rename;
        private System.Windows.Forms.ToolStripMenuItem New;
        private System.Windows.Forms.ToolStripMenuItem Folder;
        private System.Windows.Forms.ToolStripMenuItem Prop;
        private System.Windows.Forms.ContextMenuStrip cmsmenu_list1;
        private System.Windows.Forms.ToolStripMenuItem lOpen;
        private System.Windows.Forms.ToolStripMenuItem lZip;
        private System.Windows.Forms.ToolStripMenuItem lCut;
        private System.Windows.Forms.ToolStripMenuItem lCopy;
        private System.Windows.Forms.ToolStripMenuItem lDelete;
        private System.Windows.Forms.ToolStripMenuItem lRename;
        private System.Windows.Forms.ToolStripMenuItem lProp;
        private System.Windows.Forms.ContextMenuStrip cmsmenu_list2;
        private System.Windows.Forms.ToolStripMenuItem lfOpen;
        private System.Windows.Forms.ToolStripMenuItem lfCut;
        private System.Windows.Forms.ToolStripMenuItem lfCopy;
        private System.Windows.Forms.ToolStripMenuItem lfDelete;
        private System.Windows.Forms.ToolStripMenuItem lfRename;
        private System.Windows.Forms.ToolStripMenuItem lfProp;
        private System.Windows.Forms.ToolStripMenuItem lPaste;
    }
}

