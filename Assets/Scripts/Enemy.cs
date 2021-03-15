using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   Transform target;
   GunSystem gun;

    [SerializeField]
    private Transform firePoint;

    public HealthBar hpBar;

    Rigidbody2D rb;
    PlayerStats playerStats;
    Vector2 movement;

    public GameObject deadSprite;

    //Enemy stats
    public float moveSpeed = 1f;
    public int damage = 1;
    public int health = 1;  
    public int maxHealth = 1;  
    public int goldDrop = 1;
    public float sightRange = 5f;
    private bool gotAggro;
    public float knockForce = 1.5f;

    
    private float timer;
    [Header("GunStats")]
    public float timeBetweenShooting;
    public bool canShoot = false;

    HealthSystem healthSystem;

    [SerializeField]
    private GameObject bullet;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hpBar.SetMaxHealth(maxHealth);
        Physics2D.IgnoreLayerCollision(8, 9);
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        healthSystem = new HealthSystem(health);

        gotAggro = false;     
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
            timer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        
        Vector3 direction = transform.position;

        if (target != null)
        {
            direction = target.position - transform.position;

        }


        direction.Normalize();
        movement = direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 90f;

        rb.velocity *= 0.9f;

        if (Mathf.Abs(rb.velocity.x) <= 0.04f && Mathf.Abs(rb.velocity.y) <= 0.11f)
            rb.velocity = Vector3.zero;


        if (gotAggro == false && target == null)
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, sightRange);
           
            foreach (Collider2D col in collider)
            {
                if (col.gameObject.CompareTag("Vip") || col.gameObject.CompareTag("Civilian"))
                {
                    target = col.transform;
                    gotAggro = true;
                    moveEnemy(movement);
                }

                else if (col.gameObject.CompareTag("Player"))
                {
                    timer = 0.1f;
                    target = col.transform;
                    playerStats = col.GetComponent<PlayerStats>();
                    gotAggro = true;
                    moveEnemy(movement);
                }
            }
        }
            FindClosestTarget();

        if (gotAggro == true && !canShoot)
        {
            moveEnemy(movement);
        }
       
        if(gotAggro == true && canShoot && timer <= 0 )
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, sightRange);
            foreach (Collider2D col in collider)
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    EnemyShoot();
                    return;
                }
            }
            moveEnemy(movement);
        }

    }

    void FindClosestTarget()
    {
        //zmiana aggro
        Collider2D[] col2 = Physics2D.OverlapCircleAll(transform.position, sightRange);
        if(col2 != null)
        {
            List<Collider2D> aggroTargets = new List<Collider2D>();
            
            foreach (Collider2D col in col2)
            {
                if (col.gameObject.CompareTag("Vip") || col.gameObject.CompareTag("Civilian"))
                {
                    aggroTargets.Add(col);
                }

                else if (col.gameObject.CompareTag("Player"))
                {
                    aggroTargets.Add(col);
                }
            }

            if(aggroTargets.Count > 0)
            {
                Vector2[] colliderPositions = new Vector2[aggroTargets.Count]; ;
                float[] distances = new float[aggroTargets.Count];
                int index = 0;

                for (int i = 0; i < aggroTargets.Count; i++)
                {
                    colliderPositions[i] = transform.position - aggroTargets[i].transform.position;
                    distances[i] = colliderPositions[i].magnitude;
                }

                float closestDistance = distances[0];

                for (int i = 1; i < aggroTargets.Count; i++)
                {
                    if (distances[i] < closestDistance)
                    {
                        closestDistance = distances[i];
                        index = i;
                    }
                }
                target = aggroTargets[index].transform;
                //KONIEC ZMIANA AGGRO
            }
        }
       
    }

    void EnemyShoot()
    {
        GameObject bullet1 = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();

        bullet1.GetComponent<Bullet>().damage = damage;
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        if (timeBetweenShooting > 0)
            timer = timeBetweenShooting;

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
            int bulletDamage = collision.gameObject.GetComponent<Bullet>().damage;
            TakeDamage(bulletDamage);
        }

        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Vip") || collision.gameObject.CompareTag("Civilian"))

        {
            
            Vector2 dir = target.position - transform.position;
            rb.AddForce(-dir.normalized * knockForce, ForceMode2D.Impulse);
        }

        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            return;
        }

    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);      
        FloatingText.Create(transform.position, damage.ToString(), new Vector2(0.3f, 0.75f));      
        if (healthSystem.isDead())
        {
            playerStats.gold += goldDrop;
            Instantiate(deadSprite, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       hpBar.SetHealth(healthSystem.GetHealth());
       
    }

}
