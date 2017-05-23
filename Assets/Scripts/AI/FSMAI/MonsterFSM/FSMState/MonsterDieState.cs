using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : FSMState
{
    private float duration = 1.2f;
    public MonsterDieState(BaseEntity agent) : base(agent)
    {
    }

    public override void setStateInfo()
    {
        this.SType = StateType.die;
        allowLst.Add(StateType.none);
    }

    public override void onEnter()
    {
        base.onEnter();
        EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
        if (dyAgent != null)
        {
            dyAgent.anim.CrossFade("fallBack", 0.1f);
            dyAgent.navAgent.Stop();
        }
    }

    public override void onUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            EntityMgr.Instance.removeEntity(this.agent);
        }
    }

}

