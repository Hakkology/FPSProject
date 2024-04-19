using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 8.0f;
    [SerializeField] private float mouseSensitivity = 300f;

    private float gravity = -9.81f;
    private float xRotation;
    private float speed;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        Move();
        RotateView();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotateView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");


        Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        controller.Move(move * speed * Time.deltaTime);
        float movementSpeed = new Vector3(xMovement, 0, zMovement).magnitude;


        if (movementSpeed == 0)
        {
            Idle(); 
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Run(); 
        }
        else
        {
            Walk(); 
        }
    }

    private void Walk()
    {
        speed = walkSpeed;
    }

    private void Run()
    {
        speed = runSpeed;
    }

    private void Idle()
    {
        speed = 0;
    }

    private void Jump(){
        velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity); 
    }
}