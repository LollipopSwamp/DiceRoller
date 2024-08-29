using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//overall handler
public static class SaveDieGroupGroup
{
    public static List<DieGroup> allDieGroups = new List<DieGroup>();
    public static StringArray stringArray = new StringArray();
    public static Dictionary<int, string> dieGroupGroupNames = new Dictionary<int, string>();


    public static void SaveToJSON()
    {
        CreateTestDieGroupGroup();
        stringArray = new StringArray(allDieGroups);
        string stringArrayData = JsonUtility.ToJson(stringArray);
        string filePath = Application.persistentDataPath + "/DieGroupGroupsPresets.json";
        string dieGroupGroupData = JsonUtility.ToJson(stringArrayData);
        System.IO.File.WriteAllText(filePath, stringArrayData);
    }

    public static void LoadFromJSON()
    {
        string filePath = Application.persistentDataPath + "/DieGroupGroupsPresets.json";
        if (!System.IO.File.Exists(filePath)) { SaveToJSON(); }

        string jsonString = System.IO.File.ReadAllText(filePath);
        stringArray = JsonUtility.FromJson<StringArray>(jsonString);
        allDieGroups.Clear();
        allDieGroups = stringArray.ConvertToDieGroups();
        Debug.Log("Loaded " + allDieGroups.Count.ToString() + " DieGroupGroupsPresets(s) from: " + filePath);
        ProcessDieGroupGroupNames();
    }


    public static void SetDieGroupGroupIDs()
    {

    }


    public static void DeleteDieGroupGroup(int _dieGroupGroupId)
    {
        List<DieGroup> newAllDieGroups = new List<DieGroup>();
        foreach(DieGroup _dieGroup in allDieGroups)
        {
            if (_dieGroup.dieGroupGroupId != _dieGroupGroupId)
            {
                newAllDieGroups.Add(_dieGroup);
            }
        }
        allDieGroups = newAllDieGroups;
    }

    public static void ProcessDieGroupGroupNames()
    {
        foreach(DieGroup _dieGroup in allDieGroups)
        {
            if (!dieGroupGroupNames.ContainsKey(_dieGroup.dieGroupGroupId))
            {
                dieGroupGroupNames.Add(_dieGroup.dieGroupGroupId, _dieGroup.dieGroupGroupName);
            }
        }
    }
    public static void CreateTestDieGroupGroup()
    {
        allDieGroups.Add(new DieGroup());
        allDieGroups.Add(new DieGroup());
        allDieGroups.Add(new DieGroup());
        allDieGroups.Add(new DieGroup());
    }
}
//all dieGroupGroups
public class StringArray
{
    public string[] stringArray = new string[0];

    public StringArray()
    {
        stringArray = new string[0];
    }
    public StringArray(List<DieGroup> _dieGroups)
    {
        stringArray = new string[_dieGroups.Count];
        for (int i = 0; i < _dieGroups.Count; i++)
        {
            string dieGroupGroupData = JsonUtility.ToJson(_dieGroups[i]);
            stringArray[i] = dieGroupGroupData;
        }
    }

    public List<DieGroup> ConvertToDieGroups()
    {
        List<DieGroup> diegroups = new List<DieGroup>();
        foreach(string _dieGroupString in stringArray)
        {
            diegroups.Add(JsonUtility.FromJson<DieGroup>(_dieGroupString));
        }
        return diegroups;
    }
}
