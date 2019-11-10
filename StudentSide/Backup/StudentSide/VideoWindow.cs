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

using Microsoft.DirectX.DirectSound;

namespace StudentSide
{
    public partial class VideoWindow : Form
    {
        public VideoWindow()
        {
            InitializeComponent();
            
        }

        public string serverIPAddress = "192.168.1.1";
        public string userName = "";
        public VCES fmVCES;
        SignIn sin;
        





        const short WM_CAP = 1024;
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
        int iDevice = 0;

        int hHwnd;

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "GetActiveWindow")]
        static extern int GetActiveWindow();

        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SendMessageA")]
        static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] 
			object lParam);
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetWindowPos")]
        static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        [System.Runtime.InteropServices.DllImport("user32")]
        static extern bool DestroyWindow(int hndw);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern int capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
        [System.Runtime.InteropServices.DllImport("avicap32.dll")]
        static extern bool capGetDriverDescriptionA(short wDriver, string lpszName, int cbName, string lpszVer, int cbVer);

        private bool OpenPreviewWindow()
        {
            int iWidth = 246;
            int iHeight = 178;
            
            // 
            //  Open Preview window in picturebox
            // 
            hHwnd = capCreateCaptureWindowA(iDevice.ToString(), (WS_VISIBLE | WS_CHILD), 0, 0, 640, 480, picBoxUser.Handle.ToInt32(), 0);
            // 
            //  Connect to device
            // 
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, iDevice, 0) == 1)
            {
                // 
                // Set the preview scale
                // 
                SendMessage(hHwnd, WM_CAP_SET_SCALE, 1, 0);
                // 
                // Set the preview rate in milliseconds
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 33, 0);
                // 
                // Start previewing the image from the camera
                // 
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, 1, 0);
                // 
                //  Resize window to fit in picturebox
                // 
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, iWidth, iHeight, (SWP_NOMOVE | SWP_NOZORDER));

                return true;
            }
            else
            {
                // 
                //  Error connecting to device close window
                //  
                DestroyWindow(hHwnd);
                
                return false;
            }            
        }
        private void ClosePreviewWindow()
        {
            // 
            //  Disconnect from device
            // 
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, iDevice, 0);
            // 
            //  close window
            // 
            DestroyWindow(hHwnd);
        }

        TcpClient myclient;
        MemoryStream ms;
        NetworkStream myns;
        BinaryWriter mysw;
        Thread myth;
        TcpListener mytcpl;
        Socket mysocket;
        NetworkStream ns;

        private void Start_Receiving_Video_Conference()
        {
            try
            {

                // Open The Port
                mytcpl = new TcpListener(IPAddress.Any, 5000);
                mytcpl.Start();						 // Start Listening on That Port
                mysocket = mytcpl.AcceptSocket();		 // Accept Any Request From Client and Start a Session
                ns = new NetworkStream(mysocket);	 // Receives The Binary Data From Port

                picBoxTeacher.Image = Image.FromStream(ns);
                mytcpl.Stop();							 // Close TCP Session

                Start_Receiving_Video_Conference();				 // Back to First Method              
            }
            catch (Exception)
            {
                Start_Receiving_Video_Conference();
            }
        }

        private void Start_Sending_Video_Conference()
        {
            try
            {

                ms = new MemoryStream();// Store it in Binary Array as Stream


                IDataObject data;
                Image bmap;

                //  Copy image to clipboard
                SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);

                //  Get image from clipboard and convert it to a bitmap
                data = Clipboard.GetDataObject();

                if (data.GetDataPresent(typeof(System.Drawing.Bitmap)))
                {
                    bmap = ((Image)(data.GetData(typeof(System.Drawing.Bitmap))));
                    bmap.Save(ms, ImageFormat.Bmp);
                }


                picBoxUser.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arrImage = ms.GetBuffer();


                    //Connecting with Students
                    myclient = new TcpClient(serverIPAddress, 5000);
                    myns = myclient.GetStream();
                    mysw = new BinaryWriter(myns);
                    mysw.Write(arrImage);//send the stream to above address

                    mysw.Flush();
                    myns.Flush();

                    mysw.Close();
                    myns.Close();
                    myclient.Close();
                
                ms.Flush();
                ms.Close();

            }
            catch (Exception ex)
            {
                return;
                //MessageBox.Show(ex.Message, "Video Conference Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartReceivingStudentVideo()
        {
            try
            {

                // Open The Port
                TcpListener tcpLStudentVideo = new TcpListener(IPAddress.Any, 5001);
                tcpLStudentVideo.Start();						 // Start Listening on That Port
                Socket sockStudentVideo = tcpLStudentVideo.AcceptSocket();		 // Accept Any Request From Client and Start a Session
                NetworkStream nsStudentVideo = new NetworkStream(sockStudentVideo);	 // Receives The Binary Data From Port

                picBoxStudent.Image = Image.FromStream(nsStudentVideo);
                tcpLStudentVideo.Stop();							 // Close TCP Session

                StartReceivingStudentVideo();				 // Back to First Method              
            }
            catch (Exception)
            {
                StartReceivingStudentVideo();
            }
        }
        
        private void btnQuestionRequest_Click(object sender, EventArgs e)
        {
            if(true)
            {
                
            }

            if (hHwnd == 0)
            {
                if (MessageBox.Show("Either webcam is not attached with your system or you did not start it\nPlease check your cam settings or click 'Start camera' button to start it\notherwise teacher will not be able to watch you\n\nContinue any way?", "VCES - Webcam error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;                    
                }                
            }
            Data data = new Data();
            data.cmd = Command.QuestionRequest;
            data.userName = lblUser.Text;
            byte[] outStream = data.ToByte();

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            btnQuestionRequest.Enabled = false;
        }

        private void btnJoinLeave_Click(object sender, EventArgs e)
        {
            if (btnJoinLeave.Text.Equals("Join Lecture"))
            {
                btnJoinLeave.Enabled = false;
                if (!connectToServer())
                {
                    if (MessageBox.Show("Teacher is not online right now!", "VCES - Teacher not responding", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        btnJoinLeave_Click(sender, e);
                        return;
                    }
                    else
                    {
                        btnJoinLeave.Enabled = true;
                        return;
                    }
                }

                Thread text = new Thread(messageReceived);
                text.Start();
                
                
                
                Data data = new Data();
                data.cmd = Command.Join;
                data.userName = lblUser.Text;

                byte[] outStream = data.ToByte();

                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                btnJoinLeave.Text = "Leave";
                btnJoinLeave.Enabled = true;
                btnQuestionRequest.Enabled = true;
                btnSend.Enabled = true;
            }
            else if (btnJoinLeave.Text.Equals("Leave"))
            {
                if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (tcpClientForStudent.Connected)
                    {
                        Data data = new Data();
                        data.cmd = Command.Leave;
                        data.userName = lblUser.Text;
                        byte[] outStream = data.ToByte();
                        serverStream.Write(outStream, 0, outStream.Length);
                        serverStream.Flush();
                        tcpClientForStudent.Close();
                        udpAudioReceiving.Close();
                    }

                    sin = new SignIn();
                    sin.txtVCESName.Text = lblUser.Text;

                    this.Dispose();
                    this.Hide();
                    sin.Show();
                    sin.txtPassword.Focus();
                }
            }
        }

        


        //Audio Code
        Thread threadAudioSend;
        Thread threadAudioReceive;
        UdpClient udpAudioSending;
        private UdpClient udpAudioReceiving;

        private CaptureBufferDescription captureBufferDescription;
        private AutoResetEvent autoResetEvent;
        private Notify notify;
        private WaveFormat waveFormat;
        private Capture capture;
        private int bufferSize;
        private CaptureBuffer captureBuffer;
                        //Listens and sends data on port 1550, used in synchronous mode.
        private Device device;
        private SecondaryBuffer playbackBuffer;
        private BufferDescription playbackBufferDescription;
        private Socket clientSocket;
        private bool audioReceive;                  //Flag to end the Receive thread
        private bool audioSend;                     //Flag to end the Start thread 
        private IPEndPoint otherPartyIP;            //IP of party we want to make a call.
        private EndPoint otherPartyEP;
        private volatile bool bIsCallActive;                 //Tells whether we have an active call.        
        private byte[] byteData = new byte[1024];   //Buffer to store the data received.
        private volatile int nUdpClientFlag;                 //Flag used to close the udpAudioReceiving socket.


        private Speakers speaker;
        /*
         * Initializes all the data members.
         */
        private void Initialize()
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
                nUdpClientFlag = 0;

                speaker = new Speakers();
                speaker.Mono = true;
                Volume vol;
                vol = Volume.Min;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio device(s) missing", "VCES - Audio device error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }



        /*
         * Send synchronously sends data captured from microphone across the network on port 1550.
         */
        private void Send()
        {

            try
            {
                udpAudioSending = new UdpClient();

                //The following lines get audio from microphone and then send them 
                //across network.

                captureBuffer = new CaptureBuffer(captureBufferDescription, capture);

                CreateNotifyPositions();

                int halfBuffer = bufferSize / 2;

                captureBuffer.Start(true);

                bool readFirstBufferPart = true;
                int offset = 0;

                MemoryStream memStream = new MemoryStream(halfBuffer);
                audioSend = true;
                
                while (true)
                {
                    autoResetEvent.WaitOne();
                    memStream.Seek(0, SeekOrigin.Begin);
                    captureBuffer.Read(offset, memStream, halfBuffer, LockFlag.None);
                    readFirstBufferPart = !readFirstBufferPart;
                    offset = readFirstBufferPart ? 0 : halfBuffer;
                    
                    byte[] dataToWrite = memStream.GetBuffer();

                    udpAudioSending.Send(dataToWrite, dataToWrite.Length, serverIPAddress, 1550);
                }             
            }
            catch (Exception ex)
            {
                if (captureBuffer.Capturing)
                {
                    captureBuffer.Stop();
                }
                
                Send();
                //MessageBox.Show(ex.Message, "VoiceChat-Send ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }            
        }

        /*
         * Receive audio data coming on port 1550 and feed it to the speakers to be played.
         */
        private void Receive()
        {
            try
            {

                udpAudioReceiving = new UdpClient(1550);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                audioReceive = true;
                while (audioReceive)
                {
                    
                    //Receive data.
                    byte[] byteData = udpAudioReceiving.Receive(ref remoteEP);

                    //Play the data received to the user.
                    playbackBuffer = new SecondaryBuffer(playbackBufferDescription, device);
                    playbackBuffer.Write(0, byteData, LockFlag.None);
                    playbackBuffer.Play(0, BufferPlayFlags.Default);
                    
                }
                
            }
            catch (Exception ex)
            {
                Receive();
                //MessageBox.Show(ex.Message, "StudentSide:VideoWindow:Receive()", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "StudentSide:VideoWindow:CreateNotifyPositions()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startAudioReceiving()
        {
            try
            {                
                
                
                threadAudioReceive = new Thread(new ThreadStart(Receive));                

                //Start the receiver thread.
                threadAudioReceive.Start();                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "StudentSide:VideoWindow:startAudioReceiving()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void startAudioSending()
        {
            try
            {
                threadAudioSend = new Thread(Send);

                //Start the sender thread.
                threadAudioSend.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "StudentSide:VideoWindow:startAudioSending()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Start_Sending_Video_Conference();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iDevice = 0;
            
            if (!OpenPreviewWindow())
            {
                picBoxUser.Image = Image.FromFile("webcam error.jpg");
                hHwnd = 0;
                return;
            }
            
            button1.Enabled = false;                        
        }

        private void VideoWindow_Load(object sender, EventArgs e)
        {
            lblUser.Text = userName;
            myth = new Thread(new System.Threading.ThreadStart(Start_Receiving_Video_Conference)); // Start Thread Session
            myth.Start();

            Initialize();
            startAudioReceiving();
            startAudioSending();

            Thread threadStudentVideo = new Thread(new System.Threading.ThreadStart(StartReceivingStudentVideo)); // Start Thread Session
            threadStudentVideo.Start();
        }

        public Socket socket;
        public IPEndPoint serverEP;
        public TcpClient tcpClientForStudent = new TcpClient();
        public NetworkStream serverStream = default(NetworkStream);

        public bool connectToServer()
        {
            serverEP = new IPEndPoint(IPAddress.Parse(serverIPAddress), 1450);
            
            try
            {
                tcpClientForStudent.Connect(serverEP);
                serverStream = tcpClientForStudent.GetStream();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        Data msgReceived;
        bool messages;
        DialogResult dlgAskingQuestion;

        public void messageReceived()
        {
            NetworkStream ns = tcpClientForStudent.GetStream();

            messages = true;
            try
            {
                while (messages)
                {
                    byte[] inBuffer = new byte[1024];

                    ns.Read(inBuffer, 0, inBuffer.Length);

                    msgReceived = new Data(inBuffer);
                    msg();
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                ns.Close();
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
                switch (msgReceived.cmd)
                {
                    case Command.JoinRequestAccepted:
                        lblRemote.Text = msgReceived.senderName;
                        break;
                    case Command.Join:
                        lstBoxStudents.Items.Add(msgReceived.senderName);
                        break;
                    case Command.Leave:
                        lstBoxStudents.Items.Remove(msgReceived.senderName);
                        break;

                    case Command.QuestionRequstAccepted:
                        dlgAskingQuestion = DialogResult.None;
                        using (new TimedDialog(30000))
                        {
                            dlgAskingQuestion = MessageBox.Show("Your question request has been accepted\nClick OK button to start asking your question", "VCES", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        }
                        Data data = new Data();
                        data.userName = lblUser.Text;
                        if (dlgAskingQuestion == DialogResult.OK)
                        {
                            data.cmd = Command.AskingQuestionStart;

                            serverStream.Write(data.ToByte(), 0, data.ToByte().Length);
                            serverStream.Flush();
                            if (!timer1.Enabled)
                            {
                                timer1.Enabled = true;
                            }
                            
                        }
                        else if (dlgAskingQuestion == DialogResult.Cancel)
                        {
                            data.cmd = Command.AskingQuestionCancel;
                            serverStream.Write(data.ToByte(), 0, data.ToByte().Length);
                            serverStream.Flush();
                        }
                        else
                        {
                            data.cmd = Command.AskingQuestionCancel;
                            serverStream.Write(data.ToByte(), 0, data.ToByte().Length);
                            serverStream.Flush();
                            MessageBox.Show("Your question asking time of 30 seconds is over\nYou cannot continue asking this question.\n\nTo send the request again, use 'Question Request' button.", "VCES - Question Time out", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            btnQuestionRequest.Enabled = true;                            
                        }
                        break;
                    case Command.AskingQuestionStart:
                        picBoxStudent.Visible = true;
                        break;
                    case Command.AskingQuestionStop:
                        picBoxStudent.Visible = false;
                        if (timer1.Enabled)
                        {
                            timer1.Enabled = false;
                        }
                        if (threadAudioSend.IsAlive)
                        {
                            audioSend = false;
                            //udpAudioSending.Close();
                            
                            //threadAudioSend.Abort();
                        }
                        btnQuestionRequest.Enabled = true;
                        break;                    
                    case Command.LectureEnded:
                        picBoxTeacher.Image = Image.FromFile("NUMLLOGO.jpg");
                        break;
                    case Command.TextMessage:
                        rtChat.Text = rtChat.Text + Environment.NewLine + msgReceived.senderName + " >>   " + msgReceived.chatText + Environment.NewLine;
                        break;
                }
            }
        }

        private void VideoWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tcpClientForStudent.Connected)
                {
                    Data data = new Data();
                    data.cmd = Command.Leave;
                    data.userName = lblUser.Text;
                    byte[] outStream = data.ToByte();
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                    tcpClientForStudent.Close();
                    udpAudioReceiving.Close();
                }

                sin = new SignIn();
                sin.txtVCESName.Text = lblUser.Text;
                
                this.Dispose();
                sin.Show();
                sin.txtPassword.Focus();                
            }
            else
            {                
                e.Cancel = true;                
            }
            
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tcpClientForStudent.Connected)
                {
                    Data data = new Data();
                    data.cmd = Command.Leave;
                    data.userName = lblUser.Text;
                    byte[] outStream = data.ToByte();
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                    tcpClientForStudent.Close();
                    udpAudioReceiving.Close();
                }

                sin = new SignIn();
                sin.txtVCESName.Text = lblUser.Text;

                this.Dispose();
                sin.Show();
                sin.txtPassword.Focus();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to leave the class room", "VCES - Leave confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (tcpClientForStudent.Connected)
                {
                    Data data = new Data();
                    data.cmd = Command.Leave;
                    data.userName = lblUser.Text;
                    byte[] outStream = data.ToByte();
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                    tcpClientForStudent.Close();
                    udpAudioReceiving.Close();
                }

                sin = new SignIn();
                sin.txtVCESName.Text = lblUser.Text;

                this.Dispose();
                sin.Show();
                sin.txtPassword.Focus();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Data data = new Data();
            data.cmd = Command.TextMessage;
            data.userName = lblUser.Text;
            data.chatText = txtChat.Text;
            byte[] outStream = data.ToByte();
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            txtChat.Text = "";
        }

        private void VideoWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

    }
}
