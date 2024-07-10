using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //dice prefabs
    public GameObject selectedPrefab;
    public GameObject d20Prefab;
    public GameObject d6Prefab;

    //public int resultsSaved = 0;
    public List<DieGroup> dieGroups = new List<DieGroup>();


    void Start()
    {
        CreateDummyDice();
        InstantiateDice();
        PrintAllDice();
    }

    void CheckFinalResults()
    {
        //check if dice are still rolling
        bool allResultsStored = true;
        foreach (DieGroup g in dieGroups)
        {
            if (g.groupResult == -1)
            {
                allResultsStored = false;
                break;
            }
        }

        //if dice done rolling, print results
        if(allResultsStored)
        {
            foreach (DieGroup g in dieGroups)
            {
                Debug.Log(string.Concat("GroupID: ", g.groupId, " || Group Result: ", g.groupResult));
            }
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
            MeshRenderer mr;
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
                mr = dieObj.GetComponent<MeshRenderer>();
                mr.material.color = new Color(0, 204, 102, 255);


                Roll roll = dieObj.GetComponent<Roll>();
                roll.SetDie(d);
                roll.SetDiceManager(gameObject);
                position.x += 3;
            }
            position = new Vector3(-9,15,position.z - 3);
        }

        //change dice colors
    }

    void CreateDummyDice()
    {

        //create DieGroup objects
        DieGroup dieGroup0 = new DieGroup(
            DieGroup.ResultsType.Advantage
        );
        DieGroup dieGroup1 = new DieGroup(
            DieGroup.ResultsType.Sum
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
        //add dice to DieGroups
        dieGroup0.AddDice(dieList0);
        dieGroup1.AddDice(dieList1);


        //add DieGroups object to dieGroup list
        dieGroups.Add(dieGroup0);
        dieGroups.Add(dieGroup1);
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
