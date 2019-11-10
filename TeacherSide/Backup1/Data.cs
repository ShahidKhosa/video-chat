using System;
using System.Collections.Generic;
using System.Text;

namespace Video_Conference
{

    //The data structure by which the server and the client interact with 
    //each other.
    class Data
    {
        public Command cmd;
        public string senderName;
        public string receiverName;
        public string userName;
        public string pwd;
        public int senderNameLength;
        public int receiverNameLength;
        public string signUpRecord;
        public int signUpRecordLength;
        public string[] receivedData;
        public string videoPort;
        public int videoPortLength;
        public string record;
        public string chatText;
        public int chatTextLength;

        public Data()
        {
            this.cmd = Command.Null;
            this.senderName = null;
            this.userName = null;
            this.pwd = null;
            this.record = null;
            this.chatText = null;
        }

        public Data(byte[] data)
        {
            cmd = Command.Null;
            senderName = "";
            receiverName = "";
            pwd = "";
            senderNameLength = 0;
            chatText = null;
            chatTextLength = 0;

            cmd = (Command)BitConverter.ToInt32(data, 0);

            switch (cmd)
            {
                case Command.Join:
                case Command.Leave:
                case Command.QuestionRequest:
                case Command.AskingQuestionStart:
                    senderNameLength = BitConverter.ToInt32(data, 4);
                    senderName = Encoding.UTF8.GetString(data, 8, senderNameLength);
                    break;
                case Command.TextMessage:
                    senderNameLength = BitConverter.ToInt32(data, 4);
                    senderName = Encoding.UTF8.GetString(data, 8, senderNameLength);
                    chatTextLength = BitConverter.ToInt32(data, (8 + senderNameLength));
                    chatText = Encoding.UTF8.GetString(data, (12 + senderNameLength), chatTextLength);
                    break;
            }
        }

        public byte[] ToClient()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes((int)cmd));
            return result.ToArray();
        }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes((int)cmd));
            if (senderName != null)
            {
                result.AddRange(BitConverter.GetBytes(senderName.Length));
                result.AddRange(Encoding.UTF8.GetBytes(senderName));
            }
            if (userName != null)
            {
                result.AddRange(BitConverter.GetBytes(userName.Length));
                result.AddRange(Encoding.UTF8.GetBytes(userName));
            }

            if (pwd != null)
            {
                result.AddRange(BitConverter.GetBytes(pwd.Length));
                result.AddRange(Encoding.UTF8.GetBytes(pwd));
            }

            if (record != null)
            {
                result.AddRange(BitConverter.GetBytes(record.Length));
                result.AddRange(Encoding.UTF8.GetBytes(record));
            }

            if (chatText != null)
            {
                result.AddRange(BitConverter.GetBytes(chatText.Length));
                result.AddRange(Encoding.UTF8.GetBytes(chatText));
            }
            return result.ToArray();
        }

        public void createAccount(string signUpRecord)
        {
            receivedData = signUpRecord.Split('|');
            string userId = receivedData[0];
            string userName = receivedData[1];
            string pwd = receivedData[2];
            string rePwd = receivedData[3];
            string email = receivedData[4];
            string userType = receivedData[5];
            string gender = receivedData[6];
            string clas = receivedData[7];
            string course = receivedData[8];
            string date = receivedData[9];

        }

        public void signIn(byte[] data)
        {
            senderNameLength = BitConverter.ToInt32(data, 4);
            senderName = Encoding.UTF8.GetString(data, 8, senderNameLength);
            int pwdLength = BitConverter.ToInt32(data, (8 + senderNameLength));
            pwd = Encoding.UTF8.GetString(data, (12 + senderNameLength), pwdLength);
        }

        public void getSenderName(byte[] data)
        {
            senderNameLength = BitConverter.ToInt32(data, 4);
            senderName = Encoding.UTF8.GetString(data, 8, senderNameLength);
        }

        public void getReceiverName(byte[] data)
        {
            receiverNameLength = BitConverter.ToInt32(data, 4);
            receiverName = Encoding.UTF8.GetString(data, 8, receiverNameLength);
        }

        public void senderReceiverName(byte[] data)
        {
            senderNameLength = BitConverter.ToInt32(data, 4);
            senderName = Encoding.UTF8.GetString(data, 8, senderNameLength);
            receiverNameLength = BitConverter.ToInt32(data, (8 + senderNameLength));
            receiverName = Encoding.UTF8.GetString(data, (12 + senderNameLength), receiverNameLength);
            
        }
    }


    //The commands for interaction between the two parties.
    enum Command
    {
        SignInStudent,
        SignInTeacher,
        SignOutClient,
        SignOutHead,
        SignUpStudent,
        SignUpTeacher,
        SignUpTrue,
        SignUpFalse,
        Varified,
        NotVarified,
        LectureStarted,
        LectureNotStarted,
        LectureEnded,
        Join,
        QuestionRequest,
        Leave,
        Disconnect,
        JoinRequestAccepted,
        JoinRequestRejected,
        QuestionRequstAccepted,
        QuestionRequstRejected,
        AskingQuestionStart,
        AskingQuestionStop,
        AskingQuestionCancel,
        ClientJoined,
        ClientLeaved,
        DisJoin,
        TextMessage,
        Null
    }
}
