using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemRotation : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        playerTransform = GameObject.Find("Sphere").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        // Set the inverse rotation of the player to the parent GameObject
        transform.rotation = Quaternion.Inverse(playerTransform.rotation);
    }
}

