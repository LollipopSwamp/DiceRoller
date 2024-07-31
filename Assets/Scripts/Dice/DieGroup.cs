using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DieGroup;

public class DieGroup
{
    public int groupId;
    private static int groupIdIndex = 0;

    public string groupName;

    public List<Die.DieType> toHitBonusDice = new List<Die.DieType>(); //damage if attack
    public List<Die.DieType> damageDice = new List<Die.DieType>(); //damage if attack

    public int toHitModifier;
    public int damageModifier;
    
    //results type
    public enum ToHitType { Standard, Advantage, Disadvantage, None };
    public ToHitType toHitType = ToHitType.Standard;

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

        toHitType = ToHitType.Standard;
    }

    //includes to hit dice and bonus to hit dice
    public DieGroup(string _groupName, List<Die.DieType> _toHitBonusDice, ToHitType _toHitType, int _toHitModifier, List<Die.DieType> _damageDice, int _damageModifier, int _colorIndex)
    {
        groupId = groupIdIndex;
        groupIdIndex++;

        groupName = _groupName;

        toHitBonusDice = _toHitBonusDice;
        damageDice = _damageDice;

        toHitModifier = _toHitModifier;
        damageModifier = _damageModifier;

        colorIndex = _colorIndex;

        toHitType = _toHitType;
    }

    //includes to hit dice and no bonus to hit dice
    public DieGroup(string _groupName, ToHitType _toHitType, int _toHitModifier, List<Die.DieType> _damageDice, int _damageModifier, int _colorIndex)
    {
        groupId = groupIdIndex;
        groupIdIndex++;

        groupName = _groupName;

        toHitBonusDice.Clear();
        damageDice = _damageDice;

        toHitModifier = _toHitModifier;
        damageModifier = _damageModifier;

        colorIndex = _colorIndex;

        toHitType = _toHitType;
    }

    //standard roll
    public DieGroup(string _groupName,  List<Die.DieType> _damageDice, int _damageModifier, int _colorIndex)
    {
        groupId = groupIdIndex;
        groupIdIndex++;

        groupName = _groupName;

        toHitBonusDice.Clear();
        damageDice = _damageDice;

        toHitModifier = 0;
        damageModifier = _damageModifier;

        colorIndex = _colorIndex;

        toHitType = ToHitType.None;
    }

    public void SetNewGroupID()
    {
        groupId = groupIdIndex;
        groupIdIndex++;
    }

    public Color GetColor(bool _darkColor)
    {
        Debug.Log("colorIndex: " + colorIndex.ToString());
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
    public int[] GetToHitBonusDieTypeInts()
    {
        int[] toHitBonusDieTypesCount = new int[6];
        foreach (Die.DieType d in toHitBonusDice)
        {
            toHitBonusDieTypesCount[Die.DieTypeToIndex(d)]++;
        }
        return toHitBonusDieTypesCount;
    }
    public int[] GetDamageDieTypeInts()
    {
        int[] damageDieTypesCount = new int[6];
        foreach (Die.DieType d in damageDice)
        {
            damageDieTypesCount[Die.DieTypeToIndex(d)]++;
        }
        return damageDieTypesCount;
    }
    public void CommitDieGroup()
    {
        groupId = groupIdIndex;
        ++groupIdIndex;
        PrintDieGroup();
    }

    public string GetDamageDiceTypesString()
    {
        string damageDieTypes = "";
        int[] damageDieTypesCount = new int[6];
        foreach (Die.DieType dieType in damageDice) { damageDieTypesCount[Die.DieTypeToIndex(dieType)]++; }
        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            if (damageDieTypesCount[i] != 0)
            {
                damageDieTypes += damageDieTypesCount[i].ToString() + Die.DieTypeToString(i) + " + ";
            }
        }
        damageDieTypes += damageModifier.ToString();
        return damageDieTypes;
    }
    public string GetToHitDiceTypesString()
    {
        string toHitDieTypes = "";
        //get count of each type of die
        int[] toHitBonusDieTypesCount = new int[6];
        foreach (Die.DieType dieType in toHitBonusDice) { toHitBonusDieTypesCount[Die.DieTypeToIndex(dieType)]++; }
        //add die type names to string
        switch (toHitType)
        {
            case ToHitType.Standard:
                toHitDieTypes += "d20 + ";
                break;
            case ToHitType.Advantage:
                toHitDieTypes += "d20 (ADV) + ";
                break;
            case ToHitType.Disadvantage:
                toHitDieTypes += "d20 (DIS) + ";
                break;
            case ToHitType.None:
                Debug.Log("Error with ToHitType");
                break;
        }
        for (int i = 0; i < toHitBonusDieTypesCount.Length; i++)
        {
            if (toHitBonusDieTypesCount[i] != 0)
            {
                toHitDieTypes += toHitBonusDieTypesCount[i].ToString() + Die.DieTypeToString(i) + " + ";
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
        return newDieGroup;
    }
    public void PrintDieGroup()
    {
        Debug.Log("==================================");
        Debug.Log("Group ID: " + groupId.ToString());
        Debug.Log("Group Name: " + groupName);
        string toHitBonusDiceString = "";
        foreach (Die.DieType d in toHitBonusDice) { toHitBonusDiceString += (Die.DieTypeToString(d) + " "); }
        Debug.Log("To Hit Bonus Dice: " + toHitBonusDiceString);
        string damageDiceString = "";
        foreach (Die.DieType d in damageDice) { damageDiceString += (Die.DieTypeToString(d) + " "); }
        Debug.Log("Damage Dice: " + damageDiceString);
        Debug.Log("To Hit Modifier: " + toHitModifier.ToString());
        Debug.Log("Damage Modifier: " + damageModifier.ToString());
        Debug.Log("To Hit Type: " + toHitType.ToString());
        Debug.Log("Color Index: " + colorIndex.ToString());
    }
}
