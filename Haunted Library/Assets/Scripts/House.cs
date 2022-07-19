using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class House : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hpText;
    public int hp;
    [SerializeField] int startHP = 100;

    private void Start()
    {
        hp = startHP;
    }

    void Update()
    {
        hpText.text = $"{hp}";
    }

    public void Damage(int amount)
    {
        hp -= amount;
        if (hp < 0)
        {
            hp = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();

            if (enemy.attacking && !enemy.hitTarget)
            {
                enemy.hitTarget = true;
                Damage(enemy.damage);
            }
        }
    }
}
