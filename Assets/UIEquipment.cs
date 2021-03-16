using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : MonoBehaviour
{
    public Equipment eq;
    public GameObject slotPrefab;
    public Transform slotPanel;

    private int numberOfSlots;

    private void Start()
    {
        Debug.Log(eq.GetBoughtGun().Count);
        numberOfSlots = eq.GetBoughtGun().Count;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);           
        }
    }


}
