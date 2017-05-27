using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonsterZombieRunState : FSMState
{
    private EntityDynamicActor dyAgent = null;
    private float minDis;
    public int speed = 0;
    public MonsterZombieRunState(BaseEntity agent) : base(agent)
    {

    }

    public override void setStateInfo()
    {
        this.SType = StateType.zombieRun;
    }

    public override void onEnter()
    {
        base.onEnter();
        speed = UnityEngine.Random.Range(1, 10);
        dyAgent = this.agent as EntityDynamicActor;
        //随机出来的速度 决定僵尸行进的姿态
        if (dyAgent != null)
        {
            if (speed < 3)
            {
                dyAgent.onChangeState(StateType.shamble);
                return;
            }
            else if (speed >= 3 && speed <= 5)
            {
                dyAgent.onChangeState(StateType.crawl);
                return;
            }
            dyAgent.anim.CrossFade("zombieRun", 0.3f);
            dyAgent.navAgent.speed = speed;
            dyAgent.navAgent.Resume();
        }
        minDis = dyAgent.SonType == EntitySonType.first ? 10f : 2f;
    }
    public override void onUpdate()
    {
        if (dyAgent != null)
        {
            if (dyAgent.Target == null)
            {
                dyAgent.navAgent.Stop();
                dyAgent.onChangeState(StateType.idle);
                return;
            }
            dyAgent.navAgent.SetDestination(dyAgent.Target.CacheTrans.position);
            if (Vector3.Distance(dyAgent.CacheTrans.position, dyAgent.Target.CacheTrans.position) < minDis)
            {
                //第一种僵尸 查找水晶
                //第二种僵尸 查找玩家
                dyAgent.onChangeState(StateType.idle);
                if (dyAgent.SonType == EntitySonType.first)
                {
                    EntityStaticActor ES = dyAgent.Target as EntityStaticActor;
                    if (ES != null)
                    {
                        ES.onEnter(dyAgent);
                    }
                }
            }
        }
    }
}
