using System;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
// IPEndPoint
using System.Net;

// Add a reference to NetworkingBasics.dll: classes used - BufferChunk
// Add a reference to LSTCommon.dll: classes used -  UnhandledExceptionHandler
using MSR.LST;              

// Add a reference to MSR.LST.Net.Rtp.dll
// Classes used - RtpSession, RtpSender, RtpParticipant, RtpStream
using MSR.LST.Net.Rtp;      

// Code Flow (CF)
// 1. Hook Rtp events:
//   a.   RtpParticipant Added / Removed
//   b.   RtpStream Added / Removed
//   c.   Hook / Unhook FrameReceived event for that stream
// 2. Join RtpSession by providing an RtpParticipant and Multicast EndPoint
// 3. Retrieve RtpSender
// 4. Send data over network
// 5. Receive data from network
// 6. Unhook events, dispose RtpSession

namespace RtpChat
{
    public class frmChat : System.Windows.Forms.Form
    {
        #region Windows Form Designer generated code

        // Form variables
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnJoinLeave;
        private PictureBox pictureBox_Receive;
        private TextBox text_IP_Multicast;
        private Label label1;
        private ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem fullScreenToolStripMenuItem;
        private ToolStripMenuItem normalScreenToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private System.ComponentModel.IContainer components;

        // Required designer variable

        // Constructor
        public frmChat()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        }


