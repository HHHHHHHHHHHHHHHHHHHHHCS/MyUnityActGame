using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour,IUnitBaseEvent
{
    protected Animator anim;
    protected CharacterController enemyCtrl;
    protected Transform player;
    protected UnitInfoBase unitInfo = new UnitInfoBase();

    protected float sqr_attackDistance;
    protected float attackTimer;

    public EnemyBase()
    {
        unitInfo.attackDistance = 1.5f;
        unitInfo.attackTime = 1.5f;
        unitInfo.moveSpeed = 2f;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyCtrl = GetComponent<CharacterController>();
        player = FightGameManager.Instance.Player.transform;
        sqr_attackDistance = unitInfo.attackDistance * unitInfo.attackDistance;
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
                attackTimer = unitInfo.attackTime;
                isRun = false;
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
        enemyCtrl.SimpleMove(transform.forward * unitInfo.moveSpeed);
        return true;
    }

    protected virtual void Attack()
    {
        anim.SetTrigger("attack1");
    }

    public virtual bool TakeDamage(float damage)
    {
        unitInfo.nowHp -= damage;
        if (unitInfo.nowHp <= 0)
        {
            unitInfo.nowHp = 0;
            Dead();
            return true;
        }
        return false;
    }


    public virtual void Dead()
    {
        
    }


}
