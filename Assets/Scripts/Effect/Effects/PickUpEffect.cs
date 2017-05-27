using System;
using UnityEngine;

public class PickUpEffect : BaseEffect
{
    private bool isSetInfo = false;
    private float stopTime = 2f;
    private float duration = 0f;
    public BaseEntity agent;
    private float _randomAngle;
    private float _randomDistance;
    private float _randomHeight;

    private void Update()
    {
        if (!isSetInfo)
            return;
        stopTime -= Time.deltaTime;
        if (stopTime < 0 && agent != null)
        {
            getPos(duration);
            duration += Time.deltaTime;
            if (duration > 1f)
            {
                getPos(duration);
                PoolMgr.Instance.saveObj(this.CacheObj, this.info.config.tempId + this.info.config.path);
                duration = 0;
            }
        }
    }

    //P0起始位置  P1中间点(自己去算) P2终点位置 t时间(0-1)
    //返回点vector3 point = (1-t)^2*P0 + 2*t(1-t)*P1 + t^2*P2
    Vector3 P0;
    Vector3 P1;
    Vector3 P2;
    private void getPos(float t)
    {
        //p0 this.info.initPos p2 agent.CacheTrans.pos P1
        P0 = this.info.initPos;
        P2 = agent.CacheTrans.position;
        //P1 = (P0 - P2) / 2;
        //P1 = new Vector3(P1.x, P1.y + 3f, P1.z);   


        float rid = _randomAngle * Mathf.PI / 180;
        float x = _randomDistance * Mathf.Cos(rid);
        float z = _randomDistance * Mathf.Sin(rid);
        Vector3 endPos = new Vector3(P2.x, P2.y + UnityEngine.Random.Range(0,2f) , P2.z);
        P1 = new Vector3(endPos.x + x, endPos.y + _randomHeight, endPos.z + z);

        this.CacheTrans.position = (1 - t) * (1 - t) * P0 + 2 * t * (1 - t) * P1 + t * t * P2;

    }

    public override void refreshEffect()
    {
        stopTime = this.info.config.life;
        this.CacheTrans.position = info.initPos;
        this.agent = this.info.agent;
        P0 = this.info.initPos;
        P2 = agent.CacheTrans.position;
        //计算贝塞尔曲线p2点位于p3点的偏移弧度
        if (Math.Abs(P1.x - P2.x) < Math.Abs(P1.z - P2.z))
        {
            if (P1.x > P2.x)
            {
                _randomAngle = 0;
            }
            else
            {
                _randomAngle = 180;
            }
        }
        else
        {
            if (P0.z > P2.z)
            {
                _randomAngle = 90;
            }
            else
            {
                _randomAngle = 270; ;
            }
        }

        //计算贝塞尔曲线p2位于p3点的距离
        _randomDistance = Vector3.Distance(P0, P2);
        _randomHeight = UnityEngine.Random.Range(1, 6);
        isSetInfo = true;
    }


}

