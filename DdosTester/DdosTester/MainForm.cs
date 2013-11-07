using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using DdosTester.WorkClasses;
using DdosTester.HelpClasses;

namespace DdosTester
{
    public enum AttackType
    {
        TCP_SYN_Flood = 1,
        ICMP_Flood = 2,
        TCP_Flood = 3,
        Database_Attack = 4
    };
    
    
    public partial class MainForm : Form
    {
        private AttackType Type;
        
        private Server objServer;
        private bool aContinue = false;
        private bool isRefreshBase = true;
        private static Thread thRefreshClientBase;
                
        public static ArrayList ClientBase { get; set; }
        public static long PacketCounter { get; set; }
        public MainForm()
        {
            InitializeComponent();
            ClientBase = new ArrayList();
            PacketCounter = 0;

            int MaxThreadsCount = Environment.ProcessorCount * 8;
            // Max quantity of working threads
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            // Min quantity of working threads
            ThreadPool.SetMinThreads(2, 2);

            objServer = new Server(1111);
            objServer.Run();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(new delegate{RefreshDgvClientBase}));
            thRefreshClientBase = new Thread(RefreshDgvClientBase);
            thRefreshClientBase.IsBackground = true;
            thRefreshClientBase.Priority = ThreadPriority.Lowest;
            thRefreshClientBase.Start();


        }

        private void dgv_NetIPBase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!aContinue)
            {
                aContinue = true;
                btn_Start.Text = "Stop";
                grbox_Attacks.Enabled = false;
                dgv_ClientBase.Enabled = false;

                // Make object of Settings to send it to client
                // Set the "objSets" object;
                MakeSets();
                

            }

            else
            {
                aContinue = false;
                btn_Start.Text = "Start";
                grbox_Attacks.Enabled = true;
                dgv_ClientBase.Enabled = true;
                GC.Collect();
            }


        }


        private void GetClientBase(object sender,
            DataGridViewCellEventArgs e)
        {
            ClientBase = new ArrayList();
            for (int i = 0; i < dgv_ClientBase.RowCount - 1; i++)
            {
                if (Check.isIP(dgv_ClientBase[0, i].Value.ToString()))
                {
                    Client objToBase = new Client(dgv_ClientBase[0, i].Value.ToString());
                    bool isAlreadyIn = false;
                    foreach (Client client in ClientBase)
                    {
                        if (client.IP.ToString() == objToBase.IP.ToString())
                        {
                            isAlreadyIn = true;
                        }
                    }

                    if (!isAlreadyIn)
                    {
                        ClientBase.Add(objToBase);
                        dgv_ClientBase[1, i].Value = "Offline";
                        dgv_ClientBase[1, i].Style.ForeColor = Color.Gray;
                    }
                    else
                    {
                        dgv_ClientBase[1, i].Value = "Already in Client Base";
                        dgv_ClientBase[1, i].Style.ForeColor = Color.DarkRed;
                    }
                }
                else
                {
                    dgv_ClientBase[1, i].Value = "Wrong IP";
                    dgv_ClientBase[1, i].Style.ForeColor = Color.DarkRed;
                }
            }
        }

        /*
        private void GetClientBase(object sender,
            DataGridViewCellEventArgs e)
        {
            ClientBase = new ArrayList();
            for (int i = 0; i < dgv_ClientBase.RowCount - 1; i++)
            {
                if (Check.isIP(dgv_ClientBase[0, i].Value.ToString()))
                {
                    Client objToBase = new Client(dgv_ClientBase[0, i].Value.ToString());
                    bool isAlreadyIn = false;
                    foreach (Client client in ClientBase)
                    {
                        if (client.IP.ToString() == objToBase.IP.ToString())
                        {
                            isAlreadyIn = true;
                        }
                    }

                    if (!isAlreadyIn)
                    {
                        ClientBase.Add(objToBase);
                        dgv_ClientBase[1, i].Value = "Offline";
                        dgv_ClientBase[1, i].Style.ForeColor = Color.Gray;
                    }
                    else
                    {
                        dgv_ClientBase[1, i].Value = "Already in Client Base";
                        dgv_ClientBase[1, i].Style.ForeColor = Color.DarkRed;
                    }
                }
                else
                {
                    dgv_ClientBase[1, i].Value = "Wrong IP";
                    dgv_ClientBase[1, i].Style.ForeColor = Color.DarkRed;
                }
            }
        }
        */
        private void MakeSets()
        {
            Attack.objSets = new Settings();

            //Check IP or ORL or nothing
            //Put IP Addresses into Setting object
                
            if (Check.isIP(tb_Address.Text))
            {
                Attack.objSets.IPs = new IPAddress[1];
                Attack.objSets.IPs[0] = IPAddress.Parse(tb_Address.Text);
            }

            else
            {
                if (Check.isURL(tb_Address.Text))
                {
                    try
                    {
                        Attack.objSets.IPs = Dns.GetHostAddresses(tb_Address.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ddos Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Wrong IP or URL", "Ddos Tester", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //Get a type of attack
            //Put it into Settings object

            if (radbtn_TCPSYNFlood.Checked)
            {
                Attack.objSets.Type = AttackType.TCP_SYN_Flood;
            }
            if (radbtn_ICMPFlood.Checked)
            {
                Attack.objSets.Type = AttackType.ICMP_Flood;
            }
            if (radbtn_TCPFlood.Checked)
            {
                Attack.objSets.Type = AttackType.TCP_Flood;
            }
            if (radbtn_DatabaseAttack.Checked)
            {
                Attack.objSets.Type = AttackType.Database_Attack;
            }


        }

        private void RefreshDgvClientBase()
        // Refreshes the DataGridView every 0.8 second
        {
            while (isRefreshBase)
            {
                try
                {
                    int i = 0;
                    if (ClientBase.Count != 0)
                    {
                        for (i = 0; i < ClientBase.Count; i++ )
                        {
                            dgv_ClientBase[0, i].Value = ((Client)ClientBase[i]).IP.ToString();
                            dgv_ClientBase[1, i].Value = ((Client)ClientBase[i]).Status.ToString();
                        }
                        Thread.Sleep(800);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            objServer.isRun = false;
            isRefreshBase = false;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

    }


}
