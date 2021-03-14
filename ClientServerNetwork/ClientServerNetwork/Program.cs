using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;

namespace ClientServerNetwork
{
    class Program
    {
        public static int Main(string[] args)
        {
            StartServer();
            return 0;
        }
        public static void StartServer()
        {
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);


            try
            {

                // Create a Socket that will use Tcp protocol      
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                // Incoming data from the client.    
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (!string.IsNullOrEmpty(data))
                    {
                        break;
                    }
                }

                Console.WriteLine("Text received : {0}", data);

                string response;
                if (data.Contains("order"))
                {
                    response = getOrderedNumbers(data.Replace("order ", "").Split(',').Select(Int32.Parse).ToArray());
                }
                else if (data.Contains("fibonacci"))
                {
                    response = getFibonacci(Int32.Parse(data.Replace("fibonacci ", "")));
                }
                else
                {
                    response = "Unknown command!";
                }

                byte[] msg = Encoding.ASCII.GetBytes(response);
                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }

        private static string getOrderedNumbers(int[] numbers)
        {
            Array.Sort(numbers);
            return string.Join(",", numbers);
        }
        private static string getFibonacci(int len)
        {
            string response= "0, 1";
            int a = 0, b = 1, c = 0;
            for (int i = 2; i < len; i++)
            {
                c = a + b;
                response += $", {c}";
                a = b;
                b = c;
            }
            return response;
        }

    }
}
