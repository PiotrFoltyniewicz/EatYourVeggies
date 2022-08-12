using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float attackSpeed;
    private float attackTime;
    public float durability;
    public float bulletLifeTime;
    public float bulletSpeed;
    public float bulletPierce;

    public GameObject attackEffect;
    private Transform firePoint;
    private PlayerMovement playerMove;
    protected void Start()
    {
        playerMove = transform.parent.GetComponent<PlayerMovement>();
        firePoint = playerMove.firePoint;
    }

    protected void Update()
    {
        attackTime -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && attackTime < 0)
        {
            playerMove.animator.SetTrigger("Attack");
            attackTime = attackSpeed;
            Attack();
        }
    }

    protected void Attack()
    {
        GameObject tempAttack = Instantiate(attackEffect, firePoint.position, firePoint.rotation, null);
        tempAttack.GetComponent<Attack>().damage = damage;
        tempAttack.GetComponent<Attack>().weapon = gameObject;
    }
}
