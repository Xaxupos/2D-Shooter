using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{

    [SerializeField]
    private List<Slot> slots;
                      
    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            
            Sprite tempSpriteSlot;
            Sprite tempGunSpriteSlot;
            List<Sprite> spritesCopy = new List<Sprite>();
            List<Sprite> spritesGunCopy = new List<Sprite>();

            foreach (Slot slot in slots)
            {
                spritesCopy.Add(slot.GetSprite());
                spritesGunCopy.Add(slot.GetGunSprite());
            }

            slots[spritesCopy.Count - 1].SetImage(spritesCopy[0]);
            slots[spritesGunCopy.Count - 1].SetGunImage(spritesGunCopy[0]);

            for (int i = slots.Count - 2; i >= 0; i--)
            {
                tempSpriteSlot = spritesCopy[i + 1];
                tempGunSpriteSlot = spritesGunCopy[i + 1];
                slots[i].SetImage(tempSpriteSlot);
                slots[i].SetGunImage(tempGunSpriteSlot);
            }


        }
        
        if (Input.mouseScrollDelta.y < 0)
        {
            Sprite tempSpriteSlot;
            Sprite tempGunSpriteSlot;

            List<Sprite> spritesCopy = new List<Sprite>();
            List<Sprite> spritesGunCopy = new List<Sprite>();

            foreach (Slot slot in slots)
            {
                spritesCopy.Add(slot.GetSprite());
                spritesGunCopy.Add(slot.GetGunSprite());
            }


            slots[0].SetImage(spritesCopy[spritesCopy.Count - 1]);
            slots[0].SetGunImage(spritesGunCopy[spritesGunCopy.Count - 1]);

            for (int i = 1; i < slots.Count; i++)
            {
                tempSpriteSlot = spritesCopy[i - 1];
                tempGunSpriteSlot = spritesGunCopy[i - 1];
                slots[i].SetImage(tempSpriteSlot);
                slots[i].SetGunImage(tempGunSpriteSlot);

            }
        }
    }
    
}