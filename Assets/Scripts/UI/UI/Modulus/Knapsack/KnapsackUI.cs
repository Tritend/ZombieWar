using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnapsackUI : BaseUI
{
    private GameObject itemSlot;
    private Transform content;
    private Dictionary<int, KnapsackItem> dictItems;

    public override void resetUIInfo()
    {
        uiEnum = UIEnum.knapsack;
        this.uiNode = UINode.main;
    }

    public override void onStart()
    {
        base.onStart();
        itemSlot = this.cacheTrans.Find("content/itemSlot").gameObject;
        itemSlot.SetActive(false);
        content = this.cacheTrans.Find("content");
        dictItems = new Dictionary<int, KnapsackItem>();
    }


    public override void refreshUI()
    {
        base.refreshUI();
        KnapsackData dt = this.data as KnapsackData;
        if (dt != null)
        {
            List<KnapsackItemData> lst = new List<KnapsackItemData>(dt.dict.Values); //dt.list;
            for (int i = 0; i < lst.Count; i++)
            {
                if (!dictItems.ContainsKey(i))
                {
                    GameObject go = MonoBehaviour.Instantiate(itemSlot, content) as GameObject;
                    go.SetActive(true);
                    KnapsackItem item = go.AddComponent<KnapsackItem>();
                    dictItems.Add(i, item);
                }

                dictItems[i].setData(lst[i]);
            }
        }
    }
}
