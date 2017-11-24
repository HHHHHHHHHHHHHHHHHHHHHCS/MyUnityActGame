using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RespawnPoint : MonoBehaviour
{
    public delegate void EndSpawn();

    [System.Serializable]
    public struct EnemyArray
    {
        public GameObject[] enemyArray;
    }

    private static Transform parent;

    public event EndSpawn endSpawn;

    [SerializeField]
    private float radius = 14;
    [SerializeField]
    private float waitSpawnTime = 1;
    [SerializeField]
    private float nextWaveTime = 15;

    [SerializeField]
    private EnemyArray[] enemyArray;


    public void Spawn()
    {
        if (!parent)
        {
            parent = FightGameManager.Instance.EnemyManager;
        }
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        if (enemyArray != null && enemyArray.Length > 0)
        {
            foreach (var itemArray in enemyArray)
            {
                var enemyArray = itemArray.enemyArray;
                if (enemyArray != null && enemyArray.Length > 0)
                {
                    foreach (var item in enemyArray)
                    {
                        SpawnEnemy(item);
                    }
                }
                yield return new WaitForSeconds(waitSpawnTime);
            }
        }
        yield return new WaitForSeconds(nextWaveTime);
        endSpawn();
    }


    private void SpawnEnemy(GameObject go)
    {
        if(go!=null)
        {
            float offestRaius = Random.Range(0, radius);
            float offestRot = Random.Range(0, 360);
            float posX = transform.position.x + offestRaius * (Mathf.Cos(offestRot * Mathf.Deg2Rad));
            float posZ = transform.position.z + offestRaius * (Mathf.Sin(offestRot * Mathf.Deg2Rad));
            Vector3 pos = new Vector3(posX, go.transform.position.y, posZ);
            Instantiate(go, pos, Quaternion.identity, parent);
        }
    }


#if UNITY_EDITOR 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;//为随后绘制的gizmos设置颜色。
        Gizmos.DrawWireSphere(transform.position + Vector3.up, radius);//使用center和radius参数，绘制一个线框球体。
    }
#endif
}
