using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CritOn : MonoBehaviour
{
    public GameObject text;
    public GameObject dieGroupSetup;

    public int critOn = 20;

    public void Init()
    {
        critOn = 20;
        UpdateText();
    }
    public void Init(int _critOn)
    {
        critOn = _critOn;
        UpdateText();
    }

    void UpdateText()
    {
        //Debug.Log(dieType.ToString());
        if (critOn == 20)
        {
            text.GetComponent<TMP_Text>().text = "20";
        }
        else
        {
            text.GetComponent<TMP_Text>().text = critOn.ToString() + "-20";
        }
    }

    public void PlusButton()
    {
        if (critOn + 1 <= 20)
        {
            critOn++;
        }
        UpdateText();
        dieGroupSetup.GetComponent<DieGroupSetup>().dieGroup.critOn = critOn;
    }

    public void MinusButton()
    {

        if (critOn - 1 >= 0)
        {
            critOn--;
        }
        UpdateText();
        dieGroupSetup.GetComponent<DieGroupSetup>().dieGroup.critOn = critOn;
    }
}
