using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletArrow : BaseBullet
{

    private bool isUsed = false;
    private float lifeTime = 2f;

    public override void onStart()
    {
        this.gameObject.layer = 13;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0 && !isUsed)
        {
            onExp();
        }
    }


    public override void onBulletCrash(Collision collision)
    {
        onExp();
    }

    //爆炸
    private void onExp()
    {
        if (isUsed)
        {
            return;
        }
        isUsed = true;
        EffectMgr.Instance.createEffect(10005, new EffectInfo(this.transform.position));
        //Collider[] cols = Physics.OverlapSphere(this.transform.position, 10);
        //for (int i = 0; i < cols.Length; i++)
        //{

        //}
        List<BaseEntity> lst = EntityMgr.Instance.getEntityByType(EntityType.monster);
        int damage = (int)(this.info.BaseDamage + this.info.AddDamage * this.agent.getValue(BType.energy) / 100);
        for (int i = 0; i < lst.Count; i++)
        {
            if (Vector3.Distance(this.transform.position, lst[i].CacheTrans.position) < 4.5f)
                lst[i].onDamage(damage);
        }
        DestroyObject(this.gameObject, 1f);
    }

}

