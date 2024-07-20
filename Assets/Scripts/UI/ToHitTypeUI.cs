using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToHitTypeUI : MonoBehaviour
{
    public List<GameObject> toHitTypeButtons = new List<GameObject>();
    public DieGroup.ToHitType toHitType = 0;

    void Start()
    {
        UpdateButtons(0);
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
    }
}
