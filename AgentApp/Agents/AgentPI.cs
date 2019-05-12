using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AgentManager;
using System.Threading;

namespace AgentApp
{
    [Serializable]
    public class AgentPI : Agent
    {
        #region Fields
        private int dec;
        #endregion Fields

        #region Constructor
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
        }
        public override void Run()
        {
            Thread agentThread = new Thread(new ThreadStart(CalculPi));
            agentThread.IsBackground = true;
            agentThread.Start();
        }
        #endregion Methods
    }

}
