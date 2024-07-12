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
    public List<GameObject> textObjects = new List<GameObject>();

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
        ShowResultsText();
    }

    void ShowResultsText()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i.ToString() + " | " + resultStrings.Count.ToString()) ;
            if (i > resultStrings.Count-1)
            {
                textObjects[i].GetComponent<TMP_Text>().text = "";
            }
            else
            {
                textObjects[i].GetComponent<TMP_Text>().text = resultStrings[i];
            }
        }
    }

    void GetResultsStrings(List<DieGroup> _dieGroups)
    {
        Debug.Log("Showing results");
        foreach (DieGroup g in  _dieGroups)
        {
            string currResultsString = "";

            //dieGroup name
            currResultsString += g.groupName + " | ";

            //dice types
            string[] dieTypesStrings = new string[]
            {
                "d20",//0
                "d12",//1
                "d10",//2
                "d8",//3
                "d6",//4
                "d4"//5
            };

            int[] dieTypes = new int[7];
            foreach (Die d in g.dice)
            {
                switch (d.dieType)
                {
                    case Die.DieType.d20:
                        dieTypes[0] += 1;
                        break;
                    case Die.DieType.d12:
                        dieTypes[1] += 1;
                        break;
                    case Die.DieType.d10:
                        dieTypes[2] += 1;
                        break;
                    case Die.DieType.d8:
                        dieTypes[3] += 1;
                        break;
                    case Die.DieType.d6:
                        dieTypes[4] += 1;
                        break;
                    case Die.DieType.d4:
                        dieTypes[5] += 1;
                        break;
                }
            }
            for (int i = 0; i < dieTypes.Length; i++)
            {
                if (dieTypes[i] != 0)
                {
                    currResultsString += dieTypes[i].ToString() + dieTypesStrings[i].ToString() + " + ";
                }
            }
            currResultsString += g.modifier.ToString() + " | ";

            //groupResult
            currResultsString += "Result: " + (g.groupResult + g.modifier).ToString();
            resultStrings.Add(currResultsString);

        }
    }
}
