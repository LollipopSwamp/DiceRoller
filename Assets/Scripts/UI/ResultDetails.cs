using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultDetails : MonoBehaviour
{
    public List<GameObject> detailPanels = new List<GameObject>();

    public GameObject detailPanelPrefabTitle;
    public GameObject detailPanelPrefabSingle;
    public GameObject detailPanelPrefabDouble;
    public GameObject detailPanelPrefabTriple;

    public GameObject title;
    public GameObject scrollContent;

    public GameObject resultsUI;
    public GameObject settings;

    public void InitAttackPanels(DieGroupBehaviour _dieGroupB)
    {
        //title panel
        CreateTitlePanel("To Hit");

        //to hit type
        CreateSinglePanel("To Hit Type", _dieGroupB.dieGroup.ToHitTypeString());

        //to hit die types
        CreateSinglePanel("To Hit Dice", _dieGroupB.dieGroup.GetToHitDiceTypesString());

        //to hit die results
        List<string> toHitStrings = new List<string>();
        foreach (GameObject _dieObj in _dieGroupB.dice)
        {
            Die _die = _dieObj.GetComponent<DieBehaviour>().die;
            if (_die.dieSubGroup == 0 || _die.dieSubGroup == 1)
            {
                toHitStrings.Add("Die Result (" + Die.DieTypeToString(_die.dieType) + ")");
                toHitStrings.Add(_die.result.ToString());
            }
        }
        while (toHitStrings.Count % 6 > 0)
        {
            toHitStrings.Add("");
        }
        for (int i = 0; i < toHitStrings.Count; i += 6)
        {
            CreateTriplePanel(toHitStrings[i], toHitStrings[i + 1], toHitStrings[i + 2], toHitStrings[i + 3], toHitStrings[i + 4], toHitStrings[i + 5]);
        }

        //to hit  + to hit total resultto hit total result
        CreateDoublePanel("To Hit Modifier", _dieGroupB.dieGroup.toHitModifier.ToString(), "To Hit Result", (_dieGroupB.toHitResult + _dieGroupB.dieGroup.toHitModifier).ToString());

        //crit math
        CreateSinglePanel("Crit Example", settings.GetComponent<Settings>().GetExampleText());

        //crit? + crit type
        if (_dieGroupB.crit)
        {
            CreateDoublePanel("Crit Type", Settings.critType.ToString(), "Critical?", "Critical Hit!");
        }
        else if (_dieGroupB.critFail)
        {
            CreateDoublePanel("Crit Type", Settings.critType.ToString(), "Critical?", "Critical Fail");
        }
        else
        {
            CreateDoublePanel("Crit Type", Settings.critType.ToString(), "Critical?", "Not a crit");
        }
    }
    public void InitDamagePanels(DieGroupBehaviour _dieGroupB)
    {
        //title panel
        CreateTitlePanel("Damage");

        //damage dice types
        CreateSinglePanel("Damage Dice", _dieGroupB.dieGroup.GetDamageDiceTypesString());

        //damage results
        List<string> damageResultStrings = new List<string>();
        foreach (GameObject _dieObj in _dieGroupB.dice)
        {
            Die _die = _dieObj.GetComponent<DieBehaviour>().die;
            if (_die.dieSubGroup == 2)
            {
                damageResultStrings.Add("Die Result (" + Die.DieTypeToString(_die.dieType) + ")");
                damageResultStrings.Add(_die.result.ToString());
            }
        }
        while (damageResultStrings.Count % 6 > 0)
        {
            damageResultStrings.Add("");
        }
        for (int i = 0; i < damageResultStrings.Count; i += 6)
        {
            CreateTriplePanel(damageResultStrings[i], damageResultStrings[i + 1], damageResultStrings[i + 2], damageResultStrings[i + 3], damageResultStrings[i + 4], damageResultStrings[i + 5]);
        }

        //damage modifier + damage total result
        CreateDoublePanel("Damage Modifier", _dieGroupB.dieGroup.damageModifier.ToString(), "Damage Result", (_dieGroupB.damageResult + _dieGroupB.dieGroup.damageModifier).ToString());

        //crit calculation
        if (_dieGroupB.crit)
        {
            CreateSinglePanel("Crit Calculation", CritHandler.GetCritCalculation(_dieGroupB));
        }
    }
    public void InitPercentilePanels(DieGroupBehaviour _dieGroupB)
    {
        //title panel
        CreateTitlePanel("Percentile");

        //die results
        List<string> toHitStrings = new List<string>();
        foreach (GameObject _dieObj in _dieGroupB.dice)
        {
            Die _die = _dieObj.GetComponent<DieBehaviour>().die;
            if (_die.dieSubGroup == 1 || _die.dieSubGroup == 2)
            {
                toHitStrings.Add("Die Result (" + Die.DieTypeToString(_die.dieType) + ")");
                toHitStrings.Add(_die.result.ToString());
            }
        }
        while (toHitStrings.Count % 6 > 0)
        {
            toHitStrings.Add("");
        }
        for (int i = 0; i < toHitStrings.Count; i += 6)
        {
            CreateTriplePanel(toHitStrings[i], toHitStrings[i + 1], toHitStrings[i + 2], toHitStrings[i + 3], toHitStrings[i + 4], toHitStrings[i + 5]);
        }
        //damage modifier + damage total result
        CreateDoublePanel("Modifier", _dieGroupB.dieGroup.damageModifier.ToString(), "Result", (_dieGroupB.damageResult + _dieGroupB.dieGroup.damageModifier).ToString());

    }
    public void InitSkillCheckPanels(DieGroupBehaviour _dieGroupB)
    {
        //title panel
        CreateTitlePanel("Skill Check");

        //die results
        List<string> toHitStrings = new List<string>();
        foreach (GameObject _dieObj in _dieGroupB.dice)
        {
            Die _die = _dieObj.GetComponent<DieBehaviour>().die;
            if (_die.dieSubGroup == 0 || _die.dieSubGroup == 1)
            {
                toHitStrings.Add("Die Result (" + Die.DieTypeToString(_die.dieType) + ")");
                toHitStrings.Add(_die.result.ToString());
            }
        }
        while (toHitStrings.Count % 6 > 0)
        {
            toHitStrings.Add("");
        }
        for (int i = 0; i < toHitStrings.Count; i += 6)
        {
            CreateTriplePanel(toHitStrings[i], toHitStrings[i + 1], toHitStrings[i + 2], toHitStrings[i + 3], toHitStrings[i + 4], toHitStrings[i + 5]);
        }
        //to hit  + to hit total resultto hit total result
        CreateDoublePanel("Modifier", _dieGroupB.dieGroup.toHitModifier.ToString(), "Result", (_dieGroupB.toHitResult + _dieGroupB.dieGroup.toHitModifier).ToString());

    }
    public void Init(DieGroupBehaviour _dieGroupB)
    {
        //set title text
        title.GetComponent<TMP_Text>().text = _dieGroupB.dieGroup.groupName + " Result Details";

        //reset panels
        foreach (GameObject _panel in detailPanels)
        {
            Destroy(_panel);
        }
        detailPanels.Clear();

        //create panels
        switch (_dieGroupB.dieGroup.dieGroupType)
        {
            case 0://standard
                InitDamagePanels(_dieGroupB);
                break;
            case 1://attack
                InitAttackPanels(_dieGroupB);
                InitDamagePanels(_dieGroupB);
                break;
            case 2://percentile
                InitPercentilePanels(_dieGroupB);
                break;
            case 3://skill check
                InitSkillCheckPanels(_dieGroupB);
                break;
        }
        scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1820, detailPanels.Count * 90 + 15);
        scrollContent.transform.localPosition = new Vector2(0, detailPanels.Count * -45);
    }

    public void CreateTitlePanel(string _detailTitle)
    {
        GameObject panel = Instantiate(detailPanelPrefabTitle, Vector3.zero, Quaternion.identity, scrollContent.transform);
        panel.GetComponent<ResultDetailPanel>().Init(_detailTitle);
        detailPanels.Add(panel);
    }
    public void CreateSinglePanel(string _detailTitle, string _detailInfo)
    {
        GameObject panel = Instantiate(detailPanelPrefabSingle, Vector3.zero, Quaternion.identity, scrollContent.transform);
        panel.GetComponent<ResultDetailPanel>().Init(_detailTitle, _detailInfo);
        detailPanels.Add(panel);
    }
    public void CreateDoublePanel(string _detailTitle0, string _detailInfo0, string _detailTitle1, string _detailInfo1)
    {
        GameObject panel = Instantiate(detailPanelPrefabDouble, Vector3.zero, Quaternion.identity, scrollContent.transform);
        panel.GetComponent<ResultDetailPanel>().Init(_detailTitle0, _detailInfo0, _detailTitle1, _detailInfo1);
        detailPanels.Add(panel);
    }
    public void CreateTriplePanel(string _detailTitle0, string _detailInfo0, string _detailTitle1, string _detailInfo1, string _detailTitle2, string _detailInfo2)
    {
        GameObject panel = Instantiate(detailPanelPrefabTriple, Vector3.zero, Quaternion.identity, scrollContent.transform);
        panel.GetComponent<ResultDetailPanel>().Init(_detailTitle0, _detailInfo0, _detailTitle1, _detailInfo1, _detailTitle2, _detailInfo2);
        detailPanels.Add(panel);
    }

    public void BackBtn()
    {
        resultsUI.GetComponent<Canvas>().enabled = true;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
