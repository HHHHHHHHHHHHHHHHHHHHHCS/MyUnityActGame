using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected Animator anim;
    protected CharacterController enemyCtrl;
    protected Transform player;
    protected float attackDistance = 3;
    protected float moveSpeed = 2.5f;
    protected float attackTime = 2;

    protected float sqr_attackDistance;
    protected float attackTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyCtrl = GetComponent<CharacterController>();
        player = FightGameManager.Instance.Player.transform;
        sqr_attackDistance = attackDistance * attackDistance;
    }

    private void Update()
    {
        MoveAndAttack();
    }


    protected virtual void MoveAndAttack()
    {
        bool isRun = GetIsRun();
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        float sqrDistance = Vector3.SqrMagnitude(targetPos - transform.position);
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (sqr_attackDistance >= sqrDistance)
        {
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = attackTime;
            }
        }
        else
        {
            isRun = SimpleMove() ? true : isRun;
        }

        if (GetIsRun() != isRun)
        {
            SetIsRun(isRun);
        }
    }

    protected virtual bool GetIsRun()
    {
        return anim.GetBool("isRun");
    }

    protected virtual void SetIsRun(bool tf)
    {
        anim.SetBool("isRun", tf);
    }

    protected virtual bool SimpleMove()
    {
        enemyCtrl.SimpleMove(transform.forward * moveSpeed);
        return true;
    }

    protected virtual void Attack()
    {
        anim.SetTrigger("attack1");
    }
}
