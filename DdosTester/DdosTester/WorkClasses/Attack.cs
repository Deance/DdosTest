using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using DdosTester;
using DdosTester.HelpClasses;

namespace DdosTester.WorkClasses
{
    abstract class Attack
    {
        private static Settings _objSets;

        private static TcpClient sendSetsClient;
        public static Settings objSets
        {
            get
            {
                return _objSets;
            }
            set
            {
                _objSets = value;
            }
        }
        public static void Start()
        {
            try
            {
                sendSetsClient = new TcpClient();

                if ((MainForm.ClientBase.Count != 0) && (_objSets.IPs.ToString() != ""))
                {
                    byte[] SetsBytes = MySerialization.SettingsToBytes(_objSets);
                    sendSetsClient.ReceiveTimeout = 500;
                    sendSetsClient.GetStream().ReadTimeout = 500;
                    sendSetsClient.GetStream().Write(SetsBytes, 0, SetsBytes.Length);
                        
                    foreach (Client client in MainForm.ClientBase)
                    {                        
                        sendSetsClient.Connect(client.IP, 1112);
                        Thread.Sleep(200);
                        if (sendSetsClient.Available == 0)
                        {
                            client.Status = ClientStatus.Online;
                        }
                        else
                        {
                            client.Status = ClientStatus.Offline;
                        };
                    }
                }
                else
                {
                    throw new Exception("There are no clients in Client Base or wrong sets");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sendSetsClient.Close();
            }
        }

        public static void Stop()
        {


        }
    }
}
