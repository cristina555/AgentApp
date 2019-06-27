using System;
using System.Xml;
using MobileAgent.AgentManager;
using System.Windows.Forms;
using System.Threading;

namespace AgentApp
{
    [Serializable]
    public class AgentPI : Agent, IMobile
    {

        #region Constructor
        public AgentPI() : base()
        {
            SetType(Agent.ONEWAY);
            SetName("AgentPI");
            SetAgentInfo("Calculeaza valoarea lui PI");
        }
        public AgentPI(int id) : base(id)
        {
            SetType(Agent.ONEWAY);
            SetName("AgentPI");
            SetAgentInfo("Calculeaza valoarea lui PI");
        }
        #endregion Constructor

        #region Properties
        public int Index { get; set; } = 0;
        public int Step { get; set; } = 0;
        public int Dec { get; set; }

        public int State { get; set; } = 0;



        uint[] x;
        uint[] r;

        uint[] pi;


        public string Result { get; private set; } = "";
        #endregion Properties

        #region Private Methods
        private void CalculPi(AgencyContext agencyContext)
        {
            Console.WriteLine("Rezultatul este: " + Result);
            Dec++;
            if (State == 1)
            {
                x = new uint[Dec * 10 / 3 + 2];
                r = new uint[Dec * 10 / 3 + 2];

                pi = new uint[Dec];
                while (Step < x.Length)
                {
                    x[Step] = 20;
                    Step++;
                }
            }
            while (Index < Dec )
            {
                uint carry = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    uint num = (uint)(x.Length - j - 1);
                    uint dem = num * 2 + 1;

                    x[j] += carry;

                    uint q = x[j] / dem;
                    r[j] = x[j] % dem;

                    carry = q * num;
                }
                Thread.Sleep(3000);
                
                pi[Index] = (x[x.Length - 1] / 10);
                if (Index == 0)
                {
                    Result = Result + pi[Index].ToString() + ",";
                }
                else
                {
                    Result = Result + pi[Index];
                }
                Console.WriteLine("Rezultatul este: " + Result);
                r[x.Length - 1] = x[x.Length - 1] % 10; ;

                for (int j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;


                Index++;

                if (!IsReady())
                {
                    agencyContext.SetAgencyFeedback();
                    break;
                }
            }

            this.SetAgentStateInfo("Rezultatul este: " + Result + "\n");
            Console.WriteLine("Rezultatul este " + Result);
        }
        private void button1_Click(object sender, EventArgs e, Form ui)
        {
            try
            {
                int dec = 0;
                foreach (Control c in ui.Controls)
                {
                    if (c is TextBox)
                    {
                        TextBox control = (TextBox)c;
                        dec = int.Parse(control.Text);
                        break;
                    }
                }
                Dec = dec;
                ui.Close();
            }
            catch (NullReferenceException nre)
            {
                MessageBox.Show("NullReferenceException !" + nre.Message + " --> Trimite parametrii agentului!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception !" + ex.Message + " --> Trimite parametrii agentului!");
            }
        }
        private void ResetState()
        {
            Index = 0;
            Step = 0;
            Result = "";
        }
        #endregion Private Methods

        #region Public Override Methods
        public override void Run()
        {
            if(Result.Length-1 == Dec)
            {
                ResetState();
            }
            ResetLifetime();
            SetWorkStatus(Agent.READY);
            State++;
            AgencyContext agencyContext = GetAgentCurrentContext();
            CalculPi(agencyContext);
        }
        public override void GetUI()
        {
            Form ui = new Form();
            Label label1 = new Label();
            Button button1 = new Button();
            TextBox textBox1 =  new TextBox();
            ui.SuspendLayout();
            //
            //label1
            //
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(28, 29);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(105, 13);
            label1.TabIndex = 5;
            label1.Text = "Numarul de zecimale";
            //
            //button1
            //
            button1.Location = new System.Drawing.Point(154, 58);
            button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(80, 31);
            button1.TabIndex = 4;
            button1.Text = "Trimite";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox1.Location = new System.Drawing.Point(28, 58);
            textBox1.Margin = new Padding(2, 2, 2, 2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(80, 32);
            textBox1.TabIndex = 3;
            // 
            // AgentPiUI
            // 
            ui.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            ui.AutoScaleMode = AutoScaleMode.Font;
            ui.ClientSize = new System.Drawing.Size(262, 118);
            ui.Controls.Add(label1);
            ui.Controls.Add(button1);
            ui.Controls.Add(textBox1);
            ui.Margin = new Padding(2, 2, 2, 2);
            ui.Name = "AgentPiUI";
            ui.Text = "Interfata AgentRemote";
            ui.ResumeLayout(false);
            ui.PerformLayout();

            button1.Click += new EventHandler((sender, e) =>button1_Click(sender, e, ui));
            var thread = new Thread(() =>
            {
                Application.Run(ui);
            });
            thread.Start();
        }
        public override  String GetInfo()
        {
            throw new NotImplementedException();
        }  
        #endregion Public Override Methods
    }

}
