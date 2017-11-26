using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FightPlayer : MonoBehaviour, IUnitBaseEvent
{
    private CharacterController playerCtrl;
    private Animator anim;
    private UnitInfoBase playerInfo = new UnitInfoBase();
    private bool isKeepDrag, isJoystickChange;
    private Vector3 lastJoystickPos;
    private bool isAttackStart, isClickAttackB;

    public FightPlayer()
    {
        playerInfo.attackDistance = 1.5f;
        playerInfo.moveSpeed = 4;
    }

    private void Awake()
    {
        playerCtrl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        JoystickMove();

    }

    #region Joystick and Move
    private void JoystickMove()
    {
        if (isKeepDrag)
        {
            if (!isJoystickChange)
            {
                JoystickDragEvent(lastJoystickPos);
            }
            isJoystickChange = false;
        }
        else
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h != 0 || v != 0)
            {
                PlayerMove(h, v);
            }
            else
            {
                anim.SetBool("isRun", false);
            }
        }
    }

    private void PlayerMove(float h, float v)
    {
        PlayerMove(new Vector3(h, 0, v));
    }

    private void PlayerMove(Vector3 targetDir)
    {
        //targetDir.x = targetDir.x != 0 ? (targetDir.x > 0 ? 1 : -1) : 0;
        //targetDir.z = targetDir.z != 0 ? (targetDir.z > 0 ? 1 : -1) : 0;
        if (targetDir.x != 0 || targetDir.y != 0 || targetDir.z != 0)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerStand"
                || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerRun")
            {
                transform.LookAt(transform.position + targetDir);
                playerCtrl.SimpleMove(targetDir * playerInfo.moveSpeed);
                anim.SetBool("isRun", true);
            }
        }
    }

    public void JoystickBeginEvent()
    {
        isKeepDrag = true;
    }

    public void JoystickDragEvent(Vector3 pos)
    {
        isJoystickChange = true;
        lastJoystickPos = pos;
        pos = new Vector3(pos.x, 0, pos.y);
        PlayerMove(pos);
    }

    public void JoystickEndEvent()
    {
        isKeepDrag = false;
    }
    #endregion

    #region Attack
    public void AttackStart()
    {
        isAttackStart = true;
    }

    public void AttackEnd()
    {
        if (isClickAttackB)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerAttackA")
            {
                anim.SetTrigger("attackB");
            }
            isClickAttackB = false;
        }
        else
        {
            isAttackStart = false;
        }

    }

    public void PlayNormalAttackButton()
    {
        if (isAttackStart)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerAttackA")
            {
                isClickAttackB = true;
            }
            else
            {
                isAttackStart = false;
            }
        }
        else
        {
            isClickAttackB = false;
            isAttackStart = false;
            anim.SetTrigger("attackA");
        }

    }

    public void PlayRangeAttackButton()
    {
        anim.SetTrigger("rangeAttack");
    }

    public void AttackTakeDamage_A()
    {
        Collider[] enemyList = Physics.OverlapSphere(transform.position, playerInfo.attackDistance, LayerMask.GetMask("Enemy"));
        foreach (var target in enemyList)
        {
            Vector3 temVec = target.transform.position - transform.position;
            Vector3 norVec = transform.rotation * Vector3.forward * 5;//此处*5只是为了画线更清楚,可以不要
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
            if (angle <= 45)
            {
                target.GetComponent<EnemyBase>().TakeDamage(playerInfo.attackDamage);
            }
        }
    }

    public void AttackTakeDamage_B()
    {
        Collider[] enemyList = Physics.OverlapSphere(transform.position, playerInfo.attackDistance, LayerMask.GetMask("Enemy"));
        foreach (var target in enemyList)
        {
            Vector3 temVec = target.transform.position - transform.position;
            Vector3 norVec = transform.rotation * Vector3.forward * 5;//此处*5只是为了画线更清楚,可以不要
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
            if (angle <= 45)
            {
                target.GetComponent<EnemyBase>().TakeDamage(playerInfo.attackDamage*1.25f);
            }
        }
    }
    #endregion

    public bool TakeDamage(float damage)
    {
        playerInfo.nowHp -= damage;
        if (playerInfo.nowHp <= 0)
        {
            playerInfo.nowHp = 0;
            Dead();
            return true;
        }
        return false;
    }

    public void Dead()
    {
    }
}
