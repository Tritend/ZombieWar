using System;
using System.Collections.Generic;
using UnityEngine;

public class BloodPart : BasePart
{
    protected SpriteRenderer bloodSp;
    protected SpriteRenderer bgBloodSp;
    protected GameObject bgPart;

    public BloodPart(PartType type) : base(type)
    {
    }

    public override void create(Transform parent, EntityInfo info)
    {
        bgPart = new GameObject(this.PType.ToString() + "bg");
        bgPart.transform.SetParent(parent);
        bgPart.transform.localPosition = new Vector3(0, 2.05f, 0.15f);
        bgPart.transform.localScale = new Vector3(1.01f, 0.15f, 1);
        bgBloodSp = bgPart.AddComponent<SpriteRenderer>();
        bgBloodSp.sprite = SpriteMgr.Instance.getSprite("whiteblood2");
        bgBloodSp.color = new Color(0, 0, 0, 0.15f);
        bgBloodSp.sortingOrder = 0;

        part = new GameObject(this.PType.ToString());
        part.transform.SetParent(parent);
        part.transform.localPosition = new Vector3(0, 2.05f, 0.15f);
        part.transform.localScale = new Vector3(1f, 0.1f, 1);
        bloodSp = part.AddComponent<SpriteRenderer>();
        bloodSp.sprite = SpriteMgr.Instance.getSprite("whiteblood2");
        bloodSp.color = Color.red;
        bloodSp.sortingOrder = 1;
    }

    public override void setFloat(float rate)
    {
        if (part != null)
        {
            // 1  0 
            //0.6 -0.2
            //0.5 -0.25
            //0.1 -0.45

            //0.1 * -0.05
            float end = (1 - rate) / 0.1f * -0.05f;
            part.transform.localScale = new Vector3(rate, 0.1f, 1f);
            part.transform.localPosition = new Vector3(end, 2.05f, 0.15f);
        }
    }

}

