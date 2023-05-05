using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamDunk : MonoBehaviour
{
    public KeyCode stopKey = KeyCode.LeftShift; // the key to stop the sphere's movement
    public float stopSpeed = 10f; // the speed at which the sphere will stop
    public float slamForce = 500f; // the force with which the sphere will slam into the ground

    private Rigidbody rb; // reference to the sphere's Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(stopKey))
        {
            StopSphere();
        }
    }

    void StopSphere()
    {
        // stop the sphere's movement by setting its velocity to zero
        rb.velocity = Vector3.zero;

        // apply a force to the sphere in the negative y-direction to make it slam into the ground
        rb.AddForce(Vector3.down * slamForce, ForceMode.Impulse);
    }

}

