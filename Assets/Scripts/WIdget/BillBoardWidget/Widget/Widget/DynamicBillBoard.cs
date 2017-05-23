using System;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBillBoard : BillBoardWidget
{
    public override void onAwake()
    {
        base.onAwake();
        this.dictParts.Add(PartType.namePart, new NamePart(PartType.namePart));
        this.dictParts.Add(PartType.bloodPart, new BloodPart(PartType.bloodPart));
    }
}

