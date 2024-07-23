using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;

public class DieBehaviour : MonoBehaviour
{
    public LayerMask layersToHit;
    public bool randomVelocityRotation;
    public GameObject dmObj;
    public bool dieIsMoving;
    public Vector3 velocity;
    //public Quaternion quaternion;
    public int dieResult = -1;
    public Vector3 initialPosition;
    //private DieGroup dieGroup;
    private DieGroupBehaviour dieGroupBehaviour;

    private Rigidbody rb;
    private MeshRenderer mr;
    private Material material;
    private static DateTime start;
    private bool lockMovement;
    public Die die;
    public bool collisionOn = true;

    //rays and directions
    private List<Ray> rays = new List<Ray>();

    void Start()
    {
        //init variables
        rb = GetComponent<Rigidbody>();
        start = DateTime.Now;
        GameObject parent = transform.parent.gameObject;
        dieGroupBehaviour = parent.GetComponent<DieGroupBehaviour>();
        Debug.Log(dieGroupBehaviour);


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
        if (DieIsStopped())
        {
            //check all directions for rays
            //Debug.Log(die.rayDirections.Length);
            for (int i = 0; i < die.rayDirections.Length; ++i)
            {
                //create + draw ray
                Ray ray = new Ray(transform.position, transform.rotation * die.rayDirections[i]);
                Debug.DrawRay(transform.position, ray.direction, Color.red, 1f, true);
                //Debug.Log(ray);

                //if ray is hitting floor, lock rotation and store globalVariables
                if (Physics.Raycast(ray, out RaycastHit hit, die.rayLength, layersToHit) && die.result == -1)
                {
                    die.result = i + 1;
                    dieResult = die.result;
                    Debug.Log(string.Concat(die.dieType, " (ID ", die.dieId, ") rolled ", die.result));
                    dieGroupBehaviour.UpdateDie(die);

                    lockMovement = true;
                };

            }
        }

        //disable rotation from physics
        if (lockMovement && die.result != -1)
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void SetVars(Die _die, GameObject _dmObj, Vector3 _initialPosition)
    {
        die = _die;
        dmObj = _dmObj;
        initialPosition = _initialPosition;
    }

    bool DieIsStopped()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.005f && Mathf.Abs(rb.velocity.y) < 0.005f && Mathf.Abs(rb.velocity.z) < 0.005f)
        {
            //Debug.Log(string.Concat(transform.name, "No velocity"));
            dieIsMoving = false;
            return true;
        }
        else
        {
            dieIsMoving = true;
            return false;
        }
    }

    public void SetCollision(bool _collisionOn)
    {
        collisionOn = _collisionOn;
        gameObject.GetComponent<Collider>().enabled = collisionOn;
    }

    public void ToggleCollision()
    {
        collisionOn = !collisionOn;
        if (collisionOn)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

    public void ResetDie()
    {
        Debug.Log("Resetting die");
        transform.rotation = Random.rotation;
        System.Random r = new System.Random();
        float velocityX = (float)(r.NextDouble() - 0.5) * 50;
        float velocityZ = (float)(r.NextDouble() - 0.5) * 50;
        rb.velocity = new Vector3(velocityX, 3, velocityZ);
        transform.position = initialPosition;
    }
    public void printDirections()
    {
        Debug.Log(die.DieTypeToString());
        foreach (Vector3 v in die.rayDirections) 
        {
            Debug.Log(v);
        }
    }

}
