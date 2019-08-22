namespace InTheForest
{
    partial class Compress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Folder = new System.Windows.Forms.Label();
            this.label_File = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.bt_Folder = new System.Windows.Forms.Button();
            this.bt_File = new System.Windows.Forms.Button();
            this.bt_CompFolder = new System.Windows.Forms.Button();
            this.bt_CompFile = new System.Windows.Forms.Button();
            this.progressBar_Compress = new System.Windows.Forms.ProgressBar();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label_Folder
            // 
            this.label_Folder.AutoSize = true;
            this.label_Folder.Location = new System.Drawing.Point(31, 29);
            this.label_Folder.Name = "label_Folder";
            this.label_Folder.Size = new System.Drawing.Size(41, 12);
            this.label_Folder.TabIndex = 0;
            this.label_Folder.Text = "폴더 : ";
            // 
            // label_File
            // 
            this.label_File.AutoSize = true;
            this.label_File.Location = new System.Drawing.Point(33, 79);
            this.label_File.Name = "label_File";
            this.label_File.Size = new System.Drawing.Size(37, 12);
            this.label_File.TabIndex = 1;
            this.label_File.Text = "파일 :";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(76, 76);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(166, 21);
            this.txtFile.TabIndex = 3;
            // 
            // bt_Folder
            // 
            this.bt_Folder.Location = new System.Drawing.Point(248, 24);
            this.bt_Folder.Name = "bt_Folder";
            this.bt_Folder.Size = new System.Drawing.Size(38, 23);
            this.bt_Folder.TabIndex = 4;
            this.bt_Folder.Text = "...";
            this.bt_Folder.UseVisualStyleBackColor = true;
            this.bt_Folder.Click += new System.EventHandler(this.Bt_Folder_Click);
            // 
            // bt_File
            // 
            this.bt_File.Location = new System.Drawing.Point(248, 73);
            this.bt_File.Name = "bt_File";
            this.bt_File.Size = new System.Drawing.Size(38, 23);
            this.bt_File.TabIndex = 5;
            this.bt_File.Text = "...";
            this.bt_File.UseVisualStyleBackColor = true;
            this.bt_File.Click += new System.EventHandler(this.Bt_File_Click);
            // 
            // bt_CompFolder
            // 
            this.bt_CompFolder.Location = new System.Drawing.Point(291, 22);
            this.bt_CompFolder.Name = "bt_CompFolder";
            this.bt_CompFolder.Size = new System.Drawing.Size(75, 23);
            this.bt_CompFolder.TabIndex = 6;
            this.bt_CompFolder.Text = "압축";
            this.bt_CompFolder.UseVisualStyleBackColor = true;
            this.bt_CompFolder.Click += new System.EventHandler(this.Bt_CompFolder_Click);
            // 
            // bt_CompFile
            // 
            this.bt_CompFile.Location = new System.Drawing.Point(292, 73);
            this.bt_CompFile.Name = "bt_CompFile";
            this.bt_CompFile.Size = new System.Drawing.Size(75, 23);
            this.bt_CompFile.TabIndex = 7;
            this.bt_CompFile.Text = "압축";
            this.bt_CompFile.UseVisualStyleBackColor = true;
            this.bt_CompFile.Click += new System.EventHandler(this.Bt_CompFile_Click);
            // 
            // progressBar_Compress
            // 
            this.progressBar_Compress.Location = new System.Drawing.Point(35, 115);
            this.progressBar_Compress.Name = "progressBar_Compress";
            this.progressBar_Compress.Size = new System.Drawing.Size(331, 23);
            this.progressBar_Compress.TabIndex = 8;
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(76, 26);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(166, 21);
            this.txtFolder.TabIndex = 2;
            // 
            // Compress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 169);
            this.Controls.Add(this.progressBar_Compress);
            this.Controls.Add(this.bt_CompFile);
            this.Controls.Add(this.bt_CompFolder);
            this.Controls.Add(this.bt_File);
            this.Controls.Add(this.bt_Folder);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label_File);
            this.Controls.Add(this.label_Folder);
            this.Name = "Compress";
            this.Text = "Compress";
            this.Load += new System.EventHandler(this.Compress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Folder;
        private System.Windows.Forms.Label label_File;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button bt_Folder;
        private System.Windows.Forms.Button bt_File;
        private System.Windows.Forms.Button bt_CompFolder;
        private System.Windows.Forms.Button bt_CompFile;
        private System.Windows.Forms.ProgressBar progressBar_Compress;
        private System.Windows.Forms.TextBox txtFolder;
    }
}