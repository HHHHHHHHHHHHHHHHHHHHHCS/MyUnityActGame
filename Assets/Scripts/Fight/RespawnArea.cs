using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    public event Action RespawnEnd;
    [SerializeField]
    private float startSpawnTime = 5f;

    private int nowIndex;
    private RespawnPoint[] respawnArray;
    


    private void Awake()
    {
        respawnArray = GetComponentsInChildren<RespawnPoint>();
    }

    private void Start()
    {
        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        yield return new WaitForSeconds(startSpawnTime);
        NextSpawn();
    }

    private void NextSpawn()
    {
        if(nowIndex< respawnArray.Length)
        {
            respawnArray[nowIndex].endSpawn += NextSpawn;
            respawnArray[nowIndex].Spawn();
            nowIndex++;
            if(nowIndex >= respawnArray.Length)
            {
                RespawnEnd();
            }
        }
    }
}