        // Required method for Designer support - do not modify
        // the contents of this method with the code editor.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChat));
            this.lblName = new System.Windows.Forms.Label();
            this.btnJoinLeave = new System.Windows.Forms.Button();
            this.text_IP_Multicast = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox_Receive = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Receive)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(16, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 16);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Group";
            // 
            // btnJoinLeave
            // 
            this.btnJoinLeave.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinLeave.Location = new System.Drawing.Point(213, 5);
            this.btnJoinLeave.Name = "btnJoinLeave";
            this.btnJoinLeave.Size = new System.Drawing.Size(75, 29);
            this.btnJoinLeave.TabIndex = 1;
            this.btnJoinLeave.Text = "Join";
            this.btnJoinLeave.Click += new System.EventHandler(this.btnJoinLeave_Click);
            // 
            // text_IP_Multicast
            // 
            this.text_IP_Multicast.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_IP_Multicast.Location = new System.Drawing.Point(78, 5);
            this.text_IP_Multicast.MaxLength = 32;
            this.text_IP_Multicast.Name = "text_IP_Multicast";
            this.text_IP_Multicast.Size = new System.Drawing.Size(129, 26);
            this.text_IP_Multicast.TabIndex = 10;
            this.text_IP_Multicast.Text = "224.0.0.1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(452, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Join Log";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(428, 34);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(136, 316);
            this.listBox1.TabIndex = 14;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenToolStripMenuItem,
            this.normalScreenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 48);
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.fullScreenToolStripMenuItem.Text = "Full Screen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
            // 
            // normalScreenToolStripMenuItem
            // 
            this.normalScreenToolStripMenuItem.Name = "normalScreenToolStripMenuItem";
            this.normalScreenToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.normalScreenToolStripMenuItem.Text = "Normal Screen";
            this.normalScreenToolStripMenuItem.Click += new System.EventHandler(this.normalScreenToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "RTP Student Presenter";
            this.notifyIcon1.Visible = true;
            // 
            // pictureBox_Receive
            // 
            this.pictureBox_Receive.Image = global::RTPStudentPresenter.Properties.Resources.Presenter;
            this.pictureBox_Receive.Location = new System.Drawing.Point(1, 37);
            this.pictureBox_Receive.Name = "pictureBox_Receive";
            this.pictureBox_Receive.Size = new System.Drawing.Size(421, 309);
            this.pictureBox_Receive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Receive.TabIndex = 6;
            this.pictureBox_Receive.TabStop = false;
            // 
            // frmChat
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(576, 358);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.pictureBox_Receive);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_IP_Multicast);
            this.Controls.Add(this.btnJoinLeave);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTP Student Presenter -  (C) www.fadidotnet.org ";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmChat_Closing);
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Receive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }


        #endregion

        #region Statics / App.Config overrides

        private static IPEndPoint ep;//= RtpSession.DefaultEndPoint;

        static frmChat()
        {
            string setting;

            // See if there was a multicast IP address set in the app.config
            if((setting = ConfigurationManager.AppSettings["EndPoint"]) != null)
            {
                string[] args = setting.Split(new char[]{':'}, 2);
                ep = new IPEndPoint(IPAddress.Parse(args[0]), int.Parse(args[1]));
            }
        }


        [STAThread]
        static void Main() 
        {
            Application.EnableVisualStyles();
            // Make sure no exceptions escape unnoticed
            UnhandledExceptionHandler.Register();
            Application.Run(new frmChat());
        
        }


        #endregion Statics / App.Config overrides

        #region Members

        /// <summary>
        /// Manages the connection to a multicast address and all the objects related to Rtp
        /// </summary>
        private RtpSession rtpSession;

        /// <summary>
        /// Sends the data across the network
        /// </summary>
        private RtpSender rtpSender;

        #endregion Members

        #region Private

        private void btnJoinLeave_Click(object sender, System.EventArgs e)
        {
            ep = new IPEndPoint(IPAddress.Parse(text_IP_Multicast.Text), 5000);
            if(btnJoinLeave.Text == "Join")
            {
                    HookRtpEvents(); // 1
                    JoinRtpSession(Dns.GetHostName()); // 2

                    // Change the UI
                    btnJoinLeave.Text = "Leave";
                    //txtSend.Enabled = true;
                    //txtSend.Focus();
                    text_IP_Multicast.Enabled = false;    
            }
            else
            {
                Cleanup(); // 6

                // Change the UI
                btnJoinLeave.Text = "Join";
                //txtReceive.Clear();
                text_IP_Multicast.Enabled = true;
            }
        }

        
        // CF1 Hook Rtp events
        private void HookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded += new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved += new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        
        // CF2 Create participant, join session
        // CF3 Retrieve RtpSender
        private void JoinRtpSession(string name)
        {
            rtpSession = new RtpSession(ep, new RtpParticipant(name, name), true, true);
            rtpSender = rtpSession.CreateRtpSenderFec(name, PayloadType.Chat, null, 0, 200);
        }
        MemoryStream ms;

        // CF4 Send the data
        private void btnSend_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = true;
          
        }
        // CF5 Receive data from network
        private void RtpParticipantAdded(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            ShowMessage(string.Format("{0} has joined", ea.RtpParticipant.Name));
        }

        private void RtpParticipantRemoved(object sender, RtpEvents.RtpParticipantEventArgs ea)
        {
            ShowMessage(string.Format("{0} has left", ea.RtpParticipant.Name));
        }

        private void RtpStreamAdded(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived += new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void RtpStreamRemoved(object sender, RtpEvents.RtpStreamEventArgs ea)
        {
            ea.RtpStream.FrameReceived -= new RtpStream.FrameReceivedEventHandler(FrameReceived);
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            System.IO.MemoryStream ms = new MemoryStream(ea.Frame.Buffer);
            pictureBox_Receive.Image = Image.FromStream(ms);
        }


        // CF6 Unhook events, dispose RtpSession
        private void Cleanup()
        {
            UnhookRtpEvents();
            LeaveRtpSession();
        }

        private void UnhookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded -= new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved -= new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
            RtpEvents.RtpStreamAdded -= new RtpEvents.RtpStreamAddedEventHandler(RtpStreamAdded);
            RtpEvents.RtpStreamRemoved -= new RtpEvents.RtpStreamRemovedEventHandler(RtpStreamRemoved);
        }

        private void LeaveRtpSession()
        {
            if(rtpSession != null)
            {
                // Clean up all outstanding objects owned by the RtpSession
                rtpSession.Dispose();
                rtpSession = null;
                rtpSender = null;
            }
        }

        private void frmChat_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                
                Cleanup();
            }
            catch (Exception){}
        }       
      #endregion Private
        private void ShowMessage(string msg)
        {
            listBox1.Items.Add(msg);
        }

        private void frmChat_Load(object sender, EventArgs e)
        {

        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            full_screen();
        }

        private void normalScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            normal_screen();
        }

        void full_screen()
        {
                pictureBox_Receive.Dock = DockStyle.Fill;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
        }
        void normal_screen()
        {
            pictureBox_Receive.Dock = DockStyle.None;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
       }

    }
}
