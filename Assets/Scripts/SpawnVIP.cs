using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVIP : MonoBehaviour
{

    public GameObject vip;
    public GameObject civilian;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(vip, transform.position, transform.rotation);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(civilian, transform.position, transform.rotation);
        }
    }
}
