using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public int amountOfWater;
    public float attackRate;
    private float attackTime;
    private Animator animator;
    private Transform player;
    private EnemySystem enemySystem;
    private bool isAttacking;
    private bool isDead;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        enemySystem = GameObject.Find("EnemySystem").GetComponent<EnemySystem>();
    }

    void Update()
    {
        if (isDead) return;
        attackTime -= Time.deltaTime;
        if(playerInAttackRange && attackTime < 0)
        {
            transform.Find("AttackRange").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            isAttacking = true;
            attackTime = attackRate;
            animator.SetTrigger("Attack");

        }
        else if(playerInRange && !isAttacking)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, movementSpeed * Time.deltaTime);
        }

        if(player.position.x - transform.position.x > 0 && !isAttacking)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (!isAttacking)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public override void TakeDamage(float damage)
    {

        health -= damage;
        if(health <= 0)
        {
            Die();
            return;
        }
        enemyHit.Play();
    }

    private void Die()
    {
        enemyDeath.Play();
        movementSpeed = 0;
        animator.SetTrigger("Death");
        isDead = true;
    }
    private void DealDamage()
    {
        transform.Find("AttackRange").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        if (!playerInAttackRange) return;
        player.GetComponent<PlayerStats>().TakeDamage();
    }
    private void Disaapear()
    {
        for(int i = 0; i < amountOfWater; i++)
        {
            Instantiate(enemySystem.waterParticle, Random.insideUnitCircle * 2 + (Vector2)transform.position, transform.rotation, null);
        }
        int rnd = Random.Range(0, 100);
        if(rnd <= 25)
        {
            int seed = Random.Range(0, enemySystem.seeds.Count);
            Instantiate(enemySystem.seeds[seed], Random.insideUnitCircle + (Vector2)transform.position, transform.rotation, null);
        }
        Destroy(gameObject);
    }
    private void StoppedAttacking()
    {
        isAttacking = false;
    }
}
