using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ScWaves : MonoBehaviour
{
    private int ActualWave = 0;

    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private Transform PositionmMin;
    [SerializeField] private Transform PositionMax;
    [SerializeField] private int spawnBase = 20;
    [SerializeField] private int spawnIncremental = 2;
    private Transform _target;
    [SerializeField] private float spawnProtection = 5;

    void Start()
    {
        _target = FindObjectOfType<ScEntityPlayer>().transform;
        Invoke("NextWave", 5f);
    }

    void NextWave()
    {
        for (int i = 0; i < (spawnBase + spawnIncremental * (ActualWave - 1)); i++)
        {
            int random = Random.Range(0, 100);
            if (random <50)
            {
                SpawnEnemy(0);
            }
            else
            {
                if (random<70)
                {
                    SpawnEnemy(1);
                }
                else
                {
                    if (random < 95)
                    {
                        SpawnEnemy(2);
                    }
                    else
                    {
                        SpawnEnemy(3);
                    }
                }
            }
        }
        ActualWave++;
        Invoke("NextWave", 30f);
    }
    public void SpawnEnemy(int Class)
    {
        bool Spawn = false;
        while (!Spawn)
        {
            Vector3 Place = new Vector3(Random.Range(PositionmMin.position.x, PositionMax.position.x), Random.Range(PositionmMin.position.y, PositionMax.position.y), Random.Range(PositionmMin.position.z, PositionMax.position.z));
            NavMeshHit hit;
            
            if (NavMesh.SamplePosition(Place, out hit, 100, NavMesh.AllAreas))
            {
                if (Vector3.Distance(hit.position, _target.position) > spawnProtection)
                {
                    GameObject SpawnedEnemy = Instantiate(Enemies[Class], hit.position, Quaternion.identity);
                    SpawnedEnemy.GetComponent<ScEntity>().level = ActualWave;
                    Spawn = true;
                }
            }
        }
    }
}