using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieTypeSetup : MonoBehaviour
{
    //[SerializeField]
    public Die.DieType dieType;

    public GameObject text;

    public int count = 0;

    void Start()
    {
        UpdateText();
    }

    void UpdateText()
    {
        Debug.Log(dieType.ToString());
        text.GetComponent<TMP_Text>().text = count.ToString() + dieType.ToString().ToLower();
    }

    public void PlusButton()
    {
        Debug.Log("Plus button hit");
        if (count + 1 < 10) 
        {
            count++;
        }
        UpdateText();
    }

    public void MinusButton()
    {

        Debug.Log("Minus button hit");
        if (count - 1 >= 0)
        {
            count--;
        }
        UpdateText();
    }
}
