using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public Text textHealth;
    public Text textGold;

    HealthSystem healthSystem;

    public int health = 15;
    public int gold = 0; 


    private void Start()
    {
        healthSystem = new HealthSystem(health);
        
    }

    private void Update()
    {
        UpdateUI();
        CheckPlayerHP();
    }

    void UpdateUI()
    {
        textHealth.text = healthSystem.ToString();
        textGold.text = "Gold:" + gold;
    }

    void CheckPlayerHP()
    {
        if(healthSystem.isDead())
        {
            //end game
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            int enemyDamage = collision.gameObject.GetComponent<Enemy>().damage;
            TakeDamage(enemyDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        if (healthSystem.isDead())
        {
           // Instantiate(transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
