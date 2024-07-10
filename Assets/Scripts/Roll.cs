using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{

    public LayerMask layersToHit;
    public bool randomVelocityRotation;
    public static GameObject diceManager;
    public bool dieIsMoving;

    private Rigidbody rb;
    //private MeshRenderer mr;
    //private Material material;
    private static DateTime start;
    private bool lockRotation;
    private Die die;

    private Quaternion savedRotation = new Quaternion(0, 0, 0, 1);

    //rays and directions
    private List<Ray> rays = new List<Ray>();

    void Start()
    {
        //init variables
        rb = GetComponent<Rigidbody>();
        //mr = GetComponent<MeshRenderer>();
        //material = GetComponent<Material>();
        start = DateTime.Now;

        //set random rotation and velocity
        if (randomVelocityRotation)
        {
            transform.rotation = Random.rotation;
            System.Random r = new System.Random();
            float velocityX = (float)(r.NextDouble() - 0.5) * 50;
            float velocityZ = (float)(r.NextDouble() - 0.5) * 50;
            rb.velocity = new Vector3(velocityX, 3, velocityZ);
        }
    }

    void Update()
    {
        //Raycast hit and draw rays
        if (dieIsStopped())
        {
            //check all directions for rays
            for (int i = 0; i < die.rayDirections.Length; ++i)
            {
                //create + draw ray
                Ray ray = new Ray(transform.position, transform.rotation * die.rayDirections[i]);
                Debug.DrawRay(transform.position, ray.direction, Color.red, 1f, true);
                //Debug.Log(ray);

                //if ray is hitting floor, lock rotation and store globalVariables
                if (Physics.Raycast(ray, out RaycastHit hit, die.rayLength, layersToHit) && die.result == -1)
                {
                    //if (hit.collider.tag == "Floor")
                    //{
                        Debug.Log(string.Concat("Rolled ", i + 1));
                        die.result = i + 1;
                        DiceManager.GetComponent<DiceManager>().UpdateDie(die);

                        savedRotation = transform.rotation;
                        lockRotation = true;
                    //}
                };

            }
        }

        //disable rotation from physics
        if (lockRotation)
        {
            transform.rotation = savedRotation;
        }

        //reset die if cocked
        if (die.result == -1 && DateTime.Now > start.AddSeconds(5) && dieIsStopped())
        {
            //resetDie();
            start = DateTime.Now;
        }

    }

    public void SetDie(Die _die)
    {
        die = _die;
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
    public void printDirections()
    {
        Debug.Log(die.GetDieTypeString());
        foreach (Vector3 v in die.rayDirections) 
        {
            Debug.Log(v);
        }
    }

}
