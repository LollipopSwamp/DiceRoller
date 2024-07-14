using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollResult
{
    public int groupId;
    public string groupName;
    public DieGroup.ResultsType resultsType;
    public string toHitDieTypes;
    public string toHitResult;
    public string damageDieTypes;
    public string damageResult;

    public bool diceDoneRolling = false;


    private static string[] dieTypesNames = new string[]
    {
                "d20",//0
                "d12",//1
                "d10",//2
                "d8",//3
                "d6",//4
                "d4"//5
    };

    public RollResult()
    {
        groupId = 0;
        groupName = string.Empty;
        resultsType = DieGroup.ResultsType.Sum;
        toHitDieTypes = string.Empty;
        toHitResult = string.Empty;
        damageDieTypes = string.Empty;
        damageResult = string.Empty;
    }

    public RollResult(GameObject gObj)
    {
        DieGroup g = gObj.GetComponent<DieGroup>();
        groupId = g.groupId;
        groupName = g.groupName;
        SetToHitVars(g);
        SetDamageVars(g);
    }

    void SetToHitVars(DieGroup g)
    {
        string displayString = "";

        //if no dice in toHitDice, return
        if (g.toHitDice.Count == 0) { return; };

        //if dice in in toHitDice, add d20 strings
        if (resultsType == DieGroup.ResultsType.Advantage)
        {
            displayString += "d20 (ADV) + ";
        }
        else if (resultsType == DieGroup.ResultsType.Disadvantage)
        {
            displayString += "d20 (DIS) + ";
        }
        else if (resultsType == DieGroup.ResultsType.Sum)
        {
            displayString += "d20 + ";
        }

        //if dice in in toHitBonusDice, add bonus strings
        if (g.toHitBonusDice.Count != 0)
        {
            int[] dieTypesCount = new int[6];
            foreach (Die d in g.toHitBonusDice)
            {
                switch (d.dieType)
                {
                    case Die.DieType.d20:
                        dieTypesCount[0] += 1;
                        break;
                    case Die.DieType.d12:
                        dieTypesCount[1] += 1;
                        break;
                    case Die.DieType.d10:
                        dieTypesCount[2] += 1;
                        break;
                    case Die.DieType.d8:
                        dieTypesCount[3] += 1;
                        break;
                    case Die.DieType.d6:
                        dieTypesCount[4] += 1;
                        break;
                    case Die.DieType.d4:
                        dieTypesCount[5] += 1;
                        break;
                }
            }
            for (int i = 0; i < dieTypesCount.Length; i++)
            {
                if (dieTypesCount[i] != 0)
                { 
                    displayString += dieTypesCount[i].ToString() + dieTypesNames[i] + " + "; 
                }
                
            }
        }

        //add modifier to string and set object var
        toHitDieTypes = displayString + g.toHitModifier.ToString();

        //set result var
        toHitResult = "Result: " + (g.toHitResult + g.toHitModifier).ToString();
    }


    void SetDamageVars(DieGroup g)
    {
        //dice0 types
        string displayString = "";

        int[] dieTypesCount = new int[6];
        foreach (Die d in g.damageDice)
        {
            switch (d.dieType)
            {
                case Die.DieType.d20:
                    dieTypesCount[0] += 1;
                    break;
                case Die.DieType.d12:
                    dieTypesCount[1] += 1;
                    break;
                case Die.DieType.d10:
                    dieTypesCount[2] += 1;
                    break;
                case Die.DieType.d8:
                    dieTypesCount[3] += 1;
                    break;
                case Die.DieType.d6:
                    dieTypesCount[4] += 1;
                    break;
                case Die.DieType.d4:
                    dieTypesCount[5] += 1;
                    break;
            }
        }
        for (int i = 0; i < dieTypesCount.Length; i++)
        {
            if (dieTypesCount[i] != 0)
            {
                displayString += dieTypesCount[i].ToString() + dieTypesNames[i] + " + ";
            }
        }
        //add modifier to string and set object var
        damageDieTypes = displayString + g.toHitModifier.ToString();

        //set result var
        damageResult = "Result: " + g.damageResult.ToString();

    }
    public void PrintRollResult()
    {
        Debug.Log(groupName + " | " + toHitDieTypes + " | " + toHitResult + " | " + damageDieTypes + " | " + damageResult);
    }
}
