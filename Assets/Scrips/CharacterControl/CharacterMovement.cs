using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    private bool isMoving =false;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
   
    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // Resetting the default velocity
        if (isGrounded &&velocity.y<0)
        {
            velocity.y = -2f;
        }
        
        // Getting the input
        float x=Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Moving Vector
        Vector3 move=transform.right*x + transform.forward*z;

        // Moving Player
        controller.Move(move *speed*Time.deltaTime);

        // Check jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Falling down
        velocity.y += gravity * Time.deltaTime;

        // Executing the jump and applying gravity
        controller.Move(velocity * Time.deltaTime);

        // Check movement
        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;
    }
}
