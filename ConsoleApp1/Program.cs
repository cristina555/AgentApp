////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Management;
////using System.Net;
////using System.Net.NetworkInformation;
////using System.Net.Sockets;
////using System.Text;
////using System.Threading.Tasks;

////namespace ConsoleApp1
////{
////    using System;

////    //namespace ConsoleApplication1
////    //{
////    //    class Program
////    //    {
////    //        static void Main(string[] args)
////    //        {
////    //            Counter c = new Counter(new Random().Next(10));
////    //            Console.WriteLine(c.threshold);
////    //            c.ThresholdReached += c_ThresholdReached;

////    //            Console.WriteLine("press 'a' key to increase total");
////    //            while (Console.ReadKey(true).KeyChar == 'a')
////    //            {
////    //                Console.WriteLine("adding one");
////    //                c.Add(1);
////    //            }
////    //        }

////    //        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
////    //        {
////    //            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
////    //            Environment.Exit(0);
////    //        }
////    //    }

////    //    class Counter
////    //    {
////    //        public int threshold;
////    //        public int total;

////    //        public Counter(int passedThreshold)
////    //        {
////    //            threshold = passedThreshold;
////    //        }

////    //        public void Add(int x)
////    //        {
////    //            total += x;
////    //            if (total >= threshold)
////    //            {
////    //                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
////    //                args.Threshold = threshold;
////    //                args.TimeReached = DateTime.Now;
////    //                OnThresholdReached(args);
////    //            }
////    //        }

////    //        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
////    //        {
////    //            //EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
////    //            //if (handler != null)
////    //            //{
////    //            //    handler(this, e);
////    //            //}
////    //            ThresholdReached?.Invoke(this, e);
////    //        }

////    //        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
////    //    }

////    //    public class ThresholdReachedEventArgs : EventArgs
////    //    {
////    //        public int Threshold { get; set; }
////    //        public DateTime TimeReached { get; set; }
////    //    }
////    //}
////    class Program
////    {
////        public static void GetLocalIPAddress()
////        {
////            var host = Dns.GetHostEntry(Dns.GetHostName());
////            foreach (var ip in host.AddressList)
////            {

////                if (ip.AddressFamily == AddressFamily.InterNetwork)
////                {
////                    Console.WriteLine(ip.ToString());
////                }
////            }
////            //throw new Exception("No network adapters with an IPv4 address in the system!");
////        }
////        public static string Method2()
////        {
////            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
////            {
////                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
////                {
////                    Console.WriteLine(ni.Name);
////                    if (ni.Name == "Wi-Fi")
////                    {
////                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
////                        {
////                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
////                            {
////                                return ip.Address.ToString();
////                            }
////                        }
////                    }
////                }
////            }
////            throw new Exception("No network adapters with an IPv4 address in the system!");
////        }
////        private static void GetOSServicePackInfo()
////        {
////            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
////            foreach (ManagementObject managementObject in mos.Get())
////            {
////                if (managementObject["CSDVersion"] != null)
////                {
////                    Console.WriteLine("Operating System Service Pack:  " + managementObject["CSDVersion"].ToString() + Environment.NewLine);
////                }
////            }
////        }
////        public class clsGraph
////        {
////            //total no of vertices
////            private int Vertices;
////            //adjency list array for all vertices.
////            private List<Int32>[] adj;
////            /* Example : vertices=4
////             *      0->[1,2]
////             *      1->[2]
////             *      2->[0,3]
////             *      3->[]
////             */

////            //constructor
////            public clsGraph(int v)
////            {
////                Vertices = v;
////                adj = new List<Int32>[v];
////                //Instantiate adjacecny list for all vertices
////                for (int i = 0; i < v; i++)
////                {
////                    adj[i] = new List<Int32>();
////                }

////            }

////            //Add edge from v->w
////            public void AddEdge(int v, int w)
////            {
////                adj[v].Add(w);
////            }

////            //Print BFS traversal
////            //s-> start node
////            //BFS uses queue as a base.
////            public void BFS(int s)
////            {
////                bool[] visited = new bool[Vertices];

////                //create queue for BFS
////                Queue<int> queue = new Queue<int>();
////                visited[s] = true;
////                queue.Enqueue(s);

////                //loop through all nodes in queue
////                while (queue.Count != 0)
////                {
////                    //Deque a vertex from queue and print it.
////                    s = queue.Dequeue();
////                    Console.WriteLine("next->" + s);

////                    //Get all adjacent vertices of s
////                    foreach (Int32 next in adj[s])
////                    {
////                        if (!visited[next])
////                        {
////                            visited[next] = true;
////                            queue.Enqueue(next);
////                        }
////                    }

////                }
////            }

////            //DFS traversal 
////            // DFS uses stack as a base.
////            public void DFS(int s)
////            {
////                bool[] visited = new bool[Vertices];

////                //For DFS use stack
////                Stack<int> stack = new Stack<int>();
////                visited[s] = true;
////                stack.Push(s);

