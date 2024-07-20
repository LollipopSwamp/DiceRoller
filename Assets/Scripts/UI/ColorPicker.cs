using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ColorPicker : MonoBehaviour
{
    public List<GameObject> colorButtons = new List<GameObject>();
    public int selectedColorIndex = 0;

    void Start()
    {
        //set color backgrounds
        for (int i = 0; i < colorButtons.Count; ++i)
        {
            colorButtons[i].GetComponent<Image>().color = DieGroup.GetColor(i,true);
        }

        UpdateButtons(selectedColorIndex);
    }

    public void UpdateButtons(int colorIndex)
    {
        selectedColorIndex = colorIndex;
        foreach (GameObject button in colorButtons)
        {
            button.GetComponent<Outline>().enabled = false;
        }
        colorButtons[colorIndex].GetComponent<Outline>().enabled = true;
    }
}
