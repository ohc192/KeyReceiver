namespace KeyReceiver
{
    partial class frmKeyReceiver
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
            this.grpClientServer = new System.Windows.Forms.GroupBox();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.rdbtnServer = new System.Windows.Forms.RadioButton();
            this.rdbtnClient = new System.Windows.Forms.RadioButton();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.btnServerStop = new System.Windows.Forms.Button();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.grpClient = new System.Windows.Forms.GroupBox();
            this.btnClientDisconnect = new System.Windows.Forms.Button();
            this.btnClientConnect = new System.Windows.Forms.Button();
            this.txtClientAddress = new System.Windows.Forms.TextBox();
            this.btnAddButtons = new System.Windows.Forms.Button();
            this.lstButtons = new System.Windows.Forms.ListBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.grpClientServer.SuspendLayout();
            this.grpServer.SuspendLayout();
            this.grpClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpClientServer
            // 
            this.grpClientServer.Controls.Add(this.lblServerPort);
            this.grpClientServer.Controls.Add(this.txtPort);
            this.grpClientServer.Controls.Add(this.rdbtnServer);
            this.grpClientServer.Controls.Add(this.rdbtnClient);
            this.grpClientServer.Location = new System.Drawing.Point(13, 8);
            this.grpClientServer.Name = "grpClientServer";
            this.grpClientServer.Size = new System.Drawing.Size(229, 50);
            this.grpClientServer.TabIndex = 0;
            this.grpClientServer.TabStop = false;
            this.grpClientServer.Text = "Client server choice";
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.Location = new System.Drawing.Point(133, 25);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(29, 13);
            this.lblServerPort.TabIndex = 2;
            this.lblServerPort.Text = "Port:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(168, 22);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(55, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "6666";
            // 
            // rdbtnServer
            // 
            this.rdbtnServer.AutoSize = true;
            this.rdbtnServer.Location = new System.Drawing.Point(63, 23);
            this.rdbtnServer.Name = "rdbtnServer";
            this.rdbtnServer.Size = new System.Drawing.Size(56, 17);
            this.rdbtnServer.TabIndex = 1;
            this.rdbtnServer.Text = "Server";
            this.rdbtnServer.UseVisualStyleBackColor = true;
            this.rdbtnServer.CheckedChanged += new System.EventHandler(this.rdbtnServer_CheckedChanged);
            // 
            // rdbtnClient
            // 
            this.rdbtnClient.AutoSize = true;
            this.rdbtnClient.Checked = true;
            this.rdbtnClient.Location = new System.Drawing.Point(6, 23);
            this.rdbtnClient.Name = "rdbtnClient";
            this.rdbtnClient.Size = new System.Drawing.Size(51, 17);
            this.rdbtnClient.TabIndex = 0;
            this.rdbtnClient.TabStop = true;
            this.rdbtnClient.Text = "Client";
            this.rdbtnClient.UseVisualStyleBackColor = true;
            this.rdbtnClient.CheckedChanged += new System.EventHandler(this.rdbtnClient_CheckedChanged);
            // 
            // grpServer
            // 
            this.grpServer.Controls.Add(this.txtServerIP);
            this.grpServer.Controls.Add(this.btnServerStop);
            this.grpServer.Controls.Add(this.btnServerStart);
            this.grpServer.Enabled = false;
            this.grpServer.Location = new System.Drawing.Point(13, 64);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(229, 53);
            this.grpServer.TabIndex = 1;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "Server settings";
            // 
            // btnServerStop
            // 
            this.btnServerStop.Location = new System.Drawing.Point(63, 19);
            this.btnServerStop.Name = "btnServerStop";
            this.btnServerStop.Size = new System.Drawing.Size(56, 23);
            this.btnServerStop.TabIndex = 1;
            this.btnServerStop.Text = "Stop";
            this.btnServerStop.UseVisualStyleBackColor = true;
            this.btnServerStop.Click += new System.EventHandler(this.btnServerStop_Click);
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(6, 19);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(51, 23);
            this.btnServerStart.TabIndex = 0;
            this.btnServerStart.Text = "Start";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // grpClient
            // 
            this.grpClient.Controls.Add(this.btnClientDisconnect);
            this.grpClient.Controls.Add(this.btnClientConnect);
            this.grpClient.Controls.Add(this.txtClientAddress);
            this.grpClient.Controls.Add(this.btnAddButtons);
            this.grpClient.Controls.Add(this.lstButtons);
            this.grpClient.Location = new System.Drawing.Point(13, 123);
            this.grpClient.Name = "grpClient";
            this.grpClient.Size = new System.Drawing.Size(229, 166);
            this.grpClient.TabIndex = 2;
            this.grpClient.TabStop = false;
            this.grpClient.Text = "Client settings";
            // 
            // btnClientDisconnect
            // 
            this.btnClientDisconnect.Location = new System.Drawing.Point(152, 17);
            this.btnClientDisconnect.Name = "btnClientDisconnect";
            this.btnClientDisconnect.Size = new System.Drawing.Size(71, 23);
            this.btnClientDisconnect.TabIndex = 4;
            this.btnClientDisconnect.Text = "Disconnect";
            this.btnClientDisconnect.UseVisualStyleBackColor = true;
            this.btnClientDisconnect.Click += new System.EventHandler(this.btnClientDisconnect_Click);
            // 
            // btnClientConnect
            // 
            this.btnClientConnect.Location = new System.Drawing.Point(85, 17);
            this.btnClientConnect.Name = "btnClientConnect";
            this.btnClientConnect.Size = new System.Drawing.Size(61, 23);
            this.btnClientConnect.TabIndex = 3;
            this.btnClientConnect.Text = "Connect";
            this.btnClientConnect.UseVisualStyleBackColor = true;
            this.btnClientConnect.Click += new System.EventHandler(this.btnClientConnect_Click);
            // 
            // txtClientAddress
            // 
            this.txtClientAddress.Location = new System.Drawing.Point(6, 19);
            this.txtClientAddress.Name = "txtClientAddress";
            this.txtClientAddress.Size = new System.Drawing.Size(73, 20);
            this.txtClientAddress.TabIndex = 2;
            this.txtClientAddress.Text = "127.0.0.1";
            // 
            // btnAddButtons
            // 
            this.btnAddButtons.Location = new System.Drawing.Point(6, 52);
            this.btnAddButtons.Name = "btnAddButtons";
            this.btnAddButtons.Size = new System.Drawing.Size(39, 108);
            this.btnAddButtons.TabIndex = 1;
            this.btnAddButtons.Text = "Add";
            this.btnAddButtons.UseVisualStyleBackColor = true;
            this.btnAddButtons.Click += new System.EventHandler(this.btnAddButtons_Click);
            // 
            // lstButtons
            // 
            this.lstButtons.FormattingEnabled = true;
            this.lstButtons.Location = new System.Drawing.Point(51, 52);
            this.lstButtons.Name = "lstButtons";
            this.lstButtons.Size = new System.Drawing.Size(172, 108);
            this.lstButtons.TabIndex = 0;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(125, 21);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.ReadOnly = true;
            this.txtServerIP.Size = new System.Drawing.Size(98, 20);
            this.txtServerIP.TabIndex = 4;
            this.txtServerIP.Text = "Waiting...";
            // 
            // frmKeyReceiver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 296);
            this.Controls.Add(this.grpClient);
            this.Controls.Add(this.grpServer);
            this.Controls.Add(this.grpClientServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmKeyReceiver";
            this.Text = "KeyReceiver";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKeyReceiver_FormClosed);
            this.grpClientServer.ResumeLayout(false);
            this.grpClientServer.PerformLayout();
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            this.grpClient.ResumeLayout(false);
            this.grpClient.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpClientServer;
        private System.Windows.Forms.RadioButton rdbtnServer;
        private System.Windows.Forms.RadioButton rdbtnClient;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.Button btnServerStop;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.GroupBox grpClient;
        private System.Windows.Forms.Button btnAddButtons;
        private System.Windows.Forms.ListBox lstButtons;
        private System.Windows.Forms.Button btnClientConnect;
        private System.Windows.Forms.TextBox txtClientAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.Button btnClientDisconnect;
        private System.Windows.Forms.TextBox txtServerIP;
    }
}

