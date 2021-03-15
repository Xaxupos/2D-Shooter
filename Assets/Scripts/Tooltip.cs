using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{

    private Text tooltipText;

    private void Start()
    {
        tooltipText = GetComponentInChildren<Text>();
        tooltipText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void GenerateTooltip(Item item)
    {
        
        tooltipText.text = item.name;
        tooltipText.gameObject.SetActive(true);
        gameObject.SetActive(true);
        

    }
    
}
