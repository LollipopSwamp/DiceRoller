using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public GameObject uiManager;

    public GameObject title;

    public GameObject toHitTypeSetup;
    public List<GameObject> toHitDieTypes;
    public GameObject toHitModifier;
    public List<GameObject> damageDieTypes;
    public GameObject damageModifier;
    public TMP_InputField groupNameInput;
    public GameObject colorPicker;
    public GameObject attackRollSlider;
    public GameObject savePresetBtn;


    //edit mode bool
    public static int editMode = 0;

    public void Init()
    {
        Debug.Log("Creating new DieGroup");
        title.GetComponent<TMP_Text>().text = "Create DieGroup";
        //edit mode false
        editMode = 0;
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

    public void Init(DieGroup _dieGroup, int _editMode)
    {
        //edit mode
        editMode = _editMode;
        if (editMode == 1)
        {
            title.GetComponent<TMP_Text>().text = "Edit DieGroup";
            Debug.Log("Editing DieGroup with groupID: " + _dieGroup.groupId.ToString());
        }
        else if (editMode == 2)
        {
            title.GetComponent<TMP_Text>().text = "Edit Preset";
            Debug.Log("Editing Preset with presetID: " + _dieGroup.groupId.ToString());
        }

        //set die group variables
        dieGroup = _dieGroup;

        //set ui variables
        groupNameInput.text = _dieGroup.groupName;
        colorPicker.GetComponent<ColorPicker>().UpdateButtons(_dieGroup.colorIndex);

        toHitTypeSetup.GetComponent<ToHitTypeUI>().Init(_dieGroup.toHitType);
        toHitBonusDieTypesCount = _dieGroup.GetToHitBonusDieTypeInts();
        for (int i = 0; i < toHitBonusDieTypesCount.Length; i++)
        {
            toHitDieTypes[i].GetComponent<DieTypeSetup>().Init(toHitBonusDieTypesCount[i]);
        }
        toHitModifier.GetComponent<ModifierSetup>().Init(1,_dieGroup.toHitModifier);

        damageDieTypesCount = _dieGroup.GetDamageDieTypeInts();
        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            damageDieTypes[i].GetComponent<DieTypeSetup>().Init(damageDieTypesCount[i]);
        }
        damageModifier.GetComponent<ModifierSetup>().Init(0, _dieGroup.damageModifier);
        UIManager.nextSetupMenu = 0;

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
        if (!attackRoll && _currMenu == 2) { uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(); }
        uiManager.GetComponent<UIManager>().PreviousDieGroupMenu();
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
                    uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                }
                uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                break;
            case 1:
                List<Die.DieType> _toHitBonusDice = DieTypeCountToDieList(toHitBonusDieTypesCount);
                dieGroup.toHitBonusDice = _toHitBonusDice;
                uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                if (editMode == 2)
                {
                    savePresetBtn.GetComponent<Button>().interactable = false;
                }
                else
                {
                    savePresetBtn.GetComponent<Button>().interactable = true;
                }
                break;
        }
    }
    public void SaveDieGroup()
    {
        List<Die.DieType> _damageDice = DieTypeCountToDieList(damageDieTypesCount);
        dieGroup.damageDice = _damageDice;
        switch (editMode)
        {
            case 0: //create new
                dieGroup.CommitDieGroup();
                diceManager.GetComponent<DiceManager>().AddDieGroup(dieGroup);
                uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                break;
            case 1: // edit created die group
                if (!attackRoll)
                {
                    dieGroup.toHitBonusDice = new List<Die.DieType>();
                    dieGroup.toHitModifier = 0;
                }
                diceManager.GetComponent<DiceManager>().UpdateDieGroup(dieGroup);
                uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                break;
            case 2: //edit preset
                if (!attackRoll)
                {
                    dieGroup.toHitBonusDice = new List<Die.DieType>();
                    dieGroup.toHitModifier = 0;
                }
                Presets.UpdatePreset(dieGroup);
                uiManager.GetComponent<UIManager>().ShowPresetsUI();
                break;
        }
    }
    public void SaveDieGroupPreset()
    {
        SaveDieGroup();
        uiManager.GetComponent<UIManager>().SaveDieGroupPreset(dieGroup);
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
