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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelShowClock = new System.Windows.Forms.Label();
            this.buttonDeactivateA = new System.Windows.Forms.Button();
            this.showButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dispatchButtonNetwork = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxN = new System.Windows.Forms.ComboBox();
            this.listAgents = new System.Windows.Forms.ComboBox();
            this.cloneButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxAgents = new System.Windows.Forms.ComboBox();
            this.deactivateButton = new System.Windows.Forms.Button();
            this.stationaryAgents = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.console);
            this.groupBox.Location = new System.Drawing.Point(12, 378);
            this.groupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox.Name = "groupBox";
            this.groupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox.Size = new System.Drawing.Size(405, 258);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Afișare informații agent";
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(5, 18);
            this.console.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.console.Size = new System.Drawing.Size(393, 228);
            this.console.TabIndex = 0;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(11, 20);
            this.info.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.info.Multiline = true;
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(389, 38);
            this.info.TabIndex = 1;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(11, 58);
            this.createButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(123, 34);
            this.createButton.TabIndex = 2;
            this.createButton.Text = "Creare agent";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // agentsList
            // 
            this.agentsList.Location = new System.Drawing.Point(11, 164);
            this.agentsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.agentsList.Multiline = true;
            this.agentsList.Name = "agentsList";
            this.agentsList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.agentsList.Size = new System.Drawing.Size(389, 83);
            this.agentsList.TabIndex = 3;
            // 
            // labelAgents
            // 
            this.labelAgents.AutoSize = true;
            this.labelAgents.Location = new System.Drawing.Point(7, 145);
            this.labelAgents.Name = "labelAgents";
            this.labelAgents.Size = new System.Drawing.Size(88, 17);
            this.labelAgents.TabIndex = 4;
            this.labelAgents.Text = "Agenți mobili";
            // 
            // dispatchButton
            // 
            this.dispatchButton.Location = new System.Drawing.Point(181, 41);
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
            this.disposeButton.Location = new System.Drawing.Point(197, 78);
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
            this.groupBox2.Location = new System.Drawing.Point(423, 25);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(385, 608);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labelShowClock);
            this.groupBox5.Controls.Add(this.buttonDeactivateA);
            this.groupBox5.Controls.Add(this.showButton);
            this.groupBox5.Controls.Add(this.tabControl1);
            this.groupBox5.Controls.Add(this.listAgents);
            this.groupBox5.Controls.Add(this.cloneButton);
            this.groupBox5.Controls.Add(this.disposeButton);
            this.groupBox5.Location = new System.Drawing.Point(12, 137);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(345, 427);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Alege agent - clonare/trimitere";
            // 
            // labelShowClock
            // 
            this.labelShowClock.AllowDrop = true;
            this.labelShowClock.AutoSize = true;
            this.labelShowClock.Location = new System.Drawing.Point(12, 374);
            this.labelShowClock.Name = "labelShowClock";
            this.labelShowClock.Size = new System.Drawing.Size(0, 17);
            this.labelShowClock.TabIndex = 19;
            this.labelShowClock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDeactivateA
            // 
            this.buttonDeactivateA.Location = new System.Drawing.Point(197, 160);
            this.buttonDeactivateA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonDeactivateA.Name = "buttonDeactivateA";
            this.buttonDeactivateA.Size = new System.Drawing.Size(133, 30);
            this.buttonDeactivateA.TabIndex = 18;
            this.buttonDeactivateA.Text = "Agenți dezactivați";
            this.buttonDeactivateA.UseVisualStyleBackColor = true;
            this.buttonDeactivateA.Click += new System.EventHandler(this.buttonDeactivateA_Click);
            // 
            // showButton
            // 
            this.showButton.Location = new System.Drawing.Point(11, 160);
            this.showButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.showButton.Name = "showButton";
            this.showButton.Size = new System.Drawing.Size(133, 30);
            this.showButton.TabIndex = 17;
            this.showButton.Text = "Afișare informații";
            this.showButton.UseVisualStyleBackColor = true;
            this.showButton.Click += new System.EventHandler(this.showButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(11, 217);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(329, 142);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dispatchButtonNetwork);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(321, 113);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "În rețea";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dispatchButtonNetwork
            // 
            this.dispatchButtonNetwork.Location = new System.Drawing.Point(116, 30);
            this.dispatchButtonNetwork.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dispatchButtonNetwork.Name = "dispatchButtonNetwork";
            this.dispatchButtonNetwork.Size = new System.Drawing.Size(104, 47);
            this.dispatchButtonNetwork.TabIndex = 18;
            this.dispatchButtonNetwork.Text = "Trimitere";
            this.dispatchButtonNetwork.UseVisualStyleBackColor = true;
            this.dispatchButtonNetwork.Click += new System.EventHandler(this.dispatchButtonNetwork_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dispatchButton);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.comboBoxN);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(321, 113);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "La o adresă specificată";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Gazde vecine";
            // 
            // comboBoxN
            // 
            this.comboBoxN.FormattingEnabled = true;
            this.comboBoxN.Location = new System.Drawing.Point(27, 47);
            this.comboBoxN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxN.Name = "comboBoxN";
            this.comboBoxN.Size = new System.Drawing.Size(132, 24);
            this.comboBoxN.TabIndex = 21;
            // 
            // listAgents
            // 
            this.listAgents.FormattingEnabled = true;
            this.listAgents.Location = new System.Drawing.Point(11, 36);
            this.listAgents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listAgents.Name = "listAgents";
            this.listAgents.Size = new System.Drawing.Size(319, 24);
            this.listAgents.TabIndex = 9;
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(11, 78);
            this.cloneButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(115, 34);
            this.cloneButton.TabIndex = 8;
            this.cloneButton.Text = "Clonare agent";
            this.cloneButton.UseVisualStyleBackColor = true;
            this.cloneButton.Click += new System.EventHandler(this.cloneButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.createButton);
            this.groupBox4.Controls.Add(this.comboBoxAgents);
            this.groupBox4.Location = new System.Drawing.Point(12, 20);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(345, 103);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Alege agent -  creare";
            // 
            // comboBoxAgents
            // 
            this.comboBoxAgents.FormattingEnabled = true;
            this.comboBoxAgents.Location = new System.Drawing.Point(11, 28);
            this.comboBoxAgents.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAgents.Name = "comboBoxAgents";
            this.comboBoxAgents.Size = new System.Drawing.Size(319, 24);
            this.comboBoxAgents.TabIndex = 15;
            // 
            // deactivateButton
            // 
            this.deactivateButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.deactivateButton.Location = new System.Drawing.Point(224, 572);
            this.deactivateButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deactivateButton.Name = "deactivateButton";
            this.deactivateButton.Size = new System.Drawing.Size(133, 28);
            this.deactivateButton.TabIndex = 12;
            this.deactivateButton.Text = "Iesire";
            this.deactivateButton.UseVisualStyleBackColor = true;
            this.deactivateButton.Click += new System.EventHandler(this.deactivateButton_Click);
            // 
            // stationaryAgents
            // 
            this.stationaryAgents.Location = new System.Drawing.Point(11, 48);
            this.stationaryAgents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stationaryAgents.Multiline = true;
            this.stationaryAgents.Name = "stationaryAgents";
            this.stationaryAgents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.stationaryAgents.Size = new System.Drawing.Size(389, 70);
            this.stationaryAgents.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Agenți staționari";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stationaryAgents);
            this.groupBox1.Controls.Add(this.agentsList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelAgents);
            this.groupBox1.Location = new System.Drawing.Point(12, 103);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(405, 258);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Listă agenți";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.info);
            this.groupBox3.Location = new System.Drawing.Point(12, 25);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(405, 71);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informații agenție";
            // 
            // AgencyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 644);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AgencyForm";
            this.Text = "Interfață agenție";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Button deactivateButton;
        private System.Windows.Forms.ComboBox comboBoxAgents;
        private System.Windows.Forms.Button dispatchButtonNetwork;
        private System.Windows.Forms.TextBox stationaryAgents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxN;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button showButton;
        private System.Windows.Forms.Button buttonDeactivateA;
        private System.Windows.Forms.Label labelShowClock;
    }
}

