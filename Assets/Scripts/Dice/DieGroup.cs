using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DieGroup;

public class DieGroup
{
    public int groupId;
    private static int groupIdIndex = 0;

    public string groupName;

    public List<int> toHitBonusDice = new List<int>(); //damage if attack
    public List<int> damageDice = new List<int>(); //damage if attack

    public int toHitModifier;
    public int damageModifier;
    
    //results type
    public enum ToHitType { None, Standard, Advantage, Disadvantage};
    public int toHitType = 0;
    public int critOn = 20;

    //die color
    public int colorIndex = 0;
    private static List<Color> dieColorDark = new List<Color>()
    {
        new Color(0.9f,0.9f,0.9f,1), //white grey 0 
        new Color(1,0,0,1), //red 1
        new Color(1,0.40f,0,1), //orange 2
        new Color(1,1,0,1), //yellow 3
        new Color(0,1,0,1), //green 4
        new Color(0,0.4f,1,1), //blue 5
        new Color(1,0,1,1), //pink 6
        new Color(0.5f,0,0.5f,1), //purple 7
        new Color(0.4f,0.25f,0,1), //brown 8
        new Color(0.4f,0.4f,0.4f,1) //grey 9
    };
    private static List<Color> dieColorLight = new List<Color>()
    {
        Color.white, //white 0
        new Color(1, 0.3f, 0.3f,1), //red 1
        new Color(1,0.60f,0.3f,1), //orange 2
        new Color(1,1,.60f,1), //yellow 3
        new Color(0.4f,1,0.4f,1), //green 4
        new Color(0.4f,0.65f,1,1), //blue 5
        new Color(1,0.5f,1,1), //pink 6
        new Color(0.6f,0,0.4f,1), //purple 7
        new Color(0.6f,0.4f,0,1), //brown 8
        new Color(0.5f,0.5f,0.5f,1) //grey 9
    };

    public DieGroup()
    {
        groupId = -1;

        groupName = "dieGroup" + groupId.ToString();

        toHitBonusDice.Clear();
        damageDice.Clear();

        toHitModifier = 0;
        damageModifier = 0;

        colorIndex = 0;

        toHitType = 1;
    }

    //includes to hit dice and bonus to hit dice
    public DieGroup(string _groupName, List<int> _toHitBonusDice, int _toHitType, int _toHitModifier, List<int> _damageDice, int _damageModifier, int _colorIndex)
    {
        GetNewGroupID();

        groupName = _groupName;

        toHitBonusDice = _toHitBonusDice;
        damageDice = _damageDice;

        toHitModifier = _toHitModifier;
        damageModifier = _damageModifier;

        colorIndex = _colorIndex;

        toHitType = _toHitType;
    }

    //includes to hit dice and no bonus to hit dice
    public DieGroup(string _groupName, int _toHitType, int _toHitModifier, List<int> _damageDice, int _damageModifier, int _colorIndex)
    {
        GetNewGroupID();

        groupName = _groupName;

        toHitBonusDice.Clear();
        damageDice = _damageDice;

        toHitModifier = _toHitModifier;
        damageModifier = _damageModifier;

        colorIndex = _colorIndex;

        toHitType = _toHitType;
    }

    //standard roll
    public DieGroup(string _groupName,  List<int> _damageDice, int _damageModifier, int _colorIndex)
    {
        GetNewGroupID();

        groupName = _groupName;

        toHitBonusDice.Clear();
        damageDice = _damageDice;

        toHitModifier = 0;
        damageModifier = _damageModifier;

        colorIndex = _colorIndex;

        toHitType = 0;
    }

    public void SetNewGroupID()
    {
        groupId = groupIdIndex;
        groupIdIndex++;
    }

