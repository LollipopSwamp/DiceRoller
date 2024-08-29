using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Settings;

public static class CritHandler
{
    public static int GetCritValue(List<int> _damageDice, int _damageValue, int _modifier)
    {
        switch (Settings.critType)
        {
            case CritType.DoubleDice:
                return (_damageValue * 2);
            case CritType.DoubleTotal:
                Debug.Log(_damageValue);
                Debug.Log(_modifier);
                return (_damageValue * 2) + _modifier;
            case CritType.MaxPlusRoll:
                int maxDamage = 0;
                foreach(int _dieType in _damageDice)
                {
                    maxDamage += Die.GetFacesInt(_dieType);
                }
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
                    if (_die.dieSubGroup == 2)
                    {
                        critCalculation += "(" + _die.DieTypeString() + ") " + _die.result.ToString() + "*2 + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString();
                break;
            case CritType.DoubleTotal:
                critCalculation += "{";
                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == 2)
                    {
                        critCalculation += "(" + _die.DieTypeString() + ") " + _die.result.ToString() + " + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString() + "}*2";
                break;
            case CritType.MaxPlusRoll:
                critCalculation += "{";
                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == 2)
                    {
                        critCalculation += Die.GetFacesInt(_die.dieType) + " + ";
                    }
                }
                critCalculation += _dieGroupB.dieGroup.damageModifier.ToString() + "} + ";

                foreach (GameObject _dieB in _dieGroupB.dice)
                {
                    Die _die = _dieB.GetComponent<DieBehaviour>().die;
                    if (_die.dieSubGroup == 2)
                    {
                        critCalculation += "(" + _die.DieTypeString() + ") " + _die.result.ToString() + " + ";
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
