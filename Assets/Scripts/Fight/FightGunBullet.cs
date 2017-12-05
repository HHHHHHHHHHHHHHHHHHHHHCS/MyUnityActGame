using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightGunBullet : MonoBehaviour
{
    private float speed = 20f;

    public void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.enemy))
        {
            var player = FightGameManager.Instance.Player;
            player.AttackGunTakeDamage(other.GetComponent<EnemyBase>());
        }
    }
}
