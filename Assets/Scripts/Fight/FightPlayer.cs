using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FightPlayer : MonoBehaviour
{
    private CharacterController playerCtrl;
    private float moveSpeed = 4;
    private bool isKeepDrag,isJoystickChange;
    private Vector3 lastJoystickPos;


    private void Awake()
    {
        playerCtrl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(isKeepDrag)
        {
            if(!isJoystickChange)
            {
                JoystickDragEvent(lastJoystickPos);
            }
            isJoystickChange = false;
        }
        else
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            PlayerMove(h, v);
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
            transform.LookAt(transform.position + targetDir);
            playerCtrl.SimpleMove(targetDir * moveSpeed);
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
}
