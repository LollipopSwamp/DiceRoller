using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public GameObject globalVariables;

    private DateTime delayedTime;
    void Start()
    {
        delayedTime = DateTime.Now.AddSeconds(5);
    }
        
    void Update()
    {
        int unsavedResults = globalVariables.GetComponent<Results>().numOfDice - globalVariables.GetComponent<Results>().resultsSaved;
        //Debug.Log(unsavedResults);
        if (DateTime.Now > delayedTime && unsavedResults > 0)
        {
            Debug.Log("Dice results pending, moving floor down");
            //transform.Translate(-Vector3.up);
            delayedTime = DateTime.Now.AddSeconds(5);
        }
    }
}
