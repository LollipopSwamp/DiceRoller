using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieTypeSetup : MonoBehaviour
{
    //[SerializeField]
    public int dieType;

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
        text.GetComponent<TMP_Text>().text = count.ToString() + Die.DieTypeToString(dieType);
    }

    public void PlusButton(int _dieSubGroup)
    {
        if (count + 1 <= 20) 
        {
            count++;
        }
        UpdateText();
        if (_dieSubGroup == 0)
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().toHitBonusDieTypesCount[dieType] = count;
        }
        else
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().damageDieTypesCount[dieType] = count;
        }
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }

    public void MinusButton(int _dieSubGroup)
    {

        if (count - 1 >= 0)
        {
            count--;
        }
        UpdateText();
        if (_dieSubGroup == 0)
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().toHitBonusDieTypesCount[dieType] = count;
        }
        else
        {
            dieGroupSetup.GetComponent<DieGroupSetup>().damageDieTypesCount[dieType] = count;
        }
        dieGroupSetup.GetComponent<DieGroupSetup>().SetDieTypeString();
    }
}
