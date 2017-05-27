using System;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBillBoard : BillBoardWidget
{
    public override void onAwake()
    {
        base.onAwake();
        this.dictParts.Add(PartType.namePart, new CrystalNamePart(PartType.namePart));
        this.dictParts.Add(PartType.bloodPart, new CrystalBloodPart(PartType.bloodPart));

        BillBoradObj.transform.localPosition = new Vector3(0, -2, 0);
        BillBoradObj.transform.localScale = new Vector3(10, 10, 10);
    }
}

