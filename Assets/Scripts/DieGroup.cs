using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using static DieGroup;

public class DieGroup : MonoBehaviour
{
    private static int groupIdIndex = 0;
    public int groupId;
    public string groupName;

    public List<Die> toHitDice = new List<Die>(); //to hit if attack
    public List<Die> toHitBonusDice = new List<Die>(); //damage if attack
    public List<Die> damageDice = new List<Die>(); //damage if attack

    public int toHitResult;
    public int damageResult;

    public int toHitModifier;
    public int damageModifier;

    public bool diceStillRolling = true;

    //roll type
    public enum GroupType { Attack, Standard };
    private GroupType groupType = GroupType.Attack;

    //results type
    public enum ResultsType { Sum, Advantage, Disadvantage };
    private ResultsType resultsType = ResultsType.Sum;

    //die color
    public int colorIndex = 0;
    private static List<Color> dieColorDark = new List<Color>()
    {
        new Color(0.9f,0.9f,0.9f,1), //white grey
        new Color(1,0,0,1), //red
        new Color(1,0.40f,0,1), //orange
        new Color(1,1,0,1), //yellow
        new Color(0,0.8f,1,1), //green
        new Color(0,0.4f,1,1), //blue
        new Color(0.75f,0,1,1), //purple
        new Color(1,0.4f,1,1) //pink
    };
    private static List<Color> dieColorLight = new List<Color>()
    {
        Color.white, //white
        new Color(1, 0.4f, 0.4f,1), //red
        new Color(1,0.60f,0.3f,1), //orange
        new Color(1,1,.04f,1), //yellow
        new Color(0.4f,1,0.4f,1), //green
        new Color(0.4f,0.65f,1,1), //blue
        new Color(0.85f,0.3f,1,1), //purple
        new Color(1,0.6f,1,1) //pink
    };

    //set variables for attack, no bonus to hit
    public void SetVariables(string _groupName, GroupType _groupType, ResultsType _resultsType, int _modifier, int _colorIndex)
    {
        groupName = _groupName;
        resultsType = _resultsType;
        groupType = _groupType;
        colorIndex = _colorIndex;
        groupId = groupIdIndex;
        groupIdIndex += 1;
        toHitModifier = _modifier;
        toHitResult = 0;
        damageResult = 0;
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

    //add attack dice, no bonus to hit
    public void AddToHitDice(ResultsType _resultsType, int _toHitModifier)
    {
        List<Die> dieList = new List<Die>();
        switch (_resultsType)
        {
            case ResultsType.Sum:
                dieList.Add(new Die(Die.DieType.d20, groupId, 1));
                break;
            case ResultsType.Advantage:
            case ResultsType.Disadvantage:
                dieList.Add(new Die(Die.DieType.d20, groupId, 1));
                dieList.Add(new Die(Die.DieType.d20, groupId, 1));
                break;
            default:
                Debug.Log("Error adding toHitDice");
                break;
        }
        toHitDice = dieList;
    }
    //add attack dice, bonus to hit
    public void AddToHitBonusDice(List<Die> _toHitBonusDice)
    {
        toHitBonusDice = _toHitBonusDice;
    }
    //add damage dice
    public void AddDamageDice(List<Die> _damageDice, int _damageModifier)
    {
        damageDice = _damageDice;
        damageModifier = _damageModifier;
    }

    public void UpdateDie(Die die)
    {
        bool diceUpdated = false;
        //check in toHitDice
        for (int i = 0; i < toHitDice.Count; ++i)
        {
            if (toHitDice[i].dieId == die.dieId)
            {
                toHitDice[i] = die;
                diceUpdated = true;
            }
        }
        //check in toHitBonusDice
        if(!diceUpdated)
        {
            for (int i = 0; i < toHitBonusDice.Count; ++i)
            {
                if (toHitBonusDice[i].dieId == die.dieId)
                {
                    toHitBonusDice[i] = die;
                    diceUpdated = true;
                }
            }
        }
        //check in damageDice
        if (!diceUpdated)
        {
            for (int i = 0; i < damageDice.Count; ++i)
            {
                if (damageDice[i].dieId == die.dieId)
                {
                    damageDice[i] = die;
                }
            }
        }

        CheckResults();
        //CheckFinalResults();
        return;
    }
    public bool DiceStillRolling()
    {
        foreach (Die d in toHitDice)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
                return true;
            }
        }
        foreach (Die d in toHitBonusDice)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
                return true;
            }
        }
        foreach (Die d in damageDice)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
                return true;
            }
        }
        diceStillRolling = false;
        return false;
    }

    public void CheckResults()
    {
        //if dice done rolling, process results
        if (!DiceStillRolling())
        {
            foreach (Die d in toHitDice)
            {
                switch (resultsType)
                {
                    case ResultsType.Sum:
                        toHitResult += d.result;
                        break;
                    case ResultsType.Advantage:
                        toHitResult = Mathf.Max(d.result, toHitResult);
                        break;
                    case ResultsType.Disadvantage:
                        toHitResult = Mathf.Min(d.result, toHitResult);
                        break;
                }
            }
            foreach (Die d in toHitBonusDice)
            {
                toHitResult += d.result;
            }
            foreach (Die d in damageDice)
            {
                damageResult += d.result;
            }
            damageResult += damageModifier;

            //tell DiceManager to check final results
            transform.parent.gameObject.GetComponent<DiceManager>().CheckFinalResults();
        }
    }

    public void PrintDieGroup()
    {
        Debug.Log(string.Concat("Group ID: ", groupId, "Group Name: ", groupName, " || To Hit Modifer: ", toHitModifier, " || Damage Modifer: ", damageModifier, " || To Hit Result: ", toHitResult + toHitModifier, " || Damage Result: ", damageResult + damageModifier));
    }
}
