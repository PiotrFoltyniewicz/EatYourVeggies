using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    private Transform player;
    public float offsetX, offsetY;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void LateUpdate()
    {
        transform.position = new Vector2(player.position.x + offsetX, player.position.y + offsetY);
    }
}
