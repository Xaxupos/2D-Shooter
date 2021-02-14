using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSystem : MonoBehaviour
{

    GameObject textAmmoGO;
    public Text textAmmo;


    //Gun stats
    public float bulletSpeed;
    public float timeBetweenShooting;
    public float timeBetweenShots;
    public float shootingSpeed;
    public float reloadTime;
    public float range;

    public int damage;
    public int magazineSize;
    public int bulletsPerTap;
    public int bulletsLeft;
    public int bulletsShot;

    //Bools
    public bool allowButtonHold;
    private bool shooting;
    private bool readyToShoot;
    private bool reloading;

    //References
    GameObject firePointGO;
    public Transform firePoint;

    public GameObject bullet;


    private void Awake()
    {
        textAmmoGO = GameObject.FindGameObjectWithTag("Ammo");
        textAmmo = textAmmoGO.gameObject.GetComponent<Text>();
        firePointGO = GameObject.FindGameObjectWithTag("FirePoint");
        firePoint = firePointGO.gameObject.GetComponent<Transform>();

        bullet = GameObject.FindGameObjectWithTag("Bullet");

        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        UpdateAmmoUI();
        
    }

    void UpdateAmmoUI()
    {
        textAmmo.text = bulletsLeft + "/" + magazineSize;
    }

    void MyInput()
    {
        if (allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
            

    }

    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    void Shoot()
    {
        readyToShoot = false;

        GameObject bullet1 = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet1.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
      
    }

    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
