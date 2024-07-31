using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class Presets
{
    public static List<DieGroup> dieGroups = new List<DieGroup>();

    public static void SaveToJSON()
    {
        PresetsString presetsString = new PresetsString(dieGroups);
        string filePath = Application.persistentDataPath + "/Presets.json";
        Debug.Log(filePath);
        string presetsData = JsonUtility.ToJson(presetsString);
        System.IO.File.WriteAllText(filePath, presetsData);
    }

    public static void LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/Presets.json";
        if (!System.IO.File.Exists(filePath)){SaveToJSON();}

        string jsonString = System.IO.File.ReadAllText(filePath);
        PresetsString presetsString = JsonUtility.FromJson<PresetsString>(jsonString);
        Debug.Log(presetsString.presetsString);
        dieGroups.Clear();
        dieGroups = presetsString.ConvertToDiegroups();
        Debug.Log("Presets loaded: " + dieGroups.Count.ToString());
    }

    public static void AddPreset(DieGroup _dieGroup)
    {
        LoadFromJSON();
        _dieGroup.groupId = -1;
        dieGroups.Add(_dieGroup);
        SaveToJSON();
    }

    public static DieGroup GetPreset(int _index)
    {
        return dieGroups[_index];
    }
}

public class PresetsString
{
    public string presetsString = "";

    public PresetsString(List<DieGroup> presets)
    {
        foreach (DieGroup dieGroup in presets)
        {
            string json = JsonUtility.ToJson(dieGroup);
            presetsString += json + "|";
        }
        if (presetsString != "")
        {
            presetsString = presetsString.Substring(0, presetsString.Length - 1);
        }
    }

    public List<DieGroup> ConvertToDiegroups()
    {
        string[] strings = presetsString.Split('|');
        List<DieGroup> presets = new List<DieGroup>();
        Debug.Log(strings[0]);
        foreach (string s in strings)
        {
            if (s != "")
            {
                DieGroup dieGroup = JsonUtility.FromJson<DieGroup>(s);
                dieGroup.PrintDieGroup();
                presets.Add(dieGroup);
            }
        }
        return presets;
    }
}
