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
    //public List<Die> dice0 = new List<Die>(); //to hit if attack
    //public List<Die> dice1 = new List<Die>(); //damage if attack

    public List<Die> diceToHit = new List<Die>(); //to hit if attack
    public List<Die> diceToHitBonus = new List<Die>(); //damage if attack
    public List<Die> damageDice = new List<Die>(); //damage if attack

    public int toHitResult = -1;
    public int damageResult = -1;

    public int toHitModifier;
    public int damageModifier;

    //roll type
    public enum GroupType { Attack, Normal };
    private GroupType groupType = GroupType.Attack;

    //results type
    public enum ResultsType { Sum, Advantage, Disadvantage };
    private ResultsType resultsType = ResultsType.Sum;

    //die color
    public int colorIndex = 0;
    private static List<Color> dieColor = new List<Color>()
    {
        Color.white, //white
        Color.red, //red
        new Color(1,0.65f,0,1), //orange
        Color.yellow, //yellow
        Color.green, //green
        Color.blue, //blue
        new Color(0.62f,0.13f,0.94f,1), //purple
        new Color(1,0.75f,0.8f,1) //pink
    };

    //set variables for attack, no bonus to hit
    public void SetVariables(string _groupName, GroupType _groupType, ResultsType _resultsType, int _modifier, int _colorIndex)
    {
        groupName = _groupName;
        resultsType = _resultsType;
        groupType = _groupType;
        colorIndex = _colorIndex;
        //groupId = groupIdIndex;
        groupIdIndex += 1;
        toHitModifier = _modifier;
    }

    public Color GetColor()
    {
        return dieColor[colorIndex];
    }

    //add attack dice, no bonus to hit
    public void AddToHitDice(ResultsType _resultsType, int _toHitModifier)
    {
        List<Die> dieList = new List<Die>();
        Die die = new Die(Die.DieType.d20, groupId, 1);
        switch (_resultsType)
        {
            case ResultsType.Sum:
                dieList.Add(die);
                break;
            case ResultsType.Advantage | ResultsType.Disadvantage:
                dieList.Add(die);
                dieList.Add(die);
                break;
            default:
                Debug.Log("Error adding toHitDice");
                break;
        }
        diceToHit = dieList;
    }
    //add damage dice
    public void AddDamageDice(List<Die> _damageDice, int _damageModifier)
    {
        damageDice = _damageDice;
        damageModifier = _damageModifier;
    }
    //add attack dice, bonus to hit
    public void AddToHitBonusDice(List<Die> _diceToHitBonus)
    {
        diceToHitBonus = _diceToHitBonus;
    }

    public void UpdateDie(Die die)
    {
        bool diceUpdated = false;
        //check in diceToHit
        for (int i = 0; i < diceToHit.Count; ++i)
        {
            if (diceToHit[i].dieId == die.dieId)
            {
                diceToHit[i] = die;
                diceUpdated = true;
            }
        }
        //check in diceToHitBonus
        if(!diceUpdated)
        {
            for (int i = 0; i < diceToHitBonus.Count; ++i)
            {
                if (diceToHitBonus[i].dieId == die.dieId)
                {
                    diceToHitBonus[i] = die;
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

    public void CheckResults()
    {
        //check if dice0 are still rolling
        bool diceStillRolling = false;
        foreach (Die d in diceToHit)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
            }
        }
        foreach (Die d in diceToHitBonus)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
            }
        }
        foreach (Die d in damageDice)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
            }
        }

        //if dice0 done rolling, process results
        if (!diceStillRolling)
        {
            int _toHitResult = 0;
            switch (resultsType)
            {
                case ResultsType.Sum:
                    foreach (Die d in dice0)
                    {
                        _toHitResult += d.result;
                    }
                    break;
                case ResultsType.Advantage:
                    foreach (Die d in dice0)
                    {
                        _toHitResult = Math.Max(d.result, _toHitResult);
                    }
                    break;
                case ResultsType.Disadvantage:
                    foreach (Die d in dice0)
                    {
                        _toHitResult = Math.Min(d.result, _toHitResult);
                    }
                    break;
            }
            toHitResult = _toHitResult;
        }
    }

    public void PrintDieGroup()
    {
        Debug.Log(string.Concat("Group ID: ", groupId, "Group Name: ", groupName, " || To Hit Modifer: ", toHitModifier, " || Damage Modifer: ", damageModifier, " || To Hit Result: ", toHitResult + toHitModifier, " || Damage Result: ", damageResult + damageModifier));
    }
}
