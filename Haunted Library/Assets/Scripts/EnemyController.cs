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
    public float deathTime;

    public bool targetInRange = false;

    bool canAttack;
    public bool attacking;
    public bool hitTarget;
    bool buildingUp;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = enemyPreset.sprite;
        health = enemyPreset.health;
        damage = enemyPreset.damage;
        attackBuildupTime = enemyPreset.attackBuildupTime;
        attackDuration = enemyPreset.attackDuration;
        deathTime = enemyPreset.deathTime;
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
        if (health > 0)
        {
            rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Update()
    {
        canAttack = !attacking && !buildingUp;
        if (targetInRange && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "House" || other.gameObject.tag == "Player")
        {
            targetInRange = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
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
        hitTarget = false;

        yield return null;
    }

    IEnumerator Die()
    {
        // play death animation
        // 

        yield return new WaitForSeconds(deathTime);

        Destroy(gameObject);

        yield return null;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            attacking = false;
            hitTarget = false;
            StartCoroutine(Die());
        }
    }
}
