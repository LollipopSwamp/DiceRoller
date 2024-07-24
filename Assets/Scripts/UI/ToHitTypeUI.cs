using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToHitTypeUI : MonoBehaviour
{
    public List<GameObject> toHitTypeButtons = new List<GameObject>();
    public DieGroup.ToHitType toHitType = 0;
    public GameObject dieGroupSetup;

    public void Init()
    {
        UpdateButtons(0);
    }
    public void Init(DieGroup.ToHitType _toHitType)
    {
        switch (_toHitType)
        {
            case DieGroup.ToHitType.Standard:
                UpdateButtons(0);
                break;
            case DieGroup.ToHitType.Advantage:
                UpdateButtons(1);
                break;
            case DieGroup.ToHitType.Disadvantage:
                UpdateButtons(2);
                break;
            case DieGroup.ToHitType.None:
                UpdateButtons(0);
                break;
        }
    }

    public void UpdateButtons(int _toHitTypeIndex)
    {
        //selectedColorIndex = colorIndex;
        foreach (GameObject button in toHitTypeButtons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
        switch (_toHitTypeIndex)
        {
            case 0:
                toHitType = DieGroup.ToHitType.Standard;
                break;
            case 1:
                toHitType = DieGroup.ToHitType.Advantage;
                break;
            case 2:
                toHitType = DieGroup.ToHitType.Disadvantage;
                break;
        }
        toHitTypeButtons[_toHitTypeIndex].GetComponent<Outline>().enabled = true;
        
        dieGroupSetup.GetComponent<DieGroupSetup>().SetToHitType(_toHitTypeIndex);
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }
}
