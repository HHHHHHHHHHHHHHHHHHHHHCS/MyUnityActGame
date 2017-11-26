using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBoss : EnemyBase
{
    protected static GameObject hitFX;

    public SoulBoss()
    {
        unitInfo.nowHp = unitInfo.maxHp = 200;
        unitInfo.attackDistance = 2f;
    }

    protected override bool SimpleMove()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BossStand01"
            || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BossRun01")
        {
            enemyCtrl.SimpleMove(transform.forward * unitInfo.moveSpeed);
            return true;
        }
        return false;
    }

    protected override void Attack()
    {
        int num = Random.Range(0, 2);
        if (num == 0)
        {
            anim.SetTrigger("attack1");
        }
        else
        {
            anim.SetTrigger("attack2");
        }
    }

    public override bool TakeDamage(float damage)
    {
        var tf = base.TakeDamage(damage);
        if(tf)
        {
            ProduceHitEffect();
        }
        return tf;
    }

    public override void Dead()
    {
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
        InitEffect("Effects/HitBoss");
        if (hitFX)
        {
            var effect = Instantiate(hitFX, transform);
        }
    }
}
