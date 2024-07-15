using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollResultPanel : MonoBehaviour
{
    //result strings
    public int groupId;
    public string groupName;
    public DieGroup.ResultsType resultsType;
    public string toHitDieTypes;
    public string toHitResult;
    public string damageDieTypes;
    public string damageResult;

    //is roll attack or standard
    public bool attackRoll;

    //prefabs
    public List<GameObject> standardTextPanels;
    public List<GameObject> attackTextPanels;


    private static string[] dieTypesNames = new string[]
    {
                "d20",//0
                "d12",//1
                "d10",//2
                "d8",//3
                "d6",//4
                "d4"//5
    };

    public void Init(GameObject gObj)
    {
        DieGroup g = gObj.GetComponent<DieGroup>();
        groupId = g.groupId;
        groupName = g.groupName;
        gameObject.name = groupName + " Panel";
        gameObject.GetComponent<CanvasRenderer>().SetColor(g.GetColor(false));
        //if roll was standard
        if (g.toHitDice.Count == 0)
        {
            SetDamageVars(g);
            SetTextStandard();
        }
        //if roll is attack
        else
        {
            SetToHitVars(g);
            SetDamageVars(g);
            SetTextAttack();
        }
    }

    void SetToHitVars(DieGroup g)
    {
        string displayString = "";

        //if no dice in toHitDice, return
        if (g.toHitDice.Count == 0) 
        {
            attackRoll = false;
            return; 
        }
        else
        {
            attackRoll = true;
        }

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
    
    void SetTextStandard()
    {
        standardTextPanels[0].GetComponent<TMP_Text>().text = groupName;
        standardTextPanels[1].GetComponent<TMP_Text>().text = damageDieTypes;
        standardTextPanels[2].GetComponent<TMP_Text>().text = damageResult;
    }
    void SetTextAttack()
    {
        attackTextPanels[0].GetComponent<TMP_Text>().text = groupName;
        attackTextPanels[1].GetComponent<TMP_Text>().text = toHitDieTypes;
        attackTextPanels[2].GetComponent<TMP_Text>().text = toHitResult;
        attackTextPanels[3].GetComponent<TMP_Text>().text = damageDieTypes;
        attackTextPanels[4].GetComponent<TMP_Text>().text = damageResult;
    }

    public void PrintRollResult()
    {
        if (attackRoll)
        {
            Debug.Log("Attack Roll | " + groupName + " | " + toHitDieTypes + " | " + toHitResult + " | " + damageDieTypes + " | " + damageResult);
        }
        else if (!attackRoll)
        {
            Debug.Log("Standard Roll | " + groupName + " | " + damageDieTypes + " | " + damageResult);
        }
        else
        {
            Debug.Log("Error printing RollResult");
        }
        
    }
}
