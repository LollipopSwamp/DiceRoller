using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //dice prefabs
    public GameObject d20Prefab;
    public GameObject d6Prefab;

    //public int resultsSaved = 0;
    private List<DieGroup> dieGroups = new List<DieGroup>();



    void Start()
    {
        CreateDummyDice();
        InstantiateDice();
        //PrintAllDice();
    }
    void InstantiateDice()
    {
        Vector3 position = new Vector3(-9,15,6);
        foreach (DieGroup g in dieGroups)
        {
            foreach (Die d in g.dice)
            {
                switch (d.dieType)
                {
                    case Die.DieType.d6:
                        Instantiate(d6Prefab, position, Quaternion.identity);
                        break;
                    case Die.DieType.d20:
                        Instantiate(d20Prefab, position, Quaternion.identity);
                        break;
                }
                position.x += 3;
            }
            position = new Vector3(-9,15,position.z - 3);
        }
    }
    void CreateDummyDice()
    {
        List<Die> dieGroupList0 = new List<Die>()
        {
            new Die(Die.DieType.d20, 1),
            new Die(Die.DieType.d20, 1)
        };
        DieGroup dieGroup0 = new DieGroup(
            0,
            dieGroupList0,
            DieGroup.ResultsType.Advantage
        );
        //dieGroups.Add( dieGroup0 );

        List<Die> dieGroupList1 = new List<Die>()
        {
            new Die(Die.DieType.d6, 1),
            new Die(Die.DieType.d6, 1),
            new Die(Die.DieType.d6, 1)
        };
        DieGroup dieGroup1 = new DieGroup(
            0,
            dieGroupList1,
            DieGroup.ResultsType.Sum
        );
        dieGroups.Add(dieGroup1);
    }

    public void PrintAllDice()
    {
        foreach (DieGroup g in  dieGroups)
        {
            Debug.Log(string.Concat("GroupID: ", g.groupId, " || Results Type: ", g.resultsType));
            foreach (Die d in g.dice)
            {
                Debug.Log(d.dieType);
            }
        }
    }

}
