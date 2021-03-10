using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bilboard : MonoBehaviour
{

    public Transform target;
    public Vector2 offset;


    private void Update()
    {
        transform.position = ((Vector2)target.position + offset);
        transform.rotation = Quaternion.identity;
    }

}
