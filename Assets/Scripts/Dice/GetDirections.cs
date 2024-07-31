using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;
using Math = System.Math;

public class GetDirections : MonoBehaviour
{
    void Update()
    {
        Vector3 direction = Quaternion.Inverse(transform.rotation) * Vector3.down;
        string x = Math.Round(direction.x, 2).ToString();
        string y = Math.Round(direction.y, 2).ToString();
        string z = Math.Round(direction.z, 2).ToString();
        string stringOut = "new Vector3(" + x + "f," + y + "f," + z + "f),";
        Debug.Log(stringOut);
    }
}













