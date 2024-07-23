using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieGroupSetup : MonoBehaviour
{
    public DieGroup dieGroupToCreate = new DieGroup();
    public bool attackRoll;
    public int[] toHitBonusDieTypesCount = new int[6];
    public int[] damageDieTypesCount = new int[6];

    public GameObject toHitBonusDieTypesString;
    public GameObject damageDieTypesString;

    //settings gameobjects
    public GameObject diceManager;
    public GameObject mainUI;
    public GameObject toHitTypeSetup;
    public List<GameObject> toHitDieTypes;
    public GameObject toHitModifier;
    public List<GameObject> damageDieTypes;
    public GameObject damageModifier;
    public TMP_InputField groupNameInput;
    public GameObject colorPicker;
    public GameObject attackRollSlider;

    public void Init()
    {
        //reset die group variables
        dieGroupToCreate = new DieGroup();
        attackRoll = true;
        toHitBonusDieTypesCount = new int[6];
        damageDieTypesCount = new int[6];

        //reset ui variables
        groupNameInput.text = "(Group Name)";
        colorPicker.GetComponent<ColorPicker>().UpdateButtons(0);
        attackRollSlider.GetComponent<ToggleSlider>().SetState(true);
        toHitTypeSetup.GetComponent<ToHitTypeUI>().Init();
        foreach (GameObject g in toHitDieTypes) { g.GetComponent<DieTypeSetup>().Init(); }
        toHitModifier.GetComponent<ModifierSetup>().Init();
        foreach (GameObject g in damageDieTypes) { g.GetComponent<DieTypeSetup>().Init(); }
        damageModifier.GetComponent<ModifierSetup>().Init();

        SetDieTypeString();
    }

    public void SetToHitType(int _toHitTypeIndex)
    {
        switch (_toHitTypeIndex)
        {
            case 0:
                dieGroupToCreate.toHitType = DieGroup.ToHitType.Standard;
                break;
            case 1:
                dieGroupToCreate.toHitType = DieGroup.ToHitType.Advantage;
                break;
            case 2:
                dieGroupToCreate.toHitType = DieGroup.ToHitType.Disadvantage;
                break;
        }

    }
    public void SetModifier(bool _toHit, int _modifier)
    {
        if (_toHit)
        {
            dieGroupToCreate.toHitModifier = _modifier;
        }
        else
        {
            dieGroupToCreate.damageModifier = _modifier;
        }
    }
    public void SetDieTypeString()
    {
        //to hit die type string
        string dieTypeString = "";
        switch (dieGroupToCreate.toHitType)
        {
            case DieGroup.ToHitType.Standard:
                dieTypeString += "d20";
                break;
            case DieGroup.ToHitType.Advantage:
                dieTypeString += "d20 (ADV)";
                break;
            case DieGroup.ToHitType.Disadvantage:
                dieTypeString += "d20 (DIS)";
                break;
            case DieGroup.ToHitType.None:
                //Debug.Log("Error with ToHitType");
                break;
        }
        for (int i = toHitBonusDieTypesCount.Length - 1; i >= 0; i--)
        {
            if (toHitBonusDieTypesCount[i] != 0)
            {
                dieTypeString += " + " + toHitBonusDieTypesCount[i].ToString() + Die.DieTypeToString(i);
            }
        }
        if (dieGroupToCreate.toHitModifier < 0)
        {dieTypeString += " - ";}
        else { dieTypeString += " + "; }
        dieTypeString += Mathf.Abs(dieGroupToCreate.toHitModifier).ToString();
        toHitBonusDieTypesString.GetComponent<TMP_Text>().text = dieTypeString;

        //damage die type string
        dieTypeString = "";
        for (int i = damageDieTypesCount.Length-1; i >= 0; i--)
        {
            if (damageDieTypesCount[i] != 0)
            {
                dieTypeString += " + " + damageDieTypesCount[i].ToString() + Die.DieTypeToString(i);
            }
        }
        if (dieTypeString.Length > 2) { dieTypeString = dieTypeString.Substring(3); }
        if (dieGroupToCreate.damageModifier < 0){ dieTypeString += " - "; }
        else { dieTypeString += " + "; }
        dieTypeString += Mathf.Abs(dieGroupToCreate.damageModifier).ToString();
        damageDieTypesString.GetComponent<TMP_Text>().text = dieTypeString;
    }

    public void BackButton(int _currMenu)
    {
        if (!attackRoll && _currMenu == 2) { mainUI.GetComponent<MainUI>().PreviousDieGroupPanel(); }
        mainUI.GetComponent<MainUI>().PreviousDieGroupPanel();
    }
    public void NextButton(int _currMenu)
    {
        switch (_currMenu)
        {
            case 0:
                dieGroupToCreate.groupName = groupNameInput.text;
                dieGroupToCreate.colorIndex = colorPicker.GetComponent<ColorPicker>().selectedColorIndex;
                attackRoll = attackRollSlider.GetComponent<ToggleSlider>().state;
                if (!attackRoll) 
                { 
                    dieGroupToCreate.toHitType = DieGroup.ToHitType.None;
                    mainUI.GetComponent<MainUI>().NextDieGroupPanel();
                }
                mainUI.GetComponent<MainUI>().NextDieGroupPanel();
                dieGroupToCreate.PrintDieGroup();
                break;
            case 1:
                List<Die.DieType> _toHitBonusDice = DieTypeCountToDieList(toHitBonusDieTypesCount);
                dieGroupToCreate.toHitBonusDice = _toHitBonusDice;
                mainUI.GetComponent<MainUI>().NextDieGroupPanel();
                dieGroupToCreate.PrintDieGroup();
                break;
        }
    }
    public void SaveDieGroup()
    {
        List<Die.DieType> _damageDice = DieTypeCountToDieList(damageDieTypesCount);
        dieGroupToCreate.damageDice = _damageDice;
        dieGroupToCreate.CommitDieGroup();
        diceManager.GetComponent<DiceManager>().CreateDieGroupBehaviour(dieGroupToCreate);
        mainUI.GetComponent<MainUI>().NextDieGroupPanel();
    }

    List<Die.DieType> DieTypeCountToDieList(int[] dieTypesCount)
    {
        List<Die.DieType> _diceList = new List<Die.DieType>();
        for (int i = 0; i < dieTypesCount.Length; i++)
        {
            Debug.Log(i);
            Debug.Log(dieTypesCount[i]);
            for (int j = 0; j < dieTypesCount[i]; ++j)
            {
                Debug.Log("Adding " + Die.DieTypeToString(i));
                _diceList.Add(Die.IndexToDieType(i));
            }
        }
        return _diceList;
    }

    void PrintDamageDieTypeCounts()
    {
        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            Debug.Log(Die.DieTypeToString(i) + ": " + damageDieTypesCount[i].ToString());
        }
    }
}
