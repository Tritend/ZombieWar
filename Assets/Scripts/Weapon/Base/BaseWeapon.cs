using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    protected EntityMainPlayer agent;
    private GameObject bullet;
    private Transform firePoint;
    private ParticleSystem fxSystem;
    protected WeaponInfo info;

    private void Start()
    {
        onStart();
    }

    public virtual void onStart()
    {
        bullet = this.transform.Find("model/bullet").gameObject;
        firePoint = this.transform.Find("model/firePoint");
        fxSystem = this.transform.Find("model/firePoint/fireFX").GetComponent<ParticleSystem>();
        bullet.SetActive(false);
    }

    public virtual void onDispose()
    {
        Destroy(this);
        Destroy(this.gameObject);
    }
    //武器信息 伤害 花费
    public virtual void setInfo(WeaponInfo info)
    {
        this.info = info;
    }
    //设置agent(武器的持有者实体)
    public virtual void setAgent(BaseEntity entity)
    {
        if (entity != null)
        {
            agent = entity as EntityMainPlayer;
            resetTrans();
        }
    }
    //重置位置
    public virtual void resetTrans()
    {
        agent.setRightWeapon(this);
        this.transform.SetParent(agent.RightHand);
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    //是否可以使用
    public virtual bool isCanUse()
    {
        return true;
    }
    //普通的武器 射击
    public virtual void onFire()
    {
        SoundMgr.Instance.playAudioPoint("fire",this.transform.position);

        fxSystem.Play();
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 300, 1 << 8 | 1 << 9 | 1 << 10))
        {
            if (hitInfo.collider.tag == "Wall")
            {
                EffectInfo info = new EffectInfo(hitInfo.point, new Vector3(180, 0, 0), hitInfo.collider.transform);
                EffectMgr.Instance.createEffect(10001, info);
                EffectInfo info2 = new EffectInfo(hitInfo.point, new Vector3(180, 0, 0), hitInfo.collider.transform);
                EffectMgr.Instance.createEffect(10004, info2);
            }
            else if (hitInfo.collider.tag == "Ground")
            {
                //值传递  引用传递
                EffectInfo info = new EffectInfo(hitInfo.point, new Vector3(-90, 0, 0), hitInfo.collider.transform);
                EffectMgr.Instance.createEffect(10002, info);
                EffectInfo info2 = new EffectInfo(hitInfo.point, new Vector3(-90, 0, 0), hitInfo.collider.transform);
                EffectMgr.Instance.createEffect(10003, info2);
            }
            else if (hitInfo.collider.gameObject.GetComponent<EntityMonster>() != null)
            {
                EffectInfo info = new EffectInfo(new Vector3(hitInfo.point.x, hitInfo.point.y - 1f, hitInfo.point.z), new Vector3(0, 0, 0), hitInfo.collider.transform);
                EffectMgr.Instance.createEffect(10007, info);
                int damage = (int)(this.info.BaseDamage + this.info.AddDamage * this.agent.getValue(BType.energy) / 100);
                hitInfo.collider.gameObject.GetComponent<EntityMonster>().onDamage(damage);
            }

        }
    }
    //弓箭射击
    public virtual void bowOnFire()
    {

    }
    //拉弓 定义到子类 强转也可以
    public virtual void bowOnPull(float dis)
    {

    }
}

