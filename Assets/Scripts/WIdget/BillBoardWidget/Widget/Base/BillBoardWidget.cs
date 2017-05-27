using System;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardWidget : MonoBehaviour
{
    protected Dictionary<PartType, BasePart> dictParts = null;
    private GameObject billBoardObj = null;
    protected GameObject BillBoradObj
    {
        get
        {
            if (billBoardObj == null)
            {
                billBoardObj = new GameObject("billBorad");
                billBoardObj.transform.SetParent(this.transform);
                billBoardObj.transform.localPosition = new Vector3(0, 0, 0);
                billBoardObj.transform.localScale = new Vector3(1, 1, 1);
            }
            return billBoardObj;
        }
        set
        {
            billBoardObj = value;
        }
    }

    private void Awake()
    {
        dictParts = new Dictionary<PartType, BasePart>();
        onAwake();
    }
    public virtual void onAwake()
    {

    }

    void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {

    }
    private Quaternion rot = Quaternion.identity;
    private void Update()
    {
        if (Camera.main != null)
        {
            rot = Camera.main.transform.rotation;
            rot.x = 0;
            rot.z = 0;
            BillBoradObj.transform.rotation = Quaternion.Lerp(BillBoradObj.transform.rotation, rot, 0.3f);
        }
    }

    //创建所有的part
    public virtual void onCreate(EntityInfo info)
    {
        foreach (KeyValuePair<PartType, BasePart> item in dictParts)
        {
            item.Value.create(BillBoradObj.transform, info);
        }
    }
    //设置part的string接口
    public virtual void setStrByType(PartType type, string str)
    {
        if (dictParts.ContainsKey(type))
        {
            dictParts[type].setStr(str);
        }
    }
    //设置part的float接口
    public virtual void setFloatByType(PartType type, float rate)
    {
        if (dictParts.ContainsKey(type))
        {
            dictParts[type].setFloat(rate);
        }
    }

}

