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
    }

    private void HideAll()
    {
        mainSetup.GetComponent<Canvas>().enabled = false;
        dieGroupSetupTitle.GetComponent<Canvas>().enabled = false;
        dieGroupBasicSetup.GetComponent<Canvas>().enabled = false;
        dieGroupToHitSetup.GetComponent<Canvas>().enabled = false;
        dieGroupDamageSetup.GetComponent<Canvas>().enabled = false;
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
    public void InitDieGroupPanels()
    {
        List<GameObject> dieGroups = diceManager.GetComponent<DiceManager>().dieGroupObjects;
        //reset panels
        foreach (GameObject panel in dieGroupPanels)
        {
            Destroy(panel);
        }

        //create panels
        for (int i = 0; i < dieGroups.Count; i++)
        {
            //create panel from prefab
            DieGroupBehaviour dieGroupB = dieGroups[i].GetComponent<DieGroupBehaviour>();
            if (dieGroupB.dieGroup.toHitType == DieGroup.ToHitType.None)
            {
                GameObject dieGroupPanel = Instantiate(dieGroupPanelPrefabStandard, Vector3.zero, Quaternion.identity, mainSetup.transform);
                dieGroupPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                dieGroupPanel.GetComponent<DieGroupPanel>().Init(dieGroups[i]);
                dieGroupPanels.Add(dieGroupPanel);
            }
            else
            {
                GameObject dieGroupPanel = Instantiate(dieGroupPanelPrefabAttack, Vector3.zero, Quaternion.identity, mainSetup.transform);
                dieGroupPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                dieGroupPanel.GetComponent<DieGroupPanel>().Init(dieGroups[i]);
                dieGroupPanels.Add(dieGroupPanel);
            }
        }
    }
}
