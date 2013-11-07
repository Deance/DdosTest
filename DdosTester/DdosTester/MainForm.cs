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
    public partial class MainForm : Form
    {
        //private Server objServer;
        private bool isAttack = false;
        private bool isRefreshBase = true;
        private static Thread thRefreshClientBase;
        public static ArrayList ClientBase { get; set; }
        public static long PacketCounter { get; set; }
        public MainForm()
        {
            InitializeComponent();
            ClientBase = new ArrayList();
            PacketCounter = 0;
            radbtn_TCPSYNFlood.Checked = true;

            int MaxThreadsCount = Environment.ProcessorCount * 8;
            // Max quantity of working threads
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            // Min quantity of working threads
            ThreadPool.SetMinThreads(2, 2);

            Server.Run();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(new delegate{RefreshDgvClientBase}));
            thRefreshClientBase = new Thread(RefreshDgvClientBase);
            thRefreshClientBase.IsBackground = true;
            thRefreshClientBase.Priority = ThreadPriority.Lowest;
            thRefreshClientBase.Start();

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if (!isAttack)
            {
                isAttack = true;
                btn_Start.Text = "Stop";
                grbox_Attacks.Enabled = false;
                
                // Make object of Settings to send it to client
                // Set the "objSets" object;
                MakeSets();

                Attack.Start();
            }

            else
            {
                isAttack = false;
                btn_Start.Text = "Start";
                grbox_Attacks.Enabled = true;

                Attack.Stop();
                GC.Collect();
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
                    for (int i = 0; i < ClientBase.Count; i++)
                    {
                        dgv_ClientBase[0, i].Value = ((Client)ClientBase[i]).IP.ToString();
                        dgv_ClientBase[1, i].Value = ((Client)ClientBase[i]).Status.ToString();
                        lbl_Counter.Text = Server.SentPacketsCounter.ToString();
                        Thread.Sleep(800);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Server.Stop();
            isRefreshBase = false;
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dgv_ClientBase.Rows.Clear();
            ClientBase.Clear();
        }

    }


}
