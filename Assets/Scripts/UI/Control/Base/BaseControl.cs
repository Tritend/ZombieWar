using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class BaseControl
{
    public UIEnum uiEnum;
    public abstract void initListener();
    public abstract void initEnum();

    public virtual void updateUI(BaseData data)
    {
        UIMgr.Instance.openUI(this.uiEnum, data);
    }

}

