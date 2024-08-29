using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieGroupGroupsUI : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject diceManager;

    public GameObject deleteBtn;
    public GameObject loadBtn;

    public static int selectedDieGroupGroup;

    public GameObject dieGroupGroupPrefab;

    public List<GameObject> dieGroupGroupPanels = new List<GameObject>();

    //scroll objects
    public GameObject scrollView;
    public GameObject scrollContent;

    public void Init()
    {
        SaveDieGroupGroup.LoadFromJSON();
        CreateDieGroupGroupPanels();
    }
    public void CreateDieGroupGroupPanels()
    {
        //reset panels
        foreach (GameObject panel in dieGroupGroupPanels)
        {
            Destroy(panel);
        }
        dieGroupGroupPanels.Clear();

        //create panels
        foreach (KeyValuePair<int, string> entry in SaveDieGroupGroup.dieGroupGroupNames)
        {
            GameObject panel = Instantiate(dieGroupGroupPrefab, Vector3.zero, Quaternion.identity, scrollContent.transform);
            //panel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
            panel.GetComponent<DieGroupGroupPanel>().Init(entry.Key, entry.Value);
            dieGroupGroupPanels.Add(panel);
        }

        //set scrollContent height
        //scrollContent.GetComponent<RectTransform>().Axis.Vertical = 500;
        int newHeight = Presets.dieGroups.Count * 175 + 25;
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1820, newHeight);
        scrollContent.transform.localPosition = new Vector2(0, newHeight * -0.5f);
    }
    public void SelectPanel(int _selectedDieGroupGroup)
    {
        selectedDieGroupGroup = _selectedDieGroupGroup;
        foreach (GameObject panel in dieGroupGroupPanels)
        {
            panel.GetComponent<DieGroupGroupPanel>().DeselectPanel();
        }
    }
    public void DeleteBtn()
    {
        foreach (GameObject g in dieGroupGroupPanels)
        {
            if (g.GetComponent<DieGroupGroupPanel>().dieGroupGroupId == selectedDieGroupGroup)
            {
                SaveDieGroupGroup.DeleteDieGroupGroup(selectedDieGroupGroup);
                Destroy(g);
                CreateDieGroupGroupPanels();
                break;
            }
        }
    }
    public void LoadBtn()
    {
        List<DieGroup> newDieGroups = new List<DieGroup>();
        foreach(DieGroup _dieGroup in SaveDieGroupGroup.allDieGroups)
        {
            if (_dieGroup.dieGroupGroupId == selectedDieGroupGroup)
            {
                _dieGroup.GetNewGroupID();
                newDieGroups.Add(_dieGroup);
            }
        }
        diceManager.GetComponent<DiceManager>().dieGroups = newDieGroups;
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
    }
    public void BackBtn()
    {
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
    }
}

