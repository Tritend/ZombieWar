using System;
using System.Collections.Generic;

public abstract class FSM
{
    protected BaseEntity agent;
    private FSMState currState;
    private FSMState nextState;
    protected Dictionary<StateType, FSMState> dictStates = null;

    public FSM(BaseEntity agent)
    {
        this.agent = agent;
        dictStates = new Dictionary<StateType, FSMState>();
        init();
    }

    public abstract void init();

    //更新FSM状态
    public void onTick()
    {
        if (nextState != null)
        {
            if (currState != null)
            {
                currState.onExit();
            }
            currState = nextState;
            nextState = null;
            currState.onEnter();
        }
        if (currState != null)
        {
            currState.onUpdate();
        }

    }

    //切换状态
    public void onChangeState(StateType type)
    {
        bool isCan = true;
        if (currState != null)
        {
            isCan = currState.isCanChangeTo(type);
        }
        if (isCan)
        {
            if (dictStates.ContainsKey(type))
            {
                nextState = dictStates[type];
            }
        }
    }
    //1动态的去创建
    //2初始化状态列表
}

