using System;
using System.Collections.Generic;

public class CrystalBillBoard : BillBoardWidget
{
    public override void onAwake()
    {
        base.onAwake();
        this.dictParts.Add(PartType.namePart, new CrystalNamePart(PartType.namePart));
        this.dictParts.Add(PartType.bloodPart, new CrystalBloodPart(PartType.bloodPart));
    }
}

