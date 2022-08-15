using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !collision.isTrigger)
        {
            collision.GetComponent<PlayerStats>().TakeDamage();
            Destroy(gameObject);
        }
    }
}
