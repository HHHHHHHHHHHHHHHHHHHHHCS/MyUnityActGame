using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : EnemyBase
{
    public SoulMonster()
    {
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
        return base.TakeDamage(damage);
    }

    public override void Dead()
    {

    }
}
