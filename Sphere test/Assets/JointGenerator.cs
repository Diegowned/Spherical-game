using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointGenerator : MonoBehaviour
{
    public float maxDistance = 100f; // Maximum distance of raycast

    private GameObject player;
    private Rigidbody playerRigidbody;
    private HingeJoint currentJoint;

    void Start()
    {
        player = gameObject; // Assign player GameObject to script
        playerRigidbody = player.GetComponent<Rigidbody>(); // Get Rigidbody component of player
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position in the direction of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits a collider within the maximum distance, generate a joint
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                GameObject hitObject = hit.collider.gameObject;

                // Create a Hinge Joint component on the player and attach it to the hit object
                currentJoint = player.AddComponent<HingeJoint>();
                currentJoint.connectedBody = hitObject.GetComponent<Rigidbody>();

                // Set the anchor and connectedAnchor of the joint to the hit point on the player and object, respectively
                currentJoint.anchor = player.transform.InverseTransformPoint(hit.point);
                currentJoint.connectedAnchor = hitObject.transform.InverseTransformPoint(hit.point);

                // Enable the joint and set its limits
                currentJoint.useLimits = true;
                JointLimits limits = new JointLimits();
                limits.min = -45f;
                limits.max = 45f;
                currentJoint.limits = limits;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Destroy the current joint when the mouse button is released
            Destroy(currentJoint);
        }
    }
}

