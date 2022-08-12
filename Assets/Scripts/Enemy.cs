using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float Movementspeed;
    public int amountOfWater;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
    }
}
