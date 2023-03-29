using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTopTransform : MonoBehaviour
{
    public GameObject ball; // the transform of the sphere object

    void Update()
    {
        Vector3 pos = ball.transform.position;
        transform.position = pos;
    }
}

