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

    public void Spawn()
    {
        GameObject chosenEnemy = enemyManager.enemies[Random.Range(0, enemyManager.enemies.Length - 1)];
        chosenEnemy.GetComponent<EnemyController>().moveDirection = enemyMovementDirection;

        Vector3 randomOffset = new Vector3(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y), 0);

        GameObject enemyObject = Instantiate(chosenEnemy, transform.position + randomOffset, Quaternion.identity, enemyManager.transform);
    }
}
