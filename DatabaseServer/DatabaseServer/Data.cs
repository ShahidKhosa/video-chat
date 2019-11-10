using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseServer
{
    //The data structure by which the server and the client interact with 
    //each other.
    class Data
    {
        public Command cmd;
        public string teacherEP = "";
        public string senderName;
        public string receiverName;
        public string pwd;
        public int senderNameLength;
        public int receiverNameLength;
        public string signUpRecord;
        public int signUpRecordLength;
        public string[] receivedData;
        public string videoPort;
        public int videoPortLength;

        public Data()
        {
            this.cmd = Command.Null;
            this.senderName = null;
            this.pwd = null;            
        }

        public Data(byte[] data)
        {
            cmd = Command.Null;
            senderName = "";
            receiverName = "";
            pwd = "";
            senderNameLength = 0;
            receiverNameLength = 0;

            cmd = (Command)BitConverter.ToInt32(data, 0);

            switch (cmd)
            {

                case Command.SignInStudent:
                    signIn(data);
                    break;
                case Command.SignInTeacher:
                    signIn(data);
                    break;
                case Command.SignOutClient:
                    getSenderName(data);
                    break;
                case Command.SignOutHead:
                    getSenderName(data);
                    break;
                case Command.SignUpStudent:
                case Command.SignUpTeacher:
                    signUpRecordLength = BitConverter.ToInt32(data, 4);
                    signUpRecord = Encoding.UTF8.GetString(data, 8, signUpRecordLength);
                    createAccount(signUpRecord);
                    break;
                case Command.Join:
                    senderReceiverName(data);
                    break;
                case Command.JoinRequestAccepted:
                    getReceiverName(data);
                    break;
                case Command.JoinRequestRejected:
                    getReceiverName(data);
                    break;
                case Command.Leave:
                    senderReceiverName(data);
                    break;
                case Command.QuestionRequest:
                    senderReceiverName(data);
                    break;
                case Command.QuestionRequstAccepted:
                    getReceiverName(data);
                    break;
                case Command.QuestionRequstRejected:
                    getReceiverName(data);
                    break;
                case Command.DisJoin:
                    getReceiverName(data);
                    break;
                case Command.TextMessage:
                    getSenderName(data);
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
            if (teacherEP != null)
            {
                result.AddRange(BitConverter.GetBytes(teacherEP.Length));
                result.AddRange(Encoding.UTF8.GetBytes(teacherEP));
            }
            return result.ToArray();
        }

        public void createAccount(string signUpRecord)
        {
            receivedData = signUpRecord.Split('|');
            

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
