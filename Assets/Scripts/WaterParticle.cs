using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    private Transform player;
    private bool isCollected = false;
    public float speed;
    public float lifeTime;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        if(isCollected && player.GetComponent<PlayerStats>().waterPoints < 13)
        {
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                player.gameObject.GetComponent<PlayerStats>().GiveWater();
                Destroy(gameObject);
            }
        }
        else
        {
            isCollected = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isCollected = true;
        }
    }
}
