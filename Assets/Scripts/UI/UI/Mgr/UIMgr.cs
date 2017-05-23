using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIMgr : Singleton<UIMgr>
{

    private Dictionary<UIEnum, BaseUI> uis = null;
    private Transform canvas = null;
    public override void init()
    {
        uis = new Dictionary<UIEnum, BaseUI>();
    }


    public void openUI(UIEnum e, BaseData data)
    {
        BaseUI baseUI = null;
        if (!uis.TryGetValue(e, out baseUI))
        {
            string path = UIPath.getUIPath(e);
            ResMgr.Instance.load(path, (obj) =>
            {
                GameObject go = obj as GameObject;
                if (go != null)
                {
                    baseUI = go.GetComponent<BaseUI>();
                    if (baseUI == null)
                    {
                        Type t = UIPath.getType(e);
                        baseUI = go.AddComponent(t) as BaseUI;
                    }
                    baseUI.setData(data);
                    uis.Add(e, baseUI);
                }
            });
        }
        else
        {
            baseUI.setData(data);
            baseUI.setActive(true);
        }
    }

    public void closeUI(UIEnum e)
    {
        if (uis.ContainsKey(e))
        {
            uis[e].setActive(false);
        }
    }

    //UI是否是打开的
    public bool isOpen(UIEnum e)
    {
        return uis.ContainsKey(e) && uis[e].gameObject.activeSelf;
    }

    //切换场景 清空
    public void onClear()
    {
        this.uis.Clear();
    }

    public Transform getCanvasTrans()
    {
        return getCanvasTrans(UINode.none);
    }
    public Transform getCanvasTrans(UINode node)
    {
        if (canvas == null)
            canvas = GameObject.Find("Canvas").transform;

        Transform trans = null;

        switch (node)
        {
            case UINode.main:
                trans = canvas.transform.Find("Main");
                break;
            case UINode.pop:
                trans = canvas.transform.Find("Pop");
                break;
            case UINode.none:
                trans = canvas;
                break;
            default:
                trans = canvas.transform.Find("Main");
                break;
        }

        return trans;
    }


    public void resetCanvasPos(Vector3 pos, Quaternion rot)
    {
        if (canvas == null)
            canvas = GameObject.Find("Canvas").transform;
        if (canvas != null)
        {
            canvas.transform.position = pos;
            canvas.transform.rotation = rot;
        }
    }

    private StaticCamera staticCamera;
    public void onDamageColor()
    {
        if (staticCamera == null)
        {
            staticCamera = GameObject.Find("StaticCanvas").GetComponent<StaticCamera>();
        }
        if (staticCamera != null)
        {
            staticCamera.onDamage();
        }
    }
    public void setCountDown(int count, string desc)
    {
        if (staticCamera == null)
        {
            staticCamera = GameObject.Find("StaticCanvas").GetComponent<StaticCamera>();
        }
        if (staticCamera != null)
        {
            staticCamera.setCountDown(count, desc);
        }
    }

}
