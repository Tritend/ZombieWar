using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnapsackTips : BaseUI
{
    private Text Name;

    public override void resetUIInfo()
    {
        this.uiEnum = UIEnum.knapsackTips;
        this.uiNode = UINode.pop;
    }

    public override void onStart()
    {
        base.onStart();
        Name = this.cacheTrans.Find("itemName").GetComponent<Text>();
    }

    public override void refreshUI()
    {
        base.refreshUI();
        KnapsackItemData dt = this.data as KnapsackItemData;
        if (dt != null)
        {
            Name.text = dt.Name;
            this.cacheTrans.position = dt.Pos;
        }
    }

}

