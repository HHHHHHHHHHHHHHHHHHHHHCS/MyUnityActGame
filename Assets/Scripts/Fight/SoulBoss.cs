using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBoss : MonoBehaviour
{
    private Animator anim;
    private CharacterController bossCtrl;
    private Transform player;
    private float attackDistance = 3;
    private float moveSpeed = 2.5f;
    private float attackTime = 2;

    private float sqr_attackDistance;
    private float attackTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bossCtrl = GetComponent<CharacterController>();
        player = FightGameManager.Instance.Player.transform;
        sqr_attackDistance = attackDistance * attackDistance;
    }

    private void Update()
    {
        bool isRun = anim.GetBool("isRun");
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
                int num = Random.Range(0, 2);
                if (num == 0)
                {
                    anim.SetTrigger("attack1");
                }
                else
                {
                    anim.SetTrigger("attack2");
                }
                attackTimer = attackTime;
            }
        }
        else
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BossStand01"
       || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BossRun01")
            {
                bossCtrl.SimpleMove(transform.forward * moveSpeed);
                isRun = true;
            }
        }

        if (anim.GetBool("isRun") != isRun)
        {
            anim.SetBool("isRun", isRun);
        }
    }
}