    public Color GetColor(bool _darkColor)
    {
        if (_darkColor)
        {
            return dieColorDark[colorIndex];
        }
        else
        {
            return dieColorLight[colorIndex];
        }
    }
    public static void SetColorLists()
    {
        List<Color> dieColorDark = new List<Color>()
    {
        new Color(0.9f,0.9f,0.9f,1), //white grey 0 
        new Color(1,0,0,1), //red 1
        new Color(1,0.40f,0,1), //orange 2
        new Color(1,1,0,1), //yellow 3
        new Color(0,1,0,1), //green 4
        new Color(0,0.4f,1,1), //blue 5
        new Color(1,0,1,1), //pink 6
        new Color(0.5f,0,0.5f,1), //purple 7
        new Color(0.4f,0.25f,0,1), //brown 8
        new Color(0.4f,0.4f,0.4f,1) //grey 9
    };
        List<Color> dieColorLight = new List<Color>()
    {
        Color.white, //white 0
        new Color(1, 0.3f, 0.3f,1), //red 1
        new Color(1,0.60f,0.3f,1), //orange 2
        new Color(1,1,.60f,1), //yellow 3
        new Color(0.4f,1,0.4f,1), //green 4
        new Color(0.4f,0.65f,1,1), //blue 5
        new Color(1,0.5f,1,1), //pink 6
        new Color(0.6f,0,0.4f,1), //purple 7
        new Color(0.6f,0.4f,0,1), //brown 8
        new Color(0.5f,0.5f,0.5f,1) //grey 9
    };

    }
    public static Color GetColor(int _colorIndexbool, bool _darkColor)
    {
        //SetColorLists();
        if (_darkColor)
        {
            return dieColorDark[_colorIndexbool];
        }
        else
        {
            return dieColorLight[_colorIndexbool];
        }
    }
    public string ToHitTypeString()
    {
        switch(toHitType)
        {
            case 0:
                return "Standard";
            case 1:
                return "Advantage";
            case 2:
                return "Disadvantage";
            case 3:
                return "None";
            default:
                return "Error with ToHitTypeString";
        }
    }
    public void CommitDieGroup()
    {
        groupId = groupIdIndex;
        ++groupIdIndex;
    }

    public int[] GetToHitBonusDieTypesArray()
    {
        int[] toHitBonusDieTypesCount = new int[7];
        foreach (int d in toHitBonusDice)
        {
            toHitBonusDieTypesCount[d]++;
        }
        return toHitBonusDieTypesCount;
    }
    public int[] GetDamageDieTypesArray()
    {
        int[] damageDieTypesCount = new int[7];
        foreach (int d in damageDice)
        {
            damageDieTypesCount[d]++;
        }
        return damageDieTypesCount;
    }
    public string GetDamageDiceTypesString()
    {
        return GetDamageDiceTypesString(GetDamageDieTypesArray());
    }

    public string GetDamageDiceTypesString(int[] _damageDieTypesCount)
    {
        string damageDieTypes = "";
        for (int i = 0; i < _damageDieTypesCount.Length; i++)
        {
            if (_damageDieTypesCount[i] != 0)
            {
                damageDieTypes += _damageDieTypesCount[i].ToString() + Die.DieTypeToString(i) + " + ";
            }
        }
        damageDieTypes += damageModifier.ToString();
        return damageDieTypes;
    }
    public string GetToHitDiceTypesString()
    {
        return GetToHitDiceTypesString(GetToHitBonusDieTypesArray());
    }
    public string GetToHitDiceTypesString(int[] _toHitBonusDieTypesCount)
    {
        string toHitDieTypes = "";
        //add die type names to string
        switch (toHitType)
        {
            case 0:
                toHitDieTypes += "d20 + ";
                break;
            case 1:
                toHitDieTypes += "d20 (ADV) + ";
                break;
            case 2:
                toHitDieTypes += "d20 (DIS) + ";
                break;
            default:
                toHitDieTypes += "";
                break;
        }
        for (int i = 0; i < _toHitBonusDieTypesCount.Length; i++)
        {
            if (_toHitBonusDieTypesCount[i] != 0)
            {
                toHitDieTypes += _toHitBonusDieTypesCount[i].ToString() + Die.DieTypeToString(i) + " + ";
            }
        }
        toHitDieTypes += toHitModifier.ToString();
        return toHitDieTypes;
    }

    public static DieGroup DuplicateDieGroup(DieGroup _dieGroup)
    {
        DieGroup newDieGroup = new DieGroup();
        newDieGroup.groupName = _dieGroup.groupName;
        newDieGroup.toHitBonusDice = _dieGroup.toHitBonusDice;
        newDieGroup.damageDice = _dieGroup.damageDice;
        newDieGroup.toHitModifier = _dieGroup.toHitModifier;
        newDieGroup.damageModifier = _dieGroup.damageModifier;
        newDieGroup.toHitType = _dieGroup.toHitType;
        newDieGroup.colorIndex = _dieGroup.colorIndex;
        newDieGroup.GetNewGroupID();
        return newDieGroup;
    }
    public void GetNewGroupID()
    {
        groupId = groupIdIndex;
        groupIdIndex++;
    }

    public void PrintDieGroup()
    {
        string dieTypeIndexes = "";
        foreach (int _dieType in toHitBonusDice)
        {
            dieTypeIndexes += _dieType.ToString() + " | ";
        }
        dieTypeIndexes += " | ";
        foreach (int _dieType in damageDice)
        {
            dieTypeIndexes += _dieType.ToString() + " | ";
        }
        Debug.Log(dieTypeIndexes);
    }
}
