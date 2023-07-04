using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class SlamDunk : MonoBehaviour
{
    public float isSlamming;
    public float slammingAirtime = 3;
    public KeyCode stopKey = KeyCode.LeftShift; // the key to stop the sphere's movement
    public float stopSpeed = 10f; // the speed at which the sphere will stop
    public float slamForce = 500f; // the force with which the sphere will slam into the ground
    public PlayerController pc;
    public ParticleSystem shockwaveParticle;
    public AudioSource slamSoundEffect;

    private Rigidbody rb; // reference to the sphere's Rigidbody component

    void Start()
    {
        slamSoundEffect = GameObject.Find("Player/Sphere").GetComponent<AudioSource>();
        shockwaveParticle = GameObject.Find("Player/CFXR2 Ground Hit").GetComponent<ParticleSystem>();
        pc = GameObject.Find("Sphere").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(stopKey) && !pc.isGrounded)

        {
            StopSphere();

        }

        if (isSlamming > 0)
        {
            isSlamming -= Time.deltaTime;
        }
    }

    void StopSphere()
    {
        // stop the sphere's movement by setting its velocity to zero
        rb.velocity = Vector3.zero;

        // apply a force to the sphere in the negative y-direction to make it slam into the ground
        rb.AddForce(Vector3.down * slamForce, ForceMode.Impulse);

        isSlamming = slammingAirtime;

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isSlamming > 0) // check if the sphere has collided with an object tagged as "Ground"
        {
            slamSoundEffect.Play();
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            shockwaveParticle.Play();
            isSlamming = 0;
        }
    }

}


