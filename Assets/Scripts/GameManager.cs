using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject floatingText;

    private void Awake()
    {
        instance = this;
    }

    public static GameObject GetFloatingText()
    {
        return instance.floatingText;
    }

}
