using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private Camera mainCamera;

    private float gravity = -9.81f;
    
    private float xRotation;
    private float speed;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isZoomed = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main; 
        mainCamera.fieldOfView = playerData.normalFOV; 
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
        HandleZoom();
    }

    private void HandleZoom()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            isZoomed = !isZoomed; 
        }

        float targetFOV = isZoomed ? playerData.zoomedFOV : playerData.normalFOV;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * playerData.zoomSpeed);
    }

    private void RotateView()
    {
        float mouseX = Input.GetAxis("Mouse X") * playerData.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * playerData.mouseSensitivity * Time.deltaTime;

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
        speed = Input.GetKey(KeyCode.LeftShift) ? playerData.runSpeed : playerData.walkSpeed;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Jump() => velocity.y += Mathf.Sqrt(playerData.jumpHeight * -2f * gravity);
}
