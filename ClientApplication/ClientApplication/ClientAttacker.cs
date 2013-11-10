using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
//using DdosTester.HelpClasses;

namespace ClientAttackerNamespace
{
    class ClientAttacker
    {
        private static TcpClient _tcpClient;
        public static int Port = 1111;
        public static int PortSettings = 1112;
        public static int SentPacketsCounter = 0;
        public static bool isConnected = false;
        private static Settings attackSettings;
        public static Settings _attackSettings
        {
            get
            {
                return attackSettings;
            }
            set
            {
                attackSettings = value;
            }
        }

        public static void Connect(Object IP)
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse((string)IP), Port);
                _tcpClient = new TcpClient();
                _tcpClient.Connect(ipEndPoint);
                isConnected = true;
                ReсeiveSettings();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }
        }

        public static void Disconnect()
        {
            _tcpClient.Close();
            isConnected = false;
        }

        public static void ReсeiveSettings()
        {
            TcpListener settingsListner = new TcpListener(((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Address, PortSettings);

            settingsListner.Start();

            TcpClient settingsAccept = settingsListner.AcceptTcpClient();
            settingsAccept.ReceiveTimeout = 500;
            settingsAccept.GetStream().ReadTimeout = 500;
            byte[] settingsBytes = new byte[4];
            settingsAccept.GetStream().Read(settingsBytes, 0, 4);
            settingsBytes = new byte[MySerialization.BytesToInt32(settingsBytes)];
            settingsAccept.GetStream().Read(settingsBytes, 0, settingsBytes.Length);
            attackSettings = MySerialization.ByteToSettings(settingsBytes);
        }

        public static void StartAttack()
        {

        }

        public static void StopAttack()
        {
            isConnected = false;
            try
            {
                _tcpClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GC.Collect();
        }



    }
}
