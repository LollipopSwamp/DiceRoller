using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject mainUI;
    public static bool showMainUI = true;

    public GameObject resultsUI;
    public static bool showResultsUI = false;
    public List<GameObject> textPanels = new List<GameObject>();

    private List<string> groupNameStrings = new List<string>();
    private List<string> dieTypesStrings = new List<string>();
    private List<string> resultStrings = new List<string>();

    public void ToggleMainUI()
    {
        showMainUI = !showMainUI;
        mainUI.GetComponent<Canvas>().enabled = showMainUI;
    }

    public void ToggleResultsUI()
    {
        showResultsUI = !showResultsUI;
        resultsUI.GetComponent<Canvas>().enabled = showResultsUI;
    }
    public void ShowResults(List<DieGroup> _dieGroups)
    {
        //GetResultsStrings(_dieGroups);
        UpdateResultsText(_dieGroups);
    }

    void UpdateResultsText(List<DieGroup> _dieGroups)
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i.ToString() + " | " + resultStrings.Count.ToString()) ;
            if (i > resultStrings.Count-1)
            {
                textPanels[i].GetComponent<RollResultsPanel>().ResetPanel();
            }
            else
            {
                Color textColor = _dieGroups[i].GetColor();
                textPanels[i].GetComponent<RollResultsPanel>().UpdateText(groupNameStrings[i], dieTypesStrings[i], resultStrings[i], textColor);
            }
        }
    }
}
