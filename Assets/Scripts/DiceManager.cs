using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class DiceManager : MonoBehaviour
{
    //dice0 prefabs
    private GameObject selectedPrefab;
    public GameObject d20Prefab;
    public GameObject d6Prefab;
    public GameObject dieGroupPrefab;

    //die instantiate position
    private static Vector3 dieStartPosition = new Vector3(-9, 15, 6);

    //ui objects
    public GameObject mainUi;
    public GameObject resultsUi;


    //public int resultsSaved = 0;
    public List<GameObject> dieGroups = new List<GameObject>();
    public List<GameObject> allDiceObjects = new List<GameObject>();

    //cocked die vars
    private static DateTime start;
    private bool allResultsStored;
    public GameObject walls;


    void Start()
    {
        //RollDice();
    }
    public void RollDice()
    {
        CreateDieGroup("Bite Attack",DieGroup.GroupType.Attack, DieGroup.ResultsType.Advantage,5,2);
        CreateDieGroup("Claws Attack", DieGroup.GroupType.Attack, DieGroup.ResultsType.Advantage, 5, 5);
        Debug.Log(dieGroups.Count);
        InstantiateDieGroups();
        PrintAllDice();
        start = DateTime.Now;
        allResultsStored = false;
    }
    void Update()
    {//!allResultsStored && 
        if (DateTime.Now > start.AddSeconds(5))
        {
            start = DateTime.Now;
            foreach(GameObject d in allDiceObjects)
            {
                if (d.GetComponent<Roll>().die.result != -1)
                {
                    d.GetComponent<Roll>().SetCollision(false);
                    Color currColor = d.GetComponent<MeshRenderer>().material.color;
                    Color newColor = new Color(currColor.r, currColor.g, currColor.b, 0.25f);
                    d.GetComponent<MeshRenderer>().material.color = newColor;
                    //walls.GetComponent<Walls>().SetCollision(false);
                }
                else
                {
                    d.GetComponent<Roll>().ResetDie();
                }
            }
        }
    }
    public void CheckFinalResults()
    {
        allResultsStored = true;
        //check if dice0 are still rolling
        foreach (GameObject g in dieGroups)
        {
            if (g.GetComponent<DieGroup>().diceStillRolling)
            {
                allResultsStored = false;
                return;
            }
        }

        //if dice0 done rolling, print results and show results ui
        if (allResultsStored)
        {
            //print results
            foreach (GameObject g in dieGroups)
            {
                Debug.Log(string.Concat("Group: ", g.GetComponent<DieGroup>().groupId, " || Roll Result: ", g.GetComponent<DieGroup>().toHitResult, " || Modifer: ", g.GetComponent<DieGroup>().toHitModifier, " || Total Result: ", g.GetComponent<DieGroup>().toHitResult + g.GetComponent<DieGroup>().toHitModifier));
            }
            //send results to UI
            //ui.GetComponent<UI>().ToggleMainUI();
            resultsUi.GetComponent<ResultsUI>().ToggleVisibility();
            resultsUi.GetComponent<ResultsUI>().CreateResultsPanels(dieGroups);
            //ui.GetComponent<UI>().ShowResults(dieGroups);
        }

    }

    void CreateDieGroup(string _groupName, DieGroup.GroupType _groupType, DieGroup.ResultsType _resultsType, int _modifier,  int _colorIndex)
    {
        //create dieGroup
        GameObject dieGroup = Instantiate(dieGroupPrefab, Vector3.zero, Quaternion.identity);
        dieGroup.transform.SetParent(gameObject.transform);
        dieGroup.name = _groupName;

        //add dice
        //test damage dice, 3d6
        List<Die> dieList = new List<Die>();
        for (int i = 0; i < 3; i++)
        {
            Die die = new Die(Die.DieType.d6, dieGroup.GetComponent<DieGroup>().groupId, 1);
            dieList.Add(die);
        }
        //add to hit bonus die
        Die bonusDie = new Die(Die.DieType.d6, dieGroup.GetComponent<DieGroup>().groupId, 1);
        List<Die> bonusDieList = new List<Die>();
        bonusDieList.Add(bonusDie);
        //add dice to dieGroup
        dieGroup.GetComponent<DieGroup>().AddToHitDice(DieGroup.ResultsType.Advantage, 5);
        dieGroup.GetComponent<DieGroup>().AddToHitBonusDice(bonusDieList);
        dieGroup.GetComponent<DieGroup>().AddDamageDice(dieList, 5);
        dieGroups.Add(dieGroup);
        //dieGroup.GetComponent<DieGroup>().SetVariables();
        dieGroup.GetComponent<DieGroup>().colorIndex = 2;
        dieGroup.GetComponent<DieGroup>().SetVariables(_groupName, _groupType, _resultsType, _modifier, _colorIndex);
        

    }
    void InstantiateDieGroups()
    {

        foreach (GameObject dieGroup in dieGroups)
        {
            dieStartPosition.x += dieGroup.GetComponent<DieGroup>().groupId * 3;
            InstantiateDice(dieGroup.GetComponent<DieGroup>().toHitDice, dieGroup);
            InstantiateDice(dieGroup.GetComponent<DieGroup>().toHitBonusDice, dieGroup);
            InstantiateDice(dieGroup.GetComponent<DieGroup>().damageDice, dieGroup);
            dieStartPosition.z = 6;
        }
    }

    void InstantiateDice(List<Die> dice, GameObject parent)
    {
        foreach (Die d in dice)
        {
            string dieName = "";
            switch (d.dieType)
            {
                case Die.DieType.d6:
                    selectedPrefab = d6Prefab;
                    dieName = "d6 (ID: " + d.dieId.ToString() + ")";
                    break;
                case Die.DieType.d20:
                    selectedPrefab = d20Prefab;
                    dieName = "d20 (ID: " + d.dieId.ToString() + ")";
                    break;

            }
            //create die gameobject
            GameObject dieObj = Instantiate(selectedPrefab, dieStartPosition, Quaternion.identity);
            dieObj.name = dieName;
            dieObj.GetComponent<MeshRenderer>().material.color = parent.GetComponent<DieGroup>().GetColor();
            dieObj.transform.SetParent(parent.transform);
            allDiceObjects.Add(dieObj);

            //set roll variables
            Roll roll = dieObj.GetComponent<Roll>();
            roll.SetVars(d, gameObject, dieStartPosition);
            IterateDieStartPosition();
        }
    }
    public void IterateDieStartPosition()
    { //initiate is (-9, 15, 6);
        dieStartPosition.x += 3;
        if ( dieStartPosition.x > 9)
        {
            dieStartPosition.x = -9;
            dieStartPosition.z -= 3;
        }
        Debug.Log(dieStartPosition);
    }
    
    public void PrintAllDice()
    {
        foreach (GameObject g in dieGroups)
        {
            g.GetComponent<DieGroup>().PrintDieGroup();
            //to hit
            if (g.GetComponent<DieGroup>().toHitDice.Count != 0)
            {
                Debug.Log("== To Hit Dice ==");
            }
            foreach (Die d in g.GetComponent<DieGroup>().toHitDice)
            {
                d.PrintDie();
            }
            //to hit bonus
            if (g.GetComponent<DieGroup>().toHitDice.Count != 0)
            {
                Debug.Log("== To Hit Bonus Dice ==");
            }
            foreach (Die d in g.GetComponent<DieGroup>().toHitBonusDice)
            {
                d.PrintDie();
            }
            //damage
            if (g.GetComponent<DieGroup>().toHitDice.Count != 0)
            {
                Debug.Log("== Damage Dice ==");
            }
            foreach (Die d in g.GetComponent<DieGroup>().damageDice)
            {
                d.PrintDie();
            }
        }
    }

}
