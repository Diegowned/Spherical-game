using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // the speed at which the sphere rolls
    public float jumpForce = 500.0f; // the force applied to the sphere when it jumps
    public float gravity = 10f;
    private Rigidbody rb; // reference to the sphere's rigidbody component
    private Camera mainCamera; // reference to the main camera
    private bool isGrounded = false; // flag to check if the sphere is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) // check if the player has pressed the jump button and the sphere is grounded
        {
            rb.AddForce(Vector3.up * jumpForce); // apply force to the sphere to make it jump
            isGrounded = false; // set the flag to false, since the sphere is no longer grounded
        }
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity);
            isGrounded = false;
        }

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // get input from the player's horizontal movement
        float moveVertical = Input.GetAxis("Vertical"); // get input from the player's vertical movement

        Vector3 cameraForward = Vector3.Scale(mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized; // get the camera's forward direction, without the y component
        Vector3 movement = cameraForward * moveVertical + mainCamera.transform.right * moveHorizontal; // create a vector to represent the movement, relative to the camera

        rb.AddForce(movement * speed * Time.fixedDeltaTime); // apply force to the sphere based on the movement vector
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // check if the sphere has collided with an object tagged as "Ground"
        {
            isGrounded = true; // set the flag to true, since the sphere is now grounded
        }
    }
}
