using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/New Gun")]
public class GunStats : ScriptableObject
{

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
    public float spreadValue;

    //Bools
    public bool allowButtonHold;

    //Sprites
    public Sprite gunSprite;
    


}
