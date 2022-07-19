using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite sprite;
    public string enemyName;
    public int health;
    public int damage;
    public float attackBuildupTime;
    public float attackDuration;
    public float deathTime;
}
