using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DateTime = System.DateTime;

public class DiceManager : MonoBehaviour
{
    //diceGroup prefabs
    public List<GameObject> dicePrefabs = new List<GameObject>();
    public GameObject dieGroupBehaviourPrefab;

    //die instantiate position
    private static Vector3 dieStartPosition = new Vector3(-9, 15, 6);

    //ui objects
    public GameObject mainUi;
    public GameObject resultsUi;


    //public int resultsSaved = 0;
    public List<DieGroup> dieGroups = new List<DieGroup>();
    public List<GameObject> dieGroupObjects = new List<GameObject>();

    //cocked die vars
    private static DateTime start;
    private bool allResultsStored;
    public GameObject walls;


    void Start()
    {
    }
    public void RollDice()
    {
        CreateTestDieGroups();
        PrintAllDice();
        start = DateTime.Now;
        allResultsStored = false;
    }
    void Update()
    {//!allResultsStored && 
        if (DateTime.Now > start.AddSeconds(3))
        {
            start = DateTime.Now;
            foreach(GameObject _dieGroupObject in dieGroupObjects)
            {
                DieGroupBehaviour _dieGroupB = _dieGroupObject.GetComponent<DieGroupBehaviour>();
                foreach(GameObject dieObj in _dieGroupB.dice)
                {
                    DieBehaviour dieB = dieObj.GetComponent<DieBehaviour>();
                    if (dieB.GetComponent<DieBehaviour>().die.result != -1)
                    {
                        dieB.GetComponent<DieBehaviour>().SetCollision(false);
                        Color currColor = dieB.GetComponent<MeshRenderer>().material.color;
                        Color newColor = new Color(currColor.r, currColor.g, currColor.b, 0.25f);
                        dieB.GetComponent<MeshRenderer>().material.color = newColor;
                        //walls.GetComponent<Walls>().SetCollision(false);
                    }
                    else
                    {
                        dieB.GetComponent<DieBehaviour>().ResetDie();
                    }
                }
            }
        }
    }
    public void CheckFinalResults()
    {
        allResultsStored = true;
        //check if dice0 are still rolling
        foreach (GameObject g in dieGroupObjects)
        {
            if (g.GetComponent<DieGroupBehaviour>().diceStillRolling)
            {
                allResultsStored = false;
                return;
            }
        }

        //if dice0 done rolling, print results and show results ui
        if (allResultsStored)
        {
            //update DieGroups list
            dieGroups.Clear();
            foreach (GameObject dg in dieGroupObjects)
            {
                //Debug.Log(string.Concat("Group: ", g.GetComponent<DieGroup>().groupId, " || DieBehaviour Result: ", g.GetComponent<DieGroup>().toHitResult, " || Modifer: ", g.GetComponent<DieGroup>().toHitModifier, " || Total Result: ", g.GetComponent<DieGroup>().toHitResult + g.GetComponent<DieGroup>().toHitModifier));
            }
            //send results to UI
            //ui.GetComponent<UI>().ToggleMainUI();
            resultsUi.GetComponent<ResultsUI>().ToggleVisibility();
            resultsUi.GetComponent<ResultsUI>().CreateResultsPanels(dieGroupObjects);
            //ui.GetComponent<UI>().ShowResults(dieGroups);
        }

    }

    void CreateTestDieGroups()
    {
        //create attack die group (advantage) with 3d6 for damage
        List<Die.DieType> damageDieTypes0 = new List<Die.DieType>()
        {
            Die.DieType.d6,
            Die.DieType.d6,
            Die.DieType.d6
        };
        DieGroup dieGroup0 = new DieGroup("Claw Attack", DieGroup.ToHitType.Advantage, 5, damageDieTypes0, 3, 2);
        GameObject dieGroupB0 = Instantiate(dieGroupBehaviourPrefab, Vector3.zero, Quaternion.identity, transform);
        dieGroupB0.name = dieGroup0.groupName;
        dieGroupObjects.Add(dieGroupB0);
        dieGroupB0.GetComponent<DieGroupBehaviour>().InstantiateDice(dieGroup0);

        //create attack die group with 1d6 bonus to hit and 3d6 for damage
        List<Die.DieType> toHitBonusDice1 = new List<Die.DieType>()
        {
            Die.DieType.d6
        };
        List<Die.DieType> damageDieTypes1 = new List<Die.DieType>()
        {
            Die.DieType.d6,
            Die.DieType.d6,
            Die.DieType.d6
        };
        DieGroup dieGroup1 = new DieGroup("Bite Attack", toHitBonusDice1, DieGroup.ToHitType.Disadvantage, 7, damageDieTypes1, 5, 8);
        GameObject dieGroupB1 = Instantiate(dieGroupBehaviourPrefab, Vector3.zero, Quaternion.identity, transform);
        dieGroupB1.name = dieGroup1.groupName;
        dieGroupObjects.Add(dieGroupB1);
        dieGroupB1.GetComponent<DieGroupBehaviour>().InstantiateDice(dieGroup1);

        //create non-attack die group with 3d6 for damage
        List<Die.DieType> damageDieTypes2 = new List<Die.DieType>()
        {
            Die.DieType.d6,
            Die.DieType.d6,
            Die.DieType.d6
        };
        DieGroup dieGroup2 = new DieGroup("Just Damage", damageDieTypes2, 5, 1);
        GameObject dieGroupB2 = Instantiate(dieGroupBehaviourPrefab, Vector3.zero, Quaternion.identity, transform);
        dieGroupB2.name = dieGroup2.groupName;
        dieGroupObjects.Add(dieGroupB2);
        dieGroupB2.GetComponent<DieGroupBehaviour>().InstantiateDice(dieGroup2);
    }

    public void CreateDieGroup(DieGroup _dieGroup)
    {

    }

    void InstantiateDie(GameObject _parent, Die.DieType _dieType)
    {

    }
    
    public void PrintAllDice()
    {
        foreach (GameObject g in dieGroupObjects)
        {
        }
    }

}
