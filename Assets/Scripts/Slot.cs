using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    private Sprite image;
    private Sprite weaponSprite;

    [SerializeField]
    private Image weaponImage;

    private void Start()
    {
        image = GetComponent<Image>().sprite;
        weaponSprite = weaponImage.sprite;
    }

    private void Update()
    {
        GetComponent<Image>().sprite = image;
        weaponImage.sprite = weaponSprite;
    }

    public void SetImage(Sprite sprite)
    {
        this.image = sprite;
    }

    public Sprite GetSprite() { return image; } 
    public Sprite GetGunSprite() { return weaponSprite; }

    public void SetGunImage(Sprite weaponSprite)
    {
        this.weaponSprite = weaponSprite;
    }

}
