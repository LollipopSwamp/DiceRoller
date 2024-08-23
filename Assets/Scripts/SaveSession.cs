using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSession
{

    public int groupId;
    public string groupName;

    public string toHitBonusDice; //damage if attack
    public string damageDice; //damage if attack

    public int toHitType;

    public int toHitModifier;
    public int damageModifier;

    public int colorIndex = 0;

    public string DiceListToString(List<int> _dice)
    {
        string outputString = "";
        foreach (int dieType in _dice)
        {
            outputString += dieType.ToString() + '|';
        }
        outputString = outputString.Remove(outputString.Length - 1);
        return outputString;
    }
    public List<int> StringToDieList(string _diceString)
    {
        //example "0|0|0|1|4|4
        List<int> dice = new List<int>();

        for (int i = 0; i <_diceString.Length; i += 2)
        {
            string dyeTypeIndexString = _diceString.Substring(i,i+1);
            int dieType = int.Parse(dyeTypeIndexString);
            dice.Add(dieType);
        }
        return dice;
    }
}
