namespace Video_Conference
{
    partial class SignUp
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSignUp = new System.Windows.Forms.Button();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtRepPassword = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtVCESName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(434, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 36);
            this.btnCancel.TabIndex = 95;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSignUp
            // 
            this.btnSignUp.Location = new System.Drawing.Point(305, 331);
            this.btnSignUp.Name = "btnSignUp";
            this.btnSignUp.Size = new System.Drawing.Size(123, 36);
            this.btnSignUp.TabIndex = 94;
            this.btnSignUp.Text = "Sign Up";
            this.btnSignUp.UseVisualStyleBackColor = true;
            this.btnSignUp.Click += new System.EventHandler(this.btnSignUp_Click);
            // 
            // cmbSubject
            // 
            this.cmbSubject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Items.AddRange(new object[] {
            "ITCP",
            "Data Structure",
            "Algorithm",
            "Automata",
            "OOP",
            "AOOP",
            "Visual Programming",
            "Computer Architecture",
            "Compiler Construction",
            "Data Communication"});
            this.cmbSubject.Location = new System.Drawing.Point(197, 270);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(231, 21);
            this.cmbSubject.TabIndex = 93;
            // 
            // cmbGender
            // 
            this.cmbGender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cmbGender.Location = new System.Drawing.Point(197, 243);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(231, 21);
            this.cmbGender.TabIndex = 92;
            // 
            // txtEmail
            // 
            this.txtEmail.ForeColor = System.Drawing.Color.Gray;
            this.txtEmail.Location = new System.Drawing.Point(197, 217);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(231, 20);
            this.txtEmail.TabIndex = 91;
            this.txtEmail.Text = "Enter a valid email address";
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            this.txtEmail.Enter += new System.EventHandler(this.txtEmail_Enter);
            // 
            // txtRepPassword
            // 
            this.txtRepPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtRepPassword.Location = new System.Drawing.Point(197, 191);
            this.txtRepPassword.Name = "txtRepPassword";
            this.txtRepPassword.Size = new System.Drawing.Size(231, 20);
            this.txtRepPassword.TabIndex = 90;
            this.txtRepPassword.Text = "Minimum 8 characters required";
            this.txtRepPassword.Leave += new System.EventHandler(this.txtRepPassword_Leave);
            this.txtRepPassword.Enter += new System.EventHandler(this.txtRepPassword_Enter);
            // 
            // txtPassword
            // 
            this.txtPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtPassword.Location = new System.Drawing.Point(197, 165);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(231, 20);
            this.txtPassword.TabIndex = 89;
            this.txtPassword.Text = "Minimum 8 characters required";
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            // 
            // txtVCESName
            // 
            this.txtVCESName.ForeColor = System.Drawing.Color.Gray;
            this.txtVCESName.Location = new System.Drawing.Point(197, 139);
            this.txtVCESName.Name = "txtVCESName";
            this.txtVCESName.Size = new System.Drawing.Size(231, 20);
            this.txtVCESName.TabIndex = 88;
            this.txtVCESName.Text = "Minimum 6 characters required";
            this.txtVCESName.Leave += new System.EventHandler(this.txtVCESName_Leave);
            this.txtVCESName.Enter += new System.EventHandler(this.txtVCESName_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "VCES Name:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.label13.Location = new System.Drawing.Point(176, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(245, 23);
            this.label13.TabIndex = 86;
            this.label13.Text = "Create a new VCES account";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(153, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 85;
            this.label10.Text = "Email:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 13);
            this.label9.TabIndex = 84;
            this.label9.Text = "Repeat Password:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(132, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 83;
            this.label8.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Subject:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 246);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "Gender:";
            // 
            // txtFullName
            // 
            this.txtFullName.ForeColor = System.Drawing.Color.Gray;
            this.txtFullName.Location = new System.Drawing.Point(197, 113);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(231, 20);
            this.txtFullName.TabIndex = 80;
            this.txtFullName.Leave += new System.EventHandler(this.txtFullName_Leave);
            this.txtFullName.Enter += new System.EventHandler(this.txtFullName_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Full Name:";
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 416);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSignUp);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtRepPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtVCESName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "SignUp";
            this.Text = "SignUp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SignUp_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSignUp;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtRepPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtVCESName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label1;
    }
}