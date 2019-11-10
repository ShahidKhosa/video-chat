namespace Video_Conference
{
    partial class SignIn
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
            this.lnklblAccount = new System.Windows.Forms.LinkLabel();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtVCESName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lnklblAccount
            // 
            this.lnklblAccount.AutoSize = true;
            this.lnklblAccount.BackColor = System.Drawing.Color.Transparent;
            this.lnklblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblAccount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lnklblAccount.LinkColor = System.Drawing.Color.Yellow;
            this.lnklblAccount.Location = new System.Drawing.Point(215, 263);
            this.lnklblAccount.Name = "lnklblAccount";
            this.lnklblAccount.Size = new System.Drawing.Size(149, 15);
            this.lnklblAccount.TabIndex = 19;
            this.lnklblAccount.TabStop = true;
            this.lnklblAccount.Text = "Don\'t have a account?";
            this.lnklblAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblAccount_LinkClicked);
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.SystemColors.Control;
            this.btnSignIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignIn.ForeColor = System.Drawing.Color.Black;
            this.btnSignIn.Location = new System.Drawing.Point(167, 194);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(141, 30);
            this.btnSignIn.TabIndex = 18;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = false;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(102, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(298, 25);
            this.label3.TabIndex = 17;
            this.label3.Text = "Welcome to Video Application";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(167, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(167, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPassword.Location = new System.Drawing.Point(167, 168);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(141, 20);
            this.txtPassword.TabIndex = 14;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtVCESName
            // 
            this.txtVCESName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtVCESName.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtVCESName.Location = new System.Drawing.Point(167, 115);
            this.txtVCESName.Name = "txtVCESName";
            this.txtVCESName.Size = new System.Drawing.Size(141, 20);
            this.txtVCESName.TabIndex = 13;
            // 
            // SignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Video_Conference.Properties.Resources.LoginPageBackGround;
            this.ClientSize = new System.Drawing.Size(462, 316);
            this.Controls.Add(this.lnklblAccount);
            this.Controls.Add(this.btnSignIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtVCESName);
            this.MaximizeBox = false;
            this.Name = "SignIn";
            this.Text = "SignIn";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SignIn_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnklblAccount;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtVCESName;
        public System.Windows.Forms.TextBox txtPassword;
    }
}