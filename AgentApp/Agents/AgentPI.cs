using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AgentManager;
using System.Threading;
using System.Windows.Forms;

namespace AgentApp
{
    [Serializable]
    public class AgentPI : Agent
    {
        #region Fields
        private int dec;
        Form f = new Form();
        Button send = new Button();
        Label labelNrDec = new Label();
        TextBox textBoxNrDec = new TextBox();

        #endregion Fields

        #region Constructor
        public AgentPI()
        {
            this.SetAgentInfo("Calculate the value of PI");
        }
        public AgentPI(int id) : base(id)
        {

        }
        #endregion Constructor

        #region Methods
        public int SetDec
        {
            set
            {
                dec = value;
            }
        }
        public int GetDec
        {
            get
            {
                return dec;
            }
        }
        private void CalculPi()
        {
            string codebase="";

            dec++;

            uint[] x = new uint[dec * 10 / 3 + 2];
            uint[] r = new uint[dec * 10 / 3 + 2];

            uint[] pi = new uint[dec];

            for (int j = 0; j < x.Length; j++)
                x[j] = 20;

            for (int i = 0; i < dec; i++)
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


                pi[i] = (x[x.Length - 1] / 10);


                r[x.Length - 1] = x[x.Length - 1] % 10; ;

                for (int j = 0; j < x.Length; j++)
                    x[j] = r[j] * 10;
            }

            var result = "";

            uint c = 0;

            for (int i = pi.Length - 1; i >= 0; i--)
            {
                pi[i] += c;
                c = pi[i] / 10;

                result = (pi[i] % 10).ToString() + result;
            }
            Console.WriteLine("Rezultatul este: "+ result);
            codebase += "Rezultatul este: " + result + "\n";
            this.SetAgentCodebase(codebase);
        }
        public override void Run()
        {
            CalculPi();
        }
        public override void GetUI()
        {
            //var thread = new Thread(() =>
            //{

            //    f.SuspendLayout();


            //    //
            //    //textBoxNrDec
            //    //
            //    textBoxNrDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    textBoxNrDec.Location = new System.Drawing.Point(37, 71);
            //    textBoxNrDec.Multiline = true;
            //    textBoxNrDec.Name = "texBoxNrDec";
            //    textBoxNrDec.Size = new System.Drawing.Size(106, 38);
            //    textBoxNrDec.TabIndex = 3;

            //    //
            //    //labelNrDec
            //    //
            //    labelNrDec.AutoSize = true;
            //    labelNrDec.Location = new System.Drawing.Point(37, 36);
            //    labelNrDec.Name = "labelNrDec";
            //    labelNrDec.Size = new System.Drawing.Size(140, 17);
            //    labelNrDec.TabIndex = 5;
            //    labelNrDec.Text = "Numarul de zecimale";

            //    //
            //    //send
            //    //
            //    send.Location = new System.Drawing.Point(206, 71);
            //    send.Name = "button1";
            //    send.Size = new System.Drawing.Size(107, 38);
            //    send.TabIndex = 4;
            //    send.Text = "Trimite";
            //    send.UseVisualStyleBackColor = true;
            //    send.Click += new EventHandler(this.OnSending);

            //    //
            //    //interfata
            //    //
            //    f.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //    f.AutoScaleMode = AutoScaleMode.Font;
            //    f.ClientSize = new System.Drawing.Size(350, 145);
            //    f.Controls.Add(labelNrDec);
            //    f.Controls.Add(send);
            //    f.Controls.Add(textBoxNrDec);
            //    f.Name = "Form1";
            //    f.Text = "Interfata AgentPI ";
            //    f.ResumeLayout(false);
            //    f.PerformLayout();

            //    Application.Run(f);
            //});
            //thread.Start();
            throw new NotImplementedException();
        }
        private void OnSending(object sender, EventArgs e)
        {
            try {
                int dec = Int32.Parse(textBoxNrDec.Text);
                SetDec = dec;
            }
            catch(FormatException)
            {
                MessageBox.Show("Parametrul nu a putut fi setat!");
            }
        }

        #endregion Methods
    }

}
