using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class DiceManager : MonoBehaviour
{
    //dice prefabs
    private GameObject selectedPrefab;
    public GameObject d20Prefab;
    public GameObject d6Prefab;

    //ui object
    public GameObject ui;


    //public int resultsSaved = 0;
    public List<DieGroup> dieGroups = new List<DieGroup>();
    public List<GameObject> diceObjects = new List<GameObject>();

    //cocked die vars
    private static DateTime start;
    private bool allResultsStored;
    public GameObject walls;

    public void RollDice()
    {
        CreateDummyDice("To Hit0", DieGroup.ResultsType.Advantage, 1, 5, 2, Die.DieType.d20);
        CreateDummyDice("Damage0", DieGroup.ResultsType.Sum, 2, 5, 3, Die.DieType.d6);
        CreateDummyDice("To Hit1", DieGroup.ResultsType.Advantage, 3, 5, 2, Die.DieType.d20);
        CreateDummyDice("Damage1", DieGroup.ResultsType.Sum, 4, 5, 5, Die.DieType.d6);
        CreateDummyDice("Damage2", DieGroup.ResultsType.Sum, 5, 5, 5, Die.DieType.d6);
        //string _groupName, DieGroup.ResultsType _resultsType, int colorIndex, int _modifier, int numOfDice, Die.DieType _dieType)

        //CreateDummyDice0();
        //CreateDummyDice1();
        InstantiateDice();
        PrintAllDice();
        start = DateTime.Now;
        allResultsStored = false;
    }
    void Update()
    {//!allResultsStored && 
        if (DateTime.Now > start.AddSeconds(5))
        {
            start = DateTime.Now;
            foreach(GameObject d in diceObjects)
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
    void CheckFinalResults()
    {
        allResultsStored = true;
        //check if dice are still rolling
        foreach (DieGroup g in dieGroups)
        {
            if (g.groupResult == -1)
            {
                allResultsStored = false;
                break;
            }
        }

        //if dice done rolling, print results and show results ui
        if (allResultsStored)
        {
            //print results
            foreach (DieGroup g in dieGroups)
            {
                Debug.Log(string.Concat("Group: ", g.groupId, " || Roll Result: ", g.groupResult, " || Modifer: ", g.modifier, " || Total Result: ", g.groupResult + g.modifier));
            }
            //send results to UI
            //ui.GetComponent<UI>().ToggleMainUI();
            ui.GetComponent<UI>().ToggleResultsUI();
            ui.GetComponent<UI>().ShowResults(dieGroups);
        }

    }
    public void UpdateDie(Die die)
    {
        for (int i = 0; i < dieGroups.Count; ++i)
        {
            if (dieGroups[i].groupId == die.groupId)
            {
                for (int j = 0; j < dieGroups[i].dice.Count; ++j)
                {
                    if (dieGroups[i].dice[j].dieId == die.dieId)
                    {
                        dieGroups[i].dice[j] = die;
                    }
                }
            }
            dieGroups[i].CheckResults();
        }
        CheckFinalResults();
        return;
    }
    void InstantiateDice()
    {
        //create die objects
        Vector3 position = new Vector3(-9,15,6);
        foreach (DieGroup g in dieGroups)
        {
            string dieName = "";
            foreach (Die d in g.dice)
            {
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
                GameObject dieObj = Instantiate(selectedPrefab, position, Quaternion.identity);
                dieObj.name = dieName;
                dieObj.GetComponent<MeshRenderer>().material.color = g.dieColor[g.colorIndex];
                diceObjects.Add(dieObj);


                Roll roll = dieObj.GetComponent<Roll>();
                roll.SetVars(d, gameObject, position);
                position.x += 3;
            }
            position = new Vector3(-9,15,position.z - 3);
        }

        //change dice colors
    }

    void CreateDummyDice(string _groupName, DieGroup.ResultsType _resultsType, int colorIndex, int _modifier, int numOfDice, Die.DieType _dieType)
    {
        //create DieGroup objects
        DieGroup dieGroup = new DieGroup(
            _groupName,
            _resultsType,
            colorIndex,
            _modifier
        );
        //create die lists
        List<Die> dieList = new List<Die>();
        for (int i = 0; i < numOfDice; i++)
        {
            Die die = new Die(_dieType, dieGroup.groupId, 1);
            dieList.Add(die);
        }
        //add dice to DieGroups
        dieGroup.AddDice(dieList);

        //add DieGroups object to dieGroup list
        dieGroups.Add(dieGroup);

    }
    void CreateDummyDice0()
    {

        //create DieGroup objects
        DieGroup dieGroup0 = new DieGroup(
            "To hit",
            DieGroup.ResultsType.Advantage,
            1,
            5
        );
        DieGroup dieGroup1 = new DieGroup(
            "Damage",
            DieGroup.ResultsType.Sum,
            2,
            3
        );
        DieGroup dieGroup2 = new DieGroup(
            "Damage",
            DieGroup.ResultsType.Sum,
            3,
            3
        );
        //create die lists
        //d20
        List<Die> dieList0 = new List<Die>();
        for (int i = 0; i < 2; i++)
        {
            Die die = new Die(Die.DieType.d20, dieGroup0.groupId, 1);
            dieList0.Add(die);
        }
        //d6
        List<Die> dieList1 = new List<Die>();
        for (int i = 0; i < 3; i++)
        {
            Die die = new Die(Die.DieType.d6, dieGroup1.groupId, 1);
            dieList1.Add(die);
        }
        //d6
        List<Die> dieList2 = new List<Die>();
        for (int i = 0; i < 3; i++)
        {
            Die die = new Die(Die.DieType.d6, dieGroup2.groupId, 1);
            dieList2.Add(die);
        }
        //add dice to DieGroups
        dieGroup0.AddDice(dieList0);
        dieGroup1.AddDice(dieList1);
        dieGroup2.AddDice(dieList2);


        //add DieGroups object to dieGroup list
        dieGroups.Add(dieGroup0);
        dieGroups.Add(dieGroup1);
        dieGroups.Add(dieGroup2);
    }

    void CreateDummyDice1()
    {

        //create DieGroup objects
        DieGroup dieGroup0 = new DieGroup(
            "To hit",
            DieGroup.ResultsType.Advantage,
            1,
            2
        );
        //create die lists
        List<Die> dieList0 = new List<Die>();
        Die die = new Die(Die.DieType.d6, dieGroup0.groupId, 1);
        dieList0.Add(die);

        //add dice to DieGroups
        dieGroup0.AddDice(dieList0);


        //add DieGroups object to dieGroup list
        dieGroups.Add(dieGroup0);
    }
    public void PrintAllDice()
    {
        foreach (DieGroup g in dieGroups)
        {
            Debug.Log(string.Concat("GroupID: ", g.groupId, " || Results Type: ", g.resultsType));
            foreach (Die d in g.dice)
            {
                d.PrintDie();
            }
        }
    }

}
