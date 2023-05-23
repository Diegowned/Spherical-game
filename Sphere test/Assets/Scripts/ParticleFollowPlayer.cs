using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
        target = GameObject.Find("Player/Sphere").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = target.position + offset;
    }
}
