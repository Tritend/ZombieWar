using UnityEngine;
using System.Collections;



public class AttackInfo
{
    public int skillId;
    public float atkRange;
    public int isHorTest;
    public float horAngle;
    public int isVerTest;
    public float verAngle;

    public AttackInfo()
    {

    }
    public AttackInfo(float range)
    {
        this.atkRange = range;
        this.isHorTest = 0;
        this.horAngle = 0;
        this.isVerTest = 0;
        this.verAngle = 0;
    }
    public AttackInfo(float range, int ishor, float hor, int isver, float ver)
    {
        this.atkRange = range;
        this.isHorTest = ishor;
        this.horAngle = hor;
        this.isVerTest = isver;
        this.verAngle = ver;
    }
}


public class AttackHelper
{
    public static bool isAttacked(BaseEntity source, BaseEntity target, AttackInfo info)
    {
        return isAttacked(source.CacheTrans, target.CacheTrans, info);
    }

    public static bool isAttacked(Transform source, Transform target, AttackInfo info)
    {
        bool isAttacked = false;
        //攻击范围检测
        Vector3 vec = target.position - source.position;
        float len = vec.magnitude;
        if (len >= info.atkRange)
        {
            Debug.Log("<color=red>距离不够  </color>" + vec.magnitude);
            return isAttacked;
        }
        //攻击角度检测(水平检测 垂直检测)
        Vector3 sourceForward = source.TransformDirection(Vector3.forward);
        //水平检测
        if (info.isHorTest == Defines.isTest)
        {
            Vector3 tar = target.position;
            tar.y = source.position.y;
            Vector3 horVec = tar - source.position;
            float ang = calculateAngle(sourceForward, horVec);
            if (ang > info.horAngle / 2)
            {
                Debug.Log("<color=red>水平角度不够  </color>" + ang);
                return isAttacked;
            }
        }
        //垂直检测
        if (info.isVerTest == Defines.isTest)
        {
            float height = Mathf.Abs(target.position.y - source.position.y);
            float sinAng = height / len;
            float ang = 180 / Mathf.PI * Mathf.Asin(sinAng);
            if (ang > info.verAngle / 2)
            {
                Debug.Log("<color=red>垂直角度不够  </color>" + ang);
                return isAttacked;
            }
        }
        Debug.Log("<color=green>可以攻击到敌人 </color>");
        return true;
    }
    //抽出方法 计算角度
    private static float calculateAngle(Vector3 axis, Vector3 orgAndTarget)
    {
        float dot = Vector3.Dot(axis.normalized, orgAndTarget.normalized);
        float ang = 180 / Mathf.PI * Mathf.Acos(dot);
        return ang;
    }
    //public bool isAttacked(Transform source, Transform target, bool isDebug)
    //{
    //    bool isAttacked = false;
    //    //攻击范围检测
    //    Vector3 vec = target.position - source.position;
    //    //攻击角度检测(水平检测 垂直检测)
    //    Vector3 sourceForward = source.TransformDirection(Vector3.forward);
    //    //水平检测
    //    Vector3 tar = target.position;
    //    tar.y = source.position.y;
    //    Vector3 horVec = tar - source.position;
    //    float ang = calculateAngle(sourceForward, horVec);
    //    //垂直检测
    //    float len = vec.magnitude;
    //    float height = Mathf.Abs(target.position.y - source.position.y);
    //    float sinAng = height / len;
    //    float ang2 = 180 / Mathf.PI * Mathf.Asin(sinAng);
    //    LoggerHelper.Instance.logWarnning("<color=red> dis hor  ver  </color>" + vec.magnitude + "  " + ang + "   " + ang2);
    //    return true;
    //}

    /*
     * 客户端AI 攻击计算由本地客户端检测
     * 客户端其他玩家 攻击计算由玩家客户端检测 攻击伤害包服务器下发
     * 客户端主角玩家 攻击由本地客户端检测 
     */

}