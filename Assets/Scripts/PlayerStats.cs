using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    GameObject textHealthGO;
    public Text textHealth;

    GameObject textGoldGO;
    public Text textGold;



    public float maxHealth = 15f;
    public float health = 15f;
    public int gold = 0; 


    private void Start()
    {
        health = maxHealth;

        textHealthGO = GameObject.FindGameObjectWithTag("Health");
        textHealth = textHealthGO.gameObject.GetComponent<Text>();

        textGoldGO = GameObject.FindGameObjectWithTag("Gold");
        textGold = textGoldGO.gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        UpdateUI();
        CheckPlayerHP();
    }

    void UpdateUI()
    {
        textHealth.text = "HP:"+health + "/" + maxHealth;
        textGold.text = "Gold:" + gold;
    }

    void CheckPlayerHP()
    {
        if(health==0 || health<0)
        {
            //end game
            Destroy(gameObject);
        }
    }


}
