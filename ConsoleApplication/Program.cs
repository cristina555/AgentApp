using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList v = new ArrayList();
            v.Add(1);
            v.Add(3);
            IEnumerator e = v.GetEnumerator();
            while (e.MoveNext())
            {
                Console.WriteLine(e.Current);
            }
            Console.WriteLine(v.Count);
        }
        
    }
    
}
