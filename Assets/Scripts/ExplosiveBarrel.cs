using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [SerializeField]
    float explosionRange = 5f;
    [SerializeField]
    int explosionPower = 1;

    bool hasExploded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && !hasExploded)
        {
            hasExploded = true;
            Explode();
        }
    }

    void Explode()
    {
        //find all eniemies in range     

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRange);
        foreach(Collider2D col in colliders)
        {
            if(col.CompareTag("Enemy"))
            {
                //deal dmg to all enemies in range
                col.GetComponent<Enemy>().TakeDamage(explosionPower);
            }
            if(col.CompareTag("Player"))
            {
                col.GetComponent<PlayerStats>().TakeDamage(explosionPower);
            }
        }

        Destroy(gameObject);
    }

}