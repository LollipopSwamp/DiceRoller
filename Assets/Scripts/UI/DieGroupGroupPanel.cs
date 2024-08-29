using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DieGroupGroupPanel : MonoBehaviour
{
    public int dieGroupGroupId;
    public GameObject dieGroupGroupName;
    public GameObject text;

    public void Init(int _dieGroupGroupId, string _dieGroupGroupName)
    {
        dieGroupGroupId = _dieGroupGroupId;
        dieGroupGroupName.GetComponent<TMP_Text>().text = SaveDieGroupGroup.dieGroupGroupNames[_dieGroupGroupId];
        //text.GetComponent<TMP_Text>().text = SaveDieGroupGroup.dieGroupGroupNames[_dieGroupGroupId];
        DeselectPanel();

    }
    public void SelectPanel()
    {
        gameObject.GetComponentInParent<DieGroupGroupsUI>().SelectPanel(dieGroupGroupId);
        gameObject.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
        gameObject.GetComponent<CanvasRenderer>().SetColor(new Color(0.9f, 0.9f, 0.9f, 1));
    }
    public void DeselectPanel()
    {
        gameObject.GetComponent<Outline>().effectDistance = new Vector2(2.5f, 2.5f);
        gameObject.GetComponent<CanvasRenderer>().SetColor(Color.white);
    }
}
