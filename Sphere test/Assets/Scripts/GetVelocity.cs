using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetVelocity : MonoBehaviour
{
    private Rigidbody rb;
    public float sphereRadius = 0.5f; // replace 0.5 with the actual radius of the sphere
    public TMP_Text velocityText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;
        float speedInMps = speed / Time.deltaTime;
        float speedInKph = speedInMps * 3.6f; // 3.6 is the conversion factor from m/s to km/h
        float speedInRpm = speed / (2 * Mathf.PI * sphereRadius) * 60f; // calculate speed in revolutions per minute (RPM)
        velocityText.text = "Velocity: " + speedInKph.ToString("F2") + " km/h | " + speedInRpm.ToString("F2") + " RPM";
    }
}





