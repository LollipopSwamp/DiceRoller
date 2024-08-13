using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultDetailPanel : MonoBehaviour
{
    public List<GameObject> detailTitles = new List<GameObject>();
    public List<GameObject> detailInfos = new List<GameObject>();


    public void Init(string _detailTitleText)
    {
        detailTitles[0].GetComponent<TMP_Text>().text = _detailTitleText;
    }
    public void Init(string _detailTitleText, string _detailInfoText)
    {
        detailTitles[0].GetComponent<TMP_Text>().text = _detailTitleText;
        detailInfos[0].GetComponent<TMP_Text>().text = _detailInfoText;
    }

    public void Init(string _detailTitleText0, string _detailInfoText0, string _detailTitleText1, string _detailInfoText1)
    {
        detailTitles[0].GetComponent<TMP_Text>().text = _detailTitleText0;
        detailInfos[0].GetComponent<TMP_Text>().text = _detailInfoText0;
        detailTitles[1].GetComponent<TMP_Text>().text = _detailTitleText1;
        detailInfos[1].GetComponent<TMP_Text>().text = _detailInfoText1;
    }

    public void Init(string _detailTitleText0, string _detailInfoText0, string _detailTitleText1, string _detailInfoText1, string _detailTitleText2, string _detailInfoText2)
    {
        detailTitles[0].GetComponent<TMP_Text>().text = _detailTitleText0;
        detailInfos[0].GetComponent<TMP_Text>().text = _detailInfoText0;
        detailTitles[1].GetComponent<TMP_Text>().text = _detailTitleText1;
        detailInfos[1].GetComponent<TMP_Text>().text = _detailInfoText1;
        detailTitles[2].GetComponent<TMP_Text>().text = _detailTitleText2;
        detailInfos[2].GetComponent<TMP_Text>().text = _detailInfoText2;
    }

}
