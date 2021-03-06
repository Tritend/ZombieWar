﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseSceneControl : MonoBehaviour
{
    protected int uid = 1;
    protected SceneInfo info = null;

    private void Awake()
    {
        createEntityPlayer();
        onAwake();
    }

    public virtual void onAwake()
    {

    }

    private void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {

    }

    //创建玩家
    protected void createEntityPlayer()
    {
        EntityMgr.Instance.createEntity(1008611, 1008611);
    }
    //创建房子
    protected IEnumerator createEntityHouse(float timer)
    {
        yield return new WaitForSeconds(timer);
        //for (int i = 0; i < info.LstCrystal.Count; i++)
        //{
        //    EntityMgr.Instance.createEntity(info.LstCrystal[i], uid);
        //}
        EntityMgr.Instance.createEntity(info.HouseId, uid);
        uid++;
    }

    //创建怪
    protected IEnumerator createEntityMonster(float timer)
    {
        while (info.AIWave > 0)
        {
            yield return new WaitForSeconds(timer);
            for (int i = 0; i < info.LstAI.Count; i++)
            {
                EntityMgr.Instance.createEntity(info.LstAI[i], uid);
                uid++;
            }
            info.AIWave--;
        }
        yield break;
    }

    protected void countDown()
    {
        UIMgr.Instance.setCountDown(info.AISpawnTimer, "游戏即将开始...请做好准备");
    }


    public void setData(SceneInfo info)
    {
        this.info = info;
        if (info != null)
        {
            countDown();
            onSetData();
        }
    }

    public virtual void onSetData()
    {

    }
}

