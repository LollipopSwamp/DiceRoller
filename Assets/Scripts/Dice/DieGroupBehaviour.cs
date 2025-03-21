using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using static DieGroupBehaviour;

public class DieGroupBehaviour : MonoBehaviour
{
    public List<GameObject> diePrefabs;

    public DieGroup dieGroup;

    public List<GameObject> dice = new List<GameObject>(); //damage if attack

    public int toHitResult;
    public int skippedToHitResult;
    public bool crit = false;
    public bool critFail = false;
    public int damageResult;

    public bool diceStillRolling = true;

    public static float diceScale = 1f;


    //instantiation position
    public static Vector3 nextStartPosition = new Vector3(-15, 15, 6);

    //add dice
    public void InstantiateDice()
    {
        //to hit dice if standard, 1x d20
        if (dieGroup.toHitType == 0)
        {
            //create die gameobject
            GameObject dieObj = Instantiate(diePrefabs[6], nextStartPosition, Quaternion.identity, transform);
            dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(true);
            dice.Add(dieObj);

            //set DieBehaviour variables
            Die d20 = new Die(6, dieGroup.groupId, 0);
            DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
            dieBehaviour.SetVars(d20, gameObject, nextStartPosition, diceScale);

            //set die obj name
            dieObj.name = "d20 (ID: " + dieBehaviour.die.dieId.ToString() + ")";
            IterateStartPosition();
        }
        //to hit dice if advantage or disadvantage, 2x d20
        else if (dieGroup.toHitType == 1 || dieGroup.toHitType == 2)
        {
            for (int i = 0; i < 2; i++)
            {
                //create die gameobject
                GameObject dieObj = Instantiate(diePrefabs[6], nextStartPosition, Quaternion.identity, transform);
                dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(true);
                dice.Add(dieObj);

                //set DieBehaviour variables
                Die d20 = new Die(6, dieGroup.groupId,0);
                DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
                dieBehaviour.SetVars(d20, gameObject, nextStartPosition, diceScale);

                //set die obj name
                dieObj.name = "d20 (ID: " + dieBehaviour.die.dieId.ToString() + ")";
                IterateStartPosition();
            }
        }

        //create bonus to hit dice, if any
        foreach (int dieType in dieGroup.toHitBonusDice)
        {
            //create die gameobject
            GameObject dieObj = Instantiate(diePrefabs[dieType], nextStartPosition, Quaternion.identity, transform);
            dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(true);
            dice.Add(dieObj);

            //set DieBehaviour variables
            Die die = new Die(dieType, dieGroup.groupId, 1);
            DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
            dieBehaviour.SetVars(die, gameObject, nextStartPosition, diceScale);

            //set die obj name
            dieObj.name = die.DieTypeString() + " (ID: " + dieBehaviour.die.dieId.ToString() + ")";
            IterateStartPosition();
        }

        //create damage/standard dice
        foreach (int dieType in dieGroup.damageDice)
        {
            //create die gameobject
            GameObject dieObj = Instantiate(diePrefabs[dieType], nextStartPosition, Quaternion.identity, transform);
            dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(false);
            dice.Add(dieObj);

            //set DieBehaviour variables
            Die die = new Die(dieType, dieGroup.groupId, 2);
            DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
            dieBehaviour.SetVars(die, gameObject, nextStartPosition, diceScale);

            //set die obj name
            dieObj.name = die.DieTypeString() + " (ID: " + dieBehaviour.die.dieId.ToString() + ")";

            //dieBehaviour.SetKinematic(true);
            IterateStartPosition();

        }
    }

    public void UpdateDie(Die _die)
    {
        //check all dice
        for (int i = 0; i < dice.Count; ++i)
        {
            Die storedDie = dice[i].GetComponent<DieBehaviour>().die;
            if (storedDie.dieId == _die.dieId)
            {
                storedDie = _die;
            }
        }

        CheckResults();
        return;
    }

    public bool DiceStillRolling()
    {
        for (int i = 0; i < dice.Count; ++i)
        {
            Die storedDie = dice[i].GetComponent<DieBehaviour>().die;
            if (storedDie.result == -1)
            {
                diceStillRolling = true;
                return true;
            }
        }
        diceStillRolling = false;
        return false;
    }

    public void CheckResults()
    {
        //if dice done rolling, process results
        if (!DiceStillRolling())
        {
            foreach (GameObject dObj in dice)
            {
                Die d = dObj.GetComponent<DieBehaviour>().die;
                switch (d.dieSubGroup)
                {
                    case 0://to hit
                        switch (dieGroup.toHitType)
                        {
                            case 0://standard
                                toHitResult = d.result;
                                break;
                            case 1://advantage
                                if (toHitResult == -1)
                                {
                                    toHitResult = d.result;
                                }
                                else
                                {
                                    skippedToHitResult = Mathf.Min(d.result, toHitResult);
                                    toHitResult = Mathf.Max(d.result, toHitResult);
                                }
                                break;
                            case 2://disadvantage
                                if (toHitResult == -1)
                                {
                                    toHitResult = d.result;
                                }
                                else
                                {
                                    toHitResult = Mathf.Min(d.result, toHitResult);
                                    skippedToHitResult = Mathf.Max(d.result, toHitResult);
                                }
                                break;
                        }
                        if (toHitResult >= dieGroup.critOn)
                        {
                            crit = true;
                            critFail = false;
                        }
                        else if(toHitResult == 1)
                        {
                            crit = false;
                            critFail = true;
                        }
                        else 
                        { 
                            crit = false;
                            critFail = false;
                        }
                        break;
                    case 1://to hit bonus
                        toHitResult += d.result;
                        break;
                    case 2://damage
                        damageResult += d.result;
                        break;
                }
            }
            //get crit value if crit
            if (crit)
            {
                damageResult = CritHandler.GetCritValue(dieGroup.damageDice, damageResult, dieGroup.damageModifier);
            }

            //tell DiceManager to check final results
            transform.parent.gameObject.GetComponent<DiceManager>().CheckFinalResults();
        }
    }
    public static void ResetStartPosition()
    {
        //initial location
        nextStartPosition = new Vector3(-15, 15, 6);
    }
    public static void IterateStartPosition()
    {
        //iterate vector3
        nextStartPosition.x += 3* diceScale;
        if (nextStartPosition.x > 15)
        {
            nextStartPosition.x = -15;
            nextStartPosition.z -= 3* diceScale;
        }
    }
}
