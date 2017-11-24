using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBoss : EnemyBase
{
    public SoulBoss()
    {
        unitInfo.attackDistance = 2.5f;
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
        return base.TakeDamage(damage);
    }

    public override void Dead()
    {

    }
}
