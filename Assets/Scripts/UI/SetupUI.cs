using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SetupUI : MonoBehaviour
{
    //die groups to roll prefabs
    public GameObject dieGroupPanelPrefabAttack;
    public GameObject dieGroupPanelPrefabStandard;

    //panel objects
    public List<GameObject> dieGroupPanels = new List<GameObject>();

    //menu gameobjects
    public GameObject diceManager;
    public GameObject uiManager;
    public GameObject scrollContent;

    public GameObject mainSetup;

    public GameObject dieGroupSetup;
    public GameObject dieGroupSetupTitle;
    public GameObject dieGroupBasicSetup;
    public GameObject dieGroupToHitSetup;
    public GameObject dieGroupDamageSetup;

    public GameObject presetsMenu;
    public GameObject settingsMenu;

    public GameObject rollingDiceUI;

    public GameObject resultsUI;
    public GameObject resultDetails;

    public GameObject rollBtn;

    //current menu showing
    public static int nextSetupMenu = 0;

    public void Init()
    {
        InitDieGroupPanels();
        UpdateRollBtn();
    }

    public void CreateDiegroupBtn()
    {
        uiManager.GetComponent<UIManager>().ShowDieGroupSetup();
    }
    public void LoadDieGroupPresetBtn()
    {
        uiManager.GetComponent<UIManager>().ShowPresetsUI();
    }
    public void Settingsbtn()
    {
        settingsMenu.GetComponent<Canvas>().enabled = true;
        settingsMenu.GetComponent<Settings>().Init();
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
                dieGroupSetup.GetComponent<DieGroupSetup>().Init(_dieGroup,1);
                return;
            }
        }
    }
    public void DeleteDieGroup(int _groupId)
    {
        diceManager.GetComponent<DiceManager>().DeleteDieGroup(_groupId);
        SaveSession.Save(diceManager.GetComponent<DiceManager>().dieGroups);
        InitDieGroupPanels();
    }
    public void InitDieGroupPanels()
    {
        //reset panels
        foreach (GameObject panel in dieGroupPanels){Destroy(panel);}
        dieGroupPanels.Clear();

        //delete diegroup objects
        diceManager.GetComponent<DiceManager>().DestroyDieGroups();

        //create panels
        int scrollHeight = 0;
        for (int i = 0; i < diceManager.GetComponent<DiceManager>().dieGroups.Count; i++)
        {
            //create panel from prefab
            if (diceManager.GetComponent<DiceManager>().dieGroups[i].dieGroupType == 1)
            {
                GameObject dieGroupPanel = Instantiate(dieGroupPanelPrefabAttack, Vector3.zero, Quaternion.identity, scrollContent.transform);
                //dieGroupPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                dieGroupPanel.GetComponent<DieGroupPanel>().Init(diceManager.GetComponent<DiceManager>().dieGroups[i]);
                dieGroupPanels.Add(dieGroupPanel);
            }
            else
            {
                GameObject dieGroupPanel = Instantiate(dieGroupPanelPrefabStandard, Vector3.zero, Quaternion.identity, scrollContent.transform);
                //dieGroupPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                dieGroupPanel.GetComponent<DieGroupPanel>().Init(diceManager.GetComponent<DiceManager>().dieGroups[i]);
                dieGroupPanels.Add(dieGroupPanel);
            }
        }
        ResizeScrollContent();
    }
    public void ResizeScrollContent()
    {
        int newHeight = dieGroupPanels.Count * 200 + 25;
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1820, newHeight);
        scrollContent.transform.localPosition = new Vector2(0, newHeight * -0.5f);
    }
    public void UpdateRollBtn()
    {
        //enable roll btn
        if (diceManager.GetComponent<DiceManager>().dieGroups.Count > 0)
        {
            rollBtn.GetComponent<Button>().interactable = true;
        }
        else
        {
            rollBtn.GetComponent<Button>().interactable = false;
        }
    }
}
