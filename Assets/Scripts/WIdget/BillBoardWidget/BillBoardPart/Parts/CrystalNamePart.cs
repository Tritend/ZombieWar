using System;
using System.Collections.Generic;
using UnityEngine;

public class CrystalNamePart : NamePart
{
    public CrystalNamePart(PartType type) : base(type)
    {
    }


    public override void create(Transform parent, EntityInfo info)
    {
        base.create(parent, info);
        this.mesh.fontSize = 280;
        this.mesh.fontStyle =FontStyle.Bold;
        part.transform.localPosition = new Vector3(0, 1.2f, 0);
    }
}

