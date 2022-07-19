using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemySpawner[] spawners;
    public GameObject[] enemies;

    [SerializeField] float spawnRate = 1.0f;

    float timer = 0.0f;

    public bool spawn = false;


    void Start()
    {
        
    }


    void Update()
    {
        if (spawn)
        {
            if (timer >= spawnRate)
            {
                timer = 0.0f;
                spawners[Random.Range(0, spawners.Length)].Spawn(); // spawn from random spawner
            }

            timer += Time.deltaTime;
        }
    }
}
