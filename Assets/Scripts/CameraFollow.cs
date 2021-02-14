using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;      
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(player.position.x, player.position.y, player.position.z-2);
    }

}
