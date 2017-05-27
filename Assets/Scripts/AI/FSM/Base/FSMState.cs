using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    none = -1,
    idle,
    run,
    attack,
    beHit,
    die,
    spawn,
    zombieRun,
    shamble,
    crawl,
}

//状态基类
public abstract class FSMState
{
    protected StateType SType;
    protected BaseEntity agent;
    protected List<StateType> allowLst = null;

    public FSMState(BaseEntity agent)
    {
        this.agent = agent;
        allowLst = new List<StateType>();
        setStateInfo();
    }

    //1赋值状态类型 2添加可切换列表
    public abstract void setStateInfo();


    public virtual void onEnter()
    {
        Debug.Log("<color=yellow>进入状态 -> </color>" + this.SType.ToString());
    }

    public virtual void onUpdate()
    {


    }

    public virtual void onExit()
    {
        Debug.Log("<color=yellow>退出状态 -> </color>" + this.SType.ToString());
    }

    //beHit -> attack 
    //当前状态是否可以切换到B状态
    public virtual bool isCanChangeTo(StateType type)
    {
        bool isCan = true;
        if (allowLst != null && allowLst.Count > 0)
        {
            isCan = allowLst.Contains(type) ? true : false;
        }
        return isCan;
    }
}

