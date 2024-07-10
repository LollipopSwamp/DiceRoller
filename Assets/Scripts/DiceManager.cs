using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //dice prefabs
    public GameObject d20Prefab;
    public GameObject d6Prefab;

    //public int resultsSaved = 0;
    public Dictionary<int, DieGroup> dieGroups = new Dictionary<int,DieGroup>();


    void Start()
    {
        CreateDummyDice();
        InstantiateDice();
        PrintAllDice();
    }

    void Update()
    {
        //check if dice are still rolling
        bool allResultsStored = true;
        foreach (var g in dieGroups)
        {
            if (g.Value.groupResult == -1)
            {
                allResultsStored = false;
                break;
            }
        }

        //if dice done rolling, print results
        if(allResultsStored)
        {
            foreach (var g in dieGroups)
            {
                Debug.Log(string.Concat("GroupID: ", g.Value.groupId, " || Group Result: ", g.Value.groupResult));
            }
        }
    }
    public static void UpdateDie(Die die)
    {
        return;
    }
    void InstantiateDice()
    {
        Vector3 position = new Vector3(-9,15,6);
        foreach (var g in dieGroups)
        {
            foreach (var d in g.Value.dice)
            {
                GameObject diePrefab;
                switch (d.Value.dieType)
                {
                    case Die.DieType.d6:
                        diePrefab = d6Prefab;
                        break;
                    case Die.DieType.d20:
                        diePrefab = d20Prefab;
                        break;

                }
                GameObject dieObj = Instantiate(d6Prefab, position, Quaternion.identity);
                Roll roll = dieObj.GetComponent<Roll>();
                roll.SetDie(d.Value);
                position.x += 3;
            }
            position = new Vector3(-9,15,position.z - 3);
        }
    }

    void CreateDummyDice()
    {
        //create DieGroup object
        DieGroup dieGroup0 = new DieGroup(
            DieGroup.ResultsType.Sum
        );

        //create die dictionary
        Dictionary<int, Die> dieGroupDict0 = new Dictionary<int, Die>();
        for (int i = 0; i < 3; i++)
        {
            die = new Die(Die.DieType.d6, dieGroupDict0.groupId, 1);
            dieGroupDict0.Add(die.dieId,die);
        }


        //add DieGroup object to dieGroup dictionary
        dieGroups.Add(dieGroup0.groupId,dieGroup0);
    }

    public void PrintAllDice()
    {
        foreach (DieGroup g in  dieGroups)
        {
            Debug.Log(string.Concat("GroupID: ", g.groupId, " || Results Type: ", g.resultsType));
            foreach (Die d in g.dice)
            {
                Debug.Log(string.Concat("DieType: ", d.dieType, " || Result: ", d.result));
            }
        }
    }

}
