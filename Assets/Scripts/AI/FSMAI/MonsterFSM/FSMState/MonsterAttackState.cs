using System;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : FSMState
{
    private EntityDynamicActor dyAgent = null;
    private float animTime = 0.2f;

    public MonsterAttackState(BaseEntity agent) : base(agent)
    {
    }

    public override void setStateInfo()
    {
        this.SType = StateType.attack;
    }

    public override void onEnter()
    {
        base.onEnter();
        dyAgent = this.agent as EntityDynamicActor;
        onAttack();
    }

    public override void onUpdate()
    {
        animTime -= Time.deltaTime;
        if (animTime < 0)
        {
            if (dyAgent != null)
            {
                dyAgent.onChangeState(StateType.idle);
            }
        }
    }

    public override void onExit()
    {
        base.onExit();
        animTime = 1f;
    }

    private void onAttack()
    {
        if (dyAgent != null)
        {
            dyAgent.navAgent.Stop();
            int num = UnityEngine.Random.Range(1, 3);
            dyAgent.anim.CrossFade("attack" + num, 0.1f);
            Debug.Log("释放技能  id == " + 1001);
        }
    }

}

