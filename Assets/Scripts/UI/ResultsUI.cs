using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour


{
    //prefabs
    public GameObject resultsPanelPrefabAttack;
    public GameObject resultsPanelPrefabStandard;

    //visibility
    private static bool showResultsUI = false;

    void Start()
    {

    }
    public void ToggleVisibility()
    {
        showResultsUI = !showResultsUI;
        gameObject.GetComponent<Canvas>().enabled = showResultsUI;
    }

    public void CreateResultsPanels(List<GameObject> dieGroups)
    {
        for (int i = 0; i < dieGroups.Count; i++)
        {
            //create panel from prefab
            GameObject resultPanel = Instantiate(resultsPanelPrefabAttack, Vector3.zero, Quaternion.identity, transform);
            resultPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
            resultPanel.GetComponent<RollResultPanel>().Init(dieGroups[i]);

            //set text
            //RollResult rollResult = dieGroups[i].GetComponent<dieGroups>().rollResult;


        }
        ResetPanel();
    }
    public void ResetPanel()
    {
        //set all text to empty
        //groupNameText.GetComponent<TMP_Text>().text = "";
        //diceTypesText.GetComponent<TMP_Text>().text = "";
        //resultsText.GetComponent<TMP_Text>().text = "";
        //separator0.SetActive(false);
        //separator1.SetActive(false);
    }

    public void UpdateText(string _groupText, string _diceTypeText, string _resultsText, Color _color)
    {
        //groupNameText.GetComponent<TMP_Text>().text = _groupText;
        //diceTypesText.GetComponent<TMP_Text>().text = _diceTypeText;
        //resultsText.GetComponent<TMP_Text>().text = _resultsText;
        //separator0.SetActive(true);
        //separator1.SetActive(true);
    }
}
