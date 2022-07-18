using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(0,100)] float movementSpeed;
    Rigidbody2D rb;
    int position = 1;
    [SerializeField] Transform[] spawnPoints;

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    public void Movement(Vector2 newMovement)
    {
        movement = newMovement;
    }

    public void Rotation(int rotationDifference)
    {
        if (rotationDifference == 0) return;
        int oldPosition = position;
        position += rotationDifference;
        if (position == 0) position = 4; // if rotated left from position 1, set position to 4 instead of 0
        if (position == 5) position = 1; // if rotated right from position 4, set position to 1 instead of 5
        
        SwitchPosition(spawnPoints[position - 1], transform.position - spawnPoints[oldPosition - 1].position, rotationDifference);
    }

    public void Attack()
    {
        
    }

    public void SwitchPosition(Transform spawnPoint, Vector2 offset, int direction)
    {
        Vector3 newOffset = direction == 1 ? new Vector3(offset.y, -offset.x) : new Vector3(-offset.y, offset.x);

        transform.position = spawnPoint.position + newOffset;
    }
}
