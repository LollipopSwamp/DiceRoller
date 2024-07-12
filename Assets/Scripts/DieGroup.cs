using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;
using static DieGroup;

public class DieGroup
{
    private static int groupIdIndex = 0;
    public int groupId;
    public string groupName;
    public List<Die> dice = new List<Die>();
    public int groupResult = -1;
    public int modifier;

    //results type
    public enum ResultsType { Sum, Advantage, Disadvantage };
    public ResultsType resultsType = ResultsType.Sum;

    //die color
    public int colorIndex;
    public List<Color> dieColor = new List<Color>()
    {
        Color.white, //white
        Color.red, //red
        new Color(1,0.65f,0,1), //orange
        Color.yellow, //yellow
        Color.green, //green
        Color.blue, //blue
        new Color(0.62f,0.13f,0.94f,1), //purple
        new Color(1,0.75f,0.8f,1) //pink
    };

    public DieGroup(string _groupName, ResultsType _resultsType, int _colorIndex, int _modifier)
    {
        groupName = _groupName;
        resultsType = _resultsType;
        colorIndex = _colorIndex;
        groupId = groupIdIndex;
        groupIdIndex += 1;
        modifier = _modifier;
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
    public Color GetColor()
    {
        return dieColor[colorIndex];
    }
}
