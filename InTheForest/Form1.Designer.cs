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
            this.listView_Window = new System.Windows.Forms.ListView();
            this.treeView_Window = new System.Windows.Forms.TreeView();
            this.button_Back = new System.Windows.Forms.Button();
            this.button_Front = new System.Windows.Forms.Button();
            this.label_Path = new System.Windows.Forms.Label();
            this.process_FileStart = new System.Diagnostics.Process();
            this.SuspendLayout();
            // 
            // listView_Window
            // 
            this.listView_Window.Location = new System.Drawing.Point(210, 65);
            this.listView_Window.Name = "listView_Window";
            this.listView_Window.Size = new System.Drawing.Size(578, 373);
            this.listView_Window.TabIndex = 0;
            this.listView_Window.UseCompatibleStateImageBehavior = false;
            this.listView_Window.View = System.Windows.Forms.View.List;
            this.listView_Window.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView_Window_DragDrop);
            this.listView_Window.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView_Window_DragEnter);
            this.listView_Window.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView_Window_KeyDown);
            this.listView_Window.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Window_MouseDoubleClick);
            // 
            // treeView_Window
            // 
            this.treeView_Window.Location = new System.Drawing.Point(12, 65);
            this.treeView_Window.Name = "treeView_Window";
            this.treeView_Window.Size = new System.Drawing.Size(192, 373);
            this.treeView_Window.TabIndex = 1;
            this.treeView_Window.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_Window_BeforeExpand);
            this.treeView_Window.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Window_AfterSelect);
            this.treeView_Window.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Window_NodeMouseClick);
            this.treeView_Window.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Window_NodeMouseDoubleClick);
            this.treeView_Window.Click += new System.EventHandler(this.TreeView_Window_Click);
            this.treeView_Window.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeView_Window_MouseDoubleClick);
            // 
            // button_Back
            // 
            this.button_Back.Location = new System.Drawing.Point(12, 36);
            this.button_Back.Name = "button_Back";
            this.button_Back.Size = new System.Drawing.Size(75, 23);
            this.button_Back.TabIndex = 2;
            this.button_Back.Text = "<<";
            this.button_Back.UseVisualStyleBackColor = true;
            this.button_Back.Click += new System.EventHandler(this.button_Back_Click);
            // 
            // button_Front
            // 
            this.button_Front.Location = new System.Drawing.Point(93, 36);
            this.button_Front.Name = "button_Front";
            this.button_Front.Size = new System.Drawing.Size(75, 23);
            this.button_Front.TabIndex = 3;
            this.button_Front.Text = ">>";
            this.button_Front.UseVisualStyleBackColor = true;
            this.button_Front.Click += new System.EventHandler(this.button_Front_Click);
            // 
            // label_Path
            // 
            this.label_Path.AutoSize = true;
            this.label_Path.Location = new System.Drawing.Point(210, 47);
            this.label_Path.Name = "label_Path";
            this.label_Path.Size = new System.Drawing.Size(0, 12);
            this.label_Path.TabIndex = 4;
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
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_Path);
            this.Controls.Add(this.button_Front);
            this.Controls.Add(this.button_Back);
            this.Controls.Add(this.treeView_Window);
            this.Controls.Add(this.listView_Window);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView_Window;
        private System.Windows.Forms.TreeView treeView_Window;
        private System.Windows.Forms.Button button_Back;
        private System.Windows.Forms.Button button_Front;
        private System.Windows.Forms.Label label_Path;
        private System.Diagnostics.Process process_FileStart;
    }
}

