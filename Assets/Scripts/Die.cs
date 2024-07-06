using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{

    public int ColorIndex;
    public LayerMask layersToHit;
    public bool lockRotation;
    public bool randomVelocityRotation;
    public GameObject globalVariables;
    public int dieResult = -1;
    public static float rayLength = 1.25f;
    public bool dieIsMoving;

    private Rigidbody rb;
    private MeshRenderer mr;
    private Material material;

    private static DateTime start;
    private Quaternion savedRotation = new Quaternion(0, 0, 0, 1);
    private List<Color> colors = new List<Color> {
        new Color(0f,0f,0f,0f), //white/null 0
        new Color(255f,0f,0f,63f), //red 1
        new Color(255f,127f,0f,63f), //orange 2
        new Color(255f,255f,0f,63f), //yellow 3
        new Color(0f,255f,0f,63f), //green 4
        new Color(0f,0f,255f,63f), //blue 5
        new Color(127f,0f,127f,63f), //purple 6
        new Color(255f,0f,255f,63f) //pink 7
    };
    //rays and directions
    private List<Ray> rays = new List<Ray>();
    private Vector3[] d6Directions = {
            new Vector3(0,-rayLength,0), //1
            new Vector3(rayLength,0,0), //2
            new Vector3(0,0,rayLength), //3
            new Vector3(0,0,-rayLength), //4
            new Vector3(-rayLength,0,0), //5
            new Vector3(0,rayLength,0) //6
        };
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

    void Start()
    {
        //init variables
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        material = GetComponent<Material>();
        start = DateTime.Now;

        //set color
        if (ColorIndex != 0)
        {
            material.color = colors[ColorIndex % colors.Count];
            Debug.Log(colors[ColorIndex % colors.Count]);
        }

        //set random rotation and velocity
        if (randomVelocityRotation)
        {
            Debug.Log("Random rotation and velocity");
            transform.rotation = Random.rotation;
            System.Random r = new System.Random();
            float velocityX = (float)(r.NextDouble() - 0.5) * 50;
            float velocityZ = (float)(r.NextDouble() - 0.5) * 50;
            rb.velocity = new Vector3(velocityX, 3, velocityZ);
        }
        //ray test
    }

    void Update()
    {
        //Raycast hit and draw rays
        if (dieIsStopped() && dieResult == -1)
        {
            //check all directions for rays
            for (int i = 0; i < d20Directions.Length; ++i)
            {
                //create + draw ray
                Ray ray = new Ray(transform.position, transform.rotation * (d20Directions[i]*rayLength));
                Debug.DrawRay(transform.position, ray.direction, Color.red, 1f, true);

                //if ray is hitting floor, lock rotation and store globalVariables
                if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layersToHit))
                {
                    if (hit.collider.tag == "Floor")
                    {
                        Debug.Log(string.Concat("Rolled ", i + 1));
                        dieResult = i + 1;
                        globalVariables.GetComponent<Results>().DieResultTotal(dieResult);

                        savedRotation = transform.rotation;
                        lockRotation = true;
                    }
                };

            }
        }

        //disable rotation from physics
        if (lockRotation)
        {
            transform.rotation = savedRotation;
        }

        //reset die if cocked
        if ( dieResult == -1 && DateTime.Now > start.AddSeconds(5) && dieIsStopped())
        {
            resetDie();
            start = DateTime.Now;
        }

    }
    bool dieIsStopped()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.005f && Mathf.Abs(rb.velocity.y) < 0.005f && Mathf.Abs(rb.velocity.z) < 0.005f)
        {
            //Debug.Log(string.Concat(transform.name, "No velocity"));
            dieIsMoving = false;
            rb.velocity = Vector3.zero;
            return true;
        }
        else
        {
            dieIsMoving = true;
            return false;
        }
    }

    void resetDie()
    {
        Debug.Log("Resetting die");
        transform.rotation = Random.rotation;
        System.Random r = new System.Random();
        float velocityX = (float)(r.NextDouble() - 0.5) * 50;
        float velocityZ = (float)(r.NextDouble() - 0.5) * 50;
        rb.velocity = new Vector3(velocityX, 3, velocityZ);
        transform.position = new Vector3(0, 15, 0);

    }

}
