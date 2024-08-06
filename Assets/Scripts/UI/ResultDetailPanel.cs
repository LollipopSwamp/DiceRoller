using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultDetailPanel : MonoBehaviour
{
    public GameObject detailTitle0;
    public GameObject detailInfo0;

    public GameObject detailTitle1;
    public GameObject detailInfo1;

    public void Init(string _detailTitleText, string _detailInfoText)
    {
        detailTitle0.GetComponent<TMP_Text>().text = _detailTitleText;
        detailInfo0.GetComponent<TMP_Text>().text = _detailInfoText;
    }

    public void Init(string _detailTitleText0, string _detailInfoText0, string _detailTitleText1, string _detailInfoText1)
    {
        detailTitle0.GetComponent<TMP_Text>().text = _detailTitleText0;
        detailInfo0.GetComponent<TMP_Text>().text = _detailInfoText0;
        detailTitle1.GetComponent<TMP_Text>().text = _detailTitleText1;
        detailInfo1.GetComponent<TMP_Text>().text = _detailInfoText1;
    }

}
