using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    [SerializeField] Directions enemyMovementDirection;
    EnemyManager enemyManager;

    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        
    }

    public void Spawn()
    {
        GameObject chosenEnemy = enemyManager.enemies[Random.Range(0, enemyManager.enemies.Length - 1)];
        chosenEnemy.GetComponent<EnemyController>().moveDirection = enemyMovementDirection;

        GameObject enemyObject = Instantiate(chosenEnemy, transform.position, Quaternion.identity, enemyManager.transform);
    }
}
