using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBeHitState : FSMState
{
    float resetTime = 0.5f;
    public MonsterBeHitState(BaseEntity agent) : base(agent)
    {
    }

    public override void setStateInfo()
    {
        this.SType = StateType.beHit;
        this.allowLst.Add(StateType.idle);
        this.allowLst.Add(StateType.beHit);
        this.allowLst.Add(StateType.die);
    }

    public override void onEnter()
    {
        base.onEnter();
        EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
        if (dyAgent != null)
        {
            dyAgent.navAgent.Stop();
            int num = UnityEngine.Random.Range(1, 3);
            dyAgent.anim.CrossFade("hit" + num, 0.1f);
            dyAgent.onChangeColor();
        }
    }

    public override void onUpdate()
    {
        resetTime -= Time.deltaTime;
        if (resetTime < 0)
        {
            EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
            if (dyAgent != null)
            {
                dyAgent.onChangeState(StateType.idle);
            }
        }
    }

    public override void onExit()
    {
        base.onExit();
        resetTime = 0.5f;
    }

}

