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

    public int health = 100;
    public int damage = 10;
    public float attackBuildupTime = 0.1f;
    public float attackDuration = 0.5f;
    public float deathTime = 1.5f;

    bool canAttack;
    public bool attacking;
    public bool hitTarget;
    bool buildingUp;

    Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        canAttack = !attacking && !buildingUp && health > 0;

        // update animator variables
        anim.SetFloat("velocity", Mathf.Abs(movement.magnitude));
        if (movement.x != 0 || movement.y != 0)
        {
            if (Mathf.Abs(movement.x) >= Mathf.Abs(movement.y))
            {
                anim.SetBool("facingside", true);
                anim.SetBool("facingup", false);
                anim.SetBool("facingdown", false);
            
            
            }
            else if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
            {
                anim.SetBool("facingside", false);
                anim.SetBool("facingup", false);
                anim.SetBool("facingdown", false);
                if (movement.y > 0) anim.SetBool("facingup", true);
                else anim.SetBool("facingdown", true);
            }

            Vector3 directionToFace = new Vector3(1, 0, 0);
            if (movement.x < 0)
            {
                directionToFace = new Vector3(-1, 0, 0);
            }
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * directionToFace.x, transform.localScale.y, transform.localScale.z);
        }
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

    public void Attack(string attackName)
    {
        if (canAttack)
        {
            StartCoroutine(StartAttack(attackName));
        }
    }

    IEnumerator StartAttack(string attackName)
    {
        buildingUp = true;

        yield return new WaitForSeconds(attackBuildupTime);

        anim.SetTrigger(attackName);
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

        yield return new WaitForSeconds(deathTime);
        // display death screen

        // kill player
        Destroy(gameObject);

        yield return null;
    }

    public void SwitchPosition(Transform spawnPoint, Vector2 offset, int direction)
    {
        Vector3 newOffset = direction == 1 ? new Vector3(offset.y, -offset.x) : new Vector3(-offset.y, offset.x);

        transform.position = spawnPoint.position + newOffset;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy.attacking && !enemy.hitTarget)
            {
                enemy.hitTarget = true;
                Damage(enemy.damage);
            }
            else if (attacking && !hitTarget)
            {
                hitTarget = true;
                enemy.TakeDamage(damage);
            }
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            StartCoroutine(Die());
        }
    }
}
