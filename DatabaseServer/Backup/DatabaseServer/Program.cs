using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Data.OleDb;

namespace DatabaseServer
{
    class Program
    {
        Socket teacherSocket;
        EndPoint teacherEP;
        NetworkStream ns;

        OleDbConnection MAconn;
        OleDbCommand MAcmd;
        public static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=VCES.mdb";

        static void Main(string[] args)
        {
            Program pgm = new Program();

            TcpListener myTcpListner = new TcpListener(IPAddress.Any, 1350);
            myTcpListner.Start();
            Console.WriteLine("Database Server started.....");
            Console.Title = "VCES - Database Server";

            TcpClient myTcpClient = default(TcpClient);

            while(true)
            {
                myTcpClient = myTcpListner.AcceptTcpClient();                

                byte[] inBuffer = new byte[myTcpClient.ReceiveBufferSize];
                
                pgm.ns = myTcpClient.GetStream();
                pgm.ns.Read(inBuffer, 0, inBuffer.Length);

                Data msgReceived = new Data(inBuffer);

                switch (msgReceived.cmd)
                { 
                    case Command.SignInStudent:
                        pgm.Search(msgReceived.senderName, msgReceived.pwd, "Students");
                        break;
                    case Command.SignUpStudent:
                        pgm.Insert(msgReceived.receivedData, "Students");
                        break;
                    case Command.SignInTeacher:
                        pgm.Search(msgReceived.senderName, msgReceived.pwd, "Teachers");
                        //pgm.teacherEP = new IPEndPoint(IPAddress.Any, 0);
                        //pgm.teacherSocket = myTcpClient.Client;
                        break;
                    case Command.SignUpTeacher:
                        pgm.Insert(msgReceived.receivedData, "Teachers");
                        break;
                }
            }
        }

        private void Search(string userName, string pwd, string tableName)
        {
            try
            {
                MAconn = new OleDbConnection();
                MAconn.ConnectionString = connectionString;
                MAconn.Open();
                MAcmd = MAconn.CreateCommand();
                MAcmd.CommandText = "SELECT * FROM " + tableName + " where VCESName = '" + userName + "' and Password = '" + pwd + "'";
                MAcmd.ExecuteNonQuery();
                OleDbDataReader dr = MAcmd.ExecuteReader();

                Data data = new Data();

                if (dr.Read())
                {
                    data.cmd = Command.Varified;

                    /*if (tableName.Equals("Teachers"))
                    {
                        teacherEP = teacherSocket.RemoteEndPoint;
                        data.teacherEP = teacherEP.ToString();
                    }
                    */
                }
                else
                    data.cmd = Command.NotVarified;
                ns.Write(data.ToByte(), 0, data.ToByte().Length);
                ns.Flush();
                MAconn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseServer:Program:Search()\n" + ex.Message);
            }
        }

        private void Insert(string[] record, string tableName)
        {
            try
            {
                MAconn = new OleDbConnection();
                MAconn.ConnectionString = connectionString;
                MAconn.Open();
                MAcmd = MAconn.CreateCommand();
                MAcmd.CommandText = "INSERT INTO " + tableName + " VALUES('" + record[0] + "','" + record[1] + "','" + record[2] + "','" + record[3] + "','" + record[4] + "','" + record[5] + "','" + record[6] + "')";

                Data data = new Data();
                if (MAcmd.ExecuteNonQuery() == 1)
                {
                    data.cmd = Command.SignUpTrue;
                }
                else
                    data.cmd = Command.SignUpFalse;
                ns.Write(data.ToByte(), 0, data.ToByte().Length);
                ns.Flush();
                MAconn.Close();                
            }
            catch (Exception ex)
            {
                Console.WriteLine("DatabaseServer:Program:Insert()\n" + ex.Message);
            }
        }

    }
}
