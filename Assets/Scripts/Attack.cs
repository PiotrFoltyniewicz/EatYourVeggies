using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject weapon;
    public float damage;
    protected Animator animator;

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
