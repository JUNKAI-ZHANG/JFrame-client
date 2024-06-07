/*************************************************************
 * Author    :   Bocchi
 * Mail      :   JenksZhang@gmail.com
 * Date      :   2024-06-03
 * Brief     :   消息头定义
 ************************************************************/
 
/*
msg head total : 32 bytes
-------------------------------------
| player_guid : 8 bytes             | // 玩家GUID
-------------------------------------
| area_id : 4 bytes                 | // 区服ID
-------------------------------------
| msg_id : 4 bytes                  | // 消息ID
-------------------------------------
| msg_len : 4 bytes                 | // 消息长度, 包括消息头
-------------------------------------
| msg_type : 2 bytes                | // C2S / S2C / S2S
-------------------------------------
| msg_src : 2 bytes                 | // 消息来源, 0:客户端, 1~n:服务端
-------------------------------------
| msg_seq : 4 bytes                 | // 消息序列号
-------------------------------------
| msg_time : 4 bytes                | // 消息时间戳
-------------------------------------
|                                   |
|            IMessage               |
|                                   |
-------------------------------------
*/

using System;

namespace Script.CSharp.Lib.Net
{
    public class MessageHead
    {
        public ulong m_lPlayerGuid; // 玩家GUID
        public uint m_iAreaId; // 区服ID
        public uint m_iMsgId; // 消息ID
        public uint m_iMsgLen; // 消息长度, 包括消息头
        public ushort m_iMsgType; // 消息类型
        public ushort m_iMsgSrc; // 消息来源
        public uint m_iMsgSeq; // 消息序列号
        public uint m_iMsgTime; // 消息时间戳
        
        public MessageHead()
        {
        }
        
        public uint GetMessageHeadLen()
        {
            return MESSAGE_HEAD_LEN;
        }

        public byte[] GenMessageHeadBytes()
        {
            byte[] kMsgHeadBytes = new byte[MESSAGE_HEAD_LEN];
            byte[] kPlayerGuidBytes = BitConverter.GetBytes(m_lPlayerGuid);
            byte[] kAreaIdBytes = BitConverter.GetBytes(m_iAreaId);
            byte[] kMsgIdBytes = BitConverter.GetBytes(m_iMsgId);
            byte[] kMsgLenBytes = BitConverter.GetBytes(m_iMsgLen);
            byte[] kMsgTypeBytes = BitConverter.GetBytes(m_iMsgType);
            byte[] kMsgSrcBytes = BitConverter.GetBytes(m_iMsgSrc);
            byte[] kMsgSeqBytes = BitConverter.GetBytes(m_iMsgSeq);
            byte[] kMsgTimeBytes = BitConverter.GetBytes(m_iMsgTime);

            Array.Copy(kPlayerGuidBytes, 0, kMsgHeadBytes, 0, 8);
            Array.Copy(kAreaIdBytes, 0, kMsgHeadBytes, 8, 4);
            Array.Copy(kMsgIdBytes, 0, kMsgHeadBytes, 12, 4);
            Array.Copy(kMsgLenBytes, 0, kMsgHeadBytes, 16, 4);
            Array.Copy(kMsgTypeBytes, 0, kMsgHeadBytes, 20, 2);
            Array.Copy(kMsgSrcBytes, 0, kMsgHeadBytes, 22, 2);
            Array.Copy(kMsgSeqBytes, 0, kMsgHeadBytes, 24, 4);
            Array.Copy(kMsgTimeBytes, 0, kMsgHeadBytes, 28, 4);

            return kMsgHeadBytes;
        }
        
        private static readonly uint MESSAGE_HEAD_LEN = 32;
    }
}