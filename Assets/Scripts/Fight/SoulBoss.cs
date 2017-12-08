using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBoss : EnemyBase
{
    [SerializeField]
    private AudioClip attackAudio,deadAudio;
    protected static GameObject hitFX;

    public SoulBoss()
    {
        unitInfo.attackDamage = 15;
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
        AudioSource.PlayClipAtPoint(attackAudio, transform.position);
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


    public void HitDamage2()
    {
        Collider[] enemyList = Physics.OverlapSphere(transform.position, unitInfo.attackDistance, LayerMask.GetMask(Tags.player));
        foreach (var target in enemyList)
        {
            Vector3 temVec = target.transform.position - transform.position;
            Vector3 norVec = transform.rotation * Vector3.forward;//此处*5只是为了画线更清楚,可以不要
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
            if (angle <= 30)
            {
                target.GetComponent<FightPlayer>().TakeDamage(unitInfo.attackDamage*1.5f);
                break;
            }
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
        InitEffect("Effects/HitBoss");
        if (hitFX)
        {
            var effect = Instantiate(hitFX, transform);
        }
    }
}
