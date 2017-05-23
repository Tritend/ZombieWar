using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//动态实体
public class EntityDynamicActor : BaseEntity, ISkill
{
    [HideInInspector]
    public Animation anim = null;
    [HideInInspector]
    public NavMeshAgent navAgent = null;
    [HideInInspector]
    public CharacterController CC = null;
    public BaseEntity Target = null;
    public MonsterFSM fsm = null;
    protected SkillWidget MySkill;

    public StateType SType = StateType.none;

    public override void onStart()
    {
        anim = this.GetComponent<Animation>();
        navAgent = this.CacheObj.AddComponent<NavMeshAgent>();
        navAgent.stoppingDistance = 0.2f;
        navAgent.radius = 0.01f;
        navAgent.baseOffset = -0.1f;
        CC = this.GetComponent<CharacterController>();
    }

    public override void onCreate(EntityInfo data)
    {
        base.onCreate(data);
        //创建姓名版 血条等..
        BillBoard = this.CacheObj.AddComponent<DynamicBillBoard>();
        BillBoard.onCreate(this.info);
        //创建技能管理器
        MySkill = new SkillWidget(this, this.Skills);
    }

    public virtual void onChangeState(StateType type)
    {
        if (this.fsm != null)
        {
            SType = type;
            this.fsm.onChangeState(type);
        }
    }

    public void onReleaseSkill(int skillId)
    {
        if (MySkill.releaseSkill(skillId))
        {
            onChangeState(StateType.attack);
            this.Target.onDamage(12);
        }

    }
}

