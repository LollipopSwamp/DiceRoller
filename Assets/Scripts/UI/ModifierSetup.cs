using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifierSetup : MonoBehaviour
{
    public GameObject dieGroupSetup;
    public GameObject text;

    public static int count = 0;

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        text.GetComponent<TMP_Text>().text = "+" + count.ToString();
    }

    public void PlusButton()
    {
        if (count + 1 < 25)
        {
            count++;
            UpdateDieGroup();
        }
        UpdateText();
    }

    public void MinusButton()
    {

        if (count - 1 >= 0)
        {
            count--;
            UpdateDieGroup();
        }
        UpdateText();
    }
    public void UpdateDieGroup(bool attackModifier)
    {
        if (!attackModifier)
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().dieGroupToCreate.damageModifier = count;
        }
        else
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().dieGroupToCreate.toHitModifier = count;
        }
    }
}
