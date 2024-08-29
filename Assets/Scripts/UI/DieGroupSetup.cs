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
    public GameObject critOn;
    public List<GameObject> damageDieTypes;
    public GameObject damageModifier;
    public TMP_InputField groupNameInput;
    public GameObject colorPicker;
    public GameObject dieGroupType;
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
        toHitBonusDieTypesCount = new int[7];
        damageDieTypesCount = new int[7];

        //reset ui variables
        groupNameInput.text = "(Group Name)";
        colorPicker.GetComponent<ColorPicker>().UpdateButtons(0);
        dieGroupType.GetComponent<DieGroupType>().UpdateButtons(0);
        toHitTypeSetup.GetComponent<ToHitTypeUI>().Init();
        foreach (GameObject g in toHitDieTypes) { g.GetComponent<DieTypeSetup>().Init(); }
        toHitModifier.GetComponent<ModifierSetup>().Init();
        critOn.GetComponent<CritOn>().Init();
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
        dieGroupType.GetComponent<DieGroupType>().UpdateButtons(_dieGroup.dieGroupType);

        toHitTypeSetup.GetComponent<ToHitTypeUI>().Init(_dieGroup.toHitType);
        toHitBonusDieTypesCount = _dieGroup.GetToHitBonusDieTypesArray();
        for (int i = 0; i < toHitBonusDieTypesCount.Length; i++)
        {
            toHitDieTypes[i].GetComponent<DieTypeSetup>().Init(toHitBonusDieTypesCount[i]);
        }
        toHitModifier.GetComponent<ModifierSetup>().Init(0,_dieGroup.toHitModifier);
        critOn.GetComponent<CritOn>().Init(_dieGroup.critOn);

        damageDieTypesCount = _dieGroup.GetDamageDieTypesArray();
        for (int i = 0; i < damageDieTypesCount.Length; i++)
        {
            damageDieTypes[i].GetComponent<DieTypeSetup>().Init(damageDieTypesCount[i]);
        }
        damageModifier.GetComponent<ModifierSetup>().Init(1, _dieGroup.damageModifier);
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
        toHitBonusDieTypesString.GetComponent<TMP_Text>().text = dieGroup.GetToHitDiceTypesString(toHitBonusDieTypesCount);
        damageDieTypesString.GetComponent<TMP_Text>().text = dieGroup.GetDamageDiceTypesString(damageDieTypesCount);
    }

    public void BackButton(int _currMenu)
    {
        switch (_currMenu)
        {
            case 0: //basic setup
                uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(-1);
                break;
            case 1: //to hit setup
                uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(0);
                break;
            case 2: //damage setup
                if (dieGroup.dieGroupType == 0) //standard
                {
                    uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(0);
                }
                else
                {
                    uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(1);
                }
                break;
        }
                //if (dieGroup.dieGroupType == 0 && _currMenu == 2) { uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(); }
        //else if (dieGroup.dieGroupType == 3 && _currMenu == 3) { uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(); }
        //uiManager.GetComponent<UIManager>().PreviousDieGroupMenu(_currMenu-1);
    }

    public void NextButton(int _currMenu)
    {
        switch (_currMenu)
        {
            case 0: //basic setup
                dieGroup.groupName = groupNameInput.text;
                dieGroup.colorIndex = colorPicker.GetComponent<ColorPicker>().selectedColorIndex;
                dieGroup.dieGroupType = dieGroupType.GetComponent<DieGroupType>().dieGroupType;
                switch (dieGroup.dieGroupType) 
                {
                    case 0: //standard
                        dieGroup.toHitType = 3;
                        uiManager.GetComponent<UIManager>().NextDieGroupMenu(2);
                        toHitTypeSetup.GetComponent<ToHitTypeUI>().Init();
                        break;
                    case 1: //attack
                        uiManager.GetComponent<UIManager>().NextDieGroupMenu(1);
                        return;
                    case 2: //percentile
                        dieGroup.toHitType = 3;
                        damageDieTypesCount[4] = 1;
                        damageDieTypesCount[3] = 1;
                        SaveDieGroup();
                        return;
                    case 3: //skill check
                        uiManager.GetComponent<UIManager>().NextDieGroupMenu(1);
                        toHitTypeSetup.GetComponent<ToHitTypeUI>().Init();
                        break;
                    default:
                        break;
                }
                break;
            case 1: // to hit setup
                List<int> _toHitBonusDice = DieTypeCountToDieList(toHitBonusDieTypesCount);
                dieGroup.toHitBonusDice = _toHitBonusDice;
                dieGroup.toHitModifier = ModifierSetup.toHitCount;

                //dieGroupType
                if (dieGroup.dieGroupType == 3) //skill check
                {
                    SaveDieGroup();
                }
                else
                {
                    uiManager.GetComponent<UIManager>().NextDieGroupMenu(2);
                }

                //edit mode
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
    private int DiceCount()
    {
        List<int> _damageDice = DieTypeCountToDieList(damageDieTypesCount);
        dieGroup.damageDice = _damageDice;
        return dieGroup.damageDice.Count;
    }
    public void SaveDieGroup()
    {
        if (DiceCount() == 0 && dieGroup.dieGroupType != 3)
        {
            uiManager.GetComponent<UIManager>().ShowError("Cannot create Die Group with no dice");
            return;
        }
        else if (dieGroup.dieGroupType == 3)
        {
            dieGroup.damageDice.Clear();
        }
        switch (editMode)
        {
            case 0: //create new
                dieGroup.CommitDieGroup();
                diceManager.GetComponent<DiceManager>().AddDieGroup(dieGroup);
                uiManager.GetComponent<UIManager>().NextDieGroupMenu(3);
                break;
            case 1: // edit created die group
                if (dieGroup.dieGroupType != 1 && dieGroup.dieGroupType != 3)
                {
                    dieGroup.toHitBonusDice = new List<int>();
                    dieGroup.toHitModifier = 0;
                }
                diceManager.GetComponent<DiceManager>().UpdateDieGroup(dieGroup);
                uiManager.GetComponent<UIManager>().NextDieGroupMenu(3);
                break;
            case 2: //edit preset
                if (dieGroup.dieGroupType != 1 && dieGroup.dieGroupType != 3)
                {
                    dieGroup.toHitBonusDice = new List<int>();
                    dieGroup.toHitModifier = 0;
                }
                Presets.UpdatePreset(dieGroup);
                uiManager.GetComponent<UIManager>().ShowPresetsUI();
                break;
        }
        //save to session
        SaveSession.Save(diceManager.GetComponent<DiceManager>().dieGroups);
    }
    public void SaveDieGroupPreset()
    {
        if (DiceCount() == 0)
        {
            uiManager.GetComponent<UIManager>().ShowError("Cannot create Die Group with no dice");
            return;
        }
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
