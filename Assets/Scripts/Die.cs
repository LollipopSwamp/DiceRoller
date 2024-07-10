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

    //die group types

    //ray directions & length
    public float scale;
    public float rayLength = 1.25f;
    public Vector3[] rayDirections;

    //die result
    public int result;

    public Die(DieType _dieType, float _scale)
    {
        dieType = _dieType;
        scale = _scale;
        result = -1;

        switch (dieType)
        {
            case DieType.d6:
                rayDirections = new Vector3[] {
                        new Vector3(0,-1,0)*rayLength, //1
                        new Vector3(1,0,0)*rayLength, //2
                        new Vector3(0,0,1)*rayLength, //3
                        new Vector3(0,0,-1)*rayLength, //4
                        new Vector3(-1,0,0)*rayLength, //5
                        new Vector3(0,1,0)*rayLength //6
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
    }

    public void SetResult(int _result)
    {
        result = _result;
    }
    public string GetDieTypeString()
    {
        return nameof(dieType);
    }
}
