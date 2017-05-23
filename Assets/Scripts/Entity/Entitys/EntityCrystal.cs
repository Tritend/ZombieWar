using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityCrystal : EntityStaticActor
{
    public override void onStart()
    {
        base.onStart();
        this.CacheObj.layer = 12;
    }


    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        //通知UI刷新
        sendHpMsg();
    }

    private void sendHpMsg()
    {
        Message msg = new Message(MsgCmd.On_Crystal_HP_Change, this);
        msg["id"] = this.UID;
        msg["hp"] = this.HP;
        msg["orgHP"] = this.OrgHP;
        msg.Send();
    }

    public override void onEnter(BaseEntity entity)
    {
        onChangeColor();
        //进入水晶 水晶生命值减少     
        onDamage(entity.HP * 0.1f);
        //进入水晶 销毁实体
        EntityMgr.Instance.removeEntity(entity);
        sendHpMsg();
    }

    public override void onDamage(float damage)
    {
        base.onDamage(damage);
        this.HP -= damage;
        if (this.HP <= 0)
        {
            EntityMgr.Instance.removeEntity(this);
        }
    }
}

