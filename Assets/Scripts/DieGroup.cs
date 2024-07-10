using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using static DieGroup;

public class DieGroup
{
    private static int groupIdIndex = 0;
    public int groupId;
    public Dictionary<int, Die> dice = new Dictionary<int, Die>();
    public int groupResult = -1;

    //results type
    public enum ResultsType { Sum, Advantage, Disadvantage };
    public ResultsType resultsType = ResultsType.Sum;

    //die color
    public enum DieColor { White, Red, Orange, Yellow, Green, Blue, Purple, Pink };
    public DieColor dieColor = DieColor.White;

    public DieGroup( ResultsType _resultsType)
    {
        resultsType = _resultsType;
        groupId = groupIdIndex;
        groupIdIndex += 1;
    }

    public void AddDie(Die die)
    {
        dice.Add(die.dieId,die);
    }

    void Update()
    {
        //check if dice are still rolling
        bool diceStillRolling = false;
        foreach (var d in dice)
        {
            if (d.Value.result == -1)
            {
                diceStillRolling = true;
            }
            else
            {
                Debug.Log(d.Value.result);
            }
        }

        //if dice done rolling, process results
        if (!diceStillRolling)
        {
            int _groupResult = 0;
            switch (resultsType)
            {
                case ResultsType.Sum:
                    foreach (var d in dice)
                    {
                        _groupResult += d.Value.result;
                    }
                    break;
                case ResultsType.Advantage:
                    foreach (var d in dice)
                    {
                        _groupResult += Math.Max(d.Value.result, _groupResult);
                    }
                    break;
                case ResultsType.Disadvantage:
                    foreach (var d in dice)
                    {
                        _groupResult += Math.Min(d.Value.result, _groupResult);
                    }
                    break;
            }
            groupResult = _groupResult;
        }
    }
}
