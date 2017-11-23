using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMonster : EnemyBase
{
    protected override bool SimpleMove()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "MonStand01")
        {
            enemyCtrl.SimpleMove(transform.forward * moveSpeed);
            return true;
        }
        return false;
    }

    protected override void Attack()
    {
        anim.SetTrigger("attack1");
    }
}
