using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public enum CritType { DoubleDice, DoubleTotal, MaxPlusRoll };
    public static CritType critType = CritType.DoubleDice;

    public List<GameObject> critTypeButtons = new List<GameObject>();
    public GameObject exampleText;
    public List<string> exampleStrings = new List<string>();

    public GameObject mainUI;

    public static int CritTypeIndex()
    {
        switch (critType)
        {
            case CritType.DoubleDice: return 0;
            case CritType.DoubleTotal: return 1;
            case CritType.MaxPlusRoll: return 2;
            default: return -1;
        }
    }

    public void Init()
    {
        UpdateButtons(CritTypeIndex());
    }

    public void UpdateButtons(int _critTypeIndex)
    {
        //set outlines
        foreach (GameObject button in critTypeButtons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
        critTypeButtons[_critTypeIndex].GetComponent<Outline>().enabled = true;

        //set static variable
        switch (_critTypeIndex)
        {
            case 0:
                critType = CritType.DoubleDice;
                break;
            case 1:
                critType = CritType.DoubleTotal;
                break;
            case 2:
                critType = CritType.MaxPlusRoll;
                break;
        }

        //set example text
        exampleText.GetComponent<TMP_Text>().text = exampleStrings[_critTypeIndex];
    }

    public string GetExampleText()
    {
        return exampleStrings[CritTypeIndex()];
    }

    public void BackBtn()
    {
        mainUI.GetComponent<MainUI>().SetVisibility(true);
    }
}
