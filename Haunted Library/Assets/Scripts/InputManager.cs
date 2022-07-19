using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode rotateLeftKey;
    public KeyCode rotateRightKey;
    public KeyCode moveUpKey;
    public KeyCode moveDownKey;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode hammerKey;
    public KeyCode explosionKey;
    [SerializeField] PlayerController player;
    [SerializeField] MapController map;

    private void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector2 movement = Vector2.zero;
        int rotation = 0;
        if (Input.GetKeyDown(rotateLeftKey))
        {
            rotation += -1;
        }
        if (Input.GetKeyDown(rotateRightKey))
        {
            rotation += 1;
        }
        if (Input.GetKey(moveUpKey))
        {
            movement += new Vector2(0.0f, 1.0f);
        }
        if (Input.GetKey(moveDownKey))
        {
            movement += new Vector2(0.0f, -1.0f);
        }
        if (Input.GetKey(moveLeftKey))
        {
            movement += new Vector2(-1.0f, 0.0f);
        }
        if (Input.GetKey(moveRightKey))
        {
            movement += new Vector2(1.0f, 0.0f);
        }
        if (Input.GetKey(hammerKey))
        {
            player.Attack("hammer attack");
        }
        if (Input.GetKey(explosionKey))
        {
            player.Attack("explosion attack");
        }

        player.Movement(movement);
        player.Rotation(rotation);
    }
}
