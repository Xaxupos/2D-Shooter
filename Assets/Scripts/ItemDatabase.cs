using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
        Debug.Log("Liczba itemow w bazie: " + items.Count);
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.name == itemName);
    }

    void BuildDatabase()
    {
        //Lista wszystkich itemów
        items = new List<Item>()
        {
            new Item(0,"Apple","Just an apple."),
            new Item(1,"Rock","Throw it away.")
             
        };
            
    }


}
