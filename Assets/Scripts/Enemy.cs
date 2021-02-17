using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   Transform player;
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
    public float knockForce = 1.5f;

     HealthSystem healthSystem;


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        healthSystem = new HealthSystem(health);

        gotAggro = false;     
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = transform.position;

        if(player != null)
        {
          direction = player.position - transform.position;
        }

        // rotacja
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -90f;
        //rb.rotation = angle;

        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {

        rb.velocity *= 0.9f;

        if (Mathf.Abs(rb.velocity.x) <= 0.05f && Mathf.Abs(rb.velocity.y) <= 0.05f)
            rb.velocity = Vector3.zero;

            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, sightRange);
            foreach(Collider2D col in collider)
            {
                if(col.gameObject.CompareTag("Player"))
                {
                    player = col.transform;
                    playerStats = col.GetComponent<PlayerStats>();
                    gotAggro = true;
                    moveEnemy(movement);
                }
            }          
         
        
        if (gotAggro == true)
        {
            moveEnemy(movement);
        }
        
    }

   
    void moveEnemy(Vector2 direction)
    {
        if(rb.velocity.x ==  0 && rb.velocity.y == 0)
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(damage);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("test knockback");
            Vector2 dir = player.position - transform.position;
            rb.AddForce(-dir.normalized * knockForce, ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        if(healthSystem.isDead())
        {
            playerStats.gold += goldDrop;
            Instantiate(deadSprite, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
