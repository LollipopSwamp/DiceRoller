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
    public GameObject uiManager;
    public GameObject resultsUi;

    //floor object for audio
    public GameObject floor;


    //public int resultsSaved = 0;
    public List<DieGroup> dieGroups = new List<DieGroup>();
    public List<int> groupIds = new List<int>();
    public int dieGroupsCount = 0;
    public List<GameObject> dieGroupObjects = new List<GameObject>();

    //cocked die vars
    private static DateTime start;
    private bool allResultsStored;
    public GameObject walls;

    //die scaling
    public int totalNumOfDice = 0;
    public static float diceScale = 1f;//0.5f;

    //statistics tests
    public int numOfD6 = 0;
    public int totalRoll = 0;
    public double avgRoll = 0;
    public int[] allRolls = new int[6];

    void Start()
    {
        dieGroups = SaveSession.Load();
    }
    public void RollDice()
    {
        start = DateTime.Now;
        allResultsStored = false;
        uiManager.GetComponent<UIManager>().HideAll();
        InstantiateDieGroups();
        Debug.Log("Rolling " + dieGroups.Count.ToString() + " DieGroup(s) with " + CountAllDice().ToString() + " total dice");
    }

    public void DestroyDieGroups()
    {
        foreach (GameObject g in dieGroupObjects)
        {
            Destroy(g);
        }
        dieGroupObjects.Clear();
    }

    public void DuplicateDieGroup(int _groupId)
    {
        foreach (DieGroup dieGroup in dieGroups)
        {
            if (dieGroup.groupId == _groupId)
            {
                //create die group
                DieGroup newDieGroup = DieGroup.DuplicateDieGroup(dieGroup);
                dieGroups.Add(newDieGroup);
                //create die group behaviour
                GameObject dieGroupB = Instantiate(dieGroupBehaviourPrefab, Vector3.zero, Quaternion.identity, transform);
                dieGroupB.GetComponent<DieGroupBehaviour>().name = dieGroup.groupName;
                dieGroupB.GetComponent<DieGroupBehaviour>().dieGroup = dieGroup;
                dieGroupObjects.Add(dieGroupB);
                Debug.Log("Duplicated DieGroup with groupId: " + _groupId.ToString() + " || New groupId: " + newDieGroup.groupId.ToString());
                break;
            }
        }
    }
    public void UpdateDieGroup(DieGroup _dieGroup)
    {
        for (int i = 0; i < dieGroups.Count; i++)
        {
            if (_dieGroup.groupId == dieGroups[i].groupId)
            {
                dieGroups[i] = _dieGroup;
                Debug.Log("Updated DieGroup with groupId: " + _dieGroup.groupId.ToString());
                break;
            }
        }
    }
    public void DeleteDieGroup(int _groupId)
    {
        foreach (DieGroup dieGroup in dieGroups)
        {
            if (dieGroup.groupId == _groupId)
            {
                dieGroups.Remove(dieGroup);
                Debug.Log("Deleted DieGroup with groupId: " + _groupId.ToString());
                break;
            }
        }
    }
    public void AddDieGroup(DieGroup _dieGroup)
    {
        dieGroups.Add(_dieGroup);
        Debug.Log("Added new DieGroup with groupId: " + _dieGroup.groupId.ToString());
    }

    void Update()
    {//
        dieGroupsCount = dieGroups.Count;
        groupIds.Clear();
        for (int i = 0; i < dieGroups.Count; i++)
        {
            groupIds.Add(dieGroups[i].groupId);
        }
        if (!allResultsStored && DateTime.Now > start.AddSeconds(3))
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
                        dieB.GetComponent<DieBehaviour>().SetTransparency(true);
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
            uiManager.GetComponent<UIManager>().ShowResultsUI();
            Debug.Log("All dice rolled with result(s). Displaying results.");
            DisplayDice();
            //ProcessD6Stats();
            //RollDice();
        }

    }

    void InstantiateDieGroups()
    {
        //get die scaling
        totalNumOfDice = CountAllDice();
        if (totalNumOfDice > 115)
        {
            diceScale = 0.5f;
            DieGroupBehaviour.diceScale = 0.5f;
            Debug.Log("Scaling dice to 0.5");
        }
        else if (totalNumOfDice > 55)
        {
            diceScale = 0.67f;
            DieGroupBehaviour.diceScale = 0.67f;
            Debug.Log("Scaling dice to 0.67");
        }
        else if (totalNumOfDice <= 55f)
        {
            diceScale = 1f;
            DieGroupBehaviour.diceScale = 1f;
        }

        //destroy diegroup behaviour objects
        DestroyDieGroups();

        //instantiate die group behaviours
        DieGroupBehaviour.ResetStartPosition();
        foreach (DieGroup dieGroup in dieGroups)
        {
            GameObject dieGroupB = Instantiate(dieGroupBehaviourPrefab, new Vector3(0,15,0), Quaternion.identity, transform);
            dieGroupB.GetComponent<DieGroupBehaviour>().name = dieGroup.groupName;
            dieGroupB.GetComponent<DieGroupBehaviour>().dieGroup = dieGroup;
            dieGroupB.transform.localScale = new Vector3(diceScale, diceScale, diceScale);
            dieGroupObjects.Add(dieGroupB);
        }
        foreach (GameObject dieGroupB in dieGroupObjects)
        {
            dieGroupB.GetComponent<DieGroupBehaviour>().InstantiateDice();
        }
    }
    void DisplayDice()
    {
        foreach(GameObject g in dieGroupObjects)
        {
            DieGroupBehaviour dieGroupB = g.GetComponent<DieGroupBehaviour>();
            foreach (GameObject dieObj in dieGroupB.dice)
            {
                DieBehaviour dieB = dieObj.GetComponent<DieBehaviour>();
                dieB.MoveToStartPosition();
                dieB.SetKinematic(true);
                dieB.SetTransparency(false);
            }
        }
    }
    public int CountAllDice()
    {
        int count = 0;
        foreach (DieGroup diegroup in dieGroups)
        {
            count += diegroup.toHitBonusDice.Count;
            count += diegroup.damageDice.Count;
            switch (diegroup.toHitType)
            {
                case 0:
                    break;
                case 1:
                    count++;
                    break;
                case 2:
                case 3:
                    count += 2;
                    break;
            }
        }
        return count;
    }

    public void PrintAllDieGroups()
    {
        foreach (DieGroup _dieGroup in dieGroups)
        {
            Debug.Log(_dieGroup.groupId);
            //_dieGroup.PrintDieGroup();
        }
        Debug.Log("=========");
    }
    public void ProcessD6Stats()
    {
        foreach(GameObject dieGroupBObj in dieGroupObjects)
        {
            DieGroupBehaviour dieGroupB = dieGroupBObj.GetComponent<DieGroupBehaviour>();
            foreach (GameObject dieObj in dieGroupB.dice)
            {
                Die die = dieObj.GetComponent<DieBehaviour>().die;
                if (die.dieType == 1)
                {
                    numOfD6++;
                    totalRoll += die.result;
                    avgRoll = (double)totalRoll / (double)numOfD6;
                    allRolls[die.result - 1]++;
                }
            }
        }
        Debug.Log("D6 stats updated || numOfD6: " + numOfD6.ToString() + " || avgRoll: " + avgRoll.ToString());
        Debug.Log("All Rolls || 1: " + allRolls[0].ToString() + "|| 2: " + allRolls[1].ToString() + "|| 3: " + allRolls[2].ToString() + "|| 4: " + allRolls[3].ToString() + "|| 5: " + allRolls[4].ToString() + "|| 6: " + allRolls[5].ToString());
    }
}
