using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : EnemyBase
{
    [SerializeField]
    private AudioClip deadAudio;
    protected static GameObject hitFX;


    public SoulMonster()
    {
        unitInfo.attackDamage = 10;
        unitInfo.nowHp = unitInfo.maxHp = 100;
        unitInfo.attackDistance = 1f;
    }

    protected override bool SimpleMove()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "MonStand01"
            || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "MonRun")
        {
            enemyCtrl.SimpleMove(transform.forward * unitInfo.moveSpeed);
            return true;
        }
        return false;
    }

    protected override void Attack()
    {
        anim.SetTrigger("attack");
    }

    public override bool TakeDamage(float damage)
    {
        var tf = base.TakeDamage(damage);
        if (tf)
        {
            ProduceHitEffect();
        }
        return tf;
    }

    public void HitDamage1()
    {
        Collider[] enemyList = Physics.OverlapSphere(transform.position, unitInfo.attackDistance, LayerMask.GetMask(Tags.player));
        foreach (var target in enemyList)
        {
            Vector3 temVec = target.transform.position - transform.position;
            Vector3 norVec = transform.rotation * Vector3.forward;//此处*5只是为了画线更清楚,可以不要
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
            if (angle <= 30)
            {
                target.GetComponent<FightPlayer>().TakeDamage(unitInfo.attackDamage);
                break;
            }
        }
    }

    public override void Dead()
    {
        AudioSource.PlayClipAtPoint(deadAudio, transform.position);
        base.Dead();
    }

    protected virtual bool InitEffect(string effectName)
    {
        if (!hitFX)
        {
            hitFX = Resources.Load<GameObject>(effectName);
            return true;
        }
        return false;
    }

    protected virtual void ProduceHitEffect()
    {
        InitEffect("Effects/HitMonster");
        if (hitFX)
        {
            var effect = Instantiate(hitFX, transform);
        }
    }
}
