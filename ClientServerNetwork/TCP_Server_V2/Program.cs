using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MultiServer
{
    class Program
    {
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 11000;
        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        static void Main()
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine(); // When we press enter close everything
            CloseAllSockets();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server setup complete");
        }

        /// <summary>
        /// Close all connected client (we do not need to shutdown the server socket as its connections
        /// are already closed with the clients).
        /// </summary>
        private static void CloseAllSockets()
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket;

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) // I cannot seem to avoid this (on exit when properly closing sockets)
            {
                return;
            }

            clientSockets.Add(socket);
            Console.WriteLine("Client connected, waiting for request...");
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket current = (Socket)AR.AsyncState;
            int received;

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Client forcefully disconnected");
                // Don't shutdown because the socket may be disposed and its disconnected anyway.
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Received Text: " + text);

            if (text.ToLower().Contains("sort")) // Client requested sort
            {
                Console.WriteLine("Text is a sort command");
                byte[] data = Encoding.ASCII.GetBytes(getOrderedNumbers(text.Replace("sort ", "").Split(',').Select(Int32.Parse).ToArray()));
                current.Send(data);
                Console.WriteLine("Sorted numbers");
            }
            else if (text.ToLower().Contains("fibonacci")) //Client requested fibonacci
            {
                Console.WriteLine("Text is a fibonacci command");
                byte[] data = Encoding.ASCII.GetBytes(getFibonacci(Int32.Parse(text.Replace("fibonacci ", ""))));
                current.Send(data);
                Console.WriteLine("fibonacci generated");
            }
            else if (text.ToLower() == "exit") // Client wants to exit
            {
                // Always Shutdown before closing
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Console.WriteLine("Client disconnected");
                return;
            }
            else
            {
                Console.WriteLine("Text is an invalid request");
                byte[] data = Encoding.ASCII.GetBytes("Unknown command");
                current.Send(data);
                Console.WriteLine("Warning Sent");
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        private static string getOrderedNumbers(int[] numbers)
        {
            Array.Sort(numbers);
            return string.Join(",", numbers);
        }
        private static string getFibonacci(int len)
        {
            string response = "0, 1";
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