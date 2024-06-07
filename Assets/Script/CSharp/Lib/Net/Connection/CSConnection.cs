using Script.CSharp.Lib.Utils;

namespace Script.CSharp.Lib.Net
{
    public class CSConnection
    {
        public CSConnection(string strIp, ushort iPort)
        {
            m_strIp = strIp;
            m_iPort = iPort;
            
            m_kNetSocket = new NetSocket();
            m_kRecvBuffer = new RingBuffer();
        }
        
        public bool Connect()
        {
            return m_kNetSocket.Connect(m_strIp, m_iPort);
        }
        
        public NetSocket GetSocket()
        {
            return m_kNetSocket;
        }
        
        public RingBuffer GetRecvBuffer()
        {
            return m_kRecvBuffer;
        }

        public bool SendMsg(byte[] kMsgBytes, uint iLen)
        {
            // 取kMsgBytes的前iLen个字节
            byte[] kTmpMsgBytes = new byte[iLen];
            for (int i = 0; i < iLen; i++)
            {
                kTmpMsgBytes[i] = kMsgBytes[i];
            }
            return m_kNetSocket.SendData(kTmpMsgBytes);
        }

        public void Close()
        {
            m_kNetSocket.Close();
        }
        
        private string m_strIp;
        private ushort m_iPort;
        private ulong m_lConnMs;
        private NetSocket m_kNetSocket;
        
        private RingBuffer m_kRecvBuffer;
    }
}