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

    public int modifierType; //0 is d20, 1 is damage

    public void Init()
    {
        //text.GetComponent<TMP_Text>().text = "+" + (0).ToString();
        modifierType = 0;
        toHitCount = 0;
        damageCount = 0;
        UpdateText();
    }
    public void Init(int _modifierType, int _count)
    {
        modifierType = _modifierType;
        if (modifierType == 0) { toHitCount = _count; }
        else { damageCount = _count; }
        UpdateText();
    }

    void UpdateText()
    {
        //attack
        if (modifierType == 0)
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

    public void PlusButton(int _modifierType)
    {
        modifierType = _modifierType;
        if (modifierType == 0)
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

    public void MinusButton(int _modifierType)
    {
        modifierType = _modifierType;
        UpdateText();
        if (modifierType == 0)
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
