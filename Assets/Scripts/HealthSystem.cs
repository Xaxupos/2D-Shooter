using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{

    private int health;
    private int maxHealth;

    public bool isDead()
    {
        return health <= 0;
    }

    public int GetHealth()
    {
        return health;
    }

    public override string ToString()
    {
        return "HP:" + health + "/" + maxHealth;
    }


   public  void TakeDamage(int damage)
    {
        health -= damage;

    }

    public HealthSystem(int health)
    {
        this.health = health;
        maxHealth = health;
    }

}
