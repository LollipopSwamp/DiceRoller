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
        Vector3 rayStart = transform.position;
        Vector3 redRay = Vector3.down; //red
        Vector3 blueRay = transform.rotation * redRay; //blue
        Vector3 greenRay = transform.rotation * (redRay - blueRay); //green

        Ray ray = new Ray(rayStart, greenRay);

        Debug.Log(string.Concat("red", redRay));
        Debug.DrawRay(rayStart, redRay, Color.red, 1f, true);

        Debug.Log(string.Concat("blue", blueRay));
        Debug.DrawRay(rayStart, blueRay, Color.blue, 1f, true);

        Debug.Log(string.Concat("green", ray.direction));
        Debug.DrawRay(rayStart, ray.direction, Color.green, 1f, true);


        //disable rotation from physics
        if (lockRotation)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
            transform.position = new Vector3(0, 15, 0);
        }
    }
    
}
//(-0.54, 1.68, -0.31)(0.31, -0.93, 0.17)







