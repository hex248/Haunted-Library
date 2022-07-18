using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    up,
    right,
    down,
    left
}

public class EnemyController : MonoBehaviour
{
    public Directions moveDirection;
    [SerializeField] [Range(0, 100)] float movementSpeed;
    Vector2 movement;
    Rigidbody2D rb;

    [SerializeField] Enemy enemyPreset;

    public int health;
    public int damage;
    public float attackBuildupTime;
    public float attackDuration;

    bool targetInRange = false;

    bool canAttack;
    bool attacking;
    bool buildingUp;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = enemyPreset.sprite;
        health = enemyPreset.health;
        damage = enemyPreset.damage;
        attackBuildupTime = enemyPreset.attackBuildupTime;
        attackDuration = enemyPreset.attackDuration;
        rb = GetComponent<Rigidbody2D>();
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

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        canAttack = !attacking && !buildingUp;
        if (targetInRange && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Player")
        {
            targetInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Player")
        {
            targetInRange = false;
        }
    }

    IEnumerator Attack()
    {
        buildingUp = true;

        yield return new WaitForSeconds(attackBuildupTime);

        buildingUp = false;
        attacking = true;

        yield return new WaitForSeconds(attackDuration);

        attacking = false;

        yield return null;
    }
}
