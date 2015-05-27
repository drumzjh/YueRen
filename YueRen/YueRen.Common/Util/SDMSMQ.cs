using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace YueRen.Common.Util
{
    /// <summary>
    /// 发送msmq消息类
    /// </summary>
    public class SDMSMQ
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SDMSMQ()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 发送与COM兼容的字符串message，如BSTR
        /// </summary>
        /// <param name="strPath">队列路径</param>
        /// <param name="data">要发送消息数据</param>
        /// <param name="strTitle">消息标题</param>
        public static void SendComStr(string strPath, string data, string strTitle)
        {
            //			if(!MessageQueue.Exists(strPath))
            //				throw new ArgumentException("无效的队列名称！");
            MessageQueue q = new System.Messaging.MessageQueue(strPath);

            q.Formatter = new ActiveXMessageFormatter();

            q.Send(data, strTitle);

        }

        /// <summary>
        /// 超时等待时间为(秒
        /// )
        /// </summary>
        public static int TIME_OUT = 10;




        /// <summary>
        /// 发送msmq消息，指定数据类型，.net专用
        /// </summary>
        /// <param name="strPath">队列路径 如：.\Private$\test</param>
        /// <param name="data">要发送的消息数据</param>
        /// <param name="strTitle">消息标题</param>
        /// <param name="types">消息对象类型</param>
        public static void Send(string strPath, object data, string strTitle, Type[] types)
        {
            //			if(!MessageQueue.Exists(strPath))
            //				throw new ArgumentException("无效的队列名称！");

            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new XmlMessageFormatter(types);

            q.Send(data, strTitle);
        }


        /// <summary>
        /// 以XML字符串格式发送msmq消息，.net专用
        /// </summary>
        /// <param name="strPath">队列路径 如：.\Private$\test</param>
        /// <param name="data">要发送的消息数据</param>
        /// <param name="strTitle">消息标题</param>
        public static void Send(string strPath, string data, string strTitle)
        {
            //	if(!MessageQueue.Exists(strPath))
            //		throw new ArgumentException("无效的队列名称！");

            MessageQueue q = new System.Messaging.MessageQueue(strPath);

            q.Formatter = new XmlMessageFormatter(new string[] { "System.String" });

            q.Send(data, strTitle);
        }

        /// <summary>
        /// 以XML式接收消息，.net专用
        /// </summary>
        /// <param name="strPath">队列路径</param>
        /// <returns>消息征文</returns>
        public static string Recv(string strPath)
        {
            object msg = Recv(strPath, new Type[] { Type.GetType("System.String") });
            if (msg != null)
                return msg.ToString();
            else
                return null;
        }

        /// <summary>
        /// 按指定类型接受消息，.net专用
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static object Recv(string strPath, Type[] types)
        {
            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new XmlMessageFormatter(types);
            //return q.Receive(new TimeSpan(0,TIME_OUT,0)).Body; /* 等待三分钟 */
            return q.Receive().Body;
        }

        /// <summary>
        /// 发送对象，默认xml序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="title"></param>
        public static void SendObject(string path, object data, string title)
        {
            MessageQueue q = new System.Messaging.MessageQueue(path);

            q.Formatter = new XmlMessageFormatter();


            q.Send(data, title);
        }

        /// <summary>
        /// 接受和COM兼容的消息
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string RecvComStr(string strPath)
        {

            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new ActiveXMessageFormatter();

            //q.Formatter= new BinaryMessageFormatter();
            Message msg = q.Receive(new TimeSpan(0, 0, TIME_OUT));
            if (msg != null) return msg.Body as string;
            else
                return string.Empty;
            //return q.Receive(new TimeSpan(0,0,TIME_OUT)).Body; /* 等待三分钟 */
            //return q.Receive().Body.ToString();
        }

        public static Message RecvMSMQ(string strMQPath)
        {
            MessageQueue q = new System.Messaging.MessageQueue(strMQPath);
            q.Formatter = new ActiveXMessageFormatter();


            return q.Receive();
        }


        /// <summary>
        /// 取得所有消息数量，.net专用
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static int GetAllMessageCount(string strPath)
        {

            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            return q.GetAllMessages().Length;

        }

        /// <summary>
        /// 取得和COM兼容的消息数量
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static int GetAllComMsgCount(string strPath)
        {
            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new ActiveXMessageFormatter();
            return q.GetAllMessages().Length;
        }

        /// <summary>
        /// 取得所有消息，但不删除消息
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static System.Messaging.Message[] GetAllMessages(string strPath)
        {

            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            return q.GetAllMessages();

        }

        /// <summary>
        /// 取得所有和com兼容的消息，但不删除消息
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static Message[] GetAllComMsg(string strPath)
        {
            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new ActiveXMessageFormatter();
            return q.GetAllMessages();
        }


        /// <summary>
        /// 接受所有消息，并删除消息
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static ArrayList RecvAllMessage(string strPath)
        {
            ArrayList msgList = new ArrayList();
            msgList = RecvAllMessage(strPath, new XmlMessageFormatter());
            return msgList;
        }

        /// <summary>
        /// 接受所有和com兼容消息，并删除消息
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static ArrayList RecvAllComMsg(string strPath)
        {
            ArrayList msgList = new ArrayList();
            msgList = RecvAllMessage(strPath, new ActiveXMessageFormatter());
            return msgList;
        }

        /// <summary>
        /// 按指定格式格式化接受所有消息
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="formatter"></param>
        /// <returns></returns>
        private static ArrayList RecvAllMessage(string strPath, IMessageFormatter formatter)
        {
            ArrayList msgList = new ArrayList();

            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = formatter;

            MessageEnumerator messages = q.GetMessageEnumerator();
            while (messages.MoveNext())  /* 如果没有消息则退出循环 */
            {
                msgList.Add(messages.RemoveCurrent()); /* 删除队列中的当前消息 */
            }
            return msgList;
        }


        /// <summary>
        /// 察看消息，不删除，XmlMessageFormatter
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static System.Messaging.Message Peek(string strPath)
        {

            MessageQueue q = new System.Messaging.MessageQueue(strPath);
            q.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            return q.Peek(new TimeSpan(0, TIME_OUT, 0)); /* 等待三分钟 */

        }
    }
}

