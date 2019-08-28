namespace InTheForest_Background
{
    partial class ScreenSaver
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_Confirm = new System.Windows.Forms.Button();
            this.button_Shutdown = new System.Windows.Forms.Button();
            this.btModify = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(93, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(85, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "PW";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox1.Location = new System.Drawing.Point(127, 39);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 29);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox2.Location = new System.Drawing.Point(127, 76);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(186, 29);
            this.textBox2.TabIndex = 3;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox2_KeyDown);
            // 
            // button_Confirm
            // 
            this.button_Confirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Confirm.BackColor = System.Drawing.Color.White;
            this.button_Confirm.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.button_Confirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button_Confirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Confirm.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Confirm.Location = new System.Drawing.Point(320, 39);
            this.button_Confirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Confirm.Name = "button_Confirm";
            this.button_Confirm.Size = new System.Drawing.Size(113, 69);
            this.button_Confirm.TabIndex = 4;
            this.button_Confirm.Text = "로그인";
            this.button_Confirm.UseVisualStyleBackColor = false;
            this.button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // button_Shutdown
            // 
            this.button_Shutdown.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Shutdown.BackColor = System.Drawing.Color.White;
            this.button_Shutdown.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.button_Shutdown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button_Shutdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Shutdown.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Shutdown.Location = new System.Drawing.Point(320, 115);
            this.button_Shutdown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Shutdown.Name = "button_Shutdown";
            this.button_Shutdown.Size = new System.Drawing.Size(113, 43);
            this.button_Shutdown.TabIndex = 5;
            this.button_Shutdown.Text = "시스템 종료";
            this.button_Shutdown.UseVisualStyleBackColor = false;
            this.button_Shutdown.Click += new System.EventHandler(this.Button_Shutdown_Click);
            // 
            // btModify
            // 
            this.btModify.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btModify.BackColor = System.Drawing.Color.White;
            this.btModify.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btModify.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btModify.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btModify.Location = new System.Drawing.Point(212, 117);
            this.btModify.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btModify.Name = "btModify";
            this.btModify.Size = new System.Drawing.Size(102, 41);
            this.btModify.TabIndex = 6;
            this.btModify.Text = "수정";
            this.btModify.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.groupBox1.Controls.Add(this.button_Shutdown);
            this.groupBox1.Controls.Add(this.btModify);
            this.groupBox1.Controls.Add(this.button_Confirm);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(722, 458);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(521, 189);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.Image = global::InTheForest_Background.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(205, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(550, 486);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // ScreenSaver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 671);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScreenSaver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScreenSaver_FormClosing);
            this.Load += new System.EventHandler(this.ScreenSaver_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_Confirm;
        private System.Windows.Forms.Button button_Shutdown;
        private System.Windows.Forms.Button btModify;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}