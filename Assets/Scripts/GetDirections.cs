using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;

public class GetDirections : MonoBehaviour
{

    public bool lockRotation;

    private Rigidbody rb;
    private MeshRenderer mr;
    private Material material;
    private Vector3[] d20Directions = {
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

    //rays and directions

    void Start()
    {
        //init variables
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        material = GetComponent<Material>();
    }

    void Update()
    {


        for (int i = 0; i < d20Directions.Length; ++i)
        {
            //create + draw ray
            Ray ray = new Ray(transform.position, transform.rotation * (d20Directions[i] * 1.25f));
            Debug.DrawRay(transform.position, ray.direction, Color.red, 1f, true);
        }
        Vector3 rayStart = transform.position;
        Vector3 redRay = Vector3.down; //red
        Vector3 blueRay = transform.rotation * redRay; //blue

        Vector3 greenRay = transform.rotation * Quaternion.Inverse(new Quaternion(-0.08973f, 0.01454f, 0.15675f, 0.98345f)) * Vector3.down; //green
        Debug.Log(string.Concat("new Vector3", Quaternion.Inverse(transform.rotation) * Vector3.down, ","));
        //Vector3 greenDirection = new Vector3(-0.95f, -0.31f, 0.00f);
        //Vector3 greenRay = Quaternion.Inverse(transform.rotation) * redRay; //green
        Debug.Log(transform.rotation);

        //Ray ray = new Ray(rayStart, greenRay);

        Debug.Log(string.Concat("red", redRay));
        //Debug.DrawRay(rayStart, redRay, Color.red, 1f, true);

        Debug.Log(string.Concat("blue", blueRay));
        //Debug.DrawRay(rayStart, blueRay, Color.blue, 1f, true);

        //Debug.Log(string.Concat("green", ray.direction));
        //Debug.DrawRay(rayStart, ray.direction, Color.green, 1f, true);


        //disable rotation from physics
        if (lockRotation)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
            transform.position = new Vector3(0, 15, 0);
        }
    }

}
//(0.33f, 0.93f, 0.14f) //1
//(0.31f, -0.93f, 0.17f) //2
//(-0.31f, 0.93f, -0.17f) //19
//(-0.33f, -0.93f, -0.14f) //20
//green(-0.95f, -0.31f, 0.00f)

//rotation (-0.08973, 0.01454, 0.15675, 0.98345) //2




//blue(1.00f, 0.00f, -0.01f)
//(-0.01307, 0.08681, 0.98424, 0.15348)

















