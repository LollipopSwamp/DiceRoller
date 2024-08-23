using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DateTime = System.DateTime;

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

    public GameObject errorTextPanel;
    public GameObject errorText;
    public bool errorShown = false;
    public DateTime errorStart;

    public Color uiBackground = new Color(223, 223, 223,63);

    void Start()
    {
        mainSetupUI.GetComponent<Canvas>().enabled = true;
        presetsUI.GetComponent<Canvas>().enabled = true;

        dieGroupSetup.GetComponent<Canvas>().enabled = true;
        foreach(GameObject menu in dieGroupSetupMenus)
        {
            menu.GetComponent<Canvas>().enabled = true;
        }

        resultsUI.GetComponent<Canvas>().enabled = true;
        resultDetailsUI.GetComponent<Canvas>().enabled = true;
        diceRolledUI.GetComponent<Canvas>().enabled = true;

        settings.GetComponent<Canvas>().enabled = true;
        HideError();
        //ShowError("Cannot create DieGroup with no dice");
        ShowMainSetupUI();
    }

    void Update()
    {
        if (errorShown && DateTime.Now > errorStart.AddSeconds(3))
        {
            HideError();
        }
    }

    public void ShowError(string errorMessage)
    {
        errorStart = DateTime.Now;
        errorTextPanel.SetActive(true);
        errorShown = true;
        errorText.GetComponent<TMP_Text>().text = errorMessage;

    }
    public void HideError()
    {
        errorTextPanel.SetActive(false);
        errorShown = false;
    }

    public void HideAll()
    {
        mainSetupUI.SetActive(false);
        presetsUI.SetActive(false);

        dieGroupSetup.SetActive(false);
        foreach (GameObject menu in dieGroupSetupMenus)
        {
            menu.SetActive(false);
        }

        resultsUI.SetActive(false);
        resultDetailsUI.SetActive(false);
        diceRolledUI.SetActive(false);

        settings.SetActive(false);
    }

    public void ShowMainSetupUI()
    {
        HideAll();
        mainSetupUI.SetActive(true);
        mainSetupUI.GetComponent<SetupUI>().Init();
    }

    public void ShowDiceRolledUI()
    {
        HideAll();
        diceRolledUI.SetActive(true);
    }

    public void ShowPresetsUI()
    {
        HideAll();
        presetsUI.SetActive(true);
        presetsUI.GetComponent<PresetsMenu>().Init();
    }

    public void ShowDieGroupSetup()
    {
        HideAll();
        dieGroupSetup.SetActive(true);
        dieGroupSetup.GetComponent<DieGroupSetup>().Init();
        nextSetupMenu = 0;
        NextDieGroupMenu();
    }

    public void ShowDieGroupSetup(DieGroup _dieGroup, int _editMode)
    {
        HideAll();
        dieGroupSetup.SetActive(true);
        dieGroupSetup.GetComponent<DieGroupSetup>().Init(_dieGroup, _editMode);
        NextDieGroupMenu();
    }
    public void PreviousDieGroupMenu()
    {
        foreach (GameObject menu in dieGroupSetupMenus) { menu.SetActive(false); }
        nextSetupMenu--;
        switch (nextSetupMenu)
        {
            case 0:
                ShowMainSetupUI();
                break;
            case 1:
                dieGroupSetupMenus[0].SetActive(true);
                break;
            case 2:
                dieGroupSetupMenus[1].SetActive(true);
                break;
            case 3:
                dieGroupSetupMenus[2].SetActive(true);
                break;
        }
    }
    public void NextDieGroupMenu()
    {
        foreach (GameObject menu in dieGroupSetupMenus) { menu.SetActive(false); }
        switch (nextSetupMenu)
        {
            case 0:
                dieGroupSetupMenus[0].SetActive(true);
                break;
            case 1:
                dieGroupSetupMenus[1].SetActive(true);
                dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
                break;
            case 2:
                dieGroupSetupMenus[2].SetActive(true);
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
        resultsUI.SetActive(true);
        List<GameObject> dieGroupObjects = diceManager.GetComponent<DiceManager>().dieGroupObjects;
        resultsUI.GetComponent<ResultsUI>().CreateResultsPanels(dieGroupObjects);
    }

    public void ShowResultDetails(DieGroupBehaviour _dieGroupB)
    {
        HideAll();
        resultDetailsUI.SetActive(true);
        resultDetailsUI.GetComponent<ResultDetails>().Init(_dieGroupB);
    }

    public void ShowSettings()
    {
        HideAll();
        settings.SetActive(true);
        settings.GetComponent<Settings>().Init();
    }
}
