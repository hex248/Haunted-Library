using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Directions
{
    up,
    right,
    down,
    left
}

public class EnemyController : MonoBehaviour
{
    [SerializeField] Directions moveDirection;
    [SerializeField] [Range(0, 100)] float movementSpeed;
    Vector2 movement;
    Rigidbody2D rb;

    void Start()
    {
        switch (moveDirection)
        {
            case Directions.up:
                movement = new Vector2(0.0f, 1.0f);
                break;
            case Directions.right:
                movement = new Vector2(1.0f, 0.0f);
                break;
            case Directions.down:
                movement = new Vector2(0.0f, -1.0f);
                break;
            case Directions.left:
                movement = new Vector2(-1.0f, 0.0f);
                break;
        }
    }

    void Update()
    {
        
    }
}
