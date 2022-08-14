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
    public string seedName;
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

    public void Glow()
    {
        isGlowing = true;
        spriteRenderer.sprite = seedSprites[1];
    }
    public void StopGlow()
    {
        isGlowing = false;
        spriteRenderer.sprite = seedSprites[0];
    }
}
