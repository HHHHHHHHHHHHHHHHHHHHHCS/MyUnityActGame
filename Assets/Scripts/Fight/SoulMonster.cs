using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : EnemyBase
{
    protected static GameObject hitFX;


    public SoulMonster()
    {
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
        InitEffect("Effects/HitMonster");
        if (hitFX)
        {
            var effect = Instantiate(hitFX, transform);
        }
    }
}
