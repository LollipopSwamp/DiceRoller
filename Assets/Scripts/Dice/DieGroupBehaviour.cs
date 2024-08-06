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
        if (dieGroup.toHitType == DieGroup.ToHitType.Standard)
        {
            //create die gameobject
            GameObject dieObj = Instantiate(diePrefabs[5], nextStartPosition, Quaternion.identity, transform);
            dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(true);
            dice.Add(dieObj);

            //set DieBehaviour variables
            Die d20 = new Die(Die.DieType.d20, dieGroup.groupId, Die.DieSubGroup.ToHit);
            DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
            dieBehaviour.SetVars(d20, gameObject, nextStartPosition, diceScale);

            //set die obj name
            dieObj.name = "d20 (ID: " + dieBehaviour.die.dieId.ToString() + ")";
            IterateStartPosition();
        }
        //to hit dice if advantage or disadvantage, 2x d20
        else if (dieGroup.toHitType == DieGroup.ToHitType.Advantage || dieGroup.toHitType == DieGroup.ToHitType.Disadvantage)
        {
            for (int i = 0; i < 2; i++)
            {
                //create die gameobject
                GameObject dieObj = Instantiate(diePrefabs[5], nextStartPosition, Quaternion.identity, transform);
                dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(true);
                dice.Add(dieObj);

                //set DieBehaviour variables
                Die d20 = new Die(Die.DieType.d20, dieGroup.groupId, Die.DieSubGroup.ToHit);
                DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
                dieBehaviour.SetVars(d20, gameObject, nextStartPosition, diceScale);

                //set die obj name
                dieObj.name = "d20 (ID: " + dieBehaviour.die.dieId.ToString() + ")";
                IterateStartPosition();
            }
        }

        //create bonus to hit dice, if any
        foreach (Die.DieType dieType in dieGroup.toHitBonusDice)
        {
            //create die gameobject
            int dieTypeIndex = Die.DieTypeToIndex(dieType);
            GameObject dieObj = Instantiate(diePrefabs[dieTypeIndex], nextStartPosition, Quaternion.identity, transform);
            dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(true);
            dice.Add(dieObj);

            //set DieBehaviour variables
            Die die = new Die(dieType, dieGroup.groupId, Die.DieSubGroup.ToHitBonus);
            DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
            dieBehaviour.SetVars(die, gameObject, nextStartPosition, diceScale);

            //set die obj name
            dieObj.name = Die.DieTypeToString(dieType) + " (ID: " + dieBehaviour.die.dieId.ToString() + ")";
            IterateStartPosition();
        }

        //create damage/standard dice
        foreach (Die.DieType dieType in dieGroup.damageDice)
        {
            //create die gameobject
            int dieTypeIndex = Die.DieTypeToIndex(dieType);
            GameObject dieObj = Instantiate(diePrefabs[dieTypeIndex], nextStartPosition, Quaternion.identity, transform);
            dieObj.GetComponent<MeshRenderer>().material.color = dieGroup.GetColor(false);
            dice.Add(dieObj);

            //set DieBehaviour variables
            Die die = new Die(dieType, dieGroup.groupId, Die.DieSubGroup.Damage);
            DieBehaviour dieBehaviour = dieObj.GetComponent<DieBehaviour>();
            dieBehaviour.SetVars(die, gameObject, nextStartPosition, diceScale);

            //set die obj name
            dieObj.name = Die.DieTypeToString(dieType) + " (ID: " + dieBehaviour.die.dieId.ToString() + ")";

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
                    case Die.DieSubGroup.ToHit:
                        switch (dieGroup.toHitType)
                        {
                            case DieGroup.ToHitType.Standard:
                                toHitResult = d.result;
                                skippedToHitResult = 0;
                                break;
                            case DieGroup.ToHitType.Advantage:
                                toHitResult = Mathf.Max(d.result, toHitResult);
                                skippedToHitResult = Mathf.Min(d.result, toHitResult);
                                break;
                            case DieGroup.ToHitType.Disadvantage:
                                toHitResult = Mathf.Min(d.result, toHitResult);
                                skippedToHitResult = Mathf.Max(d.result, toHitResult);
                                break;
                        }
                        if (toHitResult == 20)
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
                    case Die.DieSubGroup.ToHitBonus:
                        toHitResult += d.result;
                        break;
                    case Die.DieSubGroup.Damage:
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
