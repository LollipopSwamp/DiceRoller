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
    public GameObject scrollContent;

    //visibility
    private static bool showResultsUI = false;

    public GameObject resultDetails;

    //main ui object
    public GameObject uiManager;


    public void CreateResultsPanels(List<GameObject> dieGroups)
    {
        //reset results panels
        foreach(GameObject g in resultPanels) { Destroy(g); }
        resultPanels.Clear();

        //create results panels
        for (int i = 0; i < dieGroups.Count; i++)
        {
            //create panel from prefab
            DieGroupBehaviour dieGroupB = dieGroups[i].GetComponent<DieGroupBehaviour>();
            if (dieGroupB.dieGroup.toHitType == DieGroup.ToHitType.None)
            {
                GameObject resultPanel = Instantiate(resultsPanelPrefabStandard, Vector3.zero, Quaternion.identity, scrollContent.transform);
                resultPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                resultPanel.GetComponent<RollResultPanel>().Init(dieGroups[i]);
                resultPanels.Add(resultPanel);
            }
            else
            {
                GameObject resultPanel = Instantiate(resultsPanelPrefabAttack, Vector3.zero, Quaternion.identity, scrollContent.transform);
                resultPanel.transform.localPosition = new Vector3(0, 390 - (80 * i), 0);
                resultPanel.GetComponent<RollResultPanel>().Init(dieGroups[i]);
                resultPanels.Add(resultPanel);
            }
        }
        ResizeScrollContent();
    }


    public void ShowButton()
    {
        uiManager.GetComponent<UIManager>().ShowResultsUI();
    }
    public void HideButton()
    {
        uiManager.GetComponent<UIManager>().ShowDiceRolledUI();
    }
    public void SetupButton()
    {
        uiManager.GetComponent<UIManager>().ShowMainSetupUI();
    }
    public void ShowResultDetails(DieGroupBehaviour _dieGroupB)
    {
        uiManager.GetComponent<UIManager>().ShowResultDetails(_dieGroupB);
    }
    public void ResizeScrollContent()
    {
        int newHeight = resultPanels.Count * 175 + 25;
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1820, newHeight);
        scrollContent.transform.localPosition = new Vector2(0, newHeight * -0.5f);
    }
}
