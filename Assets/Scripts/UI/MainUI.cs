using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    //private GameObject parent;
    private static bool showMainUI = true;

    //die groups to roll prefabs
    public GameObject dieGroupPanelPrefabAttack;
    public GameObject dieGroupPanelPrefabStandard;

    //panel objects
    public List<GameObject> dieGroupPanels = new List<GameObject>();

    //menu gameobjects
    public GameObject diceManager;
    public GameObject dieGroupSetup;
    public GameObject mainSetup;
    public GameObject dieGroupSetupTitle;
    public GameObject dieGroupBasicSetup;
    public GameObject dieGroupToHitSetup;
    public GameObject dieGroupDamageSetup;
    public GameObject rollingDiceUI;

    public GameObject resultsUI;

    //current menu showing
    private static int nextSetupMenu = 0;

    void Start()
    {
        mainSetup.GetComponent<Canvas>().enabled = true;
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
        dieGroupBasicSetup.GetComponent<Canvas>().enabled = false;
        dieGroupToHitSetup.GetComponent<Canvas>().enabled = false;
        dieGroupDamageSetup.GetComponent<Canvas>().enabled = false;

        //NextDieGroupPanel();
    }

    public void SetVisibility(bool visible)
    {
        HideAll();
        showMainUI = visible;
        gameObject.GetComponent<Canvas>().enabled = visible;
        mainSetup.GetComponent<Canvas>().enabled = visible;
        if (visible) { InitDieGroupPanels(); }
    }

    public void SetRollingDiceUIVisibility(bool visible)
    {
        if (visible) { gameObject.GetComponent<Canvas>().enabled = false; } 
        else { resultsUI.GetComponent<Canvas>().enabled = true; }
        
        rollingDiceUI.GetComponent<Canvas>().enabled = visible;
    }
    

    private void HideAll()
    {
        mainSetup.GetComponent<Canvas>().enabled = false;
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
        dieGroupBasicSetup.GetComponent<Canvas>().enabled = false;
        dieGroupToHitSetup.GetComponent<Canvas>().enabled = false;
        dieGroupDamageSetup.GetComponent<Canvas>().enabled = false;
        rollingDiceUI.GetComponent<Canvas>().enabled = false;
    }

    public void CreateDiegroupBtn()
    {
        nextSetupMenu = 0;
        dieGroupSetup.GetComponent<DieGroupSetup>().Init();
        NextDieGroupPanel();
    }
    public void PreviousDieGroupPanel()
    {
        HideAll();
        nextSetupMenu--;
        Debug.Log("Showing prev menu: " + nextSetupMenu.ToString());
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = true;
        switch (nextSetupMenu)
        {
            case 0:
                dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
                mainSetup.GetComponent<Canvas>().enabled = true;
                InitDieGroupPanels();
                break;
            case 1:
                dieGroupBasicSetup.GetComponent<Canvas>().enabled = true;
                break;
            case 2:
                dieGroupToHitSetup.GetComponent<Canvas>().enabled = true;
                dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
                break;
            case 3:
                dieGroupDamageSetup.GetComponent<Canvas>().enabled = true;
                break;
        }
    }
    public void NextDieGroupPanel()
    {
        HideAll();
        Debug.Log("Showing next menu: " + nextSetupMenu.ToString());
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = true;
        switch (nextSetupMenu)
        {
            case 0:
                dieGroupBasicSetup.GetComponent<Canvas>().enabled = true;
                break;
            case 1:
                dieGroupToHitSetup.GetComponent<Canvas>().enabled = true;
                dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
                break;
            case 2:
                dieGroupDamageSetup.GetComponent<Canvas>().enabled = true;
                break;
            case 3:
                dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
                mainSetup.GetComponent<Canvas>().enabled = true;
                InitDieGroupPanels();
                break;
        }
        nextSetupMenu++;
    }
    public void DuplicateDieGroup(int _groupId)
    {
        diceManager.GetComponent<DiceManager>().DuplicateDieGroup(_groupId);
        InitDieGroupPanels();
    }
    public void EditDieGroup(int _groupId)
    {
        foreach(DieGroup _dieGroup in diceManager.GetComponent<DiceManager>().dieGroups)
        {
            if (_dieGroup.groupId == _groupId)
            {
                nextSetupMenu = 0;
                dieGroupSetup.GetComponent<DieGroupSetup>().Init(_dieGroup);
                NextDieGroupPanel();
                return;
            }
        }
    }
    public void DeleteDieGroup(int _groupId)
    {
        diceManager.GetComponent<DiceManager>().DeleteDieGroup(_groupId);
        InitDieGroupPanels();
    }
    public void InitDieGroupPanels()
    {
        //reset panels
        foreach (GameObject panel in dieGroupPanels)
        {
            Destroy(panel);
        }

        //create panels
        for (int i = 0; i < diceManager.GetComponent<DiceManager>().dieGroups.Count; i++)
        {
            //create panel from prefab
            if (diceManager.GetComponent<DiceManager>().dieGroups[i].toHitType == DieGroup.ToHitType.None)
            {
                GameObject dieGroupPanel = Instantiate(dieGroupPanelPrefabStandard, Vector3.zero, Quaternion.identity, mainSetup.transform);
                dieGroupPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                dieGroupPanel.GetComponent<DieGroupPanel>().Init(diceManager.GetComponent<DiceManager>().dieGroups[i]);
                dieGroupPanels.Add(dieGroupPanel);
            }
            else
            {
                GameObject dieGroupPanel = Instantiate(dieGroupPanelPrefabAttack, Vector3.zero, Quaternion.identity, mainSetup.transform);
                dieGroupPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                dieGroupPanel.GetComponent<DieGroupPanel>().Init(diceManager.GetComponent<DiceManager>().dieGroups[i]);
                dieGroupPanels.Add(dieGroupPanel);
            }
        }
    }
}
