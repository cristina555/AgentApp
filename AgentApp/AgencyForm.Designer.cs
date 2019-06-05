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
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelChoose = new System.Windows.Forms.Label();
            this.comboBoxAgents = new System.Windows.Forms.ComboBox();
            this.deactivateButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.port = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.listAgents = new System.Windows.Forms.ComboBox();
            this.cloneButton = new System.Windows.Forms.Button();
            this.textBoxStationaryAgent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.console);
            this.groupBox.Location = new System.Drawing.Point(12, 231);
            this.groupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox.Size = new System.Drawing.Size(386, 263);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(7, 19);
            this.console.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.console.Size = new System.Drawing.Size(368, 228);
            this.console.TabIndex = 0;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(19, 42);
            this.info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.info.Multiline = true;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(368, 37);
            this.info.TabIndex = 1;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(21, 30);
            this.createButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(133, 34);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "Creare agent";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // agentsList
            // 
            this.agentsList.Location = new System.Drawing.Point(19, 170);
            this.agentsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.agentsList.Multiline = true;
            this.agentsList.Name = "agentsList";
            this.agentsList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.agentsList.Size = new System.Drawing.Size(368, 57);
            this.agentsList.TabIndex = 3;
            // 
            // labelAgents
            // 
            this.labelAgents.AutoSize = true;
            this.labelAgents.Location = new System.Drawing.Point(16, 138);
            this.labelAgents.Name = "labelAgents";
            this.labelAgents.Size = new System.Drawing.Size(121, 17);
            this.labelAgents.TabIndex = 4;
            this.labelAgents.Text = "Lista agenti mobili";
            // 
            // dispatchButton
            // 
            this.dispatchButton.Location = new System.Drawing.Point(21, 126);
            this.dispatchButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dispatchButton.Name = "dispatchButton";
            this.dispatchButton.Size = new System.Drawing.Size(133, 34);
            this.dispatchButton.TabIndex = 6;
            this.dispatchButton.Text = "Trimite agent";
            this.dispatchButton.UseVisualStyleBackColor = true;
            this.dispatchButton.Click += new System.EventHandler(this.dispatchButton_Click);
            // 
            // disposeButton
            // 
            this.disposeButton.Location = new System.Drawing.Point(21, 225);
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
            this.groupBox2.Controls.Add(this.textBoxIpAddress);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.labelAddress);
            this.groupBox2.Controls.Add(this.labelChoose);
            this.groupBox2.Controls.Add(this.comboBoxAgents);
            this.groupBox2.Controls.Add(this.deactivateButton);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.port);
            this.groupBox2.Controls.Add(this.textBoxPort);
            this.groupBox2.Controls.Add(this.listAgents);
            this.groupBox2.Controls.Add(this.cloneButton);
            this.groupBox2.Controls.Add(this.disposeButton);
            this.groupBox2.Controls.Add(this.dispatchButton);
            this.groupBox2.Controls.Add(this.createButton);
            this.groupBox2.Location = new System.Drawing.Point(423, 25);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(327, 465);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(173, 187);
            this.textBoxIpAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxIpAddress.Multiline = true;
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(135, 24);
            this.textBoxIpAddress.TabIndex = 19;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(21, 177);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 34);
            this.button2.TabIndex = 18;
            this.button2.Text = "Trimitere";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(170, 164);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(69, 17);
            this.labelAddress.TabIndex = 17;
            this.labelAddress.Text = "Adresa IP";
            // 
            // labelChoose
            // 
            this.labelChoose.AutoSize = true;
            this.labelChoose.Location = new System.Drawing.Point(170, 113);
            this.labelChoose.Name = "labelChoose";
            this.labelChoose.Size = new System.Drawing.Size(84, 17);
            this.labelChoose.TabIndex = 16;
            this.labelChoose.Text = "Alege agent";
            // 
            // comboBoxAgents
            // 
            this.comboBoxAgents.FormattingEnabled = true;
            this.comboBoxAgents.Location = new System.Drawing.Point(175, 56);
            this.comboBoxAgents.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAgents.Name = "comboBoxAgents";
            this.comboBoxAgents.Size = new System.Drawing.Size(133, 24);
            this.comboBoxAgents.TabIndex = 15;
            // 
            // deactivateButton
            // 
            this.deactivateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deactivateButton.Location = new System.Drawing.Point(180, 425);
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
            this.button1.Location = new System.Drawing.Point(21, 420);
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
            this.port.Location = new System.Drawing.Point(170, 236);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(34, 17);
            this.port.TabIndex = 11;
            this.port.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(209, 240);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(97, 22);
            this.textBoxPort.TabIndex = 10;
            // 
            // listAgents
            // 
            this.listAgents.FormattingEnabled = true;
            this.listAgents.Location = new System.Drawing.Point(173, 132);
            this.listAgents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listAgents.Name = "listAgents";
            this.listAgents.Size = new System.Drawing.Size(135, 24);
            this.listAgents.TabIndex = 9;
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(21, 78);
            this.cloneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(133, 34);
            this.cloneButton.TabIndex = 8;
            this.cloneButton.Text = "Clonare agent";
            this.cloneButton.UseVisualStyleBackColor = true;
            // 
            // textBoxStationaryAgent
            // 
            this.textBoxStationaryAgent.Location = new System.Drawing.Point(19, 103);
            this.textBoxStationaryAgent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxStationaryAgent.Multiline = true;
            this.textBoxStationaryAgent.Name = "textBoxStationaryAgent";
            this.textBoxStationaryAgent.Size = new System.Drawing.Size(368, 25);
            this.textBoxStationaryAgent.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Agent stationar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Informatii agentie";
            // 
            // AgencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 505);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxStationaryAgent);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelAgents);
            this.Controls.Add(this.agentsList);
            this.Controls.Add(this.info);
            this.Controls.Add(this.groupBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AgencyForm";
            this.Text = "Interfata agentie";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.ComboBox comboBoxAgents;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelChoose;
        private System.Windows.Forms.TextBox textBoxStationaryAgent;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

