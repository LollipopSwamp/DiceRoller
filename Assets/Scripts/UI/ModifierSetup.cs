using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifierSetup : MonoBehaviour
{
    public GameObject dieGroupSetup;
    public GameObject text;

    public static int toHitCount = 0;
    public static int damageCount = 0;

    public int toHitType = 0;

    public void Init()
    {
        //text.GetComponent<TMP_Text>().text = "+" + (0).ToString();
        toHitCount = 0;
        damageCount = 0;
        UpdateText();
    }
    public void Init(int toHitType, int _count)
    {
        toHitType = toHitType;
        toHitCount = _count;
        damageCount = _count;
        UpdateText();
    }

    void UpdateText()
    {
        //attack
        if (toHitType == 0)
        {
            string modifierText = "";
            if (toHitCount >= 0) { modifierText += "+"; }
            modifierText += toHitCount.ToString();
            text.GetComponent<TMP_Text>().text = modifierText;
        }
        //standard
        else
        {
            string modifierText = "";
            if (damageCount >= 0) { modifierText += "+"; }
            modifierText += damageCount.ToString();
            text.GetComponent<TMP_Text>().text = modifierText;
        }
    }

    public void PlusButton(int _toHitType)
    {
        toHitType = _toHitType;
        if (_toHitType == 0)
        {
            if (toHitCount + 1 <= 25)
            {
                toHitCount++;
            }
            dieGroupSetup.GetComponent<DieGroupSetup>().SetModifier(true, toHitCount);
        }
        else
        {
            if (damageCount + 1 <= 25)
            {
                damageCount++;
            }
            dieGroupSetup.GetComponent<DieGroupSetup>().SetModifier(false, damageCount);
        }
        UpdateText();
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }

    public void MinusButton(int _toHitType)
    {

        UpdateText();
        if (_toHitType == 0)
        {
            if (toHitCount - 1 >= -25)
            {
                toHitCount--;
            }
            dieGroupSetup.GetComponent<DieGroupSetup>().SetModifier(true, toHitCount);
        }
        else
        {
            if (damageCount - 1 >= -25)
            {
                damageCount--;
            }
            dieGroupSetup.GetComponent<DieGroupSetup>().SetModifier(false, damageCount);
        }
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }
}
