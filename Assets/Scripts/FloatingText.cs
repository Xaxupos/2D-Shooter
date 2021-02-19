using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{


    public static FloatingText Create(Vector3 position, string text, Vector2 offset)
    {
        
        GameObject floatingText = Instantiate(GameManager.GetFloatingText());
        floatingText.transform.position = (Vector2)position + offset;
        floatingText.GetComponent<TMP_Text>().text = text;
        return floatingText.GetComponent<FloatingText>();
    }
  


}

