using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float maxDistance = 10f; // Maximum distance the grapple can reach
    public float grappleSpeed = 1f; // Speed at which the player swings on the grapple
    public LayerMask grappleableLayers; // Layers that the grapple can attach to
    public Color lineColor = Color.white; // Color of the line renderer

    private Rigidbody playerRigidbody;
    [SerializeField]
    private float distanceGrapple;
    public float mingrappleDistance;
    public float maxgrappleDistance;
    private bool isGrappling = false;
    private Vector3 grapplePoint;
    private SpringJoint grappleJoint;
    private LineRenderer lineRenderer;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material.color = lineColor;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isGrappling)
        {
            StartGrapple();
        }
        else if (Input.GetButtonUp("Fire1") && isGrappling)
        {
            StopGrapple();
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            // Calculate the direction from the player to the grapple point
            Vector3 grappleDirection = (grapplePoint - transform.position).normalized;
            distanceGrapple = Vector3.Distance(grapplePoint, transform.position);

            distanceGrapple = Mathf.Clamp(distanceGrapple, mingrappleDistance, maxgrappleDistance);

            // Apply force to the player to swing on the grapple
            playerRigidbody.AddForce(grappleDirection * grappleSpeed, ForceMode.Acceleration);

            // Adjust the grapple joint's anchor position to match the grapple point
            grappleJoint.anchor = transform.InverseTransformPoint(grapplePoint);

            // Update the line renderer to show the spring joint
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);

            if(distanceGrapple > maxgrappleDistance)
            {
                

                Vector3 newPlayerPosition = grapplePoint + grappleDirection * distanceGrapple;
                playerRigidbody.MovePosition(newPlayerPosition);


            }


        }
    }

    void StartGrapple()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, grappleableLayers))
        {
            isGrappling = true;
            grapplePoint = hit.point;

            // Create a spring joint between the player and the grapple point
            grappleJoint = gameObject.AddComponent<SpringJoint>();
            grappleJoint.autoConfigureConnectedAnchor = false;
            grappleJoint.connectedAnchor = grapplePoint;
            grappleJoint.spring = 1f;
            grappleJoint.damper = 1f;
            grappleJoint.tolerance = 1f;
            grappleJoint.maxDistance = 0f;
            grappleJoint.minDistance = 0f;
            grappleJoint.massScale = 0f;

            // Enable the line renderer to show the spring joint
            lineRenderer.enabled = true;
        }
    }

    void StopGrapple()
    {
        isGrappling = false;

        // Remove the spring joint component
        Destroy(grappleJoint);

        // Disable the line renderer
        lineRenderer.enabled = false;
    }
}

