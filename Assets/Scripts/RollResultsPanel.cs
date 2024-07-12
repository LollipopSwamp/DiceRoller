using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RollResultsPanel : MonoBehaviour


{
    public GameObject groupNameText;
    public GameObject diceTypesText;
    public GameObject resultsText;
    public GameObject separator0;
    public GameObject separator1;

    void Start()
    {
        ResetPanel();
    }
    public void ResetPanel()
    {
        //set all text to empty
        groupNameText.GetComponent<TMP_Text>().text = "";
        diceTypesText.GetComponent<TMP_Text>().text = "";
        resultsText.GetComponent<TMP_Text>().text = "";
        separator0.SetActive(false);
        separator1.SetActive(false);
    }

    public void UpdateText(string _groupText, string _diceTypeText, string _resultsText, Color _color)
    {
        groupNameText.GetComponent<TMP_Text>().text = _groupText;
        diceTypesText.GetComponent<TMP_Text>().text = _diceTypeText;
        resultsText.GetComponent<TMP_Text>().text = _resultsText;
        separator0.SetActive(true);
        separator1.SetActive(true);
    }
}
