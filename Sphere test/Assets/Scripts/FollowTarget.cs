using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        target = GameObject.Find("Player/Sphere").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = target.position;
    }

}
