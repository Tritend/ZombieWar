using System;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    none = -1,
    namePart,
    hpPart,
    bloodPart,
}

public class BasePart
{
    protected PartType PType;
    protected TextMesh mesh;
    protected string str;
    protected GameObject part;

    public BasePart(PartType type)
    {
        PType = type;
    }

    public virtual void create(Transform parent, EntityInfo info)
    {
        part = new GameObject(this.PType.ToString());
        part.transform.SetParent(parent);
        part.transform.localPosition = new Vector3(0, 2.2f, 0.15f);
        this.mesh = part.AddComponent<TextMesh>();
        this.mesh.anchor = TextAnchor.MiddleCenter;
        this.mesh.characterSize = 0.1f;
        this.mesh.fontSize = 20;
        setStr(info.Name);
    }

    public virtual void setStr(string str)
    {
        this.str = str;
        if (this.mesh != null)
        {
            this.mesh.text = str;
        }
    }
    public virtual void setFloat(float rate)
    {

    }

}

