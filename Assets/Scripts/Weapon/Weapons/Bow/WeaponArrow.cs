using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponArrow : BaseWeapon
{
    private GameObject arrow;
    //搭上弦的距离
    private const float attachDis = 0.2f;

    public override void onStart()
    {
        arrow = this.transform.Find("arrow").gameObject;
    }

    public override void resetTrans()
    {
        agent.setRightWeapon(this);
        this.transform.SetParent(agent.RightHand);
        this.transform.localPosition = new Vector3(0, 0, 0.34f);
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public override bool isCanUse()
    {
        Vector3 rightVec = agent.RightHand.InverseTransformPoint(agent.LeftHand.position);
        float dis = rightVec.magnitude;
        return dis < attachDis;
    }

    public override void onFire()
    {
        //trigger按下 搭上弦
        arrow.SetActive(false);
    }
    public override void bowOnFire()
    {
        arrow.SetActive(true);
    }

    //放弃
    public void onGiveUP()
    {
        arrow.SetActive(true);
    }
}

