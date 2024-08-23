using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveSession
{
    public static List<DieGroup> dieGroups = new List<DieGroup>();

    private static int dieGroupId = 0;

    public static void Save(List<DieGroup> _dieGroups)
    {
        dieGroups = _dieGroups;
        SetDieGroupIDs();
        SaveSessionString saveSessionString = new SaveSessionString(dieGroups);
        string filePath = Application.persistentDataPath + "/SavedSession.json";
        string sessionData = JsonUtility.ToJson(saveSessionString);
        System.IO.File.WriteAllText(filePath, sessionData);
        Debug.Log("Saved " + dieGroups.Count.ToString() + " DieGroups(s) to Saved Session: " + filePath);
    }

    public static List<DieGroup> Load()
    {
        string filePath = Application.persistentDataPath + "/SavedSession.json";
        if (!System.IO.File.Exists(filePath)) { Save(new List<DieGroup>()); }

        string jsonString = System.IO.File.ReadAllText(filePath);
        SaveSessionString saveSessionString = JsonUtility.FromJson<SaveSessionString>(jsonString);
        dieGroups.Clear();
        dieGroups = saveSessionString.ConvertToDiegroups();
        Debug.Log("Loaded " + dieGroups.Count.ToString() + " DieGroups(s) from Saved Session: " + filePath);
        SetDieGroupIDs();
        return dieGroups;
    }
    private static void SetDieGroupIDs()
    {
        dieGroupId = 0;
        foreach (DieGroup _dieGroup in dieGroups)
        {
            _dieGroup.groupId = dieGroupId;
            dieGroupId++;
        }
    }
}

public class SaveSessionString
{
    public string savedSessionString = "";

    public SaveSessionString(List<DieGroup> savedDieGroups)
    {
        foreach (DieGroup dieGroup in savedDieGroups)
        {
            string json = JsonUtility.ToJson(dieGroup);
            savedSessionString += json + "|";
        }
        if (savedSessionString != "")
        {
            savedSessionString = savedSessionString.Substring(0, savedSessionString.Length - 1);
        }
    }

    public List<DieGroup> ConvertToDiegroups()
    {
        string[] strings = savedSessionString.Split('|');
        List<DieGroup> savedSession = new List<DieGroup>();
        foreach (string s in strings)
        {
            if (s != "")
            {
                DieGroup dieGroup = JsonUtility.FromJson<DieGroup>(s);
                savedSession.Add(dieGroup);
            }
        }
        return savedSession;
    }
}

