using Golden.Fish.Core.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static Golden.Fish.Core.CoreDI;

namespace Golden.Fish.Core
{
    public class BaseTcpServer : IServer
    {
        private TcpListener mServer = null;
        private Dictionary<int, Tuple<TcpClient, Thread>> mClients;
        private readonly object mLockingObject = new object();
        private int mLastId;
        private bool mIsRunning = false;

        public void StartListening()
        {
            lock (mLockingObject)
            {
                if (mIsRunning)
                {
                    return;
                }
                mIsRunning = true;
                mClients = new Dictionary<int, Tuple<TcpClient, Thread>>();
                mLastId = 0;
            }
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            mServer = new TcpListener(localAddr, 1234);
            mServer.Start();
            try
            {
                while (mIsRunning)
                {
                    Console.WriteLine("Waiting for a connection...");
                    TcpClient client = mServer.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));
                    t.Start(client);
                    mClients[mLastId++] = new Tuple<TcpClient, Thread>(client, t);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            mServer.Stop();
        }

        public void StopListening()
        {
            lock (mLockingObject)
            {
                if (!mIsRunning)
                {
                    return;
                }
            }
            foreach (var client in mClients)
            {
                try
                {
                    client.Value.Item1.Close();
                    client.Value.Item2.Join();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Logger
                }
            }
        }

        private void HandleDeivce(object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = string.Empty;

            string data = null;
            byte[] bytes = new byte[256];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("{1}: Received: {0}", data, Thread.CurrentThread.ManagedThreadId);

                    string str = "Hey Device!";
                    byte[] reply = System.Text.Encoding.ASCII.GetBytes(str);
                    stream.Write(reply, 0, reply.Length);
                    Console.WriteLine("{1}: Sent: {0}", str, Thread.CurrentThread.ManagedThreadId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }
    }
}
