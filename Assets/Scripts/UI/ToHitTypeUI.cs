using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToHitTypeUI : MonoBehaviour
{
    public List<GameObject> toHitTypeButtons = new List<GameObject>();
    public int toHitType = 0;
    public GameObject dieGroupSetup;

    public void Init()
    {
        UpdateButtons(0);
    }
    public void Init(int _toHitType)
    {
        UpdateButtons(_toHitType);
    }

    public void UpdateButtons(int _toHitType)
    {
        toHitType = _toHitType;
        toHitTypeButtons[_toHitType].GetComponent<Outline>().enabled = true;
        
        dieGroupSetup.GetComponent<DieGroupSetup>().dieGroup.toHitType = toHitType;
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }
}
