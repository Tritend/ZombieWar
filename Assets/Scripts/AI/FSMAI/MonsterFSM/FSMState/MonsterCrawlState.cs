

using UnityEngine;

public class MonsterCrawlState:FSMState
{
    private EntityDynamicActor dyAgent = null;
    private float minDis;
    public MonsterCrawlState(BaseEntity agent) : base(agent)
    {
    }

    public override void onEnter()
    {
        base.onEnter();
        dyAgent = this.agent as EntityDynamicActor;
        if (dyAgent != null)
        {
            dyAgent.anim.CrossFade("crawl", 0.3f);
            dyAgent.navAgent.speed = 3f;
            dyAgent.navAgent.Resume();
        }
        minDis = dyAgent.SonType == EntitySonType.first ? 10f : 2f;
    }

    public override void setStateInfo()
    {
        this.SType =StateType.crawl;
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
