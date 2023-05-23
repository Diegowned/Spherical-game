using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour
{
    public float maxDistance = 10f; // Maximum distance the grapple can reach
    public float grappleSpeed = 1f; // Speed at which the player swings on the grapple
    public LayerMask grappleableLayers; // Layers that the grapple can attach to
    public Color lineColor = Color.white; // Color of the line renderer
    public Image crosshair;

    private Rigidbody playerRigidbody;
    [SerializeField]
    private float distanceGrapple;
    public float mingrappleDistance;
    public float maxgrappleDistance;
    public bool isGrappling = false;
    private Vector3 grapplePoint;
    private SpringJoint grappleJoint;
    private LineRenderer lineRenderer;

    void Start()
    {
        crosshair = GameObject.Find("Canvas/Crosshair").GetComponent<Image>();
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
        Ray ray = Camera.main.ScreenPointToRay(crosshair.rectTransform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, grappleableLayers))
        {
            crosshair.color = Color.green;
        }

        else
        {
            crosshair.color = Color.white;
        }

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


        Ray ray = Camera.main.ScreenPointToRay(crosshair.rectTransform.position);
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

            // Change the crosshair colour to another
            crosshair.color = Color.yellow;

        }
    }

    void StopGrapple()
    {
        isGrappling = false;

        //Change the colour of the crosshair to normal

        // Remove the spring joint component
        Destroy(grappleJoint);

        // Disable the line renderer
        lineRenderer.enabled = false;


        // Change the crosshair colour to another
        crosshair.color = Color.white;
    }
}

