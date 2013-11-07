namespace DdosTester
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                objServer.isRun = false;
                isRefreshBase = false;
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radbtn_DatabaseAttack = new System.Windows.Forms.RadioButton();
            this.radbtn_TCPFlood = new System.Windows.Forms.RadioButton();
            this.radbtn_ICMPFlood = new System.Windows.Forms.RadioButton();
            this.radbtn_TCPSYNFlood = new System.Windows.Forms.RadioButton();
            this.lbl_IPorURL = new System.Windows.Forms.Label();
            this.tb_Address = new System.Windows.Forms.TextBox();
            this.grbox_Attacks = new System.Windows.Forms.GroupBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.dgv_ClientBase = new System.Windows.Forms.DataGridView();
            this.Client_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Client_Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grbox_Attacks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ClientBase)).BeginInit();
            this.SuspendLayout();
            // 
            // radbtn_DatabaseAttack
            // 
            this.radbtn_DatabaseAttack.AutoSize = true;
            this.radbtn_DatabaseAttack.Location = new System.Drawing.Point(39, 151);
            this.radbtn_DatabaseAttack.Name = "radbtn_DatabaseAttack";
            this.radbtn_DatabaseAttack.Size = new System.Drawing.Size(133, 21);
            this.radbtn_DatabaseAttack.TabIndex = 5;
            this.radbtn_DatabaseAttack.TabStop = true;
            this.radbtn_DatabaseAttack.Text = "Database Attack";
            this.radbtn_DatabaseAttack.UseVisualStyleBackColor = true;
            // 
            // radbtn_TCPFlood
            // 
            this.radbtn_TCPFlood.AutoSize = true;
            this.radbtn_TCPFlood.Location = new System.Drawing.Point(39, 113);
            this.radbtn_TCPFlood.Name = "radbtn_TCPFlood";
            this.radbtn_TCPFlood.Size = new System.Drawing.Size(95, 21);
            this.radbtn_TCPFlood.TabIndex = 6;
            this.radbtn_TCPFlood.TabStop = true;
            this.radbtn_TCPFlood.Text = "TCP Flood";
            this.radbtn_TCPFlood.UseVisualStyleBackColor = true;
            // 
            // radbtn_ICMPFlood
            // 
            this.radbtn_ICMPFlood.AutoSize = true;
            this.radbtn_ICMPFlood.Location = new System.Drawing.Point(39, 77);
            this.radbtn_ICMPFlood.Name = "radbtn_ICMPFlood";
            this.radbtn_ICMPFlood.Size = new System.Drawing.Size(100, 21);
            this.radbtn_ICMPFlood.TabIndex = 7;
            this.radbtn_ICMPFlood.TabStop = true;
            this.radbtn_ICMPFlood.Text = "ICMP Flood";
            this.radbtn_ICMPFlood.UseVisualStyleBackColor = true;
            // 
            // radbtn_TCPSYNFlood
            // 
            this.radbtn_TCPSYNFlood.AutoSize = true;
            this.radbtn_TCPSYNFlood.Location = new System.Drawing.Point(39, 40);
            this.radbtn_TCPSYNFlood.Name = "radbtn_TCPSYNFlood";
            this.radbtn_TCPSYNFlood.Size = new System.Drawing.Size(127, 21);
            this.radbtn_TCPSYNFlood.TabIndex = 8;
            this.radbtn_TCPSYNFlood.TabStop = true;
            this.radbtn_TCPSYNFlood.Text = "TCP SYN Flood";
            this.radbtn_TCPSYNFlood.UseVisualStyleBackColor = true;
            // 
            // lbl_IPorURL
            // 
            this.lbl_IPorURL.AutoSize = true;
            this.lbl_IPorURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_IPorURL.Location = new System.Drawing.Point(147, 33);
            this.lbl_IPorURL.Name = "lbl_IPorURL";
            this.lbl_IPorURL.Size = new System.Drawing.Size(93, 20);
            this.lbl_IPorURL.TabIndex = 4;
            this.lbl_IPorURL.Text = "IP or URL: ";
            // 
            // tb_Address
            // 
            this.tb_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_Address.Location = new System.Drawing.Point(252, 30);
            this.tb_Address.Name = "tb_Address";
            this.tb_Address.Size = new System.Drawing.Size(300, 26);
            this.tb_Address.TabIndex = 3;
            // 
            // grbox_Attacks
            // 
            this.grbox_Attacks.Controls.Add(this.radbtn_TCPSYNFlood);
            this.grbox_Attacks.Controls.Add(this.radbtn_DatabaseAttack);
            this.grbox_Attacks.Controls.Add(this.radbtn_ICMPFlood);
            this.grbox_Attacks.Controls.Add(this.radbtn_TCPFlood);
            this.grbox_Attacks.Location = new System.Drawing.Point(12, 164);
            this.grbox_Attacks.Name = "grbox_Attacks";
            this.grbox_Attacks.Size = new System.Drawing.Size(253, 194);
            this.grbox_Attacks.TabIndex = 9;
            this.grbox_Attacks.TabStop = false;
            this.grbox_Attacks.Text = "Choose the kind of attack";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(579, 30);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(82, 26);
            this.btn_Start.TabIndex = 10;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // dgv_ClientBase
            // 
            this.dgv_ClientBase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ClientBase.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Client_IP,
            this.Client_Status});
            this.dgv_ClientBase.Location = new System.Drawing.Point(412, 164);
            this.dgv_ClientBase.Name = "dgv_ClientBase";
            this.dgv_ClientBase.RowTemplate.Height = 24;
            this.dgv_ClientBase.Size = new System.Drawing.Size(443, 194);
            this.dgv_ClientBase.TabIndex = 11;
            this.dgv_ClientBase.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GetClientBase);
            // 
            // Client_IP
            // 
            this.Client_IP.HeaderText = "Client IP";
            this.Client_IP.Name = "Client_IP";
            this.Client_IP.ReadOnly = true;
            this.Client_IP.Width = 300;
            // 
            // Client_Status
            // 
            this.Client_Status.HeaderText = "Status";
            this.Client_Status.Name = "Client_Status";
            this.Client_Status.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 375);
            this.Controls.Add(this.dgv_ClientBase);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.grbox_Attacks);
            this.Controls.Add(this.lbl_IPorURL);
            this.Controls.Add(this.tb_Address);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(MainForm_FormClosing);
            this.grbox_Attacks.ResumeLayout(false);
            this.grbox_Attacks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ClientBase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            
        }

        

        #endregion

        private System.Windows.Forms.RadioButton radbtn_DatabaseAttack;
        private System.Windows.Forms.RadioButton radbtn_TCPFlood;
        private System.Windows.Forms.RadioButton radbtn_ICMPFlood;
        private System.Windows.Forms.RadioButton radbtn_TCPSYNFlood;
        private System.Windows.Forms.Label lbl_IPorURL;
        private System.Windows.Forms.TextBox tb_Address;
        private System.Windows.Forms.GroupBox grbox_Attacks;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.DataGridView dgv_ClientBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Client_Status;

    }
}