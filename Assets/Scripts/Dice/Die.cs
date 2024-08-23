using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;
using static Die;

public class Die
{

    //sides on die
    //public enum DieType { d4, d6, d8, d10, d12, d20 };
    //public DieType dieType = DieType.d4;
    public int dieType;
    public int dieFaces;

    //sub diegroup type
    //public enum DieSubGroup { ToHit, ToHitBonus, Damage };
    //public DieSubGroup dieSubGroup = DieSubGroup.Damage;
    public int dieSubGroup; // 0 is to hit, 1 is to hit bonus, 2 is damage


    //ray directions & length
    public float scale;
    public Vector3[] rayDirections;

    //other vars
    public int result = -1;
    public int groupId;
    public int dieId;
    private static int dieIdIndex = 0;

    public Die(int _dieType, int _groupId, int _dieSubGroup)
    {
        dieType = _dieType;
        groupId = _groupId;
        dieSubGroup = _dieSubGroup;
        result = -1;

        dieId = dieIdIndex;
        dieIdIndex += 1;

        switch (dieType)
        {
            case 0:
                rayDirections = new Vector3[] {
                        new Vector3(0,0,-1), //1
                        new Vector3(0, -0.94f, 0.33f), //2
                        new Vector3(0.82f, 0.47f, 0.33f), //3
                        new Vector3(-0.81f, 0.48f, 0.34f) //4
                };
                dieFaces = 4;
                break;
            case 1:
                rayDirections = new Vector3[] {
                        new Vector3(0,0,-1), //1
                        new Vector3(1,0,0), //2
                        new Vector3(0,-1,0), //3
                        new Vector3(0,1,0), //4
                        new Vector3(-1,0,0), //5
                        new Vector3(0,0,1) //6
                };
                dieFaces = 6;
                break;
            case 2:
                rayDirections = new Vector3[] {
                        new Vector3(0.54f,0.54f,-0.65f), //1
                        new Vector3(-0.54f,0.54f,0.65f), //2
                        new Vector3(-0.54f,0.54f,-0.65f), //3
                        new Vector3(0.54f,0.54f,0.65f), //4
                        new Vector3(-0.54f,-0.54f,-0.65f), //5
                        new Vector3(0.54f,-0.54f,0.65f), //6
                        new Vector3(0.54f,-0.54f,-0.65f), //7
                        new Vector3(-0.54f,-0.54f,0.65f) //8
                };
                dieFaces = 8;
                break;
            case 3:
            case 4:
                rayDirections = new Vector3[] {
                        new Vector3(0f,-0.74f,-0.67f), //1
                        new Vector3(0.71f,0.23f,0.67f), //2
                        new Vector3(-0.44f,0.6f,-0.67f), //3
                        new Vector3(-0.44f,-0.6f,0.67f), //4
                        new Vector3(0.44f,0.6f,-0.67f), //5
                        new Vector3(0.44f,-0.6f,0.67f), //6
                        new Vector3(-0.71f,-0.23f,-0.67f), //7
                        new Vector3(0f,0.75f,0.67f), //8
                        new Vector3(0.71f,-0.23f,-0.67f), //9
                        new Vector3(-0.71f,0.23f,0.67f) //10
                };
                break;
                dieFaces = 10;
            case 5:
                rayDirections = new Vector3[] {
                        new Vector3(0f,0f,-1f), //1
                        new Vector3(0f,-0.89f,-0.45f), //2
                        new Vector3(-0.85f,-0.28f,-0.45f), //3
                        new Vector3(0.85f,-0.28f,-0.45f), //4
                        new Vector3(-0.53f,0.72f,-0.45f), //5
                        new Vector3(0.53f,0.72f,-0.45f), //6
                        new Vector3(-0.53f,-0.72f,0.45f), //7
                        new Vector3(0.53f,-0.72f,0.45f), //8
                        new Vector3(-0.85f,0.28f,0.45f), //9
                        new Vector3(0.85f,0.28f,0.45f), //10
                        new Vector3(0f,0.89f,0.45f), //11
                        new Vector3(0f,0f,1f) //12
                };
                dieFaces = 12;
                break;
            case 6:
                rayDirections = new Vector3[] {
                    new Vector3(-0.30f, 0.94f, -0.17f), //1
                    new Vector3(-0.31f, -0.93f, -0.18f), //2
                    new Vector3(0.79f, 0.57f, -0.21f), //3
                    new Vector3(-0.49f, -0.37f, 0.79f), //4
                    new Vector3(-0.98f, 0.00f, -0.19f), //5
                    new Vector3(0.62f, 0.00f, 0.78f), //6
                    new Vector3(-0.21f, 0.57f, -0.80f), //7
                    new Vector3(0.79f, -0.58f, -0.20f), //8
                    new Vector3(0.21f, 0.57f, 0.79f), //9
                    new Vector3(0.48f, -0.37f, -0.79f), //10
                    new Vector3(-0.48f, 0.37f, 0.79f), //11
                    new Vector3(-0.21f, -0.57f, -0.79f), //12
                    new Vector3(-0.79f, 0.58f, 0.20f), //13
                    new Vector3(0.21f, -0.57f, 0.80f), //14
                    new Vector3(-0.62f, -0.00f, -0.78f), //15
                    new Vector3(0.98f, -0.00f, 0.19f), //16
                    new Vector3(0.49f, 0.37f, -0.79f), //17
                    new Vector3(-0.79f, -0.57f, 0.21f), //18
                    new Vector3(0.31f, 0.93f, 0.18f), //19
                    new Vector3(0.30f, -0.94f, 0.17f) //20
                };
                dieFaces = 20;
                break;

        }
    }


    public void SetResult(int _result)
    {
        result = _result;
    }

    public void SetGroupID(int _groupId)
    {
        groupId = _groupId;
    }
    public string DieTypeString()
    {
        return DieTypeToString(dieType);
    }
    public static string DieTypeToString(int _dieType)
    {
        switch (_dieType)
        {
            case 0:
                return "d4";
            case 1:
                return "d6";
            case 2:
                return "d8";
            case 3:
                return "d10";
            case 4:
                return "d%";
            case 5:
                return "d12";
            case 6:
                return "d20";
            default:
                return "DieTypeError";
        }
    }
    public void PrintDie()
    {
        Debug.Log(string.Concat("DieType: ", dieType, " || GroupID: ", groupId, " || Result: ", result));
    }
    public static int GetFacesInt(int _dieType)
    {
        switch (_dieType)
        {
            case 0:
                return 4;
            case 1:
                return 6;
            case 2:
                return 8;
            case 3:
                return 10;
            case 4:
                return 100;
            case 5:
                return 12;
            case 6:
                return 20;
            default:
                Debug.Log("Error in GetFacesInt, returning -1");
                return -1;
        }
    }
}
