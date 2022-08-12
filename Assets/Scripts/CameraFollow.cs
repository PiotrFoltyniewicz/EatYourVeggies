using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    void Start()
    {
        
    }

    void LateUpdate()
    {
        transform.position = new Vector3(follow.position.x, follow.position.y, -10);
    }
}
