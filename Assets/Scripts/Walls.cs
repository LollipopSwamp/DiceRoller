using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    private bool collisionOn = true;
    private Rigidbody rb;
    public GameObject wall0;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;

    public void SetCollision(bool _collisionOn)
    {
        collisionOn = _collisionOn;
        wall0.GetComponent<Collider>().enabled = collisionOn;
        wall1.GetComponent<Collider>().enabled = collisionOn;
        wall2.GetComponent<Collider>().enabled = collisionOn;
        wall3.GetComponent<Collider>().enabled = collisionOn;
    }
}
