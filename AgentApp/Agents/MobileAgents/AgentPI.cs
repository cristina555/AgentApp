using System;
using System.Xml;
using MobileAgent.AgentManager;

namespace AgentApp
{
    [Serializable]
    public class AgentPI : Agent
    {
 
        #region Constructor
        public AgentPI() : base()
        {
            SetName("AgentPI");
            SetAgentInfo("Calculate the value of PI");
        }
        public AgentPI(int id) : base(id)
        {

        }
        #endregion Constructor

        #region Properties
        public int SetDec
        {
            set
            {
                GetDec = value;
            }
        }
        public int GetDec { get; private set; }
        #endregion Properties

        #region Private Methods
        private void CalculPi()
        {
            string codebase="";

            GetDec++;

            uint[] x = new uint[GetDec * 10 / 3 + 2];
            uint[] r = new uint[GetDec * 10 / 3 + 2];

            uint[] pi = new uint[GetDec];

            for (int j = 0; j < x.Length; j++)
                x[j] = 20;

            for (int i = 0; i < GetDec; i++)
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
            this.SetAgentStateInfo(codebase);
        }
        #endregion Private Methods

        #region Public Methods
        public override void Run()
        {
            CalculPi();
        }
        public override void GetUI()
        {
            XmlDocument xmlDoc = new XmlDocument();
        }
        #endregion Public Methods
    }

}
