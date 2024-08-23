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
        if (dieGroup.toHitType == 3)
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
        toHitDieTypes = dieGroup.GetToHitDiceTypesString();
        damageDieTypes = dieGroup.GetDamageDiceTypesString();
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
