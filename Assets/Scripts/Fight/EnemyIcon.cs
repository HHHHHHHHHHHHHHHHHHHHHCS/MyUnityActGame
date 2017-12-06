using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour
{
    private Transform player;
    private Transform enemy;

    public void Init(Transform _player,Transform _enemy)
    {
        player = _player;
        enemy = _enemy;
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if(!enemy)
        {
            Destroy(gameObject);
            return;
        }
        if(!player)
        {
            return;
        }
    }
}
