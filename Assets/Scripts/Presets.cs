using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class Presets
{
    public static List<DieGroup> dieGroups = new List<DieGroup>();

    private static int presetId = 0;

    public static void SaveToJSON()
    {
        SetPresetIDs();
        PresetsString presetsString = new PresetsString(dieGroups);
        string filePath = Application.persistentDataPath + "/Presets.json";
        string presetsData = JsonUtility.ToJson(presetsString);
        System.IO.File.WriteAllText(filePath, presetsData);
    }

    public static void LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/Presets.json";
        if (!System.IO.File.Exists(filePath)){SaveToJSON();}

        string jsonString = System.IO.File.ReadAllText(filePath);
        PresetsString presetsString = JsonUtility.FromJson<PresetsString>(jsonString);
        dieGroups.Clear();
        dieGroups = presetsString.ConvertToDiegroups();
        Debug.Log("Loaded " + dieGroups.Count.ToString() + " preset(s) from: " + filePath);
        SetPresetIDs();
    }
    public static void SetPresetIDs()
    {
        presetId = 0;
        foreach (DieGroup _dieGroup in dieGroups)
        {
            _dieGroup.groupId = presetId;
            presetId++;
        }
    }

    public static void AddPreset(DieGroup _dieGroup)
    {
        LoadFromJSON();
        dieGroups.Add(_dieGroup);
        SaveToJSON();
    }
    public static void UpdatePreset(DieGroup _dieGroup)
    {
        Debug.Log("Updating preset: " + _dieGroup.groupId.ToString());
        for (int i = 0; i < dieGroups.Count; i++)
        {
            if (dieGroups[i].groupId == _dieGroup.groupId)
            {
                dieGroups[i] = _dieGroup;
                break;
            }
        }
        SaveToJSON();
    }
    public static void DeletePreset(DieGroup _dieGroup)
    {
        Debug.Log("Deleting preset: " + _dieGroup.groupId.ToString());
        for (int i = 0; i < dieGroups.Count; i++)
        {
            if (dieGroups[i].groupId == _dieGroup.groupId)
            {
                dieGroups.RemoveAt(i);
                break;
            }
        }
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
        foreach (string s in strings)
        {
            if (s != "")
            {
                DieGroup dieGroup = JsonUtility.FromJson<DieGroup>(s);
                presets.Add(dieGroup);
            }
        }
        return presets;
    }
}
