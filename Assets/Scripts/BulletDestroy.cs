using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    public GameObject hitEffect;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {

       
        if(GameObject.FindGameObjectsWithTag("Bullet").Length>1)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                return;
            }
            else
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
                Destroy(gameObject);
                
            }
        }
        
          
        
        
    }

}
