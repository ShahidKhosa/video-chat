using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentSide
{
    class Data
    {
        public Command cmd;
        public string userName;
        public string pwd;
        public string receiverName;
        public string record;
        public string chatText;
        public int chatTextLength;
        public string senderName;
        public int senderNameLength;

        public Data()
        {
            this.cmd = Command.Null;
            this.userName = null;
            this.pwd = null;
            this.record = null;
            this.chatText = null;
            this.senderName = null;
        }

        public Data(byte[] data)
        {
            cmd = Command.Null;
            senderName = "";
            senderNameLength = 0;
            pwd = "";
            chatText = "";

            cmd = (Command)BitConverter.ToInt32(data, 0);

            switch (cmd)
            {
                case Command.Join:
                case Command.Leave:
                case Command.JoinRequestAccepted:
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

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes((int)cmd));
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
    }

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
