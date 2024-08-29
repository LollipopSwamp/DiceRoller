using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetsMenu : MonoBehaviour
{
    public GameObject uiManager;
    public GameObject diceManager;

    public GameObject editBtn;
    public GameObject deleteBtn;
    public GameObject loadBtn;

    public DieGroup selectedPreset;

    public GameObject attackPresetPanelPrefab;
    public GameObject damagePresetPanelPrefab;

    public List<GameObject> presetPanels = new List<GameObject>();

    //scroll objects
    public GameObject scrollView;
    public GameObject scrollContent;

    public void Init()
    {
        Presets.LoadFromJSON();
        CreatePresetPanels();
    }
    public void CreatePresetPanels()
    {
        //reset panels
        Debug.Log(presetPanels.Count);
        foreach (GameObject panel in presetPanels)
        {
            Destroy(panel);
        }
        presetPanels.Clear();

        //create panels
        for (int i = 0; i < Presets.dieGroups.Count; i++)
        {
            //create panel from prefab
            if (Presets.dieGroups[i].dieGroupType == 1)
            {
                GameObject panel = Instantiate(attackPresetPanelPrefab, Vector3.zero, Quaternion.identity, scrollContent.transform);
                //panel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                panel.GetComponent<PresetPanel>().Init(i, Presets.dieGroups[i]);
                presetPanels.Add(panel);
            }
            else
            {
                GameObject panel = Instantiate(damagePresetPanelPrefab, Vector3.zero, Quaternion.identity, scrollContent.transform);
                //panel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                panel.GetComponent<PresetPanel>().Init(i, Presets.dieGroups[i]);
                presetPanels.Add(panel);
            }
        }

        //set scrollContent height
        //scrollContent.GetComponent<RectTransform>().Axis.Vertical = 500;
        int newHeight = Presets.dieGroups.Count * 175 + 25;
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1820, newHeight);
        scrollContent.transform.localPosition = new Vector2(0, newHeight * -0.5f);
    }
    public void SelectPanel(int panelIndex)
    {
        editBtn.GetComponent<Button>().interactable = true;
        deleteBtn.GetComponent<Button>().interactable = true;
        loadBtn.GetComponent<Button>().interactable = true;
        selectedPreset = Presets.dieGroups[panelIndex];
        foreach(GameObject panel in presetPanels)
        {
            panel.GetComponent<PresetPanel>().DeselectPanel();
        }
    }
    public void EditBtn()
    {
        //selectedPreset.PrintDieGroup();
        uiManager.GetComponent<UIManager>().ShowDieGroupSetup(selectedPreset, 2);
    }
    public void DeleteBtn()
    {
        int presetId = selectedPreset.groupId;
        foreach (GameObject g in presetPanels)
        {
            if (g.GetComponent<PresetPanel>().dieGroup.groupId == presetId)
            {
                Presets.DeletePreset(selectedPreset);
                Destroy(g);
                CreatePresetPanels();
                break;
            }
        }
    }
    public void LoadBtn()
    {
        int presetId = selectedPreset.groupId;
        selectedPreset.GetNewGroupID();
        diceManager.GetComponent<DiceManager>().dieGroups.Add(selectedPreset);
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
        Debug.Log("Loaded preset (" + presetId.ToString() + ") with new groupId: " + selectedPreset.groupId.ToString());
    }
    public void BackBtn()
    {
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
    }
}
