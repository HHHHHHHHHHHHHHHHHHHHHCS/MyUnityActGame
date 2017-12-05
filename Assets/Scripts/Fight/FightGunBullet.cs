using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGunBullet : MonoBehaviour
{
    public void Update()
    {
        transform.position += (transform.up*Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.enemy))
        {
            var player = FightGameManager.Instance.Player;

        }
    }
}
