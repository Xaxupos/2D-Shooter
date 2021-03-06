using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilStats : MonoBehaviour
{

    enum CivilType
    {
        Civilian,
        Vip
    }

    public HealthBar hpBar;
    GameObject playerGO;
    PlayerStats playerStats;

    [SerializeField]
    private CivilType type;

    HealthSystem hpSystem;
    public int health;
    public int maxHealth;

    private void Start()
    {
        hpBar.SetMaxHealth(maxHealth);
        hpSystem = new HealthSystem(health);
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerStats = playerGO.gameObject.GetComponent<PlayerStats>();

        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            hpSystem.TakeDamage(collision.gameObject.GetComponent<Bullet>().damage);
            hpBar.SetHealth(hpSystem.GetHealth());
            if (type == CivilType.Civilian)
            {

                if(hpSystem.GetHealth() > 0)
                {
                    playerStats.gold -= 5;
                }
                if(hpSystem.isDead())
                {
                    playerStats.gold -= 15;
                    Destroy(gameObject);
                }
            }

            if(type == CivilType.Vip)
            {
                if(hpSystem.isDead())
                {
                    //game over
                    Destroy(playerGO);
                    Destroy(gameObject);
                }
               
            }
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
        

            hpSystem.TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);
            hpBar.SetHealth(hpSystem.GetHealth());
            if (hpSystem.isDead())
            {
                Destroy(gameObject);
                if(type == CivilType.Vip)
                {
                    Destroy(playerGO);
                }
            }
        }
    }


}
