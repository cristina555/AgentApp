namespace AgentApp
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
            this.startButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.agentsList = new System.Windows.Forms.TextBox();
            this.labelAgents = new System.Windows.Forms.Label();
            this.dispatchButton = new System.Windows.Forms.Button();
            this.disposeButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listAgents = new System.Windows.Forms.ComboBox();
            this.cloneButton = new System.Windows.Forms.Button();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.console);
            this.groupBox.Location = new System.Drawing.Point(12, 190);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(380, 230);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(6, 21);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(368, 203);
            this.console.TabIndex = 0;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(147, 22);
            this.info.Multiline = true;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(245, 37);
            this.info.TabIndex = 1;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 22);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(129, 37);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start agency";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(22, 30);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(134, 34);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "Create agent";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // agentsList
            // 
            this.agentsList.Location = new System.Drawing.Point(18, 102);
            this.agentsList.Multiline = true;
            this.agentsList.Name = "agentsList";
            this.agentsList.Size = new System.Drawing.Size(368, 82);
            this.agentsList.TabIndex = 3;
            // 
            // labelAgents
            // 
            this.labelAgents.AutoSize = true;
            this.labelAgents.Location = new System.Drawing.Point(15, 73);
            this.labelAgents.Name = "labelAgents";
            this.labelAgents.Size = new System.Drawing.Size(93, 17);
            this.labelAgents.TabIndex = 4;
            this.labelAgents.Text = "List of agents";
            // 
            // dispatchButton
            // 
            this.dispatchButton.Location = new System.Drawing.Point(22, 70);
            this.dispatchButton.Name = "dispatchButton";
            this.dispatchButton.Size = new System.Drawing.Size(134, 34);
            this.dispatchButton.TabIndex = 6;
            this.dispatchButton.Text = "Dispatch Agent";
            this.dispatchButton.UseVisualStyleBackColor = true;
            this.dispatchButton.Click += new System.EventHandler(this.dispatchButton_Click);
            // 
            // disposeButton
            // 
            this.disposeButton.Location = new System.Drawing.Point(22, 110);
            this.disposeButton.Name = "disposeButton";
            this.disposeButton.Size = new System.Drawing.Size(134, 34);
            this.disposeButton.TabIndex = 7;
            this.disposeButton.Text = "Dispose Agent";
            this.disposeButton.UseVisualStyleBackColor = true;
            this.disposeButton.Click += new System.EventHandler(this.disposeButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.port);
            this.groupBox2.Controls.Add(this.textBoxPort);
            this.groupBox2.Controls.Add(this.listAgents);
            this.groupBox2.Controls.Add(this.cloneButton);
            this.groupBox2.Controls.Add(this.disposeButton);
            this.groupBox2.Controls.Add(this.dispatchButton);
            this.groupBox2.Controls.Add(this.createButton);
            this.groupBox2.Location = new System.Drawing.Point(423, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 160);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // listAgents
            // 
            this.listAgents.FormattingEnabled = true;
            this.listAgents.Location = new System.Drawing.Point(175, 76);
            this.listAgents.Name = "listAgents";
            this.listAgents.Size = new System.Drawing.Size(134, 24);
            this.listAgents.TabIndex = 9;
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(175, 30);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(134, 34);
            this.cloneButton.TabIndex = 8;
            this.cloneButton.Text = "Clone Agent";
            this.cloneButton.UseVisualStyleBackColor = true;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(212, 116);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(97, 22);
            this.textBoxPort.TabIndex = 10;
            // 
            // port
            // 
            this.port.AutoSize = true;
            this.port.Location = new System.Drawing.Point(172, 116);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(34, 17);
            this.port.TabIndex = 11;
            this.port.Text = "Port";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 435);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labelAgents);
            this.Controls.Add(this.agentsList);
            this.Controls.Add(this.info);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.groupBox);
            this.Name = "MainForm";
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
        private System.Windows.Forms.Button startButton;
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
        private System.Windows.Forms.TextBox textBoxPort;
    }
}

