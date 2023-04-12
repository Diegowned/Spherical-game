using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateUITexture : MonoBehaviour
{
    public float rotationSpeed;
    public GetVelocity velocityScript;
    public float rotationDamp;

    private void Start()
    {
        velocityScript = FindObjectOfType<GetVelocity>();
    }

    void FixedUpdate()
    {
        float rotationSpeed = velocityScript.speedForUI;
        transform.Rotate(0f, 0f, rotationSpeed * rotationDamp);
    }
}

