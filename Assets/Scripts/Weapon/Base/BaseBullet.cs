using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    protected BaseEntity agent;
    protected WeaponInfo info;

    private void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        onBulletCrash(collision);
    }

    public virtual void onBulletCrash(Collision collision)
    {

    }

    public void setAgent(BaseEntity agent, WeaponInfo info)
    {
        this.agent = agent;
        this.info = info;
    }

}

