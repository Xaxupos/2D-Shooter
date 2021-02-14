using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{

    public float explosionRange = 5f;
    public float explosionPower=1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Explode();
        }
    }

    void Explode()
    {
        //find all eniemies in range      
        //deal dmg to all enemies in range
        //destroy particles
        //destroy barrel
    }

}
