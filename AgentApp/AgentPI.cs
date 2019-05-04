﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileAgent.AgentManager;

namespace AgentApp
{
    public class AgentPI : Agent
    {
        public AgentPI(int id) : base(id)
        {

        }
        public string CalculPi(int dec)
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

            return result;
        }
        public override void Run()
        {

        }
    }
}
