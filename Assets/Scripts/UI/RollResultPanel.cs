using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollResultPanel : MonoBehaviour
{
    //die group B
    public DieGroupBehaviour dieGroupB;

    //parent
    public GameObject resultsUI;

    //result strings
    public int groupId;
    public string groupName;
    public DieGroup.ToHitType toHitType;
    public string toHitDieTypes;
    public string toHitResult;
    public bool crit;
    public bool critFail;
    public string damageDieTypes;
    public string damageResult;

    //is roll attack or standard
    public bool attackRoll;

    //prefabs
    public List<GameObject> standardTextPanels;
    public List<GameObject> attackTextPanels;


    public void Init(GameObject dieGroupObj)
    {
        resultsUI = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;

        dieGroupB = dieGroupObj.GetComponent<DieGroupBehaviour>();
        DieGroup dieGroup = dieGroupB.dieGroup;
        groupId = dieGroup.groupId;
        groupName = dieGroup.groupName;
        gameObject.name = groupName + " Panel";
        gameObject.GetComponent<CanvasRenderer>().SetColor(dieGroup.GetColor(false));
        //if roll was standard
        if (dieGroupB.dieGroup.toHitType == 0)
        {
            SetVars(dieGroupB);
            SetTextStandard();
        }
        //if roll is attack
        else
        {
            SetVars(dieGroupB);
            SetTextAttack();
        }
    }

    void SetVars(DieGroupBehaviour dieGroupB)
    {
        DieGroup dieGroup = dieGroupB.dieGroup;
        //get count of each type of die
        toHitDieTypes += dieGroup.GetToHitDiceTypesString();
        damageDieTypes += dieGroup.GetDamageDiceTypesString();

        //set result vars
        if (dieGroupB.crit)
        {
            crit = true;
            critFail = false;
            toHitResult = "Critical Hit!";
            damageResult = "Result: " + (dieGroupB.damageResult + dieGroup.damageModifier).ToString();
        }
        else if (dieGroupB.critFail)
        {
            crit = false;
            critFail = true;
            toHitResult = "Critical Fail";
            damageResult = "Result: Critical Fail";
        }
        else
        {
            crit = false;
            critFail = false;
            toHitResult = "Result: " + (dieGroupB.toHitResult + dieGroup.toHitModifier).ToString();
            damageResult = "Result: " + (dieGroupB.damageResult + dieGroup.damageModifier).ToString();
        }
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
        if (crit)
        {
            attackTextPanels[2].GetComponent<TMP_Text>().color = Color.green;
            attackTextPanels[4].GetComponent<TMP_Text>().color = Color.green;
        }
        else if (critFail)
        {
            attackTextPanels[2].GetComponent<TMP_Text>().color = Color.red;
            attackTextPanels[4].GetComponent<TMP_Text>().color = Color.red;
        }
    }
    public void ShowDetails()
    {
        resultsUI.GetComponent<ResultsUI>().ShowResultDetails(dieGroupB);
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
