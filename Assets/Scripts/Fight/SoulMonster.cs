using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : EnemyBase
{
    public SoulMonster()
    {
        attackDistance = 1f;
    }

    protected override bool SimpleMove()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "MonStand01"
            || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "MonRun")
        {
            enemyCtrl.SimpleMove(transform.forward * moveSpeed);
            return true;
        }
        return false;
    }

    protected override void Attack()
    {
        anim.SetTrigger("attack");
    }
}
