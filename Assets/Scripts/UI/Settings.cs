using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public enum CritType { DoubleDice, DoubleTotal, MaxPlusRoll };
    public static CritType critType = CritType.DoubleDice;
    public int critTypeIndex = 0;

    public List<GameObject> critTypeButtons = new List<GameObject>();
    public GameObject exampleText;
    public List<string> exampleStrings = new List<string>();

    public GameObject uiManager;

    private SettingsJSON settingsJson = new SettingsJSON();

    void Start()
    {
        LoadFromJSON();
    }

    public static int CritTypeIndex()
    {
        switch (critType)
        {
            case CritType.DoubleDice:
                return 0;
            case CritType.DoubleTotal:
                return 1;
            case CritType.MaxPlusRoll:
                return 2;
            default:
                return -1;
        }
    }

    public void Init()
    {
        UpdateButtons(CritTypeIndex());
    }

    public void UpdateButtons(int _critTypeIndex)
    {
        critTypeIndex = _critTypeIndex;
        //set outlines
        foreach (GameObject button in critTypeButtons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
        critTypeButtons[_critTypeIndex].GetComponent<Outline>().enabled = true;

        //set static variable
        SetCritType(_critTypeIndex);

        //set example text
        exampleText.GetComponent<TMP_Text>().text = exampleStrings[_critTypeIndex];

    }

    public static void SetCritType(int _critTypeIndex)
    {
        switch (_critTypeIndex)
        {
            case 0:
                critType = CritType.DoubleDice;
                break;
            case 1:
                critType = CritType.DoubleTotal;
                break;
            case 2:
                critType = CritType.MaxPlusRoll;
                break;
        }
    }
    public string GetExampleText()
    {
        return exampleStrings[CritTypeIndex()];
    }

    public void BackBtn()
    {
        SaveToJSON();
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
    }

    public void SaveToJSON()
    {
        SettingsJSON settingsJson = new SettingsJSON(critTypeIndex);
        settingsJson.SaveToJSON();
    }

    public void LoadFromJSON()
    {
        SettingsJSON settingsJson = new SettingsJSON();
        settingsJson.LoadFromJSON();
    }
}
public class SettingsJSON
{
    public int critTypeIndex = 0;

    public SettingsJSON()
    {
        critTypeIndex = 0;
    }
    public SettingsJSON(int _critTypeIndex)
    {
        critTypeIndex = _critTypeIndex;
    }

    public void SaveToJSON()
    {
        string filePath = Application.persistentDataPath + "/Settings.json";
        string settingsData = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(filePath, settingsData);
        Debug.Log("Saved settings to: " + filePath + " || Crit Type: " + Settings.critType.ToString());
    }

    public void LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/Settings.json";
        if (!System.IO.File.Exists(filePath)) { SaveToJSON(); }

        string jsonString = System.IO.File.ReadAllText(filePath);
        SettingsJSON settingsJson = JsonUtility.FromJson<SettingsJSON>(jsonString);
        critTypeIndex = settingsJson.critTypeIndex;
        Settings.SetCritType(critTypeIndex);
        Debug.Log("Loaded settings from: " + filePath + " || Crit Type: " + Settings.critType.ToString());
    }
}
