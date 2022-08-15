using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool playerInRange;
    public bool playerInAttackRange;
    public float health;
    public float movementSpeed;
    public AudioSource enemyHit;
    public AudioSource enemyDeath;

    public virtual void TakeDamage(float damage)
    {

    }
}
