using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieGroupPreset
{
    public string groupName;

    public List<Die.DieType> toHitDice = new List<Die.DieType>(); //to hit if attack
    public List<Die.DieType> toHitBonusDice = new List<Die.DieType>(); //damage if attack
    public List<Die.DieType> damageDice = new List<Die.DieType>(); //damage if attack

    public int toHitModifier;
    public int damageModifier;

    public int colorIndex;

    private DieGroup.ResultsType resultsType;

    private DieGroup.GroupType groupType;

    public DieGroupPreset()
    {
        toHitDice.Clear();
        toHitBonusDice.Clear();
        damageDice.Clear();

        toHitModifier = 0;
        damageModifier = 0;
    }
    //standard roll
    public DieGroupPreset( List<Die.DieType> _damageDice, int _damageModifier)
    {
        toHitDice.Clear();
        toHitBonusDice.Clear();
        damageDice = _damageDice;

        toHitModifier = 0;
        groupType = DieGroup.GroupType.Standard;
        resultsType = DieGroup.ResultsType.Sum;
    }

    //attack roll with bonus to hit
    public DieGroupPreset(List<Die.DieType> _toHitDice, List<Die.DieType> _toHitBonusDice, int _toHitModifer, DieGroup.ResultsType _resultsType, List<Die.DieType> _damageDice, int _damageModifier)
    {
        toHitDice = _toHitDice;
        toHitBonusDice = _toHitBonusDice;
        damageDice = _damageDice;

        toHitModifier = _toHitModifer;
        damageModifier = _damageModifier;

        groupType = DieGroup.GroupType.Attack;
        resultsType = _resultsType;
    }

    //attack roll without bonus to hit
    public DieGroupPreset(List<Die.DieType> _toHitDice, int _toHitModifer, DieGroup.ResultsType _resultsType, List<Die.DieType> _damageDice, int _damageModifier)
    {
        toHitDice = _toHitDice;
        toHitBonusDice.Clear();
        damageDice = _damageDice;

        toHitModifier = _toHitModifer;
        damageModifier = _damageModifier;

        groupType = DieGroup.GroupType.Attack;
        resultsType = _resultsType;
    }
}
