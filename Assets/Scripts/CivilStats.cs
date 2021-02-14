using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilStats : MonoBehaviour
{


    GameObject playerGO;
    PlayerStats playerStats;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerStats = playerGO.gameObject.GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            if (this.CompareTag("Civilian"))
            {
                playerStats.gold -= 5;
            }
            if(this.CompareTag("Vip"))
            {
                Destroy(playerGO);
                Destroy(gameObject);
            }
        }
    }


}
