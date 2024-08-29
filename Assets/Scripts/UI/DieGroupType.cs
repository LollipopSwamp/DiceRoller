using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DieGroupType : MonoBehaviour
{
    public int dieGroupType;
    public List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        UpdateButtons(dieGroupType);
    }
    public void UpdateButtons(int _dieGroupType)
    {
        dieGroupType = _dieGroupType;
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
        buttons[dieGroupType].GetComponent<Outline>().enabled = true;
    }
}
