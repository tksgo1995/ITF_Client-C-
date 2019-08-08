namespace InTheForest
{
    partial class NewFolder
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
            this.label_Text = new System.Windows.Forms.Label();
            this.textBox_FolderName = new System.Windows.Forms.TextBox();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_Text
            // 
            this.label_Text.AutoSize = true;
            this.label_Text.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Text.Location = new System.Drawing.Point(81, 26);
            this.label_Text.Name = "label_Text";
            this.label_Text.Size = new System.Drawing.Size(87, 15);
            this.label_Text.TabIndex = 0;
            this.label_Text.Text = "폴더 이름 입력";
            // 
            // textBox_FolderName
            // 
            this.textBox_FolderName.Location = new System.Drawing.Point(23, 51);
            this.textBox_FolderName.Name = "textBox_FolderName";
            this.textBox_FolderName.Size = new System.Drawing.Size(200, 21);
            this.textBox_FolderName.TabIndex = 1;
            // 
            // button_Confirm
            // 
            this.button_Confirm.Location = new System.Drawing.Point(31, 89);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(96, 23);
            this.button_Confirm.TabIndex = 2;
            this.button_Confirm.Text = "확인";
            this.button_Confirm.UseVisualStyleBackColor = true;
            this.button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(133, 89);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(96, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "취소";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // NewFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(252, 119);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Confirm);
            this.Controls.Add(this.textBox_FolderName);
            this.Controls.Add(this.label_Text);
            this.Name = "NewFolder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Text;
        private System.Windows.Forms.TextBox textBox_FolderName;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Button button_Cancel;
    }
}