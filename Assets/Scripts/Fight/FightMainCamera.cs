using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMainCamera : MonoBehaviour
{
    private Vector3 offestPos = new Vector3(0, 3.5f, -4);
    private float speed = 2;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag(Tags.player).transform;
    }

    private void Update()
    {
        Vector3 localPos = player.position + offestPos;
        if(transform.localPosition!=localPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, localPos, speed * Time.deltaTime);
        }
        Quaternion targetRot = Quaternion.LookRotation(player.position - transform.position);
        if (transform.rotation != targetRot)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, speed * Time.deltaTime);
        }
    }
}
