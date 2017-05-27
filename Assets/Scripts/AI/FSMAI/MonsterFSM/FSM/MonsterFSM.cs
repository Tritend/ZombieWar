using System;
using System.Collections.Generic;

public class MonsterFSM : FSM
{
    public MonsterFSM(BaseEntity agent) : base(agent)
    {
    }

    public override void init()
    {
        this.dictStates.Add(StateType.idle, new MonsterIdleState(this.agent));
        this.dictStates.Add(StateType.run, new MonsterRunState(this.agent));
        this.dictStates.Add(StateType.attack, new MonsterAttackState(this.agent));
        this.dictStates.Add(StateType.die, new MonsterDieState(this.agent));
        this.dictStates.Add(StateType.beHit, new MonsterBeHitState(this.agent));
        this.dictStates.Add(StateType.spawn, new MonsterSpawnState(this.agent));
        this.dictStates.Add(StateType.zombieRun, new MonsterZombieRunState(this.agent));
        this.dictStates.Add(StateType.shamble, new MonsterShambleState(this.agent));
        this.dictStates.Add(StateType.crawl, new MonsterCrawlState(this.agent));
    }
}

