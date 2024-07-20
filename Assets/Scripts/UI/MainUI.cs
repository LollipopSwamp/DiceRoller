using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    //private GameObject parent;
    private static bool showMainUI = false;

    //menu gameobjects
    public GameObject mainSetup;
    public GameObject dieGroupSetupTitle;
    public GameObject dieGroupBasicSetup;
    public GameObject dieGroupToHitSetup;
    public GameObject dieGroupDamageSetup;

    //current menu showing
    private static int nextSetupMenu = 0;

    void Start()
    {
        mainSetup.GetComponent<Canvas>().enabled = true;
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
        dieGroupBasicSetup.GetComponent<Canvas>().enabled = false;
        dieGroupToHitSetup.GetComponent<Canvas>().enabled = true;
        dieGroupDamageSetup.GetComponent<Canvas>().enabled = false;
    }

    public void ToggleVisibility()
    {
        showMainUI = !showMainUI;
        gameObject.GetComponent<Canvas>().enabled = showMainUI;
        HideAll();
    }

    private void HideAll()
    {
        mainSetup.GetComponent<Canvas>().enabled = false;
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
        dieGroupBasicSetup.GetComponent<Canvas>().enabled = false;
        dieGroupToHitSetup.GetComponent<Canvas>().enabled = false;
        dieGroupDamageSetup.GetComponent<Canvas>().enabled = false;
    }
    public void ShowDieSetup()
    {
        HideAll();
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = true;
        NextDieGroupPanel();
    }
    public void NextDieGroupPanel()
    {
        switch(nextSetupMenu)
        {
            case 0:
                dieGroupBasicSetup.GetComponent<Canvas>().enabled = true;
                break;
            case 1:
                dieGroupToHitSetup.GetComponent<Canvas>().enabled = true;
                break;
            case 2:
                dieGroupDamageSetup.GetComponent<Canvas>().enabled = true;
                break;
        }
        nextSetupMenu++;
    }
}
