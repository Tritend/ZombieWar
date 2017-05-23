using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : BaseWeapon
{
    private GameObject fakeArrow;
    private float pullDis;
    private Transform bowString;
    private bool isGiveUP = false;

    public override void onStart()
    {
        fakeArrow = this.transform.Find("Bow/main/string/Arrow").gameObject;
        bowString = this.transform.Find("Bow/main/string");
        setArrowActive(false);
    }

    public override void resetTrans()
    {
        agent.setLeftWeapon(this);
        this.transform.SetParent(agent.LeftHand);
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        this.transform.localEulerAngles = new Vector3(90, 0, 180);
    }

    public override void onFire()
    {
        setArrowActive(true);
    }
    public override void bowOnFire()
    {
        if (isGiveUP)
        {
            isGiveUP = false;
            return;
        }
        setArrowActive(false);
        GameObject go = MonoBehaviour.Instantiate(fakeArrow) as GameObject;
        go.SetActive(true);
        go.transform.position = this.transform.position;
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.1f);
        go.transform.rotation = this.transform.rotation;
        go.AddComponent<BoxCollider>();
        go.AddComponent<Rigidbody>().AddForce(this.transform.forward * pullDis * -600);
        BulletArrow arrow = go.AddComponent<BulletArrow>();
        arrow.setAgent(this.agent, this.info);
        bowString.localPosition = new Vector3(0, -1.4f, 0);
    }

    private void setArrowActive(bool b)
    {
        if (fakeArrow != null)
        {
            fakeArrow.SetActive(b);
        }
    }

    public override void bowOnPull(float dis)
    {
        if (isGiveUP)
        {
            bowString.localPosition = new Vector3(0, -1.4f, 0);
            return;
        }
        this.pullDis = -dis * 10;
        pullDis = pullDis < -4.4f ? -4.4f : pullDis;
        if (dis < 0 || pullDis > -1.4f)
        {
            agent.bowGiveUP();
        }
        bowString.localPosition = new Vector3(0, pullDis, 0);
    }
    //放弃
    public void onGiveUP()
    {
        isGiveUP = true;
        setArrowActive(false);
        bowString.localPosition = new Vector3(0, -1.4f, 0);
    }

}

