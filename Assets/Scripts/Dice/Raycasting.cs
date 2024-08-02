using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public LayerMask layersToHit;

    public Vector3[] rayDirections;
    public float rayScale = 1f;
    private GameObject parent;
    private DieBehaviour dieB;

    public int result = -1;

    void Start()
    {
        parent = transform.parent.gameObject;
        dieB = transform.parent.gameObject.GetComponent<DieBehaviour>();
        rayDirections = dieB.die.rayDirections;
        result = -1;
    }
    void Update()
    {
        if (dieB.DieIsStopped())
        {
            for (int i = 0; i < rayDirections.Length; ++i)
            {
                //create + draw ray
                Vector3 rayDirection = parent.transform.rotation * rayDirections[i] * rayScale;
                Debug.DrawRay(transform.position, rayDirection, Color.red, 1f, true);

                //if ray is hitting floor, lock rotation and store globalVariables
                if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, rayScale, layersToHit) && result == -1)
                {
                    result = i + 1;
                    dieB.SetResult(result);
                };
            }
        }
    }
}
