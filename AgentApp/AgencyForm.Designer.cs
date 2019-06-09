namespace AgentApp
{
    partial class AgencyForm
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.console = new System.Windows.Forms.TextBox();
            this.info = new System.Windows.Forms.TextBox();
            this.createButton = new System.Windows.Forms.Button();
            this.agentsList = new System.Windows.Forms.TextBox();
            this.labelAgents = new System.Windows.Forms.Label();
            this.dispatchButton = new System.Windows.Forms.Button();
            this.disposeButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cloneButton = new System.Windows.Forms.Button();
            this.listAgents = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxN = new System.Windows.Forms.ComboBox();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.comboBoxIPAddresses = new System.Windows.Forms.ComboBox();
            this.dispatchButtonNetwork = new System.Windows.Forms.Button();
            this.labelAddress = new System.Windows.Forms.Label();
            this.comboBoxAgents = new System.Windows.Forms.ComboBox();
            this.deactivateButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.port = new System.Windows.Forms.Label();
            this.stationaryAgents = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.console);
            this.groupBox.Location = new System.Drawing.Point(12, 378);
            this.groupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox.Size = new System.Drawing.Size(387, 258);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Afisare informatii agent";
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(6, 19);
            this.console.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.console.Size = new System.Drawing.Size(368, 228);
            this.console.TabIndex = 0;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(10, 20);
            this.info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.info.Multiline = true;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(368, 38);
            this.info.TabIndex = 1;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(11, 58);
            this.createButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(122, 34);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "Creare agent";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // agentsList
            // 
            this.agentsList.Location = new System.Drawing.Point(10, 164);
            this.agentsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.agentsList.Multiline = true;
            this.agentsList.Name = "agentsList";
            this.agentsList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.agentsList.Size = new System.Drawing.Size(368, 83);
            this.agentsList.TabIndex = 3;
            // 
            // labelAgents
            // 
            this.labelAgents.AutoSize = true;
            this.labelAgents.Location = new System.Drawing.Point(7, 145);
            this.labelAgents.Name = "labelAgents";
            this.labelAgents.Size = new System.Drawing.Size(88, 17);
            this.labelAgents.TabIndex = 4;
            this.labelAgents.Text = "Agenti mobili";
            // 
            // dispatchButton
            // 
            this.dispatchButton.Location = new System.Drawing.Point(128, 20);
            this.dispatchButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dispatchButton.Name = "dispatchButton";
            this.dispatchButton.Size = new System.Drawing.Size(105, 34);
            this.dispatchButton.TabIndex = 6;
            this.dispatchButton.Text = "Trimite agent";
            this.dispatchButton.UseVisualStyleBackColor = true;
            this.dispatchButton.Click += new System.EventHandler(this.dispatchButton_Click);
            // 
            // disposeButton
            // 
            this.disposeButton.Location = new System.Drawing.Point(11, 305);
            this.disposeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.disposeButton.Name = "disposeButton";
            this.disposeButton.Size = new System.Drawing.Size(133, 34);
            this.disposeButton.TabIndex = 7;
            this.disposeButton.Text = "Dezactivare agent";
            this.disposeButton.UseVisualStyleBackColor = true;
            this.disposeButton.Click += new System.EventHandler(this.disposeButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.deactivateButton);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(423, 25);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(287, 611);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.createButton);
            this.groupBox4.Controls.Add(this.comboBoxAgents);
            this.groupBox4.Location = new System.Drawing.Point(12, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(268, 102);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Alege agent -  creare";
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(11, 77);
            this.cloneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(115, 34);
            this.cloneButton.TabIndex = 8;
            this.cloneButton.Text = "Clonare agent";
            this.cloneButton.UseVisualStyleBackColor = true;
            this.cloneButton.Click += new System.EventHandler(this.cloneButton_Click);
            // 
            // listAgents
            // 
            this.listAgents.FormattingEnabled = true;
            this.listAgents.Location = new System.Drawing.Point(11, 36);
            this.listAgents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listAgents.Name = "listAgents";
            this.listAgents.Size = new System.Drawing.Size(243, 24);
            this.listAgents.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Gazde vecine";
            // 
            // comboBoxN
            // 
            this.comboBoxN.FormattingEnabled = true;
            this.comboBoxN.Location = new System.Drawing.Point(13, 30);
            this.comboBoxN.Name = "comboBoxN";
            this.comboBoxN.Size = new System.Drawing.Size(98, 24);
            this.comboBoxN.TabIndex = 21;
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(13, 105);
            this.comboBoxPorts.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(135, 24);
            this.comboBoxPorts.TabIndex = 20;
            // 
            // comboBoxIPAddresses
            // 
            this.comboBoxIPAddresses.FormattingEnabled = true;
            this.comboBoxIPAddresses.Location = new System.Drawing.Point(13, 70);
            this.comboBoxIPAddresses.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxIPAddresses.Name = "comboBoxIPAddresses";
            this.comboBoxIPAddresses.Size = new System.Drawing.Size(135, 24);
            this.comboBoxIPAddresses.TabIndex = 19;
            // 
            // dispatchButtonNetwork
            // 
            this.dispatchButtonNetwork.Location = new System.Drawing.Point(57, 51);
            this.dispatchButtonNetwork.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dispatchButtonNetwork.Name = "dispatchButtonNetwork";
            this.dispatchButtonNetwork.Size = new System.Drawing.Size(104, 47);
            this.dispatchButtonNetwork.TabIndex = 18;
            this.dispatchButtonNetwork.Text = "Trimitere";
            this.dispatchButtonNetwork.UseVisualStyleBackColor = true;
            this.dispatchButtonNetwork.Click += new System.EventHandler(this.dispatchButtonNetwork_Click);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(155, 77);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(69, 17);
            this.labelAddress.TabIndex = 17;
            this.labelAddress.Text = "Adresa IP";
            // 
            // comboBoxAgents
            // 
            this.comboBoxAgents.FormattingEnabled = true;
            this.comboBoxAgents.Location = new System.Drawing.Point(11, 28);
            this.comboBoxAgents.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAgents.Name = "comboBoxAgents";
            this.comboBoxAgents.Size = new System.Drawing.Size(243, 24);
            this.comboBoxAgents.TabIndex = 15;
            // 
            // deactivateButton
            // 
            this.deactivateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deactivateButton.Location = new System.Drawing.Point(147, 552);
            this.deactivateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deactivateButton.Name = "deactivateButton";
            this.deactivateButton.Size = new System.Drawing.Size(133, 28);
            this.deactivateButton.TabIndex = 12;
            this.deactivateButton.Text = "Iesire";
            this.deactivateButton.UseVisualStyleBackColor = true;
            this.deactivateButton.Click += new System.EventHandler(this.deactivateButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 547);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 9;
            this.button1.Text = "Agenti";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(155, 108);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(34, 17);
            this.port.TabIndex = 11;
            this.port.Text = "Port";
            // 
            // stationaryAgents
            // 
            this.stationaryAgents.Location = new System.Drawing.Point(10, 48);
            this.stationaryAgents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stationaryAgents.Multiline = true;
            this.stationaryAgents.Name = "stationaryAgents";
            this.stationaryAgents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.stationaryAgents.Size = new System.Drawing.Size(368, 71);
            this.stationaryAgents.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Agenti stationari";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stationaryAgents);
            this.groupBox1.Controls.Add(this.agentsList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelAgents);
            this.groupBox1.Location = new System.Drawing.Point(12, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 259);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista agenti";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.info);
            this.groupBox3.Location = new System.Drawing.Point(12, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(387, 72);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informatii agentie";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tabControl1);
            this.groupBox5.Controls.Add(this.listAgents);
            this.groupBox5.Controls.Add(this.cloneButton);
            this.groupBox5.Controls.Add(this.disposeButton);
            this.groupBox5.Location = new System.Drawing.Point(12, 137);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(268, 371);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Alege agent - clonare/trimitere";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 127);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(247, 173);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dispatchButton);
            this.tabPage2.Controls.Add(this.labelAddress);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.comboBoxIPAddresses);
            this.tabPage2.Controls.Add(this.comboBoxN);
            this.tabPage2.Controls.Add(this.port);
            this.tabPage2.Controls.Add(this.comboBoxPorts);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(239, 144);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "La o adresa specificata";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dispatchButtonNetwork);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(239, 144);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "In retea";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // AgencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 644);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AgencyForm";
            this.Text = "Interfata agentie";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TextBox console;
        private System.Windows.Forms.TextBox info;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox agentsList;
        private System.Windows.Forms.Label labelAgents;
        private System.Windows.Forms.Button dispatchButton;
        private System.Windows.Forms.Button disposeButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cloneButton;
        private System.Windows.Forms.ComboBox listAgents;
        private System.Windows.Forms.Label port;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button deactivateButton;
        private System.Windows.Forms.ComboBox comboBoxAgents;
        private System.Windows.Forms.Button dispatchButtonNetwork;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox stationaryAgents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.ComboBox comboBoxIPAddresses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

