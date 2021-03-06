﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IUnitBaseEvent
{
    protected static GameObject gunItem, dualSwordItem, hpItem;
    protected Animator anim;
    protected CharacterController enemyCtrl;
    protected Transform player;
    protected UnitInfoBase unitInfo = new UnitInfoBase();
    protected EnemyIcon icon;

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
        icon = FightUIManager.Instance.MiniMap.AddIcon(this);
    }

    protected virtual void Update()
    {
        MoveAndAttack();
    }


    protected virtual void MoveAndAttack()
    {

        if (unitInfo.nowHp > 0)
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
        if (unitInfo.nowHp > 0)
        {
            unitInfo.nowHp -= damage;
            if (unitInfo.nowHp <= 0)
            {
                unitInfo.nowHp = 0;
                Dead();
            }
            return true;
        }
        return false;
    }


    public virtual void Dead()
    {
        SpawnDeadItem();
        anim.SetTrigger("death");
        enemyCtrl.enabled = false;
        icon.DestroySelf();
        StartCoroutine(DeadAnim());
    }

    protected virtual IEnumerator DeadAnim()
    {
        yield return new WaitForSeconds(1.333f);
        Vector3 offestY = Vector3.down * 0.1f;
        var wait = new WaitForSeconds(0.05f);
        for (int i = 0; i <= 20; i++)
        {
            transform.position += offestY;
            yield return wait;
        }
        Destroy(gameObject);
    }

    protected virtual void SpawnDeadItem()
    {
        if (!gunItem)
        {
            gunItem = Resources.Load<GameObject>("Prefabs/GunItem");
        }
        if (!dualSwordItem)
        {
            dualSwordItem = Resources.Load<GameObject>("Prefabs/DualSwordItem");
        }
        if (!hpItem)
        {
            hpItem = Resources.Load<GameObject>("Prefabs/HPItem");
        }

        GameObject go = null;
        float rd = UnityEngine.Random.Range(0, 1f);
        if (rd < 0.25f)
        {
            go = gunItem;
        }
        else if (rd < 0.5f)
        {
            go = dualSwordItem;
        }
        else if (rd < 0.75f)
        {
            go = hpItem;
        }

        if (go != null)
        {
            Vector3 pos = transform.position;
            pos.y = go.transform.position.y;
            Instantiate(go, pos, Quaternion.identity);
        }

    }
}
