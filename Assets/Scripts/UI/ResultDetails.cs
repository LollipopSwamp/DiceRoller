using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDetails : MonoBehaviour
{
    public List<GameObject> detailPanels = new List<GameObject>();
    public GameObject detailPanelPrefabSingle;
    public GameObject detailPanelPrefabDouble;

    public GameObject title;
    public GameObject scrollContent;

    public GameObject resultsUI;
    public GameObject settings;

    public void Init(DieGroupBehaviour _dieGroupB)
    {
        //reset panels
        foreach (GameObject _panel in detailPanels)
        {
            Destroy(_panel);
        }
        detailPanels.Clear();

        //create panels
        if (_dieGroupB.dieGroup.toHitType != DieGroup.ToHitType.None)
        {
            //to hit type
            CreatePanel("To Hit Type", _dieGroupB.dieGroup.toHitType.ToString());

            //to hit die types
            CreatePanel("To Hit Dice", _dieGroupB.dieGroup.GetToHitDiceTypesString());

            //to hit die results
            foreach(GameObject _dieObj in _dieGroupB.dice)
            {
                Die _die = _dieObj.GetComponent<DieBehaviour>().die;
                if ( _die.dieSubGroup == Die.DieSubGroup.ToHit || _die.dieSubGroup == Die.DieSubGroup.ToHitBonus)
                {
                    CreatePanel("Die Result (" + Die.DieTypeToString(_die.dieType) + ")", _die.result.ToString());
                }
            }

            //to hit modifier
            CreatePanel("To Hit Modifier", _dieGroupB.dieGroup.toHitModifier.ToString());

            //to hit total result
            CreatePanel("To Hit Result", (_dieGroupB.toHitResult + _dieGroupB.dieGroup.toHitModifier).ToString());

            //crit type
            CreatePanel("Crit Type", Settings.critType.ToString());

            //crit math
            CreatePanel("Crit Example", settings.GetComponent<Settings>().GetExampleText());

            //crit?
            if (_dieGroupB.crit)
            {
                CreatePanel("Critical?", "Critical Hit!");
            }
            else if (_dieGroupB.critFail)
            {
                CreatePanel("Critical?", "Critical Fail");
            }
            else
            {
                CreatePanel("Critical?", "Not a crit");
            }
        }

        //damagePanels
        //damage dice types
        CreatePanel("Damage Dice", _dieGroupB.dieGroup.GetDamageDiceTypesString());

        //damage results
        foreach (GameObject _dieObj in _dieGroupB.dice)
        {
            Die _die = _dieObj.GetComponent<DieBehaviour>().die;
            if (_die.dieSubGroup == Die.DieSubGroup.Damage)
            {
                CreatePanel("Die Result (" + Die.DieTypeToString(_die.dieType) + ")", _die.result.ToString());
            }
        }

        //damage modifier
        CreatePanel("Damage Modifier", _dieGroupB.dieGroup.damageModifier.ToString());

        //damage total result
        CreatePanel("Damage Result", (_dieGroupB.damageResult + _dieGroupB.dieGroup.damageModifier).ToString());

        //crit calculation
        if (_dieGroupB.crit)
        {
            CreatePanel("Crit Calculation", CritHandler.GetCritCalculation(_dieGroupB));
        }


            //set scrollContent height
            //scrollContent.GetComponent<RectTransform>().Axis.Vertical = 500;
            scrollContent.GetComponent<RectTransform>().sizeDelta = new Vector2(1820, detailPanels.Count * 90 + 15);
        scrollContent.transform.localPosition = new Vector2(0, Presets.dieGroups.Count * -45);
    }

    public void CreatePanel(string _detailTitle, string _detailInfo)
    {
        GameObject panel = Instantiate(detailPanelPrefab, Vector3.zero, Quaternion.identity, scrollContent.transform);
        panel.GetComponent<ResultDetailPanel>().Init(_detailTitle, _detailInfo);
        detailPanels.Add(panel);
    }
    public void CreatePanel(string _detailTitle0, string _detailInfo0, string _detailTitle1, string _detailInfo1)
    {
        GameObject panel = Instantiate(detailPanelPrefabDouble, Vector3.zero, Quaternion.identity, scrollContent.transform);
        panel.GetComponent<ResultDetailPanel>().Init(_detailTitle0, _detailInfo0, _detailTitle1, _detailInfo1);
        detailPanels.Add(panel);
    }

    public void BackBtn()
    {
        resultsUI.GetComponent<Canvas>().enabled = true;
        gameObject.GetComponent<Canvas>().enabled = false;
    }
}
