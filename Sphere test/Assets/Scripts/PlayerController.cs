using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f; // the speed at which the sphere rolls
    public float jumpForce = 500.0f; // the force applied to the sphere when it jumps
    public float gravity = 10f;
    public float normalGravity;
    public float gravitywhenJumping;
    public float gravitywhenGrappling;
    private Rigidbody rb; // reference to the sphere's rigidbody component
    private Camera mainCamera; // reference to the main camera
    public bool isGrounded = false; // flag to check if the sphere is grounded
    public TMP_Text playerStateText;
    public GrapplingHook grapplingHookScript;
    private GameObject slamImage;

    [SerializeField]
    private PlayerState currentState = PlayerState.Idle;


    public enum PlayerState
    {
        Idle,
        Running,
        Jumping,
        Falling,
        Grappling,
    }
    void Start()
    {
        slamImage = GameObject.Find("Canvas/Controls On Screen/Slam");
        rb = GetComponent<Rigidbody>();
        playerStateText = GameObject.Find("Canvas/Player State Text").GetComponent<TMP_Text>();
        mainCamera = Camera.main;
        grapplingHookScript = GetComponent<GrapplingHook>();
    }


    void Update()
    {

        playerStateText.text = currentState.ToString();

        if (Input.GetButtonDown("Jump") && isGrounded) // check if the player has pressed the jump button and the sphere is grounded
        {
            rb.AddForce(Vector3.up * jumpForce); // apply force to the sphere to make it jump
        }
        if (!isGrounded)
        {
            slamImage.gameObject.SetActive(true);
            rb.AddForce(Vector3.down * gravity);
            isGrounded = false;
        }
        if(isGrounded)
        {
            slamImage.gameObject.SetActive(false);
        }

        switch (currentState)
        {
            case PlayerState.Idle:


                //Changes gravity to normal
                gravity = normalGravity;

                if (isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    currentState = PlayerState.Running;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentState = PlayerState.Jumping;
                }
                if (!isGrounded)
                {
                    currentState = PlayerState.Falling;
                }

                if (grapplingHookScript.isGrappling)
                {
                    currentState = PlayerState.Grappling;
                }


                break;

            case PlayerState.Running:

                //Changes gravity to normal
                gravity = normalGravity;

                if (isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    currentState = PlayerState.Running;
                }

                else
                {
                    currentState = PlayerState.Idle;
                }
                if (!isGrounded)
                {
                    currentState = PlayerState.Falling;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    currentState = PlayerState.Jumping;
                }

                if (grapplingHookScript.isGrappling)
                {
                    currentState = PlayerState.Grappling;
                }


                break;

            case PlayerState.Jumping:
                
                //Changes the gravity while jumping for a better feel of physics
                gravity = gravitywhenJumping;

                if (Input.GetKey(KeyCode.Space))
                {
                    currentState = PlayerState.Jumping;
                }
                if (!isGrounded)
                {
                    currentState = PlayerState.Falling;
                }

                if (grapplingHookScript.isGrappling)
                {
                    currentState = PlayerState.Grappling;
                }

                break;

            case PlayerState.Falling:

                //Changes the gravity while jumping for a better feel of physics
                gravity = gravitywhenJumping;

                if (isGrounded)
                {
                    currentState = PlayerState.Idle;
                }

                if (!isGrounded)
                {
                    currentState = PlayerState.Falling;
                }

                if (isGrounded && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    currentState = PlayerState.Running;
                }

                if (grapplingHookScript.isGrappling)
                {
                    currentState = PlayerState.Grappling;
                }

                break;

            case PlayerState.Grappling:

                //Changes the gravity while jumping so the grapple does not break

                gravity = gravitywhenGrappling;

                if (!grapplingHookScript.isGrappling)
                {
                    currentState = PlayerState.Idle;
                }

                if (!grapplingHookScript.isGrappling && !isGrounded)
                {
                    currentState = PlayerState.Falling;
                }

                if (!grapplingHookScript.isGrappling && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                {
                    currentState = PlayerState.Running;
                }

                break;
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // check if the sphere has collided with an object tagged as "Ground"
        {
            isGrounded = true; // set the flag to true, since the sphere is now grounded
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        {
            if (collision.gameObject.CompareTag("Ground")) // check if the sphere has collided with an object tagged as "Ground"
            {
                isGrounded = false; // set the flag to true, since the sphere is now grounded
            }
        }
    }
}   
