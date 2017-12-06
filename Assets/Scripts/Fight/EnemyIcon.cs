using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIcon : MonoBehaviour
{
    private Transform player;
    private Transform enemy;

    public void Init(Transform _player, Transform _enemy)
    {
        player = _player;
        enemy = _enemy;
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (!enemy)
        {
            DestroySelf();
            return;
        }
        if (!player)
        {
            return;
        }
        var offest = player.position - enemy.position;
        offest.x *= -1;
        offest.y = -offest.z;
        offest.z = 0;
        ((RectTransform)transform).anchoredPosition3D = offest * 10;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
