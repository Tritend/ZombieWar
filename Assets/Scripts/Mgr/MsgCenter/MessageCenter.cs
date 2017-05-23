using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void MessageEvent(Message msg);

public class MessageCenter : Singleton<MessageCenter>
{
    private Dictionary<string, List<MessageEvent>> m_dictMsgEvent = null;
    public override void init()
    {
        m_dictMsgEvent = new Dictionary<string, List<MessageEvent>>();
    }

    public void addListener(string msgName, MessageEvent msgEvent)
    {
        if (m_dictMsgEvent.ContainsKey(msgName))
        {
            if (!m_dictMsgEvent[msgName].Contains(msgEvent))
                m_dictMsgEvent[msgName].Add(msgEvent);
        }
        else
        {
            List<MessageEvent> list = new List<MessageEvent>();
            list.Add(msgEvent);
            m_dictMsgEvent.Add(msgName, list);
        }
    }

    public void removeListener(string msgName, MessageEvent msgEvent)
    {
        if (m_dictMsgEvent.ContainsKey(msgName))
        {
            if (m_dictMsgEvent[msgName].Contains(msgEvent))
            {
                m_dictMsgEvent[msgName].Remove(msgEvent);
            }
        }
    }
    //移除cmd所有监听
    public void removeAllListenerByName(string msgName)
    {
        if (m_dictMsgEvent.ContainsKey(msgName))
        {
            m_dictMsgEvent[msgName].Clear();
        }
    }
    //移除所有消息监听
    public void removeAllListener(string msgName, MessageEvent msgEvent)
    {
        m_dictMsgEvent.Clear();
    }

    //只带msgName的消息
    public void SendMessage(string name)
    {
        dispatch(new Message(name, this));
    }
    // name sender 
    public void SendMessage(string name, object sender)
    {
        dispatch(new Message(name, sender));
    }
    // name sender content
    public void SendMessage(string name, object sender, object content)
    {
        dispatch(new Message(name, sender, content));
    }
    // name sender content dictParams
    public void SendMessage(string name, object sender, object content, params object[] dictParams)
    {
        dispatch(new Message(name, sender, content, dictParams));
    }
    //带整个msg的消息
    public void SendMessage(Message msg)
    {
        dispatch(msg);
    }

    public void dispatch(Message msg)
    {
        if (m_dictMsgEvent.ContainsKey(msg.Name))
        {
            List<MessageEvent> list = m_dictMsgEvent[msg.Name];
            for (int i = 0; i < list.Count; i++)
            {
                MessageEvent msgEvent = list[i];
                if (msgEvent != null)
                    msgEvent(msg);
            }
        }
    }

}
