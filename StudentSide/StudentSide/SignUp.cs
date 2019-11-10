using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace StudentSide
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        SignIn sin = new SignIn();

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if(txtFullName.ForeColor == Color.Gray || txtVCESName.ForeColor == Color.Gray || txtPassword.ForeColor == Color.Gray || txtRepPassword.ForeColor == Color.Gray || txtEmail.ForeColor == Color.Gray || cmbGender.Text.Length == 0 || cmbSubject.Text.Length == 0)
            {
                MessageBox.Show("Please enter complete information", "VCES - Sign up", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnSignUp.Enabled = false;

            IPEndPoint dataBaseEP = new IPEndPoint(IPAddress.Parse("169.254.133.4s"), 1350);

            TcpClient myTcpClient = new TcpClient();
            NetworkStream dataBaseStream;
            try
            {
                myTcpClient.Connect(dataBaseEP);
                dataBaseStream = myTcpClient.GetStream();

                Data data = new Data();
                data.cmd = Command.SignUpStudent;
                data.record = txtFullName.Text + "|" + txtVCESName.Text + "|" + txtPassword.Text + "|" + txtRepPassword.Text + "|" + txtEmail.Text + "|" + cmbGender.SelectedItem + "|" + cmbSubject.SelectedItem;
                byte[] outStream = data.ToByte();

                dataBaseStream.Write(outStream, 0, outStream.Length);
                dataBaseStream.Flush();

                byte[] inStream = new byte[myTcpClient.ReceiveBufferSize];

                dataBaseStream.Read(inStream, 0, inStream.Length);
                dataBaseStream.Close();
                myTcpClient.Close();

                Data receivedData = new Data(inStream);

                switch (receivedData.cmd)
                {
                    case Command.SignUpTrue:
                        if (MessageBox.Show("Congratulation!\nYour new VCES account is created succesfully", "VCES - Create new account", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                        {
                            this.Hide();
                            sin.Show();
                            sin.txtVCESName.Text = txtVCESName.Text;
                            sin.txtPassword.Focus();

                        }
                        break;
                    case Command.SignUpFalse:
                        MessageBox.Show("VCES name is not available\nPlease try different name", "VCES - Sign in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("VCES can't connect with server.\nPlease check your network connection", "VCES - Connection Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                btnSignUp.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            sin.Show();
        }

        private void txtFullName_Enter(object sender, EventArgs e)
        {
            if (txtFullName.ForeColor.Equals(Color.Gray))
            {
                txtFullName.Text = "";
                txtFullName.ForeColor = Color.Black;
            }
        }

        private void txtFullName_Leave(object sender, EventArgs e)
        {
            if (txtFullName.Text.Length == 0)
            {
                txtFullName.Text = "Enter your name";
                txtFullName.ForeColor = Color.Gray;
            }
        }

        private void txtVCESName_Enter(object sender, EventArgs e)
        {
            if (txtVCESName.ForeColor.Equals(Color.Gray))
            {
                txtVCESName.Text = "";
                txtVCESName.ForeColor = Color.Black;
            }
        }

        private void txtVCESName_Leave(object sender, EventArgs e)
        {
            if (txtVCESName.Text.Length == 0)
            {
                txtVCESName.Text = "Minimum 6 characters required";
                txtVCESName.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.ForeColor.Equals(Color.Gray))
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Length == 0)
            {
                txtPassword.Text = "Minimum 8 characters required";
                txtPassword.ForeColor = Color.Gray;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void txtRepPassword_Enter(object sender, EventArgs e)
        {
            if (txtRepPassword.ForeColor.Equals(Color.Gray))
            {
                txtRepPassword.Text = "";
                txtRepPassword.ForeColor = Color.Black;
                txtRepPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtRepPassword_Leave(object sender, EventArgs e)
        {
            if (txtRepPassword.Text.Length == 0)
            {
                txtRepPassword.Text = "Minimum 8 characters required";
                txtRepPassword.ForeColor = Color.Gray;
                txtRepPassword.UseSystemPasswordChar = false;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.ForeColor.Equals(Color.Gray))
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Length == 0)
            {
                txtEmail.Text = "Enter a valid email address";
                txtEmail.ForeColor = Color.Gray;
            }
        }
                                                                               
        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            sin.Show();
        }

        private void cmbGender_Enter(object sender, EventArgs e)
        {
            
        }                                                                                        
    }
}
