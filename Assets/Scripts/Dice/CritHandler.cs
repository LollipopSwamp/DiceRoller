using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Settings;

public static class CritHandler
{
    public static int GetCritValue(List<Die.DieType> _damageDice, int _damageValue, int _modifier)
    {
        switch (Settings.critType)
        {
            case CritType.DoubleDice:
                return (_damageValue * 2);
            case CritType.DoubleTotal:
                return (_damageValue * 2) + _modifier;
            case CritType.MaxPlusRoll:
                int maxDamage = 0;
                foreach(Die.DieType _dieType in _damageDice)
                {
                    maxDamage += Die.GetFacesInt(_dieType);
                }
                Debug.Log(maxDamage.ToString());
                return maxDamage + _damageValue + _modifier;
            default:
                Debug.Log("Error with GetCritValue, returning -1");
                return -1;
        }
    }
    public static string GetCritCalculation(DieGroupBehaviour _dieGroupB)
    {
        string critCalculation = "";
        switch (Settings.critType)
        {
            case CritType.DoubleDice:
                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == Die.DieSubGroup.Damage)
                    {
                        critCalculation += "(" + _die.DieTypeToString() + ") " + _die.result.ToString() + "*2 + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString();
                break;
            case CritType.DoubleTotal:
                critCalculation += "{";
                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == Die.DieSubGroup.Damage)
                    {
                        critCalculation += "(" + _die.DieTypeToString() + ") " + _die.result.ToString() + " + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString() + "}*2";
                break;
            case CritType.MaxPlusRoll:
                critCalculation += "{";
                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == Die.DieSubGroup.Damage)
                    {
                        critCalculation += Die.GetFacesInt(_die.dieType) + " + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString() + "}*2 + ";

                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == Die.DieSubGroup.Damage)
                    {
                        critCalculation += "(" + _die.DieTypeToString() + ") " + _die.result.ToString() + " + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString();
                break;
            default:
                Debug.Log("Error with GetCritCalculation, returning 'error'");
                critCalculation = "error";
                break;
        }
        return critCalculation;
    }
}
