using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/*
消息体
直接遍历
key访问 message[key]
send()
remove()
add()
构造函数参数=》sender type(name) content
*/


public class Message : IEnumerable<KeyValuePair<string, object>>
{
    private Dictionary<string, object> dictDatas = null;

    public string Name { get; private set; }
    public object Sender { get; private set; }
    public object Content { get; private set; }
    //索引器 实现
    public object this[string key]
    {
        get
        {
            if (dictDatas == null || !dictDatas.ContainsKey(key))
            {
                return null;
            }
            return dictDatas[key];
        }
        set
        {
            if (dictDatas == null)
            {
                dictDatas = new Dictionary<string, object>();
            }
            if (dictDatas.ContainsKey(key))
            {
                dictDatas[key] = value;
            }
            else
            {
                dictDatas.Add(key, value);
            }
        }
    }

    #region 继承ienumerator 实现
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
        if (dictDatas == null)
        {
            yield break;
        }
        foreach (KeyValuePair<string, object> kvp in dictDatas)
        {
            yield return kvp;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return dictDatas.GetEnumerator();
    }
    #endregion

    #region 构造函数
    public Message(string name, object sender)
    {
        Name = name;
        Sender = sender;
        Content = null;
    }
    public Message(string name, object sender, object content)
    {
        Name = name;
        Sender = sender;
        Content = content;
    }
    public Message(string name, object sender, object content, params object[] dictParams)
    {
        Name = name;
        Sender = sender;
        Content = content;
        if (dictParams.GetType() == typeof(Dictionary<string, object>))
        {
            foreach (object item in dictParams)
            {
                foreach (KeyValuePair<string, object> kvp in item as Dictionary<string, object>)
                {
                    //dictDatas[kvp.Key] = kvp.Value 可能dict为空 报错
                    this[kvp.Key] = kvp.Value;
                }
            }
        }
    }
    public Message(Message message)
    {
        Name = message.Name;
        Sender = message.Sender;
        Content = message.Content;
        foreach (KeyValuePair<string, object> kvp in message.dictDatas)
        {
            this[kvp.Key] = kvp.Value;
        }
    }
    #endregion

    //add remove
    public void Add(string key, object value)
    {
        this[key] = value;
    }
    public void Remove(string key)
    {
        if (dictDatas != null && dictDatas.ContainsKey(key))
        {
            dictDatas.Remove(key);
        }
    }

    //send
    public void Send()
    {
        MessageCenter.Instance.SendMessage(this);
    }


}