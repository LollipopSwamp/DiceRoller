using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public ToHitType toHitType = ToHitType.None;

    //die color
    public int colorIndex = 0;
    private static List<Color> dieColorDark = new List<Color>()
    {
        new Color(0.9f,0.9f,0.9f,1), //white grey 0 
        new Color(1,0,0,1), //red 1
        new Color(1,0.40f,0,1), //orange 2
        new Color(1,1,0,1), //yellow 3
        new Color(0,0,1,1), //green 4
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
    public void CommitDieGroup()
    {
        groupId = groupIdIndex;
        ++groupIdIndex;
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
    public static Color GetColor(int _colorIndexbool, bool _darkColor)
    {
        if (_darkColor)
        {
            return dieColorDark[_colorIndexbool];
        }
        else
        {
            return dieColorLight[_colorIndexbool];
        }
    }
}
