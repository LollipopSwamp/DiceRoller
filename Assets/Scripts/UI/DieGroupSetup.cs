using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DieGroupSetup : MonoBehaviour
{
    public DieGroup dieGroup = new DieGroup();
    public bool attackRoll;

    public GameObject toHitBonusDieTypesString;
    public GameObject damageDieTypesString;
    public int[] toHitBonusDieTypesCount = new int[7];
    public int[] damageDieTypesCount = new int[7];

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
        toHitBonusDieTypesCount = _dieGroup.GetToHitBonusDieTypesArray();
        for (int i = 0; i < toHitBonusDieTypesCount.Length; i++)
        {
            toHitDieTypes[i].GetComponent<DieTypeSetup>().Init(toHitBonusDieTypesCount[i]);
        }
        toHitModifier.GetComponent<ModifierSetup>().Init(1,_dieGroup.toHitModifier);

        damageDieTypesCount = _dieGroup.GetDamageDieTypesArray();
        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            damageDieTypes[i].GetComponent<DieTypeSetup>().Init(damageDieTypesCount[i]);
        }
        damageModifier.GetComponent<ModifierSetup>().Init(0, _dieGroup.damageModifier);
        UIManager.nextSetupMenu = 0;

        SetDieTypeString();
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
        toHitBonusDieTypesString.GetComponent<TMP_Text>().text = dieGroup.GetToHitDiceTypesString();
        damageDieTypesString.GetComponent<TMP_Text>().text = dieGroup.GetDamageDiceTypesString();
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
                    dieGroup.toHitType = 0;
                    uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                }
                uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                break;
            case 1:
                List<int> _toHitBonusDice = DieTypeCountToDieList(toHitBonusDieTypesCount);
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
        List<int> _damageDice = DieTypeCountToDieList(damageDieTypesCount);
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
                    dieGroup.toHitBonusDice = new List<int>();
                    dieGroup.toHitModifier = 0;
                }
                diceManager.GetComponent<DiceManager>().UpdateDieGroup(dieGroup);
                uiManager.GetComponent<UIManager>().NextDieGroupMenu();
                break;
            case 2: //edit preset
                if (!attackRoll)
                {
                    dieGroup.toHitBonusDice = new List<int>();
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

    public List<int> DieTypeCountToDieList(int[] dieTypesCount)
    {
        List<int> _diceList = new List<int>();
        for (int i = 0; i < dieTypesCount.Length; i++)
        {
            for (int j = 0; j < dieTypesCount[i]; ++j)
            {
                _diceList.Add(i);
            }
        }
        return _diceList;
    }

}
