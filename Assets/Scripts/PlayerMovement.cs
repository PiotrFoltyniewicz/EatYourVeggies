using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Sprite[] sprites = new Sprite[4];
    private float horizontalMove, verticalMove;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private PlayerStats playerStats;
    public Animator animator;
    public Transform firePoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
        animator = GetComponent<Animator>();
        firePoint = GameObject.Find("FirePoint").transform;
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Speed", Mathf.Abs(verticalMove) + Mathf.Abs(horizontalMove));
        Vector2 movementDirection = new Vector2(horizontalMove, verticalMove).normalized;
        rb.MovePosition(rb.position + movementDirection * Time.deltaTime * movementSpeed);
    }

    private void Update()
    {
        if(horizontalMove > 0)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }else if(horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}

