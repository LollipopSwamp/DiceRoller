using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieGroupSetup : MonoBehaviour
{
    public DieGroup dieGroup = new DieGroup();
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

    //edit mode bool
    public bool editMode = false;

    public void Init()
    {
        //edit mode false
        editMode = false;
        //reset die group variables
        dieGroup = new DieGroup();
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

    public void Init(DieGroup _dieGroup)
    {
        //edit mode true
        editMode = true;

        //set die group variables
        if (_dieGroup.toHitType == DieGroup.ToHitType.None){
            attackRoll = false;
            attackRollSlider.GetComponent<ToggleSlider>().SetState(false);
            toHitTypeSetup.GetComponent<ToHitTypeUI>().Init();
        }
        else{
            attackRoll = true;
            attackRollSlider.GetComponent<ToggleSlider>().SetState(true);
            toHitTypeSetup.GetComponent<ToHitTypeUI>().Init(_dieGroup.toHitType);
        }

        toHitBonusDieTypesCount = _dieGroup.GetToHitBonusDieTypeInts();
        damageDieTypesCount = _dieGroup.GetDamageDieTypeInts();

        //set ui variables
        groupNameInput.text = _dieGroup.groupName;
        colorPicker.GetComponent<ColorPicker>().UpdateButtons(_dieGroup.colorIndex);

        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            toHitDieTypes[i].GetComponent<DieTypeSetup>().Init(toHitBonusDieTypesCount[i]);
        }
        toHitModifier.GetComponent<ModifierSetup>().Init(1,_dieGroup.toHitModifier);

        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            damageDieTypes[i].GetComponent<DieTypeSetup>().Init(damageDieTypesCount[i]);
        }
        damageModifier.GetComponent<ModifierSetup>().Init(0, _dieGroup.damageModifier);
        SetDieTypeString();
    }

    public void SetToHitType(int _toHitTypeIndex)
    {
        switch (_toHitTypeIndex)
        {
            case 0:
                dieGroup.toHitType = DieGroup.ToHitType.Standard;
                break;
            case 1:
                dieGroup.toHitType = DieGroup.ToHitType.Advantage;
                break;
            case 2:
                dieGroup.toHitType = DieGroup.ToHitType.Disadvantage;
                break;
        }

    }
    public void SetModifier(bool _toHit, int _modifier)
    {
        if (_toHit)
        {
            dieGroup.toHitModifier = _modifier;
        }
        else
        {
            dieGroup.damageModifier = _modifier;
        }
    }
    public void SetDieTypeString()
    {
        //to hit die type string
        string dieTypeString = "";
        switch (dieGroup.toHitType)
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
        if (dieGroup.toHitModifier < 0)
        {dieTypeString += " - ";}
        else { dieTypeString += " + "; }
        dieTypeString += Mathf.Abs(dieGroup.toHitModifier).ToString();
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
        if (dieGroup.damageModifier < 0){ dieTypeString += " - "; }
        else { dieTypeString += " + "; }
        dieTypeString += Mathf.Abs(dieGroup.damageModifier).ToString();
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
                dieGroup.groupName = groupNameInput.text;
                dieGroup.colorIndex = colorPicker.GetComponent<ColorPicker>().selectedColorIndex;
                attackRoll = attackRollSlider.GetComponent<ToggleSlider>().state;
                if (!attackRoll) 
                { 
                    dieGroup.toHitType = DieGroup.ToHitType.None;
                    mainUI.GetComponent<MainUI>().NextDieGroupPanel();
                }
                mainUI.GetComponent<MainUI>().NextDieGroupPanel();
                dieGroup.PrintDieGroup();
                break;
            case 1:
                List<Die.DieType> _toHitBonusDice = DieTypeCountToDieList(toHitBonusDieTypesCount);
                dieGroup.toHitBonusDice = _toHitBonusDice;
                mainUI.GetComponent<MainUI>().NextDieGroupPanel();
                dieGroup.PrintDieGroup();
                break;
        }
    }
    public void SaveDieGroup()
    {
        List<Die.DieType> _damageDice = DieTypeCountToDieList(damageDieTypesCount);
        dieGroup.damageDice = _damageDice;
        if (editMode)
        {
            if (!attackRoll)
            {
                dieGroup.toHitBonusDice = new List<Die.DieType>();
                dieGroup.toHitModifier = 0;
            }
            diceManager.GetComponent<DiceManager>().UpdateDieGroup(dieGroup);
        }
        else
        {
            dieGroup.CommitDieGroup();
            diceManager.GetComponent<DiceManager>().dieGroups.Add(dieGroup);
        }
        mainUI.GetComponent<MainUI>().NextDieGroupPanel();
    }
    public void SaveDieGroupPreset()
    {
        SaveDieGroup();
        mainUI.GetComponent<MainUI>().SaveDieGroupPreset(dieGroup);
    }

    List<Die.DieType> DieTypeCountToDieList(int[] dieTypesCount)
    {
        List<Die.DieType> _diceList = new List<Die.DieType>();
        for (int i = 0; i < dieTypesCount.Length; i++)
        {
            //Debug.Log(i);
            //Debug.Log(dieTypesCount[i]);
            for (int j = 0; j < dieTypesCount[i]; ++j)
            {
                //Debug.Log("Adding " + Die.DieTypeToString(i));
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
