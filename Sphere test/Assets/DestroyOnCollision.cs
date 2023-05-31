using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject drillPrefab;
    public ParticleSystem drillExplosion;

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the prefab instance upon collision with any object
        Destroy(drillPrefab);
        drillExplosion.Play();
    }
}

