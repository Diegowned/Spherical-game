using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint; // The starting point of the platform
    public Transform endPoint;   // The ending point of the platform
    public float speed = 1f;     // The speed at which the platform moves
    public float pauseTime = 1f; // The time the platform waits before moving back

    private Vector3 currentTarget;   // The current target the platform is moving towards
    private bool movingToEnd = true; // Whether the platform is moving towards the end point or not
    private float timer = 0f;        // Timer for pause time

    void Start()
    {
        currentTarget = endPoint.position; // Start moving towards the end point
    }

    void FixedUpdate()
    {
        // Move the platform towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);

        // If the platform reaches the current target, switch targets and pause for a moment
        if (transform.position == currentTarget)
        {
            if (movingToEnd)
            {
                currentTarget = startPoint.position;
            }
            else
            {
                currentTarget = endPoint.position;
            }

            movingToEnd = !movingToEnd;
            timer = pauseTime;
        }

        // If the platform is paused, decrement the timer
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
    }
}

