using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;
using static Die;

public class Die
{

    //sides on die
    public enum DieType { d4, d6, d8, d10, d12, d20 };
    public DieType dieType = DieType.d4;
    public int dieTypeIndex;

    //sub diegroup type
    public enum DieSubGroup { ToHit, ToHitBonus, Damage };
    public DieSubGroup dieSubGroup = DieSubGroup.Damage;


    //ray directions & length
    public float scale;
    public float rayLength = 1.25f;
    public Vector3[] rayDirections;

    //other vars
    public int result = -1;
    public int dieId;
    public int groupId;
    private static int dieIdIndex = 0;

    public Die(DieType _dieType, int _groupId)
    {
        dieType = _dieType;
        groupId = _groupId;
        //scale = _scale;
        result = -1;

        dieId = dieIdIndex;
        dieIdIndex += 1;

        //Debug.Log(string.Concat("Creating ", dieType, " || Die ID: ", dieId));


        switch (dieType)
        {
            case DieType.d6:
                rayDirections = new Vector3[] {
                        new Vector3(0,0,-1)*rayLength, //1
                        new Vector3(1,0,0)*rayLength, //2
                        new Vector3(0,-1,0)*rayLength, //3
                        new Vector3(0,1,0)*rayLength, //4
                        new Vector3(-1,0,0)*rayLength, //5
                        new Vector3(0,0,1)*rayLength //6
                };
                break;
            case DieType.d20:
                rayDirections = new Vector3[] {
                    new Vector3(-0.30f, 0.94f, -0.17f)*rayLength, //1
                    new Vector3(-0.31f, -0.93f, -0.18f)*rayLength, //2
                    new Vector3(0.79f, 0.57f, -0.21f)*rayLength, //3
                    new Vector3(-0.49f, -0.37f, 0.79f)*rayLength, //4
                    new Vector3(-0.98f, 0.00f, -0.19f)*rayLength, //5
                    new Vector3(0.62f, 0.00f, 0.78f)*rayLength, //6
                    new Vector3(-0.21f, 0.57f, -0.80f)*rayLength, //7
                    new Vector3(0.79f, -0.58f, -0.20f)*rayLength, //8
                    new Vector3(0.21f, 0.57f, 0.79f)*rayLength, //9
                    new Vector3(0.48f, -0.37f, -0.79f)*rayLength, //10
                    new Vector3(-0.48f, 0.37f, 0.79f)*rayLength, //11
                    new Vector3(-0.21f, -0.57f, -0.79f)*rayLength, //12
                    new Vector3(-0.79f, 0.58f, 0.20f)*rayLength, //13
                    new Vector3(0.21f, -0.57f, 0.80f)*rayLength, //14
                    new Vector3(-0.62f, -0.00f, -0.78f)*rayLength, //15
                    new Vector3(0.98f, -0.00f, 0.19f)*rayLength, //16
                    new Vector3(0.49f, 0.37f, -0.79f)*rayLength, //17
                    new Vector3(-0.79f, -0.57f, 0.21f)*rayLength, //18
                    new Vector3(0.31f, 0.93f, 0.18f)*rayLength, //19
                    new Vector3(0.30f, -0.94f, 0.17f)*rayLength //20
                };
                break;

        }
        PrintDie();
    }


    public void SetResult(int _result)
    {
        result = _result;
    }

    public void SetGroupID(int _groupId)
    {
        groupId = _groupId;
    }
    public string GetDieTypeString()
    {
        return GetDieTypeString(dieTypeIndex);
    }
    public static string GetDieTypeString(int _dieTypeIndex)
    {
        switch (_dieTypeIndex)
        {
            case 0:
                return "d20";
            case 1:
                return "d12";
            case 2:
                return "d10";
            case 3:
                return "d8";
            case 4:
                return "d6";
            case 5:
                return "d4";
            default:
                return "DieTypeError";
        }
    }
    public static string GetDieTypeString(DieType _dieType)
    {
        switch (_dieType)
        {
            case DieType.d20:
                return "d20";
            case DieType.d12:
                return "d12";
            case DieType.d10:
                return "d10";
            case DieType.d8:
                return "d8";
            case DieType.d6:
                return "d6";
            case DieType.d4:
                return "d4";
            default:
                return "DieTypeError";
        }
    }
    public void PrintDie()
    {
        Debug.Log(string.Concat("DieType: ", dieType, " || GroupID: ", groupId, " || Result: ", result));
    }

    public static int DieTypeToIndex(DieType _dieType)
    {
        switch (_dieType)
        {
            case DieType.d20:
                return 0;
            case DieType.d12:
                return 1;
            case DieType.d10:
                return 2;
            case DieType.d8:
                return 3;
            case DieType.d6:
                return 4;
            case DieType.d4:
                return 5;
            default:
                Debug.Log("Error in IndexToDieType, returning d20 index (0)");
                return 0;
        }
    }
    public static DieType IndexToDieType(int _index)
    {
        switch (_index)
        {
            case 0:
                return DieType.d20;
            case 1:
                return DieType.d12;
            case 2:
                return DieType.d10;
            case 3:
                return DieType.d8;
            case 4:
                return DieType.d6;
            case 5:
                return DieType.d4;
            default:
                Debug.Log("Error in IndexToDieType, returning d20");
                return DieType.d20;
        }
    }
}
