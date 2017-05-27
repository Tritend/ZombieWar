using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMeunUI : BaseUI
{
    private GameObject slot;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.mainMeun;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {
        slot = this.cacheTrans.Find("content/meunItem").gameObject;
        slot.SetActive(false);
    }

    public override void refreshUI()
    {
        MainMeunData dt = this.data as MainMeunData;
        if (dt != null)
        {
            for (int i = 0; i < dt.ItemLst.Count; i++)
            {
                GameObject go = MonoBehaviour.Instantiate(slot, slot.transform.parent) as GameObject;
                go.SetActive(true);
                MainMeunItemUI item = go.GetComponent<MainMeunItemUI>();
                if (item == null)
                {
                    item = go.AddComponent<MainMeunItemUI>();
                    item.setData(dt.ItemLst[i]);
                }
            }
        }
    }

}

