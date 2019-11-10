using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Video_Conference
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        TeacherSide tSide;

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (txtVCESName.Text.Length == 0 && txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Please enter user name\nand password", "Video App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtVCESName.Focus();
                return;
            }
            else if (txtVCESName.Text.Length == 0)
            {
                MessageBox.Show("Please enter user name", "Video App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtVCESName.Focus();
                return;
            }
            else if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Please enter password", "Video App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }
            btnSignIn.Enabled = false;

            IPEndPoint dataBaseEP = new IPEndPoint(IPAddress.Parse("169.254.133.4"), 1350);

            TcpClient myTcpClient = new TcpClient();
            NetworkStream dataBaseStream;
            try
            {
                myTcpClient.Connect(dataBaseEP);
                dataBaseStream = myTcpClient.GetStream();

                Data data = new Data();
                data.cmd = Command.SignInTeacher;
                data.userName = txtVCESName.Text;
                data.pwd = txtPassword.Text;
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
                    case Command.Varified:
                        tSide = new TeacherSide();
                        tSide.userName = txtVCESName.Text;
                        this.Hide();
                        tSide.Show();
                        break;
                    case Command.NotVarified:
                        MessageBox.Show("Invalid User Name OR Password!", "Sign in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server not responding\n Can't connect with database server.", "Connection Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                btnSignIn.Enabled = true;
            }
        }

        private void lnklblAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp sup = new SignUp();
            this.Hide();
            sup.Show();
        }

        private void SignIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
