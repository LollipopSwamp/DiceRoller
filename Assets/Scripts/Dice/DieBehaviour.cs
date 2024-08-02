using System.Collections;
using DateTime = System.DateTime;
using System.Collections.Generic;
using UnityEngine;

public class DieBehaviour : MonoBehaviour
{
    public LayerMask layersToHit;
    public bool randomVelocityRotation = true;
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
    public float rayScale = 1f;

    public GameObject raycasting; 

    void Start()
    {
        //init variables
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        start = DateTime.Now;
        GameObject parent = transform.parent.gameObject;
        dieGroupBehaviour = parent.GetComponent<DieGroupBehaviour>();
        //raycasting = gameObject.transform.GetChild(0).gameObject;


        //set random rotation and velocity
        if (randomVelocityRotation)
        {
            transform.rotation = Random.rotation;
            System.Random r = new System.Random();
            float velocityX = (float)(r.NextDouble() - 0.5) * 50;
            float velocityZ = (float)(r.NextDouble() - 0.5) * 50;
            rb.velocity = new Vector3(velocityX, 3, velocityZ);
        }
        MoveToStartPosition();
    }

    public void SetVars(Die _die, GameObject _dmObj, Vector3 _initialPosition, float _rayScale)
    {
        die = _die;
        dmObj = _dmObj;
        initialPosition = _initialPosition;
    }

    public bool DieIsStopped()
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

    public void SetResult(int _result)
    {
        die.result = _result;
        dieGroupBehaviour.UpdateDie(die);
        SetKinematic(true);
    }
    public void SetKinematic(bool _kinematic)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = _kinematic;
    }

    public void SetCollision(bool _collisionOn)
    {
        collisionOn = _collisionOn;
        gameObject.GetComponent<Collider>().enabled = collisionOn;
    }

    public void MoveToStartPosition()
    {
        Vector3 offset = transform.rotation * raycasting.transform.localPosition * transform.localScale.x;
        transform.position = initialPosition - offset;
    }

    public void SetTransparency(bool _transparent)
    {
        Color originalColor = mr.material.color;
        Color opaqueColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.25f);
        if (_transparent) { mr.material.color = transparentColor; }
        else { mr.material.color = opaqueColor; }
    }

    public void ResetDie()
    {
        //Debug.Log("Resetting die");
        transform.rotation = Random.rotation;
        System.Random r = new System.Random();
        float velocityX = (float)(r.NextDouble() - 0.5) * 50;
        float velocityZ = (float)(r.NextDouble() - 0.5) * 50;
        rb.velocity = new Vector3(velocityX, 3, velocityZ);
        MoveToStartPosition();
        SetTransparency(false);
        SetKinematic(false);
    }
}
