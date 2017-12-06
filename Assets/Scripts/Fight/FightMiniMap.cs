using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightMiniMap : MonoBehaviour
{
    private EnemyIcon monsterIconPrefab;
    private EnemyIcon bossIconPrefab;

    private Transform player;
    private Transform playerIcon;

    private List<EnemyIcon> enemyIconList = new List<EnemyIcon>();

    public void _Init()
    {
        monsterIconPrefab = Resources.Load<EnemyIcon>("Prefabs/EnemyMonsterIcon");
        bossIconPrefab = Resources.Load<EnemyIcon>("Prefabs/EnemyBossIcon");

        player = FightGameManager.Instance.Player.transform;
        playerIcon = transform.Find(FightName.playerIcon);
    }

    public void AddIcon(EnemyBase enemy)
    {
        if (enemy is SoulBoss)
        {
            Debug.Log(1);
            var a = Instantiate(bossIconPrefab, transform);
            a.Init(player, enemy.transform);
        }
        else if (enemy is SoulMonster)
        {
            Debug.Log(2);
            var a =Instantiate(monsterIconPrefab, transform);
            a.Init(player, enemy.transform);
        }
    }

    public void UpdateMiniMap()
    {
        var angle = playerIcon.rotation.eulerAngles;
        angle.z = -player.eulerAngles.y;
        playerIcon.eulerAngles = angle;
        foreach (var item in enemyIconList)
        {
            item.UpdateIcon();
        }
    }
}
