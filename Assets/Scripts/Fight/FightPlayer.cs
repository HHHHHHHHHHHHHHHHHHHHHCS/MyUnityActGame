using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer : MonoBehaviour
{
    private CharacterController playerCtrl;
    private float moveSpeed = 4;

    private void Awake()
    {
        playerCtrl = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        PlayerMove(-v, h);
    }

    private void PlayerMove(float h, float v)
    {
        PlayerMove(new Vector3(h, 0, v));
    }

    private void PlayerMove(Vector3 targetDir)
    {
        targetDir.x = targetDir.x != 0 ? (targetDir.x > 0 ? 1 : -1) : 0;
        targetDir.z = targetDir.z != 0 ? (targetDir.z > 0 ? 1 : -1) : 0;
        if(targetDir.x!=0|| targetDir.y != 0|| targetDir.z != 0)
        {
            transform.LookAt(transform.position + targetDir);
            playerCtrl.SimpleMove(targetDir * moveSpeed);
        }

    }
}
