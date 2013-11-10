using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientAttackerNamespace;

namespace ClientApplication
{
    public partial class Form1 : Form
    {
        private Thread Connection;
        private Thread ReceiveSettingsThread;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connection = new Thread(ClientAttacker.Connect);
                Connection.Start(textBox1.Text);
                textBox2.Text = "Ожидание файла настроек от сервера";
                ReceiveSettingsThread = new Thread(UpdateTextBoxes);
                ReceiveSettingsThread.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox2.Text = "Ожидание подключения к серверу";
            }
        }
        private void UpdateTextBoxes()
        {
            while (!ClientAttacker.isConnected)
            {
                if (ClientAttacker.isConnected)
                {
                    textBox3.Text = ClientAttacker._attackSettings.IPs.ToString();
                    textBox4.Text = ClientAttacker._attackSettings.Type.ToString();
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClientAttacker.StopAttack();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClientAttacker.StartAttack();
        }
    }
}
