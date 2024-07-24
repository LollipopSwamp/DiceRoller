using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieTypeSetup : MonoBehaviour
{
    //[SerializeField]
    public Die.DieType dieType;

    public GameObject text;
    public GameObject dieGroupSetup;

    public int count = 0;

    public void Init()
    {
        count = 0;
        UpdateText();
    }
    public void Init(int _count)
    {
        count = _count;
        UpdateText();
    }

    void UpdateText()
    {
        //Debug.Log(dieType.ToString());
        text.GetComponent<TMP_Text>().text = count.ToString() + dieType.ToString().ToLower();
    }

    public void PlusButton(int _toHitType)
    {
        Debug.Log("Plus button hit");
        if (count + 1 < 10) 
        {
            count++;
        }
        UpdateText();
        if (_toHitType == 0)
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().toHitBonusDieTypesCount[Die.DieTypeToIndex(dieType)] = count;
        }
        else
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().damageDieTypesCount[Die.DieTypeToIndex(dieType)] = count;
        }
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }

    public void MinusButton(int _toHitType)
    {

        Debug.Log("Minus button hit");
        if (count - 1 >= 0)
        {
            count--;
        }
        UpdateText();
        if (_toHitType == 0)
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().toHitBonusDieTypesCount[Die.DieTypeToIndex(dieType)] = count;
        }
        else
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().damageDieTypesCount[Die.DieTypeToIndex(dieType)] = count;
        }
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }
}
