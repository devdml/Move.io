using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Enemy Setup")]
    public List<Enemy> enemies = new List<Enemy>();
    public Enemy enemy;
    public int minEne;
    public int maxEne;
    public int countEne;
    public int rangeSpawnEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if (countEne < maxEne)
        {
            CheckSpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        countEne = 0;
        for (int i = enemies.Count; i < minEne; i++)
        {
            Enemy enemis = LeanPool.Spawn(enemy, RandomNavSphere(Vector3.zero, rangeSpawnEnemy, -1), Quaternion.identity);
            enemies.Add(enemis);
            countEne++;
        }
    }

    public void CheckSpawnEnemy()
    {
        for (int i = enemies.Count; i < minEne; i++)
        {
            if (enemies.Count < minEne)
            {
                Enemy enemis = LeanPool.Spawn(enemy, RandomNavSphere(Vector3.zero, rangeSpawnEnemy, -1), Quaternion.identity);
                enemies.Add(enemis);
                countEne++;
            }
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
