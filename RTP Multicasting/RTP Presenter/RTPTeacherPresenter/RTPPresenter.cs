using System;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
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

namespace RTPPresenter
{
    public class RTPPresenter : System.Windows.Forms.Form
    {
        #region Windows Form Designer generated code

        // Form variables
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnJoinLeave;
        private TextBox text_IP_Multicast;
        private Label label1;
        private ListBox listBox1;
        private Button button1;
        private Button button3;
        private TextBox textLecture;
        private Label label2;
        private CheckBox MotionFlag;
        private Label differencelab;
        private Label sendingSt;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem actionToolStripMenuItem;
        private ToolStripMenuItem hideToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem hideToolStripMenuItem1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem showToolStripMenuItem;
        private System.ComponentModel.IContainer components;

        // Required designer variable

        // Constructor
        public RTPPresenter()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        }


        // Required method for Designer support - do not modify
        // the contents of this method with the code editor.
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RTPPresenter));
            this.lblName = new System.Windows.Forms.Label();
            this.text_IP_Multicast = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textLecture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnJoinLeave = new System.Windows.Forms.Button();
            this.MotionFlag = new System.Windows.Forms.CheckBox();
            this.differencelab = new System.Windows.Forms.Label();
            this.sendingSt = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(12, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 16);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Group";
            // 
            // text_IP_Multicast
            // 
            this.text_IP_Multicast.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_IP_Multicast.Location = new System.Drawing.Point(74, 36);
            this.text_IP_Multicast.MaxLength = 32;
            this.text_IP_Multicast.Name = "text_IP_Multicast";
            this.text_IP_Multicast.Size = new System.Drawing.Size(129, 26);
            this.text_IP_Multicast.TabIndex = 10;
            this.text_IP_Multicast.Text = "224.0.0.1";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(325, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Join Log";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(284, 94);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(203, 134);
            this.listBox1.TabIndex = 14;
            // 
            // textLecture
            // 
            this.textLecture.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLecture.Location = new System.Drawing.Point(74, 62);
            this.textLecture.MaxLength = 32;
            this.textLecture.Name = "textLecture";
            this.textLecture.Size = new System.Drawing.Size(129, 26);
            this.textLecture.TabIndex = 17;
            this.textLecture.Text = "LectureTitle";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Lecture";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Image = global::RTPPresenter.Properties.Resources.offpp;
            this.button3.Location = new System.Drawing.Point(3, 94);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 135);
            this.button3.TabIndex = 16;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Image = global::RTPPresenter.Properties.Resources.PP;
            this.button1.Location = new System.Drawing.Point(150, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 135);
            this.button1.TabIndex = 15;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnJoinLeave
            // 
            this.btnJoinLeave.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinLeave.Image = global::RTPPresenter.Properties.Resources.Join;
            this.btnJoinLeave.Location = new System.Drawing.Point(209, 35);
            this.btnJoinLeave.Name = "btnJoinLeave";
            this.btnJoinLeave.Size = new System.Drawing.Size(110, 53);
            this.btnJoinLeave.TabIndex = 1;
            this.btnJoinLeave.Text = "Join";
            this.btnJoinLeave.Click += new System.EventHandler(this.btnJoinLeave_Click);
            // 
            // MotionFlag
            // 
            this.MotionFlag.AutoSize = true;
            this.MotionFlag.Checked = true;
            this.MotionFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MotionFlag.Location = new System.Drawing.Point(325, 39);
            this.MotionFlag.Name = "MotionFlag";
            this.MotionFlag.Size = new System.Drawing.Size(107, 17);
            this.MotionFlag.TabIndex = 21;
            this.MotionFlag.Text = "Motion Detection";
            this.MotionFlag.UseVisualStyleBackColor = true;
            this.MotionFlag.CheckedChanged += new System.EventHandler(this.MotionFlag_CheckedChanged);
            // 
            // differencelab
            // 
            this.differencelab.AutoSize = true;
            this.differencelab.Location = new System.Drawing.Point(428, 40);
            this.differencelab.Name = "differencelab";
            this.differencelab.Size = new System.Drawing.Size(13, 13);
            this.differencelab.TabIndex = 22;
            this.differencelab.Text = "0";
            // 
            // sendingSt
            // 
            this.sendingSt.AutoSize = true;
            this.sendingSt.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendingSt.ForeColor = System.Drawing.Color.Red;
            this.sendingSt.Location = new System.Drawing.Point(410, 62);
            this.sendingSt.Name = "sendingSt";
            this.sendingSt.Size = new System.Drawing.Size(38, 16);
            this.sendingSt.TabIndex = 23;
            this.sendingSt.Text = "Stop";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(499, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.actionToolStripMenuItem.Text = "Action";
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(92, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "RTP Presenter - Not started yet";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.hideToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 76);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // hideToolStripMenuItem1
            // 
            this.hideToolStripMenuItem1.Name = "hideToolStripMenuItem1";
            this.hideToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.hideToolStripMenuItem1.Text = "Hide";
            this.hideToolStripMenuItem1.Click += new System.EventHandler(this.hideToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(97, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // RTPPresenter
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(499, 238);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.sendingSt);
            this.Controls.Add(this.differencelab);
            this.Controls.Add(this.MotionFlag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textLecture);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_IP_Multicast);
            this.Controls.Add(this.btnJoinLeave);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "RTPPresenter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTP Multicasting Teacher Presenter - (C)  www.fadidotnet.org FADI Abdel-qader ";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmChat_Closing);
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
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

        static RTPPresenter()
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
            Application.Run(new RTPPresenter());
        
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
                    JoinRtpSession(textLecture.Text + " Started at " + DateTime.Now.ToShortTimeString ()); // 2

                    // Change the UI
                    btnJoinLeave.Text = "Leave";
                    //txtSend.Enabled = true;

                    text_IP_Multicast.Enabled = false;
                    button1.Enabled = true;
                    textLecture.Enabled = false;

            }
            else
            {
                Cleanup(); // 6

                // Change the UI
                btnJoinLeave.Text = "Join";
                     //txtReceive.Clear();
                text_IP_Multicast.Enabled = true;
                text_IP_Multicast.Enabled = true;
                button1.Enabled = false;
                textLecture.Enabled = true;
                button3.Enabled = false;
                sendingSt.ForeColor = Color.Red; 
                sendingSt.Text = "Stop";
                notifyIcon1.Text = "RTP Presenter - Stop";
                
              }
        }

        
        // CF1 Hook Rtp events
        private void HookRtpEvents()
        {
            RtpEvents.RtpParticipantAdded += new RtpEvents.RtpParticipantAddedEventHandler(RtpParticipantAdded);
            RtpEvents.RtpParticipantRemoved += new RtpEvents.RtpParticipantRemovedEventHandler(RtpParticipantRemoved);
        }

        
        // CF2 Create participant, join session
        // CF3 Retrieve RtpSender
        private void JoinRtpSession(string name)
        {
            try
            {
                rtpSession = new RtpSession(ep, new RtpParticipant(name, name), true, true);
                rtpSender = rtpSession.CreateRtpSenderFec(name, PayloadType.Chat, null, 0, 200);
            }
            catch (Exception ex) { MessageBox.Show("Please make sure that you are connceted with the network so " + ex.Message ); }
            }


        ScreenCapture scr = new ScreenCapture();
        void send_img()
        {
            try
            {

                if (MotionFlag.Checked)
                {                
                    Image oldimage = scr.Get_Resized_Image(100, 100,scr.GetDesktopBitmapBytes());

                    while (true)
                    {

                        Image newimage = scr.Get_Resized_Image(100, 100, scr.GetDesktopBitmapBytes());
                        float difference = scr.difference(newimage, oldimage);
                        differencelab.Text = difference.ToString() + "%";
                        if (difference >= 1)
                        {
                            sendingSt.ForeColor = Color.Green;
                            sendingSt.Text = "Sending ...";
                            notifyIcon1.Text = "RTP Presenter - Sending...";
                            rtpSender.Send(scr.GetDesktopBitmapBytes());
                            oldimage = scr.Get_Resized_Image(100, 100, scr.GetDesktopBitmapBytes());
                        }
                        else { sendingSt.ForeColor = Color.Red; sendingSt.Text = "Paused"; notifyIcon1.Text = "RTP Presenter - Paused"; }
                    }
                }
                else
                {
                    sendingSt.ForeColor = Color.Green;
                    sendingSt.Text = "Sending ...";
                    notifyIcon1.Text = "RTP Presenter - Sending...";

                    while (true)
                    {
                        rtpSender.Send(scr.GetDesktopBitmapBytes());
                    }
                }
            }
            catch (Exception ) {}
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
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        Thread senderthread;

        private void button1_Click(object sender, EventArgs e)
        {
            MotionFlag.Enabled = false;
            btnJoinLeave.Enabled = false;
            senderthread = new Thread(new ThreadStart(send_img));
            senderthread.IsBackground = true;
            senderthread.Start();
            button1.Enabled = false;
            button3.Enabled = true;
            	
        }

        private void button3_Click(object sender, EventArgs e)
        {
            senderthread.Abort();
            MotionFlag.Enabled = true;
            btnJoinLeave.Enabled = true;
            button1.Enabled = true;
            button3.Enabled = false;
            sendingSt.ForeColor = Color.Red; 
            sendingSt.Text = "Stop";
            notifyIcon1.Text = "RTP Presenter - Stop";
            
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void hideToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void MotionFlag_CheckedChanged(object sender, EventArgs e)
        {
            btnJoinLeave_Click(sender, e);
        }

    }
}
