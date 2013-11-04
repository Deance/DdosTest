using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Threading;

namespace DdosTester
{
    class Server
    {
        private int _port;
        private TcpListener _tcpListener;
        private Thread _newThread;
        public int Port { get; set; }
        public bool isRun { get; set; }
        public Server(int port)
        {
            Port = port;
            isRun = false;
        }
        public void Run()
        {
            
            _newThread = new Thread(this.Listen);
            _newThread.Start();
        }

        private void Listen()
        {
            try
            {

                _tcpListener = new TcpListener(IPAddress.Any, this.Port);
                _tcpListener.Start();
                isRun = true;

                while (isRun)
                {
                    if (_tcpListener.AcceptTcpClient() != null)
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(HandleNewClient), _tcpListener.AcceptTcpClient());
                        /*
                        Thread t = new Thread(new ParameterizedThreadStart(HandleNewClient));
                        t.IsBackground = true;
                        t.Start(_tcpListener.AcceptTcpClient());
                        */
                        if (_tcpListener.AcceptTcpClient() != null)
                        {
                            _tcpListener.AcceptTcpClient().Close();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleNewClient(object client)
        {
            TcpClient connectedClient = client as TcpClient;
            IPAddress ipAddr = ((IPEndPoint)(connectedClient.Client.RemoteEndPoint)).Address;
            Client newClient = new Client(ipAddr.ToString());
            bool isContained = false;

            foreach (Client cl in MainForm.ClientBase)
            {
                if (cl == newClient)
                {
                    isContained = true;
                }
            }

            if (!isContained)
            {
                MainForm.ClientBase.Add(newClient);

            }
            else
            {
                if (connectedClient.Available != 0)
                {
                    NetworkStream stream = connectedClient.GetStream();
                    byte[] buf = new byte[stream.Length];
                    stream.Read(buf, 0, buf.Length);
                }
            }
            connectedClient.Close();
            
        }
        



         // Server stopping
         ~Server()
         {
             if (_tcpListener != null)
             {
                 
                 _tcpListener.Stop();
             }
         }
    }
}
