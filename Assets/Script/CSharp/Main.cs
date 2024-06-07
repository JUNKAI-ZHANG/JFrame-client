/*************************************************************
 * Author    :   Bocchi
 * Mail      :   JenksZhang@gmail.com
 * Date      :   2024-06-03
 * Brief     :   Main
 ************************************************************/

using System;
using System.Threading;
using CSMessage;
using Google.Protobuf;
using Script.CSharp.Lib.Net;
using UnityEngine;

namespace Script.CSharp
{
    public class Main : MonoBehaviour
    {
        void Start()
        {
            // 开启网络模块
            NetService.Instance.Working();
        }
    
        void Update()
        {
            // 休息1s
            Thread.Sleep(1000);
            // 处理网络消息
            // NetService.Instance.HandleMessage();
            MessageHead kMessageHead = new MessageHead()
            {
                m_lPlayerGuid = 7,
                m_iAreaId = 6,
                m_iMsgId = 11,
                m_iMsgLen = 0,
                m_iMsgType = 0,
                m_iMsgSrc = 0,
                m_iMsgSeq = 0,
                m_iMsgTime = 1717670441
            };
            PlayerLoginReq kPlayerLoginReq = new PlayerLoginReq()
            {
                Username = "Bocchi",
                Password = "123"
            };
            NetService.Instance.SendMsgToServer(kMessageHead.GenMessageHeadBytes(), kMessageHead.GetMessageHeadLen());
        }

        private void OnApplicationQuit()
        {
            // 关闭网络模块
            NetService.Instance.Dispose();
        }
    }
}
