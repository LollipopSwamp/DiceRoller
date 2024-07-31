using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetsMenu : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject diceManager;

    public GameObject loadBtn;

    public DieGroup selectedPreset;

    public GameObject attackPresetPanelPrefab;
    public GameObject damagePresetPanelPrefab;

    public List<GameObject> presetPanels = new List<GameObject>();

    public void Init()
    {
        Presets.LoadFromJSON();
        CreatePresetPanels();
    }
    public void CreatePresetPanels()
    {
        //reset panels
        foreach (GameObject panel in presetPanels)
        {
            Destroy(panel);
        }
        presetPanels.Clear();

        //create panels
        Debug.Log(Presets.dieGroups.Count);
        for (int i = 0; i < Presets.dieGroups.Count; i++)
        {
            //create panel from prefab
            if (Presets.dieGroups[i].toHitType == DieGroup.ToHitType.None)
            {
                GameObject panel = Instantiate(damagePresetPanelPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
                panel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                panel.GetComponent<PresetPanel>().Init(i, Presets.dieGroups[i]);
                presetPanels.Add(panel);
            }
            else
            {
                GameObject panel = Instantiate(attackPresetPanelPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
                panel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                panel.GetComponent<PresetPanel>().Init(i,Presets.dieGroups[i]);
                presetPanels.Add(panel);
            }
        }
    }
    public void SelectPanel(int panelIndex)
    {
        loadBtn.GetComponent<Button>().interactable = true;
        selectedPreset = Presets.dieGroups[panelIndex];
        foreach(GameObject panel in presetPanels)
        {
            panel.GetComponent<PresetPanel>().DeselectPanel();
        }
    }
    public void EditBtn()
    {

    }
    public void DeleteBtn()
    {

    }
    public void LoadBtn()
    {
        diceManager.GetComponent<DiceManager>().dieGroups.Add(selectedPreset);
        mainUI.GetComponent<MainUI>().SetVisibility(true);
    }
    public void BackBtn()
    {
        mainUI.GetComponent<MainUI>().SetVisibility(true);
    }
}
