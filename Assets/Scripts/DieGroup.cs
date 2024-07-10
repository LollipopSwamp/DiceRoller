using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DieGroup;

public class DieGroup
{
    public int groupId;
    public List<Die> dice = new List<Die>();

    //results type
    public enum ResultsType { Sum, Advantage, Disadvantage };
    public ResultsType resultsType = ResultsType.Sum;

    //die color
    public enum DieColor { White, Red, Orange, Yellow, Green, Blue, Purple, Pink };
    public DieColor dieColor = DieColor.White;

    public DieGroup(int _groupId, List<Die> _dice, ResultsType _resultsType)
    {
        resultsType = _resultsType;
        groupId = _groupId;
        dice = _dice;
    }
}
