using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSystem : MonoBehaviour
{

    
    private Sprite gunSprite;
    [SerializeField]
    private SpriteRenderer spriteRenderer;


    private GunStats gunStats;

    private float timer;

    [SerializeField]
    private Equipment eq;

    GameObject textAmmoGO;
    private Text textAmmo;

    GameObject gunTypeGO;
    private Text textGunType;

    private bool shooting;
    private bool reloading;

    private int weaponIndex;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private GameObject bullet;

    void AddGun()
    {
        //tutaj te co posiadam w EQ
        eq.AddGun(Equipment.GunType.Pistol);
        eq.AddGun(Equipment.GunType.Pistol);
        eq.AddGun(Equipment.GunType.Pistol);
        eq.AddGun(Equipment.GunType.Shotgun);
        eq.AddGun(Equipment.GunType.Sniper);

        gunStats = eq.GetGun(0).stats;
        gunStats.bulletsLeft = gunStats.magazineSize;
    }

    private void Awake()
    {
        weaponIndex = 0;
        textAmmoGO = GameObject.FindGameObjectWithTag("Ammo");
        textAmmo = textAmmoGO.gameObject.GetComponent<Text>();

        gunTypeGO = GameObject.FindGameObjectWithTag("GunUI");
        textGunType = gunTypeGO.gameObject.GetComponent<Text>();

        AddGun();
    }



    private void Update()
    {


        if (gunStats != null)
        {
            MyInput();
            UpdateUI();
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            weaponIndex--;
            if (weaponIndex < 0)
            {
                weaponIndex = eq.GetBoughtGun().Count-1;
                
            }          
            gunStats = eq.GetGun(weaponIndex).stats;
            gunSprite = gunStats.gunSprite;
            spriteRenderer.sprite = gunSprite;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponIndex++;
            if(weaponIndex > eq.GetBoughtGun().Count-1)
            {
                weaponIndex = 0;
                
            }
           
            gunStats = eq.GetGun(weaponIndex).stats;
            gunSprite = gunStats.gunSprite;
            spriteRenderer.sprite = gunSprite;
        }

    }

    void UpdateUI()
    {
        textAmmo.text = gunStats.bulletsLeft + "/" + gunStats.magazineSize;
        textGunType.text = "Gun: " + eq.GetGun(weaponIndex).stats;
    }

    void MyInput()
    {
        if (gunStats.allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
           shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && gunStats.bulletsLeft < gunStats.magazineSize && ! reloading)
            Reload();

        //Shoot
        if (shooting && !reloading && gunStats.bulletsLeft > 0 && timer <= 0)
        {
            gunStats.bulletsShot = gunStats.bulletsPerTap;
            Shoot();
        }

        if(timer > 0 )
            timer -= Time.deltaTime;

    }

    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", gunStats.reloadTime);
    }

    void Shoot()
    {

        float randomNumberX = Random.Range(-gunStats.spreadValue, gunStats.spreadValue);
        float randomNumberY = Random.Range(-gunStats.spreadValue/2, gunStats.spreadValue/2);

        Vector2 spread = new Vector2(randomNumberX, randomNumberY);
        Vector2 shotPos = (Vector2)firePoint.position + spread;

        GameObject bullet1 = Instantiate(bullet, shotPos, firePoint.rotation);
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();

        bullet1.GetComponent<Bullet>().damage = gunStats.damage;

       
        rb.AddForce(firePoint.up * gunStats.bulletSpeed, ForceMode2D.Impulse);

        gunStats.bulletsLeft--;
        gunStats.bulletsShot--;

        if(gunStats.timeBetweenShooting > 0)
            timer = gunStats.timeBetweenShooting;

        if (gunStats.bulletsShot > 0 && gunStats.bulletsLeft > 0)
            Invoke("Shoot", gunStats.timeBetweenShots);
      
    }

    void ReloadFinished()
    {
        gunStats.bulletsLeft = gunStats.magazineSize;
        reloading = false;
    }

}
