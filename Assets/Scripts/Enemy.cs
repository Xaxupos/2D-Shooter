using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameObject playerGO;
    Transform player;

    GameObject gunSystemGO;
    GunSystem gun;

    Rigidbody2D rb;
    PlayerStats playerStats;
    Vector2 movement;

    public GameObject deadSprite;

    //Enemy stats
    public float moveSpeed = 1f;
    public int damage = 1;
    public int health = 1;
    public bool canShoot = false;
    public int goldDrop = 1;
    public float sightRange = 5f;
    private bool gotAggro;


    // Start is called before the first frame update
    void Start()
    {
        gotAggro = false;
        playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.gameObject.GetComponent<Transform>();

        gunSystemGO = GameObject.FindGameObjectWithTag("Gun");
        gun = gunSystemGO.gameObject.GetComponent<GunSystem>();

        playerStats = playerGO.gameObject.GetComponent<PlayerStats>();
        
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 direction = player.position - transform.position;

        // rotacja

       // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -90f;
        //rb.rotation = angle;

        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(player.position,transform.position) <= sightRange)
        {
            gotAggro = true;
            moveEnemy(movement);
        } else if (gotAggro == true)
        {
            moveEnemy(movement);
        }
        
    }

   
    void moveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
                DealDamageBody();
           
        }
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }


    void DealDamageBody()
    {
        playerStats.health -= damage;
    } 
  

    void TakeDamage()
    {
        health -= damage;
        if(health == 0 || health < 0)
        {
            playerStats.gold += goldDrop;
            Instantiate(deadSprite, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
