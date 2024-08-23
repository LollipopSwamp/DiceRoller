using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PresetPanel : MonoBehaviour
{
    public int index;
    public DieGroup dieGroup;

    //strings
    public List<string> panelStrings = new List<string>();

    //panels
    public List<GameObject> standardTextPanels;
    public List<GameObject> attackTextPanels;

    public void Init(int _index, DieGroup _dieGroup)
    {
        dieGroup = _dieGroup;
        index = _index;
        panelStrings.Add(_dieGroup.groupName);
        panelStrings.Add(_dieGroup.GetToHitDiceTypesString());
        panelStrings.Add(_dieGroup.GetDamageDiceTypesString());
        DeselectPanel();

        //set strings
        if (_dieGroup.toHitType == 3)
        {
            standardTextPanels[0].GetComponent<TMP_Text>().text = panelStrings[0];
            standardTextPanels[1].GetComponent<TMP_Text>().text = panelStrings[2];
        }
        else
        {
            attackTextPanels[0].GetComponent<TMP_Text>().text = panelStrings[0];
            attackTextPanels[1].GetComponent<TMP_Text>().text = panelStrings[1];
            attackTextPanels[2].GetComponent<TMP_Text>().text = panelStrings[2];
        }
    }
    public void SelectPanel()
    {
        gameObject.GetComponentInParent<PresetsMenu>().SelectPanel(index);
        gameObject.GetComponent<Outline>().effectDistance = new Vector2(10, 10);
        gameObject.GetComponent<CanvasRenderer>().SetColor(dieGroup.GetColor(true));
        //transform.SetAsLastSibling();
    }
    public void DeselectPanel()
    {
        gameObject.GetComponent<Outline>().effectDistance = new Vector2(2.5f, 2.5f);
        gameObject.GetComponent<CanvasRenderer>().SetColor(dieGroup.GetColor(false));
    }
}
