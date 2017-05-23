using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class BaseUI : MonoBehaviour
{
    public UIEnum uiEnum { get; set; }
    public UINode uiNode { get; set; }
    protected Transform cacheTrans;
    protected GameObject cacheObj;
    protected BaseData data = null;
    protected bool isInit = false;

    public abstract void resetUIInfo();
    public BaseUI()
    {
        resetUIInfo();
    }

    private void Awake()
    {
        onAwake();
    }
    public virtual void onAwake()
    {

    }

    private void Start()
    {
        cacheTrans = this.transform;
        cacheObj = this.gameObject;
        if (this.uiNode != UINode.none)
        {
            this.cacheTrans.SetParent(UIMgr.Instance.getCanvasTrans(this.uiNode));
            this.cacheTrans.localPosition = new Vector3(0, 0, 0);
            this.cacheTrans.localEulerAngles = new Vector3(0, 0, 0);
            this.cacheTrans.localScale = new Vector3(1, 1, 1);
        }
        onStart();
        if (this.data != null)
        {
            refreshUI();
        }
        isInit = true;
    }
    public virtual void onStart()
    {

    }

    public virtual void setData(BaseData data)
    {
        this.data = data;
        if (isInit)
        {
            refreshUI();
        }
    }
    public virtual void setActive(bool b)
    {
        this.cacheObj.SetActive(b);
    }
    //处理UI刷新问题
    public virtual void refreshUI()
    {

    }

    //释放
    private void OnDestroy()
    {
        onDispose();
    }
    public virtual void onDispose()
    {

    }

}

