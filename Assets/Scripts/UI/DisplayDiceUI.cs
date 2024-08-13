using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDiceUI : MonoBehaviour
{
    public GameObject uiManager;

    public void ShowButton()
    {
        uiManager.GetComponent<UIManager>().ShowResultsUI();
    }
    public void SetupButton()
    {
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
    }
}
