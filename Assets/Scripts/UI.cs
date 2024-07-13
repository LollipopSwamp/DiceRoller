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
        GetResultsStrings(_dieGroups);
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

    void GetResultsStrings(List<DieGroup> _dieGroups)
    {
        Debug.Log("Showing results");
        foreach (DieGroup g in  _dieGroups)
        {

            //dieGroup name
            groupNameStrings.Add(g.groupName);

            //dice0 types
            string displayString = "";
            string[] dieTypesNames = new string[]
            {
                "d20",//0
                "d12",//1
                "d10",//2
                "d8",//3
                "d6",//4
                "d4"//5
            };

            int[] dieTypesCount = new int[6];
            foreach (Die d in g.dice0)
            {
                switch (d.dieType)
                {
                    case Die.DieType.d20:
                        dieTypesCount[0] += 1;
                        break;
                    case Die.DieType.d12:
                        dieTypesCount[1] += 1;
                        break;
                    case Die.DieType.d10:
                        dieTypesCount[2] += 1;
                        break;
                    case Die.DieType.d8:
                        dieTypesCount[3] += 1;
                        break;
                    case Die.DieType.d6:
                        dieTypesCount[4] += 1;
                        break;
                    case Die.DieType.d4:
                        dieTypesCount[5] += 1;
                        break;
                }
            }
            for (int i = 0; i < dieTypesCount.Length; i++)
            {
                if (dieTypesCount[i] != 0)
                {
                    displayString += dieTypesCount[i].ToString() + dieTypesNames[i] + " + ";
                }
            }
            displayString += g.toHitModifier.ToString();
            dieTypesStrings.Add(displayString);


            //toHitResult
            resultStrings.Add("Result: " + (g.toHitResult + g.toHitModifier).ToString());

        }
    }
}