////                while (stack.Count != 0)
////                {
////                    s = stack.Pop();
////                    Console.WriteLine("next->" + s);
////                    foreach (int i in adj[s])
////                    {
////                        if (!visited[i])
////                        {
////                            visited[i] = true;
////                            stack.Push(i);
////                        }
////                    }
////                }
////            }

////            public void PrintAdjacecnyMatrix()
////            {
////                for (int i = 0; i < Vertices; i++)
////                {
////                    Console.Write(i + ":[");
////                    string s = "";
////                    foreach (var k in adj[i])
////                    {
////                        s = s + (k + ",");
////                    }
////                    s = s.Substring(0, s.Length - 1);
////                    s = s + "]";
////                    Console.Write(s);
////                    Console.WriteLine();
////                }
////            }
////        }
////        static void Main(string[] args)
////        {
////            //GetLocalIPAddress();
////            //Console.WriteLine(Method2());
////            clsGraph graph = new clsGraph(5);
////            //graph.AddEdge(0, 6);
////            //graph.AddEdge(1, 4);
////            //graph.AddEdge(1, 6);
////            //graph.AddEdge(2, 4);
////            //graph.AddEdge(2, 7);
////            //graph.AddEdge(3, 4);
////            //graph.AddEdge(3, 5);
////            //graph.AddEdge(4, 1);
////            //graph.AddEdge(4, 2);
////            //graph.AddEdge(4, 3);
////            //graph.AddEdge(5, 3);
////            //graph.AddEdge(6, 0);
////            //graph.AddEdge(6, 1);
////            //graph.AddEdge(7, 2);
////            graph.AddEdge(0, 1);
////            graph.AddEdge(0, 2);
////            graph.AddEdge(1, 0);
////            graph.AddEdge(1, 3);
////            graph.AddEdge(2, 0);
////            graph.AddEdge(2, 4);
////            graph.AddEdge(3, 1);
////            graph.AddEdge(3, 4);
////            graph.AddEdge(4, 2);
////            graph.AddEdge(4, 3);

////            //Print adjacency matrix
////            graph.PrintAdjacecnyMatrix();

////            Console.WriteLine("BFS traversal starting from vertex 0:");
////            graph.BFS(3);
////            Console.WriteLine("DFS traversal starting from vertex 0:");
////            graph.DFS(0);
////            //GetOSServicePackInfo();
////        }
////    }
////}

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Management;
//using System.Reflection;
//using System.Text.RegularExpressions;

//namespace Rextester
//{
//    public class Program
//    {
//        [System.AttributeUsage(System.AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
//        sealed class ConfigurationLocationAttribute : System.Attribute
//        {
//            public string ConfigurationLocation { get; }
//            public ConfigurationLocationAttribute(string configurationLocation)
//            {
//                this.ConfigurationLocation = configurationLocation;
//            }
//        }
//        private static string GetVCInfo()
//        {
//            string _info = null;
//            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration");
//            string graphicsCard = string.Empty;
//            foreach (ManagementObject mo in mos.Get())
//            {
//                foreach (PropertyData property in mo.Properties)
//                {
//                    if (property.Name == "Description")
//                    {
//                        _info += "Placa video: " + property.Value.ToString();
//                    }

//                }
//            }
//            if(_info ==null)
//            {
//                _info += "Nu s-a gasit descrierea placii video";
//            }
//            return _info;
//        }
//        public static void Main(string[] args)
//        {
//            // Create a graph given in the above diagram
//            //var g = new Graph();

//            //g.AddEdge(0, 1);
//            //g.AddEdge(0, 3);
//            //g.AddEdge(1, 0);
//            //g.AddEdge(1, 2);
//            //g.AddEdge(2, 1);
//            //g.AddEdge(3, 0);
//            //g.AddEdge(3, 4);
//            //g.AddEdge(3, 5);
//            //g.AddEdge(4, 3);
//            //g.AddEdge(5, 3);



//            //Console.WriteLine("Following is Breadth First Traversal (starting from vertex 1)");
//            //g.BFSWalkWithStartNode(4);

//            ////Console.WriteLine("Following is Depth First Traversal (starting from vertex 1)");
//            ////g.DFSWalkWithStartNode(1);

//            //Console.WriteLine("Following is Depth First Traversal USING RECURSION (starting from vertex 1)");
//            //g.DFSWithRecursion(1);
//            //string info = null;
//            //info = GetVCInfo();
//            //Console.WriteLine(info);

//        //    var configurationLocation = Assembly.GetEntryAssembly().GetCustomAttribute<ConfigurationLocationAttribute>().ConfigurationLocation;

//        //    Console.WriteLine($"Should get config from {configurationLocation}");
//        //}

//        //public class Graph
//        //{
//        //    public Graph()
//        //    {
//        //        Adj = new Dictionary<int, HashSet<int>>();
//        //    }

//        //    public Dictionary<int, HashSet<int>> Adj { get; private set; }

