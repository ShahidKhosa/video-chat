using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;



//Audio Namespaces
using Microsoft.DirectX.DirectSound;
using System.Collections.Generic;





namespace Video_Conference
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TeacherSide : System.Windows.Forms.Form
	{
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        internal PictureBox picBoxQuestionVideo;
        private Button btnAccept;
        private Button btnStartLecture;
        private System.Windows.Forms.Timer timer1;
        public ListBox lstBoxStudents;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem signOutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private Label lblTeacherName;
        private Label label3;
        public ListBox lstBoxQuestionRequests;
        private PictureBox pictureBox1;
        private Label label4;
        private RichTextBox rtChat;
        private TextBox txtChat;
        private Button btnSend;
		
		#region WebCam API
        const short WM_CAP = 1024;
        const int WM_CAP_SEQUENCE = (WM_CAP + 62);
        const int WM_CAP_SET_SEQUENCE_SETUP = WM_CAP + 64;
        const int WM_CAP_GET_SEQUENCE_SETUP = WM_CAP + 65;
        const int WM_CAP_DLG_VIDEOCOMPRESSION = 1070;
        const int WM_CAP_FILE_SAVEAS = (WM_CAP + 23);
        const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;
        const int WM_CAP_EDIT_COPY = WM_CAP + 30;
        const int WM_CAP_SET_PREVIEW = WM_CAP + 50;
        const int WM_CAP_SET_PREVIEWRATE = WM_CAP + 52;
        const int WM_CAP_SET_SCALE = WM_CAP + 53;
        const int WS_CHILD = 1073741824;
        const int WS_VISIBLE = 268435456;
        const short SWP_NOMOVE = 2;
        const short SWP_NOSIZE = 1;
        const short SWP_NOZORDER = 4;
        const short HWND_BOTTOM = 1;
        int Device = 0;
        private ToolStripMenuItem recordingToolStripMenuItem;
        private ToolStripMenuItem startRecordingToolStripMenuItem;
        private ToolStripMenuItem stopRecordingToolStripMenuItem;
        private System.Windows.Forms.Timer tmrStudentVideo;
        int hHwnd;

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "GetActiveWindow")]
        static extern int GetActiveWindow();

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA")]
        static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] 
			object lParam);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA", SetLastError = true)]
        public static extern int SendMessage2(IntPtr webcam1, int Msg, IntPtr wParam, ref CAPTUREPARMS lParam);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "PostMessageA")]
        static extern int PostMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] 
			object lParam);

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetWindowPos")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);

        private void OpenPreviewWindow()
        {
            int iWidth = 436;
            int iHeight = 358;


            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(OpenPreviewWindow));
            }
            else
            {
                // 
                //  Open Preview window in picturebox
                // 
                hHwnd = capCreateCaptureWindowA(Device.ToString(), (WS_VISIBLE | WS_CHILD), 0, 0, 640, 480, picBoxTeacher.Handle.ToInt32(), 0);
                // 
                //  Connect to device
                // 
                Application.DoEvents();
                if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, Device, 0) == 1)
                {
                    SendMessage(hHwnd, WM_CAP_DLG_VIDEOCOMPRESSION, 0, 0);

                    // Start previewing the image from the camera
                    // 
                    SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);
                    // 
                    // Set the preview scale
                    // 
                    SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);



                    // 
                    // Set the preview rate in milliseconds
                    // 
                    SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 30, 0);
                    // 

                    // 
                    //  Resize window to fit in picturebox
                    // 
                    SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, iWidth, iHeight, (SWP_NOMOVE | SWP_NOZORDER));

                    //return true;
                }
                else
                {
                    // 
                    //  Error connecting to device close window
                    //  
                    DestroyWindow(hHwnd);

                    //return false;
                }
            }
        }
        private void ClosePreviewWindow()
        {
            // 
            //  Disconnect from device
            // 
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, Device, 0);
            // 
            //  close window
            // 
            DestroyWindow(hHwnd);
        }
		#endregion

        internal System.Windows.Forms.Button btnTestAVDevices;
        internal System.Windows.Forms.PictureBox picBoxTeacher;
        private IContainer components;

		public TeacherSide()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

            
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnTestAVDevices = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnStartLecture = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lstBoxStudents = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTeacherName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstBoxQuestionRequests = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBoxQuestionVideo = new System.Windows.Forms.PictureBox();
            this.picBoxTeacher = new System.Windows.Forms.PictureBox();
            this.rtChat = new System.Windows.Forms.RichTextBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tmrStudentVideo = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxQuestionVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTeacher)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTestAVDevices
            // 
            this.btnTestAVDevices.AutoSize = true;
            this.btnTestAVDevices.BackColor = System.Drawing.SystemColors.Control;
            this.btnTestAVDevices.ForeColor = System.Drawing.Color.DarkRed;
            this.btnTestAVDevices.Location = new System.Drawing.Point(4, 75);
            this.btnTestAVDevices.Name = "btnTestAVDevices";
            this.btnTestAVDevices.Size = new System.Drawing.Size(95, 32);
            this.btnTestAVDevices.TabIndex = 9;
            this.btnTestAVDevices.Text = "Test AV devices";
            this.btnTestAVDevices.UseVisualStyleBackColor = false;
            this.btnTestAVDevices.Click += new System.EventHandler(this.btnTestAVDevices_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.SystemColors.Control;
            this.btnAccept.Enabled = false;
            this.btnAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.ForeColor = System.Drawing.Color.DarkRed;
            this.btnAccept.Location = new System.Drawing.Point(544, 568);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(170, 43);
            this.btnAccept.TabIndex = 17;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnStartLecture
            // 
            this.btnStartLecture.AutoSize = true;
            this.btnStartLecture.BackColor = System.Drawing.SystemColors.Control;
            this.btnStartLecture.ForeColor = System.Drawing.Color.DarkRed;
            this.btnStartLecture.Location = new System.Drawing.Point(4, 113);
            this.btnStartLecture.Name = "btnStartLecture";
            this.btnStartLecture.Size = new System.Drawing.Size(95, 32);
            this.btnStartLecture.TabIndex = 18;
            this.btnStartLecture.Text = "Start Lecture";
            this.btnStartLecture.UseVisualStyleBackColor = false;
            this.btnStartLecture.Click += new System.EventHandler(this.btnStartLecture_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lstBoxStudents
            // 
            this.lstBoxStudents.BackColor = System.Drawing.SystemColors.Info;
            this.lstBoxStudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxStudents.ForeColor = System.Drawing.Color.Green;
            this.lstBoxStudents.FormattingEnabled = true;
            this.lstBoxStudents.HorizontalScrollbar = true;
            this.lstBoxStudents.ItemHeight = 20;
            this.lstBoxStudents.Location = new System.Drawing.Point(544, 75);
            this.lstBoxStudents.Name = "lstBoxStudents";
            this.lstBoxStudents.ScrollAlwaysVisible = true;
            this.lstBoxStudents.Size = new System.Drawing.Size(170, 224);
            this.lstBoxStudents.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(541, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Students online";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.recordingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.signOutToolStripMenuItem.Text = "Sign out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // recordingToolStripMenuItem
            // 
            this.recordingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startRecordingToolStripMenuItem,
            this.stopRecordingToolStripMenuItem});
            this.recordingToolStripMenuItem.Name = "recordingToolStripMenuItem";
            this.recordingToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.recordingToolStripMenuItem.Text = "Recording";
            // 
            // startRecordingToolStripMenuItem
            // 
            this.startRecordingToolStripMenuItem.Name = "startRecordingToolStripMenuItem";
            this.startRecordingToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.startRecordingToolStripMenuItem.Text = "Start recording";
            this.startRecordingToolStripMenuItem.Click += new System.EventHandler(this.startRecordingToolStripMenuItem_Click);
            // 
            // stopRecordingToolStripMenuItem
            // 
            this.stopRecordingToolStripMenuItem.Name = "stopRecordingToolStripMenuItem";
            this.stopRecordingToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.stopRecordingToolStripMenuItem.Text = "Stop recording";
            this.stopRecordingToolStripMenuItem.Click += new System.EventHandler(this.stopRecordingToolStripMenuItem_Click);
            // 
            // lblTeacherName
            // 
            this.lblTeacherName.AutoSize = true;
            this.lblTeacherName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTeacherName.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTeacherName.Location = new System.Drawing.Point(99, 54);
            this.lblTeacherName.Name = "lblTeacherName";
            this.lblTeacherName.Size = new System.Drawing.Size(70, 18);
            this.lblTeacherName.TabIndex = 24;
            this.lblTeacherName.Text = "Atta Ullah";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(541, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "Question requests";
            // 
            // lstBoxQuestionRequests
            // 
            this.lstBoxQuestionRequests.BackColor = System.Drawing.SystemColors.Info;
            this.lstBoxQuestionRequests.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstBoxQuestionRequests.ForeColor = System.Drawing.Color.Green;
            this.lstBoxQuestionRequests.FormattingEnabled = true;
            this.lstBoxQuestionRequests.HorizontalScrollbar = true;
            this.lstBoxQuestionRequests.ItemHeight = 20;
            this.lstBoxQuestionRequests.Location = new System.Drawing.Point(544, 338);
            this.lstBoxQuestionRequests.Name = "lstBoxQuestionRequests";
            this.lstBoxQuestionRequests.ScrollAlwaysVisible = true;
            this.lstBoxQuestionRequests.Size = new System.Drawing.Size(170, 224);
            this.lstBoxQuestionRequests.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(105, 619);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(377, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "National University of Modern Languages Islamabad";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Video_Conference.Properties.Resources.NUMLLOGO;
            this.pictureBox1.Location = new System.Drawing.Point(4, 568);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(95, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // picBoxQuestionVideo
            // 
            this.picBoxQuestionVideo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxQuestionVideo.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.picBoxQuestionVideo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxQuestionVideo.Image = global::Video_Conference.Properties.Resources.NUMLLOGO;
            this.picBoxQuestionVideo.Location = new System.Drawing.Point(378, 303);
            this.picBoxQuestionVideo.Name = "picBoxQuestionVideo";
            this.picBoxQuestionVideo.Size = new System.Drawing.Size(160, 130);
            this.picBoxQuestionVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxQuestionVideo.TabIndex = 12;
            this.picBoxQuestionVideo.TabStop = false;
            this.picBoxQuestionVideo.Visible = false;
            // 
            // picBoxTeacher
            // 
            this.picBoxTeacher.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoxTeacher.BackColor = System.Drawing.SystemColors.Control;
            this.picBoxTeacher.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxTeacher.Image = global::Video_Conference.Properties.Resources.NUMLLOGO;
            this.picBoxTeacher.Location = new System.Drawing.Point(102, 75);
            this.picBoxTeacher.Name = "picBoxTeacher";
            this.picBoxTeacher.Size = new System.Drawing.Size(436, 358);
            this.picBoxTeacher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxTeacher.TabIndex = 6;
            this.picBoxTeacher.TabStop = false;
            // 
            // rtChat
            // 
            this.rtChat.BackColor = System.Drawing.SystemColors.Info;
            this.rtChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtChat.Location = new System.Drawing.Point(102, 439);
            this.rtChat.Name = "rtChat";
            this.rtChat.ReadOnly = true;
            this.rtChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtChat.Size = new System.Drawing.Size(436, 123);
            this.rtChat.TabIndex = 29;
            this.rtChat.Text = "";
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.SystemColors.Info;
            this.txtChat.Location = new System.Drawing.Point(102, 568);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(344, 43);
            this.txtChat.TabIndex = 30;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.Control;
            this.btnSend.Enabled = false;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.DarkRed;
            this.btnSend.Location = new System.Drawing.Point(452, 568);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 43);
            this.btnSend.TabIndex = 31;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tmrStudentVideo
            // 
            this.tmrStudentVideo.Interval = 30;
            this.tmrStudentVideo.Tick += new System.EventHandler(this.tmrStudentVideo_Tick);
            // 
            // TeacherSide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(720, 642);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.rtChat);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstBoxQuestionRequests);
            this.Controls.Add(this.lblTeacherName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstBoxStudents);
            this.Controls.Add(this.btnStartLecture);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.picBoxQuestionVideo);
            this.Controls.Add(this.btnTestAVDevices);
            this.Controls.Add(this.picBoxTeacher);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "TeacherSide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VCES - Teacher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeacherSide_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxQuestionVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTeacher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            Application.Run(new SignIn());
		}

        public string userName = "";

        Data data = new Data();
        TcpClient myclient;        
        MemoryStream ms;
        NetworkStream myns;
        BinaryWriter mysw;
        Thread threadReceiveVideo;
        TcpListener mytcpl;
        Socket mysocket;
        NetworkStream ns;
        int hd = 0;

        private void StartReceivingVideo()
        {
            try
            {

                // Open The Port
                mytcpl = new TcpListener(IPAddress.Any, 5000);
                mytcpl.Start();						 // Start Listening on That Port
                mysocket = mytcpl.AcceptSocket();		 // Accept Any Request From Client and Start a Session
                ns = new NetworkStream(mysocket);	 // Receives The Binary Data From Port

                picBoxQuestionVideo.Image = Image.FromStream(ns);
                mytcpl.Stop();							 // Close TCP Session

                StartReceivingVideo();				 // Back to First Method                
            }
            catch (Exception ex)
            {
                StartReceivingVideo();
                //MessageBox.Show(ex.Message, "VCES:TeacherSide:Start_Receiving_Video_Conferene()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tmrStudentVideo_Tick(object sender, EventArgs e)
        {
            StartSendingStudentVideo();
        }

        private void StartSendingStudentVideo()
        {
            try
            {
                MemoryStream msStudentVideo = new MemoryStream();

                picBoxQuestionVideo.Image.Save(msStudentVideo, System.Drawing.Imaging.ImageFormat.Jpeg);

                byte[] arrImage = msStudentVideo.GetBuffer();

                foreach (DictionaryEntry item in joinedList)
                {
                    IPEndPoint ipEndPoint = (IPEndPoint)item.Value;

                    //Connecting with Students
                    TcpClient tcpCStudentVideo = new TcpClient("" + ipEndPoint.Address, 5001);
                    NetworkStream nsStudentVideo = tcpCStudentVideo.GetStream();
                    BinaryWriter bwStudentVideo = new BinaryWriter(nsStudentVideo);
                    bwStudentVideo.Write(arrImage);//send the stream to above address

                    bwStudentVideo.Flush();
                    nsStudentVideo.Flush();

                    bwStudentVideo.Close();
                    nsStudentVideo.Close();
                    tcpCStudentVideo.Close();
                }
                msStudentVideo.Flush();
                msStudentVideo.Close();
            }
            catch (Exception ex)
            {
                StartSendingStudentVideo();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StartSendingVideo();
        }

        private void StartSendingVideo()
        {
            try
            {

                ms = new MemoryStream();// Store it in Binary Array as Stream


                IDataObject data;
                Bitmap bmap = null;

                //  Copy image to clipboard
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);

                //  Get image from clipboard and convert it to a bitmap
                data = Clipboard.GetDataObject();

                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    bmap = (Bitmap)data.GetData(typeof(System.Drawing.Bitmap));
                    //saveJpeg("F:\\NUML\\4th Semester\\Work Shop\\First Demo on 12 Nov 2010\\Project 11 nov 2010\\Video recording\\Recordings\\" + System.DateTime.Now.ToFileTime() + ".jpg", bmap);
                    //bmap.Save("F:\\NUML\\4th Semester\\Work Shop\\First Demo on 12 Nov 2010\\Project 11 nov 2010\\Video recording\\Recordings\\" + System.DateTime.Now.ToFileTime() + ".jpg");
                }
                Clipboard.Clear();

                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

                // Create an Encoder object based on the GUID
                // for the Quality parameter category.
                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                // Create an EncoderParameters object.
                // An EncoderParameters object has an array of EncoderParameter
                // objects. In this case, there is only one
                // EncoderParameter object in the array.
                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmap.Save(ms, jgpEncoder, myEncoderParameters);

                byte[] arrImage = ms.GetBuffer();

                foreach (DictionaryEntry item in joinedList)
                {
                    IPEndPoint ipEndPoint = (IPEndPoint)item.Value;

                    //Connecting with Students
                    myclient = new TcpClient("" + ipEndPoint.Address, 5000);
                    myns = myclient.GetStream();
                    mysw = new BinaryWriter(myns);
                    mysw.Write(arrImage);//send the stream to above address
                                        
                    mysw.Flush();
                    myns.Flush();
                    
                    mysw.Close();
                    myns.Close();
                    myclient.Close();
                }
                ms.Flush();
                ms.Close();
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Video Conference Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void btnTestAVDevices_Click(object sender, EventArgs e)
        {
            OpenPreviewWindow();

            /*
            if (!OpenPreviewWindow())
            {
                MessageBox.Show("Webcam is absent or not connected properly\nPlease check webcam settings and try again", "VCES - Webcam error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            */

            
            btnTestAVDevices.Enabled = false;
        }
		
		private void btnStop_Click(object sender, System.EventArgs e)
		{
			ClosePreviewWindow();

		}
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{			
			ClosePreviewWindow();			
		}

        TcpClient tcpClientQuestioningStudent;
        NetworkStream nS;

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (btnAccept.Text.Equals("Accept"))
            {
                if (lstBoxQuestionRequests.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select student name \nfrom above list to accept the request", "VCES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                object val = null;
                foreach (DictionaryEntry item in studentList)
                {
                    if (item.Key.Equals(lstBoxQuestionRequests.SelectedItem))
                    {
                        val = item.Value;
                        break;
                    }
                }
                tcpClientQuestioningStudent = (TcpClient)val;
                nS = tcpClientQuestioningStudent.GetStream();    

                Data d = new Data();
                d.cmd = Command.QuestionRequstAccepted;
                byte[] outBuffer = d.ToByte();

                nS.Write(outBuffer, 0, outBuffer.Length);
                nS.Flush();
                
                lstBoxQuestionRequests.Items.Remove(lstBoxQuestionRequests.SelectedItem);
                btnAccept.Enabled = false;                
            }
            else
            {
                Data d = new Data();
                d.cmd = Command.AskingQuestionStop;
                byte[] outBuffer = d.ToByte();
                broadCastClientNotification(outBuffer);
                /*
                nS.Write(outBuffer, 0, outBuffer.Length);
                nS.Flush();
                */
                hd = hHwnd;
                questioningStudentName = null;
                picBoxQuestionVideo.Visible = false;
                tmrStudentVideo.Enabled = false;

                btnAccept.Text = "Accept";
                //picBoxQuestionVideo.Image = Image.FromFile("NUMLLOGO.jpg");
                /*
                if(threadReceiveVideo.IsAlive)
                {
                    threadReceiveVideo.Abort();
                    
                }
                if(threadReceiveAudio.IsAlive)
                {
                    threadReceiveAudio.Abort();
                }
                 */ 
                                                
            }
        }

        

        

        

        private void btnStartLecture_Click(object sender, EventArgs e)
        {
            if (btnStartLecture.Text.Equals("Start Lecture"))
            {
                if (btnTestAVDevices.Enabled)
                {
                    MessageBox.Show("Before starting lecture please test your audio/video devices by clicking the 'Test AV devices' button", "VCES - Audio/Video testing", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (lstBoxStudents.Items.Count > 0)
                {
                    btnStartLecture.Text = "End Lecture";
                    hd = hHwnd;
                    StartSendingAudio();
                    timer1.Enabled = true;

                    
                }
                else
                {
                    MessageBox.Show("No student is online at this time!", "VCES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                timer1.Enabled = false;
                ClosePreviewWindow();
                StopAudio();
                picBoxTeacher.Image = Image.FromFile("NUMLLOGO.jpg");
                lstBoxStudents.Items.Clear();
                btnTestAVDevices.Enabled = true;
                btnStartLecture.Text = "Start Lecture";                
            }
        }





        //Audio Code

        private UdpClient udpAudioReceiving;
        private UdpClient udpSending;

        private Thread threadSendAudio;
        private Thread threadReceiveAudio;

        Command sendCommand;

        private CaptureBufferDescription captureBufferDescription;
        private AutoResetEvent autoResetEvent;
        private Notify notify;        
        private WaveFormat waveFormat;
        private Capture capture;
        private int bufferSize;
        private CaptureBuffer captureBuffer;
        
        private Device device;
        private SecondaryBuffer playbackBuffer;
        private BufferDescription playbackBufferDescription;
        private Socket clientSocket;
        private bool bStop;                         //Flag to end the Start and Receive threads.
        private IPEndPoint otherPartyIP;            //IP of party we want to make a call.
        private EndPoint otherPartyEP;
        private volatile bool bIsCallActive;                 //Tells whether we have an active call.        
        private byte[] dataToReadAndBroadCast = new byte[1024];

        /*
         * Initializes all the data members.
         */        
        private void AudioInitialize()
        {
            try
            {
                device = new Device();
                device.SetCooperativeLevel(this, CooperativeLevel.Normal);

                CaptureDevicesCollection captureDeviceCollection = new CaptureDevicesCollection();
                
                DeviceInformation deviceInfo = captureDeviceCollection[0];

                capture = new Capture(deviceInfo.DriverGuid);

                short channels = 1; //Stereo.
                short bitsPerSample = 16; //16Bit, alternatively use 8Bits.
                int samplesPerSecond = 44100; //11KHz use 11025 , 22KHz use 22050, 44KHz use 44100 etc.

                //Set up the wave format to be captured.
                waveFormat = new WaveFormat();
                waveFormat.Channels = channels;
                waveFormat.FormatTag = WaveFormatTag.Pcm;
                waveFormat.SamplesPerSecond = samplesPerSecond;
                waveFormat.BitsPerSample = bitsPerSample;
                waveFormat.BlockAlign = (short)(channels * (bitsPerSample / (short)8));
                waveFormat.AverageBytesPerSecond = waveFormat.BlockAlign * samplesPerSecond;

                captureBufferDescription = new CaptureBufferDescription();
                captureBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond;//approx 200 milliseconds of PCM data.
                captureBufferDescription.Format = waveFormat;

                playbackBufferDescription = new BufferDescription();
                playbackBufferDescription.BufferBytes = waveFormat.AverageBytesPerSecond;
                playbackBufferDescription.Format = waveFormat;
                playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);

                bufferSize = captureBufferDescription.BufferBytes;
        
                bIsCallActive = false;                

                Speakers speaker = new Speakers();
                speaker.Mono = true;
                Volume vol;
                vol = Volume.Min;
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TeacherSide:AudioInitialize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }                        
        
        

        /*
         * Send() function sends data captured from microphone across the network on port 1550.
         */
        private void SendAudio()
        {            
            try
            {
                udpSending = new UdpClient();

                //The following lines get audio from microphone and then send them 
                //across network.

                captureBuffer = new CaptureBuffer(captureBufferDescription, capture);                

                CreateNotifyPositions();

                int halfBuffer = bufferSize / 2;

                captureBuffer.Start(true);

                bool readFirstBufferPart = true;
                int offset = 0;

                MemoryStream memStream = new MemoryStream(halfBuffer);
                bStop = false;
                while (!bStop)
                {
                    autoResetEvent.WaitOne();
                    memStream.Seek(0, SeekOrigin.Begin);
                    captureBuffer.Read(offset, memStream, halfBuffer, LockFlag.None);
                    readFirstBufferPart = !readFirstBufferPart;
                    offset = readFirstBufferPart ? 0 : halfBuffer;

                    byte[] dataToWrite = memStream.GetBuffer();
                    IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 1550);

                    foreach (DictionaryEntry item in joinedList)
                    {
                        ipEndPoint = (IPEndPoint)item.Value;
                        udpSending.Send(dataToWrite, dataToWrite.Length, ipEndPoint.Address.ToString(), 1550);
                        if (questioningStudentName != null)
                        {
                            if (!item.Key.Equals(questioningStudentName))
                            {
                                udpSending.Send(dataToReadAndBroadCast, dataToReadAndBroadCast.Length, ipEndPoint.Address.ToString(), 1550);
                            }
                        }
                    }

                    /*
                    if (questioningStudentName != null)
                    {
                        dataToWrite = dataToReadAndBroadCast;
                        foreach (DictionaryEntry item in joinedList)
                        {
                            if (!item.Key.Equals(questioningStudentName))
                            {
                                ipEndPoint = (IPEndPoint)item.Value;
                                udpSending.Send(dataToWrite, dataToWrite.Length, ipEndPoint.Address.ToString(), 1550);
                            }
                        }
                    }
                    else
                    { 
                        dataToWrite = memStream.GetBuffer();
                        foreach (DictionaryEntry item in joinedList)
                        {
                            ipEndPoint = (IPEndPoint)item.Value;
                            udpSending.Send(dataToWrite, dataToWrite.Length, ipEndPoint.Address.ToString(), 1550);
                        }
                    }
                    */
                }
            }
            catch (Exception ex)
            {
                SendAudio();
                //MessageBox.Show(ex.Message, "TeacherSide:SendAudio()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /*
         * Receive audio data coming on port 1550 and feed it to the speakers to be played.
         */
        private void ReceiveAudio()
        {
            try
            {

                udpAudioReceiving = new UdpClient(1550);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                
                while (true)
                {

                    //Receive data.
                    byte[] byteData = udpAudioReceiving.Receive(ref remoteEP);
                    dataToReadAndBroadCast = byteData;

                    //Play the data received to the user.
                    playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
                    playbackBuffer.Write(0, byteData, LockFlag.None);
                    playbackBuffer.Play(0, BufferPlayFlags.Default);

                }

            }
            catch (Exception ex)
            {
                ReceiveAudio();
                //MessageBox.Show(ex.Message, "TeacherSide:VideoWindow:Receive()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
        }

        private void CreateNotifyPositions()
        {
            try
            {
                autoResetEvent = new AutoResetEvent(false);
                notify = new Notify(captureBuffer);
                BufferPositionNotify bufferPositionNotify1 = new BufferPositionNotify();
                bufferPositionNotify1.Offset = bufferSize / 2 - 1;
                bufferPositionNotify1.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle();
                BufferPositionNotify bufferPositionNotify2 = new BufferPositionNotify();
                bufferPositionNotify2.Offset = bufferSize - 1;
                bufferPositionNotify2.EventNotifyHandle = autoResetEvent.SafeWaitHandle.DangerousGetHandle();

                notify.SetNotificationPositions(new BufferPositionNotify[] { bufferPositionNotify1, bufferPositionNotify2 });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VoiceChat-CreateNotifyPositions ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
        
        private void StartSendingAudio()
        {
            try
            {                                
                threadReceiveAudio = new Thread(new ThreadStart(SendAudio));
                //Start the sender thread.
                threadReceiveAudio.Start();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TeacherSide-StartSendingAudio()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartReceivingAudio()
        {
            try
            {                                
                threadReceiveAudio = new Thread(new ThreadStart(ReceiveAudio));
                //Start the receiver thread.
                threadReceiveAudio.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "TeacherSide-StartReceivingAudio()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopAudio()
        {
            captureBuffer.Stop();
            playbackBuffer.Stop();
            //Close the sockets.            
            
        }






        
        public byte[] inBuffer;
        public byte[] outBuffer;
        public static Socket socket;
        public Hashtable joinedList = new Hashtable();
                
        EndPoint studentEP;
        Command lectureStatus = Command.Null;

        Data msgReceived;

        private void Form1_Load(object sender, EventArgs e)
        {
            lblTeacherName.Text = userName;
            Thread text = new Thread(messageReceived);
            text.Start();

            threadReceiveVideo = new Thread(new ThreadStart(StartReceivingVideo)); // Start Thread Session
            threadReceiveVideo.Start();

            AudioInitialize();
            StartReceivingAudio();
            
        }

        
        public void messageReceived()
        {

            TcpListener tcpListenerForNewStudent = new TcpListener(IPAddress.Any, 1450);
            tcpListenerForNewStudent.Start();
            
            TcpClient tcpClientForNewStudent = default(TcpClient);
            
            

            while (true)
            {
                tcpClientForNewStudent = tcpListenerForNewStudent.AcceptTcpClient();

                Socket socketForClient = tcpClientForNewStudent.Client;
                studentEP = (EndPoint)(new IPEndPoint(IPAddress.Any, 0));
                studentEP = socketForClient.RemoteEndPoint;
                
                inBuffer = new byte[tcpClientForNewStudent.ReceiveBufferSize];
                NetworkStream streamForNewStudent = tcpClientForNewStudent.GetStream();
                streamForNewStudent.Read(inBuffer, 0, inBuffer.Length);
                msgReceived = new Data(inBuffer);
                string senderName = msgReceived.senderName;

                switch (msgReceived.cmd)
                {
                    case Command.Join:
                        msg();
                        if (joinedList.Count != 0)
                        {
                            SendJoinedStudentsList(tcpClientForNewStudent, msgReceived);
                        }
                        joinedList.Add(senderName, studentEP);
                        if (studentList.Count != 0)
                        {
                            broadCastClientNotification(inBuffer);
                        }
                        HandleStudent(senderName, tcpClientForNewStudent);

                        Data data = new Data();
                        data.cmd = Command.JoinRequestAccepted;
                        data.userName = lblTeacherName.Text;
                        byte[] outStream = data.ToByte();

                        streamForNewStudent.Write(outStream, 0, outStream.Length);
                        streamForNewStudent.Flush();

                        
                        break;
                }
            }
        }


        public string studentName = "";
        public string questioningStudentName = null;
        TcpClient tcpClientForJoinedStudent;
        NetworkStream studentStream;
        Hashtable studentList = new Hashtable();
        Thread studentThread;
        Data message;
        byte[] studentBuffer;

        public void HandleStudent(string name, TcpClient tcpClient)
        {
            tcpClientForJoinedStudent = new TcpClient();
            studentStream = default(NetworkStream);
            studentName = name;
            tcpClientForJoinedStudent = tcpClient;
            addClient();
        }

        public void addClient()
        {
            studentList.Add(studentName, tcpClientForJoinedStudent);

            studentThread = new Thread(new ThreadStart(startClient));
            studentThread.Start();
        }

        public void startClient()
        {
            
            studentStream = tcpClientForJoinedStudent.GetStream();
            studentBuffer = new byte[tcpClientForJoinedStudent.ReceiveBufferSize];
            while (true)
            {
                
                studentStream.Read(studentBuffer, 0, studentBuffer.Length);

                message = new Data(studentBuffer);

                StudentMessage();
            }
        }

        public void StudentMessage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(StudentMessage));
            }
            else
            {
                switch (message.cmd)
                {
                    case Command.Leave:
                        studentList.Remove(message.senderName);
                        joinedList.Remove(message.senderName);
                        lstBoxStudents.Items.Remove(message.senderName);
                        if (lstBoxQuestionRequests.Items.Contains(message.senderName))
                        {
                            lstBoxQuestionRequests.Items.Remove(message.senderName);
                        }
                        if (lstBoxStudents.Items.Count == 0)
                        {
                            timer1.Enabled = false;
                            btnStartLecture.Enabled = true;
                            btnSend.Enabled = false;
                        }
                        if (picBoxQuestionVideo.Visible && questioningStudentName.Equals(message.senderName))
                        {
                            picBoxQuestionVideo.Visible = false;
                            tmrStudentVideo.Enabled = false;
                        }
                        broadCastClientNotification(studentBuffer);
                        studentStream.Close();
                        studentThread.Abort();
                        break;
                    case Command.QuestionRequest:
                        lstBoxQuestionRequests.Items.Add(message.senderName);
                        btnAccept.Enabled = true;
                        break;
                    case Command.AskingQuestionStart:
                        hd = 0;
                        tmrStudentVideo.Enabled = true;
                        picBoxQuestionVideo.Visible = true;
                        broadCastClientNotification(studentBuffer);
                        questioningStudentName = message.senderName;
                        btnAccept.Enabled = true;
                        btnAccept.Text = "Stop";
                        break;
                    case Command.AskingQuestionCancel:
                        if (lstBoxQuestionRequests.Items.Count != 0)
                        {
                            btnAccept.Enabled = true;
                        }
                        break;
                    case Command.TextMessage:
                        rtChat.Text = rtChat.Text + Environment.NewLine + message.senderName + " >>   " + message.chatText + Environment.NewLine;
                        broadCastClientNotification(studentBuffer);
                        break;
                }
            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                lstBoxStudents.Items.Add(msgReceived.senderName);
                if (btnStartLecture.Text.Equals("End Lecture") && timer1.Enabled == false)
                {
                    timer1.Enabled = true;
                }
                if (btnSend.Enabled == false)
                {
                    btnSend.Enabled = true;
                }
            }
        }

        public void broadCastClientNotification(byte[] broadCastBuffer)
        {
            foreach (DictionaryEntry item in studentList)
            {
                try
                {
                    TcpClient tcpClientBroadCast;
                    tcpClientBroadCast = (TcpClient)item.Value;
                    //MessageBox.Show("tcp is " + tcpClientBroadCast.Client.RemoteEndPoint.ToString());
                    NetworkStream broadcastStream = tcpClientBroadCast.GetStream();

                    broadcastStream.Write(broadCastBuffer, 0, broadCastBuffer.Length);
                    broadcastStream.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Program-broadCastClientNotification()");
                    Console.WriteLine("" + ex);
                }
            }
        }
                       
        private void SendJoinedStudentsList(TcpClient tcp, Data data)
        {
            foreach (DictionaryEntry item in joinedList)
            {
                try
                {
                    data.senderName = (string)item.Key;
                    byte[] clientBuffer = data.ToByte();
                    NetworkStream clientStream = tcp.GetStream();
                    clientStream.Write(clientBuffer, 0, clientBuffer.Length);
                    clientStream.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Program-broadCastClientNotification()");
                    Console.WriteLine("" + ex);
                }
            }
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timer1.Enabled = false;
                Data data = new Data();
                data.cmd = Command.LectureEnded;
                data.userName = lblTeacherName.Text;
                byte[] outStream = data.ToByte();
                broadCastClientNotification(outStream);
                this.Hide();
                SignIn sin = new SignIn();
                sin.Show();
                sin.txtVCESName.Text = lblTeacherName.Text;
                sin.txtPassword.Focus();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timer1.Enabled = false;
                Data data = new Data();
                data.cmd = Command.LectureEnded;
                data.userName = lblTeacherName.Text;
                byte[] outStream = data.ToByte();
                broadCastClientNotification(outStream);
                this.Hide();
                SignIn sin = new SignIn();
                sin.Show();
                sin.txtVCESName.Text = lblTeacherName.Text;
                sin.txtPassword.Focus();
            }
        }

        private void TeacherSide_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timer1.Enabled = false;
                Data data = new Data();
                data.cmd = Command.LectureEnded;
                data.userName = lblTeacherName.Text;
                byte[] outStream = data.ToByte();
                broadCastClientNotification(outStream);
                this.Hide();
                SignIn sin = new SignIn();
                sin.Show();
                sin.txtVCESName.Text = lblTeacherName.Text;
                sin.txtPassword.Focus();
            }
            else
            {
                e.Cancel = true;
            }
                
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            rtChat.Text = rtChat.Text + Environment.NewLine + lblTeacherName.Text + " >>   " + txtChat.Text + Environment.NewLine;
            Data data = new Data();
            if (lstBoxStudents.Items.Count != 0)
            {
                data.cmd = Command.TextMessage;
                data.userName = lblTeacherName.Text;
                data.chatText = txtChat.Text;
                broadCastClientNotification(data.ToByte());
                txtChat.Text = "";
            }
            
        }

        private void startRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread recording = new Thread(new ThreadStart(StartRecording));
            recording.Start();
        }

        private void StartRecording()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(StartRecording));
            }
            else
            {
                Application.DoEvents();

                CAPTUREPARMS setup = default(CAPTUREPARMS);
                SendMessage2((IntPtr)hHwnd, WM_CAP_GET_SEQUENCE_SETUP, new IntPtr(Marshal.SizeOf(typeof(CAPTUREPARMS))), ref setup);

                setup.fYield = 1;
                SendMessage2((IntPtr)hHwnd, WM_CAP_SET_SEQUENCE_SETUP, new IntPtr(Marshal.SizeOf(typeof(CAPTUREPARMS))), ref setup);

                //---start recording---
                PostMessage(hHwnd, WM_CAP_SEQUENCE, 0, 0);

                /*
                //CAPTUREPARMS CaptureParams = default(CAPTUREPARMS);
                CaptureParams.fYield = 1;
                CaptureParams.fMakeUserHitOKToCapture = 1;
                
                
                CaptureParams.fAbortLeftMouse = 0;
                CaptureParams.fAbortRightMouse = 0;
                CaptureParams.dwRequestMicroSecPerFrame = 66667;
                
                CaptureParams.wPercentDropForError = 10;
                CaptureParams.wChunkGranularity = 0;
                CaptureParams.dwIndexSize = 0;
                CaptureParams.wNumVideoRequested = 10;
                CaptureParams.fCaptureAudio = 1;
                
                CaptureParams.fMCIControl = 0;
                CaptureParams.fStepMCIDevice = 1;
                CaptureParams.dwMCIStartTime = 0;
                CaptureParams.dwMCIStopTime = 0;
                CaptureParams.fStepCaptureAt2x = 0;
                CaptureParams.wStepCaptureAverageFrames = 5;
                CaptureParams.dwAudioBufferSize = 0;
                CaptureParams.wNumAudioRequested = 10;
                CaptureParams.fLimitEnabled = false;
                
                SendMessage2((IntPtr)hHwnd, WM_CAP_SET_SEQUENCE_SETUP, new IntPtr(Marshal.SizeOf(typeof(CAPTUREPARMS))), ref CaptureParams);
                */



            }
        }

        private void stopRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            //---save the recording to file---
            PostMessage(hHwnd, WM_CAP_FILE_SAVEAS, 0, "Recordings\\" + System.DateTime.Now.ToFileTime() + ".avi");
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
        public struct CAPTUREPARMS
        {
            public System.UInt32 dwRequestMicroSecPerFrame;
            public System.Int32 fMakeUserHitOKToCapture;
            public System.UInt32 wPercentDropForError;
            public System.Int32 fYield;
            public System.UInt32 dwIndexSize;
            public System.UInt32 wChunkGranularity;
            public System.Int32 fCaptureAudio;
            public System.UInt32 wNumVideoRequested;
            public System.UInt32 wNumAudioRequested;
            public System.Int32 fAbortLeftMouse;
            public System.Int32 fAbortRightMouse;
            public System.Int32 fMCIControl;
            public System.Int32 fStepMCIDevice;
            public System.UInt32 dwMCIStartTime;
            public System.UInt32 dwMCIStopTime;
            public System.Int32 fStepCaptureAt2x;
            public System.UInt32 wStepCaptureAverageFrames;
            public System.UInt32 dwAudioBufferSize;
            public bool fLimitEnabled;
        };

        

        
    }
}
