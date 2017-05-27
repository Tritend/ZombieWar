using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    protected EffectInfo info;
    private GameObject cacheObj;
    protected GameObject CacheObj
    {
        get
        {
            if (cacheObj == null)
            {
                cacheObj = this.gameObject;
            }
            return cacheObj;
        }
    }
    private Transform cacheTrans;
    protected Transform CacheTrans
    {
        get
        {
            if (cacheTrans == null)
            {
                cacheTrans = this.transform;
            }
            return cacheTrans;
        }
    }

    public void setInfo(EffectInfo info)
    {
        this.info = info;
        if (this.info != null)
        {
            refreshEffect();
        }
    }

    public virtual void refreshEffect()
    {

    }
}

