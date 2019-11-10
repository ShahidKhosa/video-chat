namespace StudentSide
{
    partial class VideoWindow
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
            this.components = new System.ComponentModel.Container();
            this.btnQuestionRequest = new System.Windows.Forms.Button();
            this.btnJoinLeave = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lstBoxStudents = new System.Windows.Forms.ListBox();
            this.rtChat = new System.Windows.Forms.RichTextBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRemote = new System.Windows.Forms.Label();
            this.picBoxTeacher = new System.Windows.Forms.PictureBox();
            this.picBoxUser = new System.Windows.Forms.PictureBox();
            this.picBoxStudent = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTeacher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuestionRequest
            // 
            this.btnQuestionRequest.Enabled = false;
            this.btnQuestionRequest.Location = new System.Drawing.Point(8, 117);
            this.btnQuestionRequest.Name = "btnQuestionRequest";
            this.btnQuestionRequest.Size = new System.Drawing.Size(107, 38);
            this.btnQuestionRequest.TabIndex = 16;
            this.btnQuestionRequest.Text = "Send Call Request";
            this.btnQuestionRequest.UseVisualStyleBackColor = true;
            this.btnQuestionRequest.Click += new System.EventHandler(this.btnQuestionRequest_Click);
            // 
            // btnJoinLeave
            // 
            this.btnJoinLeave.Location = new System.Drawing.Point(8, 73);
            this.btnJoinLeave.Name = "btnJoinLeave";
            this.btnJoinLeave.Size = new System.Drawing.Size(107, 38);
            this.btnJoinLeave.TabIndex = 18;
            this.btnJoinLeave.Text = "Join";
            this.btnJoinLeave.UseVisualStyleBackColor = true;
            this.btnJoinLeave.Click += new System.EventHandler(this.btnJoinLeave_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.Yellow;
            this.lblUser.Location = new System.Drawing.Point(565, 50);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(74, 20);
            this.lblUser.TabIndex = 19;
            this.lblUser.Text = "My Video";
            // 
            // timer1
            // 
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 161);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 38);
            this.button1.TabIndex = 20;
            this.button1.Text = "Start Camera";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.signOutToolStripMenuItem.Text = "Sign Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(565, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Online Users";
            // 
            // lstBoxStudents
            // 
            this.lstBoxStudents.BackColor = System.Drawing.SystemColors.Info;
            this.lstBoxStudents.FormattingEnabled = true;
            this.lstBoxStudents.HorizontalScrollbar = true;
            this.lstBoxStudents.Location = new System.Drawing.Point(569, 252);
            this.lstBoxStudents.Name = "lstBoxStudents";
            this.lstBoxStudents.ScrollAlwaysVisible = true;
            this.lstBoxStudents.Size = new System.Drawing.Size(183, 368);
            this.lstBoxStudents.TabIndex = 23;
            // 
            // rtChat
            // 
            this.rtChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtChat.Location = new System.Drawing.Point(121, 437);
            this.rtChat.Name = "rtChat";
            this.rtChat.ReadOnly = true;
            this.rtChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtChat.Size = new System.Drawing.Size(436, 137);
            this.rtChat.TabIndex = 24;
            this.rtChat.Text = "";
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(121, 580);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(344, 40);
            this.txtChat.TabIndex = 25;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.Control;
            this.btnSend.Enabled = false;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSend.Location = new System.Drawing.Point(471, 580);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 40);
            this.btnSend.TabIndex = 26;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(117, 623);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(305, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Barani Institute of Information Technology";
            // 
            // lblRemote
            // 
            this.lblRemote.AutoSize = true;
            this.lblRemote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemote.ForeColor = System.Drawing.Color.Yellow;
            this.lblRemote.Location = new System.Drawing.Point(117, 50);
            this.lblRemote.Name = "lblRemote";
            this.lblRemote.Size = new System.Drawing.Size(0, 20);
            this.lblRemote.TabIndex = 29;
            // 
            // picBoxTeacher
            // 
            this.picBoxTeacher.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxTeacher.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxTeacher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxTeacher.Image = global::StudentSide.Properties.Resources.uni_logo;
            this.picBoxTeacher.Location = new System.Drawing.Point(121, 73);
            this.picBoxTeacher.Name = "picBoxTeacher";
            this.picBoxTeacher.Size = new System.Drawing.Size(436, 358);
            this.picBoxTeacher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxTeacher.TabIndex = 10;
            this.picBoxTeacher.TabStop = false;
            // 
            // picBoxUser
            // 
            this.picBoxUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxUser.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxUser.Image = global::StudentSide.Properties.Resources.uni_logo;
            this.picBoxUser.Location = new System.Drawing.Point(569, 73);
            this.picBoxUser.Name = "picBoxUser";
            this.picBoxUser.Size = new System.Drawing.Size(183, 142);
            this.picBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxUser.TabIndex = 9;
            this.picBoxUser.TabStop = false;
            // 
            // picBoxStudent
            // 
            this.picBoxStudent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxStudent.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxStudent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxStudent.Image = global::StudentSide.Properties.Resources.uni_logo;
            this.picBoxStudent.Location = new System.Drawing.Point(397, 301);
            this.picBoxStudent.Name = "picBoxStudent";
            this.picBoxStudent.Size = new System.Drawing.Size(160, 130);
            this.picBoxStudent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxStudent.TabIndex = 30;
            this.picBoxStudent.TabStop = false;
            this.picBoxStudent.Visible = false;
            // 
            // VideoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(764, 653);
            this.Controls.Add(this.picBoxStudent);
            this.Controls.Add(this.lblRemote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.rtChat);
            this.Controls.Add(this.lstBoxStudents);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnJoinLeave);
            this.Controls.Add(this.btnQuestionRequest);
            this.Controls.Add(this.picBoxTeacher);
            this.Controls.Add(this.picBoxUser);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "VideoWindow";
            this.Text = "Client Side";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.VideoWindow_FormClosed);
            this.Load += new System.EventHandler(this.VideoWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTeacher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxStudent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox picBoxTeacher;
        internal System.Windows.Forms.PictureBox picBoxUser;
        private System.Windows.Forms.Button btnQuestionRequest;
        private System.Windows.Forms.Button btnJoinLeave;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstBoxStudents;
        private System.Windows.Forms.RichTextBox rtChat;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRemote;
        internal System.Windows.Forms.PictureBox picBoxStudent;

    }
}