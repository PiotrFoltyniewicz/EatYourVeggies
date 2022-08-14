using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float attackSpeed;
    private float attackTime;
    public int durability;
    public float bulletLifeTime;
    public float bulletSpeed;
    public float bulletPierce;

    public GameObject attackEffect;
    private Transform firePoint;
    private PlayerMovement playerMove;
    private PlayerStats playerStats;
    private DurabilityBar durabilityBar;
    protected void Start()
    {
        playerMove = transform.parent.GetComponent<PlayerMovement>();
        playerStats = playerMove.GetComponent<PlayerStats>();
        durabilityBar = GameObject.Find("DurabilityBar").GetComponent<DurabilityBar>();
        firePoint = playerMove.firePoint;

        durabilityBar.durability = durability;
        durabilityBar.GetComponent<SpriteRenderer>().sprite = durabilityBar.durabilitySprites[13];
        durabilityBar.durabilityPoint = (int)Mathf.Ceil(durability / 14);
        durabilityBar.durabilityPointLeft = durabilityBar.durabilityPoint;
        durabilityBar.currentSprite = 13;
    }

    protected void Update()
    {
        attackTime -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && attackTime < 0 && !playerStats.isDead)
        {
            playerMove.animator.SetTrigger("Attack");
            attackTime = attackSpeed;
            Attack();
        }
    }

    protected void Attack()
    {
        GameObject tempAttack = Instantiate(attackEffect, firePoint.position, firePoint.rotation, null);
        if(firePoint.parent.localScale.x == -1f)
        {
            tempAttack.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        tempAttack.GetComponent<Attack>().damage = damage;
        tempAttack.GetComponent<Attack>().weapon = gameObject;
        durability--;
        durabilityBar.ChangeSprite();
        if(durability <= 0)
        {
            DestroyWeapon();
        }
    }

    protected void DestroyWeapon()
    {
        transform.parent.GetComponent<PlayerStats>().currentWeapon = "None";
        transform.parent.GetComponent<PlayerStats>().GiveWeapon("None");
        Destroy(gameObject);
    }
}
