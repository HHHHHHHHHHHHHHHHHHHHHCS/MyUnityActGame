using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FightPlayer : MonoBehaviour, IUnitBaseEvent
{
    private static GameObject gunBullet;

    private WeaponBase weaponSingleSword, weaponDualSword, weaponGun;
    private CharacterController playerCtrl;
    private Animator anim;
    private UnitInfoBase playerInfo = new UnitInfoBase();
    private bool isKeepDrag, isJoystickChange;
    private Vector3 lastJoystickPos;
    private bool isAttackStart, isClickAttackB;
    private bool haveRange, haveGun ;
    private Transform gunSpawnPoint;


    public FightPlayer()
    {
        playerInfo.nowHp = playerInfo.maxHp = 200f;
        playerInfo.attackDistance = 1.5f;
        playerInfo.moveSpeed = 4;
        playerInfo.attackDamage = 100f;
    }

    private void Awake()
    {
        playerCtrl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        var weapons = transform.GetComponentsInChildren<WeaponBase>(true);
        foreach (var item in weapons)
        {
            if (item.name == "WeaponSingleSword")
            {
                weaponSingleSword = item;
            }
            else if (item.name == "WeaponDualSword")
            {
                weaponDualSword = item;
            }
            else if (item.name == "WeaponGun")
            {
                weaponGun = item;
                gunSpawnPoint = weaponGun.transform.Find("BulletSpawnPosition");
            }
        }
    }

    private void Start()
    {
        weaponDualSword.Toggle();
        weaponGun.Toggle();
        FightUIManager.Instance.SetRangeAttack(haveRange);
        FightUIManager.Instance.SetGunAttack(haveGun);
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
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerStand"
                || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerRun")
        {
            anim.SetTrigger("attackRange");
            UseRangeItem();
        }
    }

    public void PlayGunAttackButton()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerStand"
                || anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerRun")
        {
            anim.SetTrigger("attackGun");
            UseGunItem();
        }
    }

    public void AttackTakeDamage_A()
    {
        Collider[] enemyList = Physics.OverlapSphere(transform.position, playerInfo.attackDistance, LayerMask.GetMask(Tags.enemy));
        foreach (var target in enemyList)
        {
            Vector3 temVec = target.transform.position - transform.position;
            Vector3 norVec = transform.rotation * Vector3.forward;//此处*5只是为了画线更清楚,可以不要
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
            if (angle <= 45)
            {
                target.GetComponent<EnemyBase>().TakeDamage(playerInfo.attackDamage);
                break;
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
                target.GetComponent<EnemyBase>().TakeDamage(playerInfo.attackDamage * 1.25f);
                break;
            }
        }
    }

    public void AttackRange()
    {
        Collider[] enemyList = Physics.OverlapSphere(transform.position, playerInfo.attackDistance, LayerMask.GetMask("Enemy"));
        foreach (var target in enemyList)
        {
            Vector3 temVec = target.transform.position - transform.position;
            Vector3 norVec = transform.rotation * Vector3.forward;//此处*5只是为了画线更清楚,可以不要
            float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
            if (angle <= 45)
            {
                target.GetComponent<EnemyBase>().TakeDamage(playerInfo.attackDamage * 1.5f);
            }
        }
    }

    public void AttackRangeEnd()
    {
        weaponDualSword.Toggle();
        weaponSingleSword.Toggle();
    }

    public void AttackGun()
    {
        if (!gunBullet)
        {
            gunBullet = Resources.Load<GameObject>("Prefabs/GunBullet");
        }
        Vector3 rot = gunBullet.transform.eulerAngles;
        rot.y = transform.eulerAngles.y + 90;
        var bullet = Instantiate(gunBullet, gunSpawnPoint.position, Quaternion.Euler(rot));
        Destroy(bullet, 3f);
    }

    public void AttackGunTakeDamage(EnemyBase enemy)
    {
        enemy.TakeDamage(playerInfo.attackDamage * 1.5f);
    }

    public void AttackGunEnd()
    {
        weaponGun.Toggle();
        weaponSingleSword.Toggle();
    }
    #endregion

    #region Item
    public bool GetRangeItem()
    {
        if (haveRange)
        {
            return false;
        }
        haveRange = true;
        FightUIManager.Instance.SetRangeAttack(haveRange);
        return true;
    }

    public bool UseRangeItem()
    {
        if (!haveRange)
        {
            return false;
        }
        else
        {
            haveRange = false;
            weaponSingleSword.Toggle();
            weaponDualSword.Toggle();
            FightUIManager.Instance.SetRangeAttack(haveRange);
            return true;
        }
    }

    public bool GetGunItem()
    {
        if (haveGun)
        {
            return false;
        }
        haveGun = true;
        FightUIManager.Instance.SetGunAttack(haveGun);
        return true;
    }

    public bool UseGunItem()
    {
        if (!haveGun)
        {
            return false;
        }
        else
        {
            haveGun = false;
            weaponSingleSword.Toggle();
            weaponGun.Toggle();
            FightUIManager.Instance.SetGunAttack(haveGun);
            return true;
        }
    }

    public bool GetHPItem()
    {
        if(playerInfo.nowHp>0)
        {
            playerInfo.nowHp += playerInfo.maxHp * 0.1f;
            if(playerInfo.nowHp>playerInfo.maxHp)
            {
                playerInfo.nowHp = playerInfo.maxHp;
            }
            return true;
        }
        return false;
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
        Debug.Log("I AM DEAD");
    }
}
