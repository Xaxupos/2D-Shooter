using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemDatabase;
    public UIInventory inventoryUI;

    private void Start()
    {    
        GiveItem(0);
        GiveItem(1);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
            if(inventoryUI.gameObject.activeInHierarchy == true)
            {
                Time.timeScale = 0;
            } 
            else if(inventoryUI.gameObject.activeInHierarchy == false)
            {
                Time.timeScale = 1;
            }
        }
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.name);
    }

   /* public void GiveItem(string itemName)
    {
        Item itemToAdd = itemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.name);
    }
   */
    
    

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }
    
    public void RemoveItem(int id)
    {
        Item item = CheckForItem(id);
        if(item!=null)
        {
            characterItems.Remove(item);
            inventoryUI.RemoveItem(item);
            Debug.Log("Item removed: " + item.name);
        }
    }

}
