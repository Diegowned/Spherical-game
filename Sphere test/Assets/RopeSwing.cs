using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    public Transform player;
    public float swingForce = 5f;
    public float ropeLength = 5f;

    private bool isSwinging = false;
    private Rigidbody playerRigidbody;
    private DistanceJoint2D ropeJoint;

    private void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        ropeJoint = GetComponent<DistanceJoint2D>();
        ropeJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isSwinging)
        {
            StartSwing();
        }
        else if (Input.GetButtonUp("Jump") && isSwinging)
        {
            StopSwing();
        }
    }

    private void StartSwing()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, ropeLength);

        if (hit.collider != null)
        {
            ropeJoint.enabled = true;
            ropeJoint.connectedBody = hit.collider.GetComponent<Rigidbody2D>();
            ropeJoint.distance = Vector2.Distance(player.position, hit.point);

            isSwinging = true;
        }
    }

    private void StopSwing()
    {
        ropeJoint.enabled = false;
        isSwinging = false;
    }

    private void FixedUpdate()
    {
        if (isSwinging)
        {
            Vector2 direction = (ropeJoint.connectedBody.transform.position - player.position).normalized;
            playerRigidbody.AddForce(direction * swingForce);
        }
    }
}

