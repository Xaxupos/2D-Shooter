using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    [System.Serializable]
    public class Gun
    {
        public GunType type;
        public GunStats stats;
    }

    public enum GunType
    {
        Pistol,
        Shotgun,
        Sniper
    }

    private void Start()
    {
        Debug.Log("Liczba posiadanych broni: "+boughtGuns.Count);
    }

    [SerializeField]
    private List<Gun> guns;
    private List<Gun> boughtGuns = new List<Gun>();

    public void AddGun(GunType type)
    {

        foreach(Gun gun in guns)
        {
           
            if (type == gun.type)
            {
                gun.stats.bulletsLeft = gun.stats.magazineSize;
                
                boughtGuns.Add(gun);
            }
        }
    }

    public Gun GetGun(int value)
    {
        return boughtGuns[value];
    }

    public List<Gun> GetBoughtGun()
    {
        return boughtGuns;
    }

}
