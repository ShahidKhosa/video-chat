namespace StudentSide
{
    partial class VCES
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
            this.lblUserName = new System.Windows.Forms.Label();
            this.lstBoxStudents = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picBoxUser = new System.Windows.Forms.PictureBox();
            this.btnClassRoom = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUser)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(10, 57);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(35, 13);
            this.lblUserName.TabIndex = 12;
            this.lblUserName.Text = "label1";
            // 
            // lstBoxStudents
            // 
            this.lstBoxStudents.FormattingEnabled = true;
            this.lstBoxStudents.HorizontalScrollbar = true;
            this.lstBoxStudents.Location = new System.Drawing.Point(188, 72);
            this.lstBoxStudents.Name = "lstBoxStudents";
            this.lstBoxStudents.ScrollAlwaysVisible = true;
            this.lstBoxStudents.Size = new System.Drawing.Size(185, 251);
            this.lstBoxStudents.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(386, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // picBoxUser
            // 
            this.picBoxUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxUser.BackColor = System.Drawing.Color.PeachPuff;
            this.picBoxUser.BackgroundImage = global::StudentSide.Properties.Resources.P1010387;
            this.picBoxUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxUser.Location = new System.Drawing.Point(12, 73);
            this.picBoxUser.Name = "picBoxUser";
            this.picBoxUser.Size = new System.Drawing.Size(162, 122);
            this.picBoxUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxUser.TabIndex = 11;
            this.picBoxUser.TabStop = false;
            // 
            // btnClassRoom
            // 
            this.btnClassRoom.Location = new System.Drawing.Point(56, 201);
            this.btnClassRoom.Name = "btnClassRoom";
            this.btnClassRoom.Size = new System.Drawing.Size(118, 38);
            this.btnClassRoom.TabIndex = 14;
            this.btnClassRoom.Text = "Class Room";
            this.btnClassRoom.UseVisualStyleBackColor = true;
            this.btnClassRoom.Click += new System.EventHandler(this.btnClassRoom_Click);
            // 
            // VCES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(386, 383);
            this.Controls.Add(this.btnClassRoom);
            this.Controls.Add(this.lstBoxStudents);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.picBoxUser);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VCES";
            this.Text = "VCES - Student";
            this.Load += new System.EventHandler(this.VCES_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox picBoxUser;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        public System.Windows.Forms.ListBox lstBoxStudents;
        private System.Windows.Forms.Button btnClassRoom;
    }
}

