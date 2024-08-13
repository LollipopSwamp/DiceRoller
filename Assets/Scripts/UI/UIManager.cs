using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject diceManager;
    public GameObject mainSetupUI;
    public GameObject presetsUI;

    public GameObject dieGroupSetup;
    public List<GameObject> dieGroupSetupMenus = new List<GameObject>();
    public static int nextSetupMenu = 0;

    public GameObject resultsUI;
    public GameObject resultDetailsUI;
    public GameObject diceRolledUI;

    public GameObject settings;

    void Start()
    {
        ShowMainSetupUI();
    }

    public void HideAll()
    {
        mainSetupUI.GetComponent<Canvas>().enabled = false;
        presetsUI.GetComponent<Canvas>().enabled = false;

        dieGroupSetup.GetComponent<Canvas>().enabled = false;

        resultsUI.GetComponent<Canvas>().enabled = false;
        resultDetailsUI.GetComponent<Canvas>().enabled = false;
        diceRolledUI.GetComponent<Canvas>().enabled = false;

        settings.GetComponent<Canvas>().enabled = false;
    }

    public void ShowMainSetupUI()
    {
        HideAll();
        mainSetupUI.GetComponent<Canvas>().enabled = true;
        mainSetupUI.GetComponent<SetupUI>().Init();
    }

    public void ShowDiceRolledUI()
    {
        HideAll();
        diceRolledUI.GetComponent<Canvas>().enabled = true;
    }

    public void ShowPresetsUI()
    {
        HideAll();
        presetsUI.GetComponent<Canvas>().enabled = true;
        presetsUI.GetComponent<PresetsMenu>().Init();
    }

    public void ShowDieGroupSetup()
    {
        HideAll();
        dieGroupSetup.GetComponent<Canvas>().enabled = true;
        dieGroupSetup.GetComponent<DieGroupSetup>().Init();
        NextDieGroupMenu();
    }

    public void ShowDieGroupSetup(DieGroup _dieGroup, int _editMode)
    {
        HideAll();
        dieGroupSetup.GetComponent<Canvas>().enabled = true;
        dieGroupSetup.GetComponent<DieGroupSetup>().Init(_dieGroup, _editMode);
        NextDieGroupMenu();
    }
    public void PreviousDieGroupMenu()
    {
        foreach (GameObject menu in dieGroupSetupMenus) { menu.GetComponent<Canvas>().enabled = false; }
        nextSetupMenu--;
        switch (nextSetupMenu)
        {
            case 0:
                ShowMainSetupUI();
                break;
            case 1:
                dieGroupSetupMenus[0].GetComponent<Canvas>().enabled = true;
                break;
            case 2:
                dieGroupSetupMenus[1].GetComponent<Canvas>().enabled = true;
                break;
            case 3:
                dieGroupSetupMenus[2].GetComponent<Canvas>().enabled = true;
                break;
        }
    }
    public void NextDieGroupMenu()
    {
        foreach (GameObject menu in dieGroupSetupMenus) { menu.GetComponent<Canvas>().enabled = false; }
        switch (nextSetupMenu)
        {
            case 0:
                dieGroupSetupMenus[0].GetComponent<Canvas>().enabled = true;
                break;
            case 1:
                dieGroupSetupMenus[1].GetComponent<Canvas>().enabled = true;
                dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
                break;
            case 2:
                dieGroupSetupMenus[2].GetComponent<Canvas>().enabled = true;
                break;
            case 3:
                ShowMainSetupUI();
                break;
        }
        nextSetupMenu++;
    }
    public void SaveDieGroupPreset(DieGroup _dieGroup)
    {
        Presets.AddPreset(_dieGroup);
    }

    public void ShowResultsUI()
    {
        HideAll();
        resultsUI.GetComponent<Canvas>().enabled = true;
        List<GameObject> dieGroupObjects = diceManager.GetComponent<DiceManager>().dieGroupObjects;
        resultsUI.GetComponent<ResultsUI>().CreateResultsPanels(dieGroupObjects);
    }

    public void ShowResultDetails(DieGroupBehaviour _dieGroupB)
    {
        HideAll();
        resultDetailsUI.GetComponent<Canvas>().enabled = true;
        resultDetailsUI.GetComponent<ResultDetails>().Init(_dieGroupB);
    }

    public void ShowSettings()
    {
        HideAll();
        settings.GetComponent<Canvas>().enabled = true;
    }
}
