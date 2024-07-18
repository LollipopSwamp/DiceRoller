using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    //private GameObject parent;
    private static bool showMainUI = false;

    void Start()
    {

    }

    public void ToggleVisibility()
    {
        showMainUI = !showMainUI;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
