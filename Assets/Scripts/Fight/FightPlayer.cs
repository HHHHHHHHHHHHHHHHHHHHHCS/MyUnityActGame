using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FightPlayer : MonoBehaviour
{
    private CharacterController playerCtrl;
    private Animator anim;
    private float moveSpeed = 4;
    private bool isKeepDrag,isJoystickChange;
    private Vector3 lastJoystickPos;
    private bool isAttackStart, isClickAttackB;


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
            if (h!=0||v!=0)
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
                playerCtrl.SimpleMove(targetDir * moveSpeed);
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
        if(isClickAttackB)
        {
            if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name=="PlayerAttackA")
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

        if(isAttackStart)
        {
            if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerAttackA")
            {
                isClickAttackB = true;
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
        Debug.Log("AttackTakeDamage_A");
    }

    public void AttackTakeDamage_B()
    {
        Debug.Log("AttackTakeDamage_B");
    }
    #endregion
}
