using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieGroupPanel : MonoBehaviour
{
    public GameObject uiManager;
    public DieGroup dieGroup;
    //result strings
    public int groupId;
    public string groupName;
    public DieGroup.ToHitType toHitType;
    public string toHitDieTypes;
    public string damageDieTypes;

    //is roll attack or standard
    public bool attackRoll;

    //prefabs
    public List<GameObject> standardTextPanels;
    public List<GameObject> attackTextPanels;

    public int panelHeight;



    public void Init(DieGroup _dieGroup)
    {
        //set ui manager object
        uiManager = transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        dieGroup = _dieGroup;
        groupId = dieGroup.groupId;
        groupName = dieGroup.groupName;
        gameObject.name = groupName + " Panel";
        gameObject.GetComponent<CanvasRenderer>().SetColor(dieGroup.GetColor(false));
        //if roll was standard
        if (dieGroup.toHitType == DieGroup.ToHitType.None)
        {
            SetVars(dieGroup);
            SetTextStandard();
        }
        //if roll is attack
        else
        {
            SetVars(dieGroup);
            SetTextAttack();
        }
    }
    public void DuplicateBtn()
    {
        gameObject.GetComponentInParent<SetupUI>().DuplicateDieGroup(groupId);
    }
    public void EditBtn()
    {
        dieGroup.PrintDieGroup();
        uiManager.GetComponent<UIManager>().ShowDieGroupSetup(dieGroup,1);
    }

    public void DeleteBtn()
    {
        gameObject.GetComponentInParent<SetupUI>().DeleteDieGroup(groupId);
        gameObject.GetComponentInParent<SetupUI>().UpdateRollBtn();
        Destroy(gameObject);
    }
    void SetVars(DieGroup dieGroup)
    {
        //get count of each type of die
        int[] toHitBonusDieTypesCount = new int[6];
        int[] damageDieTypesCount = new int[6];
        foreach (Die.DieType dieType in dieGroup.toHitBonusDice) { toHitBonusDieTypesCount[Die.DieTypeToIndex(dieType)]++; }
        foreach (Die.DieType dieType in dieGroup.damageDice) { damageDieTypesCount[Die.DieTypeToIndex(dieType)]++; }
        //add die type names to string
        switch (dieGroup.toHitType)
        {
            case DieGroup.ToHitType.Standard:
                toHitDieTypes += "d20 + ";
                break;
            case DieGroup.ToHitType.Advantage:
                toHitDieTypes += "d20 (ADV) + ";
                break;
            case DieGroup.ToHitType.Disadvantage:
                toHitDieTypes += "d20 (DIS) + ";
                break;
            case DieGroup.ToHitType.None:
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
        toHitDieTypes += dieGroup.toHitModifier.ToString();
        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            if (damageDieTypesCount[i] != 0)
            {
                damageDieTypes += damageDieTypesCount[i].ToString() + Die.DieTypeToString(i) + " + ";
            }
        }
        damageDieTypes += dieGroup.damageModifier.ToString();
    }

    void SetTextStandard()
    {
        standardTextPanels[0].GetComponent<TMP_Text>().text = groupName;
        standardTextPanels[1].GetComponent<TMP_Text>().text = damageDieTypes;
    }
    void SetTextAttack()
    {
        attackTextPanels[0].GetComponent<TMP_Text>().text = groupName;
        attackTextPanels[1].GetComponent<TMP_Text>().text = toHitDieTypes;
        attackTextPanels[2].GetComponent<TMP_Text>().text = damageDieTypes;
    }

    public void PrintRollResult()
    {
        if (attackRoll)
        {
            Debug.Log("Attack Roll | " + groupName + " | " + toHitDieTypes + " | " + damageDieTypes);
        }
        else if (!attackRoll)
        {
            Debug.Log("Standard Roll | " + groupName + " | " + damageDieTypes);
        }
        else
        {
            Debug.Log("Error printing RollResult");
        }

    }
}