//        //    public void AddEdge(int source, int target)
//        //    {
//        //        if (Adj.ContainsKey(source))
//        //        {
//        //            try
//        //            {
//        //                Adj[source].Add(target);
//        //            }
//        //            catch
//        //            {
//        //                Console.WriteLine("This edge already exists: " + source + " to " + target);
//        //            }
//        //        }
//        //        else
//        //        {
//        //            var hs = new HashSet<int>();
//        //            hs.Add(target);
//        //            Adj.Add(source, hs);
//        //        }
//        //    }

//        //    public void BFSWalkWithStartNode(int vertex)
//        //    {
//        //        var visited = new HashSet<int>();
//        //        // Mark this node as visited
//        //        visited.Add(vertex);
//        //        // Queue for BFS
//        //        var q = new Queue<int>();
//        //        // Add this node to the queue
//        //        q.Enqueue(vertex);

//        //        while (q.Count > 0)
//        //        {
//        //            var current = q.Dequeue();
//        //            Console.WriteLine(current);
//        //            // Only if the node has a any adj notes
//        //            if (Adj.ContainsKey(current))
//        //            {
//        //                // Iterate through UNVISITED nodes
//        //                foreach (int neighbour in Adj[current].Where(a => !visited.Contains(a)))
//        //                {
//        //                    visited.Add(neighbour);
//        //                    q.Enqueue(neighbour);
//        //                }
//        //            }
//        //        }
//        //    }

//        //    public int BFSFindNodeWithStartNode(int vertex, int lookingFor)
//        //    {
//        //        if (vertex == lookingFor)
//        //        {
//        //            Console.WriteLine("Found it!");
//        //            Console.WriteLine("Steps Took: 0");
//        //            return 0;
//        //        }
//        //        var visited = new HashSet<int>();
//        //        // Mark this node as visited
//        //        visited.Add(vertex);
//        //        // Queue for BFS
//        //        var q = new Queue<int>();
//        //        // Add this node to the queue
//        //        q.Enqueue(vertex);

//        //        int count = 0;

//        //        while (q.Count > 0)
//        //        {
//        //            var current = q.Dequeue();
//        //            Console.WriteLine(current);
//        //            if (current == lookingFor)
//        //            {
//        //                Console.WriteLine("Found it!");
//        //                Console.WriteLine("Steps Took: " + count);
//        //                return visited.Count();
//        //            }

//        //            // Only if the node has a any adj notes
//        //            if (Adj.ContainsKey(current))
//        //            {
//        //                // Iterate through UNVISITED nodes
//        //                foreach (int neighbour in Adj[current].Where(a => !visited.Contains(a)))
//        //                {
//        //                    visited.Add(neighbour);
//        //                    q.Enqueue(neighbour);
//        //                }
//        //            }
//        //            count++;
//        //        }
//        //        Console.WriteLine("Could not find node!");
//        //        return count;
//        //    }

//        //    public void DFSWalkWithStartNode(int vertex)
//        //    {
//        //        var visited = new HashSet<int>();
//        //        // Mark this node as visited
//        //        visited.Add(vertex);
//        //        // Stack for DFS
//        //        var s = new Stack<int>();
//        //        // Add this node to the stack
//        //        s.Push(vertex);

//        //        while (s.Count > 0)
//        //        {
//        //            var current = s.Pop();
//        //            Console.WriteLine(current);
//        //            // ADD TO VISITED HERE
//        //            if (!visited.Contains(current))
//        //            {
//        //                visited.Add(current);
//        //            }
//        //            // Only if the node has a any adj notes
//        //            if (Adj.ContainsKey(current))
//        //            {
//        //                // Iterate through UNVISITED nodes
//        //                foreach (int neighbour in Adj[current].Where(a => !visited.Contains(a)))
//        //                {
//        //                    visited.Add(neighbour);
//        //                    s.Push(neighbour);
//        //                }
//        //            }
//        //        }
//        //    }

//        //    public void DFSWithRecursion(int vertex)
//        //    {
//        //        var visited = new HashSet<int>();
//        //        Traverse(vertex, visited);
//        //    }

//        //    private void Traverse(int v, HashSet<int> visited)
//        //    {
//        //        // Mark this node as visited
//        //        visited.Add(v);
//        //        Console.WriteLine(v);
//        //        // Only if the node has a any adj notes
//        //        if (Adj.ContainsKey(v))
//        //        {
//        //            // Iterate through UNVISITED nodes
//        //            foreach (int neighbour in Adj[v].Where(a => !visited.Contains(a)))
//        //            {
//        //                Traverse(neighbour, visited);
//        //            }
//        //        }
//        //    }
//        //}
//    }
//}

using System;
using System.Collections.Generic;
using System.Xml;

namespace org.doublecloud
{
    class XmlParsingDemo
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("test.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/book");

            List<Book> books = new List<Book>();

            foreach (XmlNode node in nodes)
            {
                Book book = new Book();

                book.author = node.SelectSingleNode("author").InnerText;
                book.title = node.SelectSingleNode("title").InnerText;
                book.id = node.Attributes["id"].Value;

                books.Add(book);
            }

            System.Console.WriteLine("Total books: " + books.Count);
        }
    }

    class Book
    {
        public string id;
        public string title;
        public string author;
    }
}
