using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Results : MonoBehaviour
{
    public int numOfDice;
    public int resultsSaved = 0;
    private bool resultsLogged = false;
    private int dieResultTotal = 0;
    public void DieResultTotal(int dieResult)
    {
        dieResultTotal += dieResult;
        resultsSaved += 1;
    }

    void Update()
    {
        if (resultsSaved == numOfDice && resultsLogged == false)
        {
            Debug.Log(string.Concat("Total Roll: ", dieResultTotal));
            resultsLogged = true;
        }
    }
}
