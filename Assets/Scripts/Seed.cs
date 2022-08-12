using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private static int aroundPlayerSeeds;
    public Sprite[] seedSprites = new Sprite[2];
    private SpriteRenderer spriteRenderer;
    public float lifeTime;
    private Transform player;
    public bool isGlowing;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            aroundPlayerSeeds++;
            if(aroundPlayerSeeds == 1)
            {
                Glow();
            }
            else
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                GameObject closestSeed = gameObject;
                foreach(GameObject seed in GameObject.FindGameObjectsWithTag("Seed"))
                {
                    if(Vector2.Distance(seed.transform.position, player.position) < distanceToPlayer)
                    {
                        closestSeed = seed;
                        distanceToPlayer = Vector2.Distance(seed.transform.position, player.position);
                    }
                }
                closestSeed.GetComponent<Seed>().Glow();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            aroundPlayerSeeds--;
            spriteRenderer.sprite = seedSprites[0];
            isGlowing = false;
        }
    }

    public void Glow()
    {
        isGlowing = true;
        spriteRenderer.sprite = seedSprites[1];
    }
}
