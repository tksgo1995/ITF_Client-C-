namespace InTheForest
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
            this.button_Front = new System.Windows.Forms.Button();
            this.button_Back = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label_Path = new System.Windows.Forms.TextBox();
            this.cboListViewMode = new System.Windows.Forms.ComboBox();
            this.process_FileStart = new System.Diagnostics.Process();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
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
            this.splitContainer1.Size = new System.Drawing.Size(811, 490);
            this.splitContainer1.SplitterDistance = 270;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1_SplitterMoved);
            // 
            // button_Front
            // 
            this.button_Front.BackColor = System.Drawing.SystemColors.Menu;
            this.button_Front.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Front.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Front.Location = new System.Drawing.Point(66, 37);
            this.button_Front.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Front.Name = "button_Front";
            this.button_Front.Size = new System.Drawing.Size(42, 28);
            this.button_Front.TabIndex = 2;
            this.button_Front.Text = ">>";
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
            this.button_Back.Location = new System.Drawing.Point(18, 37);
            this.button_Back.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(42, 28);
            this.button_Back.TabIndex = 1;
            this.button_Back.Text = "<<";
            this.button_Back.UseVisualStyleBackColor = false;
            this.button_Back.Click += new System.EventHandler(this.Button_Back_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(10, 79);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(249, 402);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1_BeforeExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
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
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.AllowDrop = true;
            this.listView1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listView1.HoverSelection = true;
            this.listView1.LargeImageList = this.imageList2;
            this.listView1.Location = new System.Drawing.Point(13, 79);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(514, 402);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListView1_DragEnter);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListView1_KeyDown);
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
            // 
            // label_Path
            // 
            this.label_Path.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Path.Location = new System.Drawing.Point(13, 42);
            this.label_Path.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label_Path.Name = "label_Path";
            this.label_Path.Size = new System.Drawing.Size(354, 23);
            this.label_Path.TabIndex = 3;
            // 
            // cboListViewMode
            // 
            this.cboListViewMode.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboListViewMode.FormattingEnabled = true;
            this.cboListViewMode.Location = new System.Drawing.Point(372, 42);
            this.cboListViewMode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboListViewMode.Name = "cboListViewMode";
            this.cboListViewMode.Size = new System.Drawing.Size(155, 23);
            this.cboListViewMode.TabIndex = 2;
            this.cboListViewMode.SelectedIndexChanged += new System.EventHandler(this.CboListViewMode_SelectedIndexChanged);
            // 
            // process_FileStart
            // 
            this.process_FileStart.StartInfo.Domain = "";
            this.process_FileStart.StartInfo.LoadUserProfile = false;
            this.process_FileStart.StartInfo.Password = null;
            this.process_FileStart.StartInfo.StandardErrorEncoding = null;
            this.process_FileStart.StartInfo.StandardOutputEncoding = null;
            this.process_FileStart.StartInfo.UserName = "";
            this.process_FileStart.SynchronizingObject = this;
            this.process_FileStart.Exited += new System.EventHandler(this.process_FileStart_Exited);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 490);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "InTheForest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
    }
}

