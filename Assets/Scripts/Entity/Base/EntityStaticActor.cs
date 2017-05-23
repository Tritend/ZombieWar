using System;
using System.Collections.Generic;
using UnityEngine;

//静态实体
public class EntityStaticActor : BaseEntity
{
    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        //创建姓名版 血条等..
        BillBoard = this.CacheObj.AddComponent<CrystalBillBoard>();
        BillBoard.onCreate(this.info);

    }

    public virtual void onEnter(BaseEntity entity)
    {

    }
}

