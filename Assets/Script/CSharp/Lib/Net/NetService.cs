using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Script.CSharp.Lib.Net
{
    public class NetService : Singleton<NetService>
    {
        // private TcpClient socketConnection;
        private Thread m_kNetThread;
        private CSConnection m_kCSConnection;

        public NetService()
        {
            m_kNetThread = new Thread(new ThreadStart(RecvData))
            {
                IsBackground = true
            };

            m_kCSConnection = new CSConnection(ServerInfo.ms_strServerIp, ServerInfo.ms_iServerPort);
        }

        public void Working()
        {
            // 判断连接是否成功
            if (!m_kCSConnection.Connect())
            {
                Debug.LogError("Connect to server failed.");
                return;
            }
            
            // 开启网络线程
            m_kNetThread.Start();
        }

        private void RecvData()
        {
            try
            {
                Byte[] kRecvBytes = new Byte[NetDefine.RecvBufferSize];
                int iRecvByteNum;
                while (true)
                {
                    do
                    {
                        iRecvByteNum = m_kCSConnection.GetSocket().RecvData(ref kRecvBytes);
                        if (iRecvByteNum == -1)
                        {
                            Debug.LogError("Receive data failed.");
                            break;
                        }

                        if (iRecvByteNum == 0)
                        {
                            Debug.Log("Receive data failed.");
                            m_kCSConnection.GetSocket().Close();
                            break;
                        }

                        m_kCSConnection.GetRecvBuffer().AddBuffer(kRecvBytes, (uint)iRecvByteNum);

                    } while (iRecvByteNum == NetDefine.RecvBufferSize);
                    
                    // 处理接收到的数据
                    // HandleRecvData();
                }
            }
            catch (SocketException kSocketException)
            {
                Debug.Log("Socket exception: " + kSocketException);
            }
        }

        // 发送数据到服务器
        public void SendMsgToServer(byte[] kBytes, uint iLen)
        {
            if (!m_kCSConnection.SendMsg(kBytes, iLen))
            {
                Console.Write("Send message to server failed.");
            }
        }

        // 当Unity客户端关闭时，关闭TCP连接和线程
        public void Dispose()
        {
            m_kCSConnection?.Close();
            m_kNetThread?.Abort();
        }
    }
}