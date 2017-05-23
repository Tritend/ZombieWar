using System;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBloodPart : BloodPart
{
    public CrystalBloodPart(PartType type) : base(type)
    {
    }

    public override void create(Transform parent, EntityInfo info)
    {
        base.create(parent, info);
        bgPart.transform.localPosition = new Vector3(0, 1.1f, 0);

        part.transform.localPosition = new Vector3(0, 1.1f, 0);
    }


    public override void setFloat(float rate)
    {
        if (part != null)
        {
            float end = (1 - rate) / 0.1f * -0.05f;
            part.transform.localScale = new Vector3(rate, 0.1f, 1f);
            part.transform.localPosition = new Vector3(end, 1.1f, 0);
        }
    }
}

