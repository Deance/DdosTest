using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using DdosTester;
using DdosTester.HelpClasses;

namespace DdosTester.WorkClasses
{
    abstract class Attack
    {
        private static Settings _objSets;
        private static int _packets;
        public static int SendPacketsCounter
        {
            get
            {
                return _packets;
            }
            set
            {
                _packets = value;
            }
        }
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
                TcpClient SendSetsClient = new TcpClient();

                if ((MainForm.ClientBase.Count != 0) && (_objSets.IPs.ToString() != ""))
                {
                    foreach (Client client in MainForm.ClientBase)
                    {
                        SendSetsClient.Connect(client.IP, 1112);
                        byte[] SetsBytes = MySerialization.SettingsToBytes(_objSets);
                        SendSetsClient.GetStream().Write(SetsBytes, 0, SetsBytes.Length);
                        
                    }
                }
                else
                {
                    throw new Exception("There are no addresses in Client Base or wrong sets");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
