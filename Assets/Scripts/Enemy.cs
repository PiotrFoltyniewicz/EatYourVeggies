using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public int amountOfWater;
    private Animator animator;
    public bool playerInRange;
    public bool playerInAttackRange;
    private Transform player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if(playerInAttackRange)
        {
            animator.SetTrigger("Attack");
        }
        else if(playerInRange)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, movementSpeed * Time.deltaTime);
        }

        if(player.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
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
        animator.SetTrigger("Death");
    }
    private void DealDamage()
    {
        if (!playerInRange) return;
    }
}
