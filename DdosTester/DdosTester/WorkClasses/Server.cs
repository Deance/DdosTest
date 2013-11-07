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
using DdosTester.HelpClasses;
using DdosTester.WorkClasses;

namespace DdosTester
{
    class Server
    {
        private TcpListener _tcpListener;
        private Thread _newThread;
        public static int SentPacketsCounter = 0;
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
        public void Stop()
        {
            isRun = false;
        }
        private void Listen()
        {
            try
            {
                // Make the listening socket
                _tcpListener = new TcpListener(IPAddress.Any, this.Port);
                _tcpListener.Start();
                isRun = true;
                
                //Handle the listening socket
                while (isRun)
                {
                    //Put an AcceptClient into "HandleNewClient" func and stand in Thread Pool queue 
                    ThreadPool.QueueUserWorkItem(new WaitCallback(HandleNewClient), _tcpListener.AcceptTcpClient());
                    
                }             

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void HandleNewClient(object client)
        {
            try
            {
                // Get from AcceptClient the TcpClient object
                TcpClient connectedClient = client as TcpClient;
                // Get IP Adress of connected client
                IPAddress ipAddr = ((IPEndPoint)(connectedClient.Client.RemoteEndPoint)).Address;
                // Make the new Client to try put or find it in Client Base
                Client newClient = new Client(ipAddr.ToString());
                bool isContained = false;

                //Check Client Base for containing connected client
                foreach (Client cl in MainForm.ClientBase)
                    if (cl == newClient)
                        isContained = true;

                if (!isContained) // Add new client to client base
                {
                    if (connectedClient.Available == 0)  
                        MainForm.ClientBase.Add(newClient);
                }
                else
                {
                    if (connectedClient.Available != 0) //take a number of sent packets by current connected client
                    {
                        try
                        {
                            NetworkStream stream = connectedClient.GetStream();
                            byte[] buf = new byte[stream.Length];
                            stream.Read(buf, 0, buf.Length);
                            stream.Close();
                            SentPacketsCounter += MySerialization.BytesToInt32(buf);
                        }
                        catch
                        {
                            throw new Exception("Not valid number");
                        }
                    }
                }

                connectedClient.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        



         // Server stopping
         ~Server()
         {
             if (_tcpListener != null)
             {
                 isRun = false;       
                 _tcpListener.Stop();
             }
         }
    }
}
