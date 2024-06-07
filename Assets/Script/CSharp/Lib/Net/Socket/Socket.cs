using System;
using System.Net;
using System.Net.Sockets;

namespace Script.CSharp.Lib.Net
{
    public class NetSocket
    {
        private Socket m_kClientSocket;
        
        public NetSocket()
        {
            m_kClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Connect(string ipAddress, int port)
        {
            try
            {
                m_kClientSocket.Connect(IPAddress.Parse(ipAddress), port);
            }
            catch (SocketException kSocketException)
            {
                Console.WriteLine("Socket exception: " + kSocketException);
                return false;
            }

            return true;
        }

        public bool SendData(byte[] data)
        {
            try
            {
                m_kClientSocket.Send(data);
            }
            catch (SocketException kSocketException)
            {
                Console.WriteLine("Socket exception: " + kSocketException);
                return false;
            }

            return true;
        }

        public int RecvData(ref byte[] kRecvBuffer)
        {
            int bytesRead;
            try
            {
                bytesRead = m_kClientSocket.Receive(kRecvBuffer);
            }
            catch (SocketException kSocketException)
            {
                Console.WriteLine("Socket exception: " + kSocketException);
                return -1;
            }

            return bytesRead;
        }

        public bool Close()
        {
            try
            {
                m_kClientSocket.Close();
            }
            catch (SocketException kSocketException)
            {
                Console.WriteLine("Socket exception: " + kSocketException);
                return false;
            }

            return true;
        }
    }
}