using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsUI : MonoBehaviour


{
    //prefabs
    public GameObject resultsPanelPrefabAttack;
    public GameObject resultsPanelPrefabStandard;

    //panel objects
    public List<GameObject> resultPanels = new List<GameObject>();

    //visibility
    private static bool showResultsUI = false;

    public GameObject resultDetails;

    //main ui object
    public GameObject mainUi;

    public void SetVisibility(bool visible)
    {
        showResultsUI = visible;
        gameObject.GetComponent<Canvas>().enabled = visible;
    }

    public void CreateResultsPanels(List<GameObject> dieGroups)
    {
        //reset results panels
        foreach(GameObject g in resultPanels) { Destroy(g); }

        //create results panels
        for (int i = 0; i < dieGroups.Count; i++)
        {
            //create panel from prefab
            DieGroupBehaviour dieGroupB = dieGroups[i].GetComponent<DieGroupBehaviour>();
            if (dieGroupB.dieGroup.toHitType == DieGroup.ToHitType.None)
            {
                GameObject resultPanel = Instantiate(resultsPanelPrefabStandard, Vector3.zero, Quaternion.identity, transform);
                resultPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                resultPanel.GetComponent<RollResultPanel>().Init(dieGroups[i]);
                resultPanels.Add(resultPanel);
            }
            else
            {
                GameObject resultPanel = Instantiate(resultsPanelPrefabAttack, Vector3.zero, Quaternion.identity, transform);
                resultPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                resultPanel.GetComponent<RollResultPanel>().Init(dieGroups[i]);
                resultPanels.Add(resultPanel);
            }


        }
    }


    public void ShowButton()
    {
        SetVisibility(true);
    }
    public void HideButton()
    {
        SetVisibility(false);
    }
    public void SetupButton()
    {
        SetVisibility(false);
        mainUi.GetComponent<MainUI>().SetVisibility(true);
    }
    public void ShowResultDetails(DieGroupBehaviour _dieGroupB)
    {
        resultDetails.GetComponent<Canvas>().enabled = true;
        resultDetails.GetComponent<ResultDetails>().Init(_dieGroupB);
        gameObject.GetComponent<Canvas>().enabled = false;
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
