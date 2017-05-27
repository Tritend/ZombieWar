using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnState : FSMState
{
    private float duration = 2f;

    public MonsterSpawnState(BaseEntity agent) : base(agent)
    {
    }

    public override void setStateInfo()
    {
        this.SType = StateType.spawn;
        allowLst.Add(StateType.idle);
        allowLst.Add(StateType.beHit);
        allowLst.Add(StateType.die);
    }

    public override void onEnter()
    {
        base.onEnter();
        EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
        if (dyAgent != null)
        {
            dyAgent.anim.CrossFade("standUp", 0.1f);
            dyAgent.navAgent.Stop();
        }
    }

    public override void onUpdate()
    {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            EntityDynamicActor dyAgent = this.agent as EntityDynamicActor;
            if (dyAgent != null)
            {
                dyAgent.onChangeState(StateType.idle);
            }
        }
    }

}
