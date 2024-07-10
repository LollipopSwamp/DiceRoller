using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using static DieGroup;

public class DieGroup
{
    private static int groupIdIndex = 0;
    public int groupId;
    public List<Die> dice = new List<Die>();
    public int groupResult = -1;

    //results type
    public enum ResultsType { Sum, Advantage, Disadvantage };
    public ResultsType resultsType = ResultsType.Sum;

    //die color
    public enum DieColor { White, Red, Orange, Yellow, Green, Blue, Purple, Pink };
    public DieColor dieColor = DieColor.White;

    public DieGroup(ResultsType _resultsType)
    {
        resultsType = _resultsType;
        groupId = groupIdIndex;
        groupIdIndex += 1;
    }

    public void AddDice(List<Die> _dice)
    {
        dice = _dice;
    }

    public void CheckResults()
    {
        //check if dice are still rolling
        bool diceStillRolling = false;
        foreach (var d in dice)
        {
            if (d.result == -1)
            {
                diceStillRolling = true;
            }
        }

        //if dice done rolling, process results
        if (!diceStillRolling)
        {
            int _groupResult = 0;
            switch (resultsType)
            {
                case ResultsType.Sum:
                    foreach (Die d in dice)
                    {
                        _groupResult += d.result;
                        Debug.Log(d.result);
                    }
                    break;
                case ResultsType.Advantage:
                    foreach (Die d in dice)
                    {
                        _groupResult = Math.Max(d.result, _groupResult);
                    }
                    break;
                case ResultsType.Disadvantage:
                    foreach (Die d in dice)
                    {
                        _groupResult = Math.Min(d.result, _groupResult);
                    }
                    break;
            }
            groupResult = _groupResult;
        }
    }
}
