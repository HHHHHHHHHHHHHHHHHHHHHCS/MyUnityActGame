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

    public EnemyIcon AddIcon(EnemyBase enemy)
    {
        EnemyIcon go = null;
        if (enemy is SoulBoss)
        {
            go = bossIconPrefab;
        }
        else if (enemy is SoulMonster)
        {
            go = monsterIconPrefab;
        }

        if(go)
        {
            var icon = Instantiate(go, transform);
            icon.Init(player, enemy.transform);
            enemyIconList.Add(icon);
            return icon;
        }
        return null;
    }

    public void UpdateMiniMap()
    {
        var angle = playerIcon.rotation.eulerAngles;
        angle.z = -player.eulerAngles.y;
        playerIcon.eulerAngles = angle;
        for (int i = enemyIconList.Count - 1; i >= 0; i--)
        {
            var item = enemyIconList[i];
            if (item)
            {
                item.UpdateIcon();
            }
            else
            {
                enemyIconList.Remove(item);
            }
        }
    }
}
