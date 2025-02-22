using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header(" References : ")]
    [SerializeField] Transform cameraParent;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    public  Camera playerCamera;

    [Header(" Settings : ")]
    [SerializeField] float moveSpeed;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float jumpHeight;
    [SerializeField] float rotationLerp;
    float sprintMultiplier = 1;

    [HideInInspector] public float mouseX, mouseY;
    float movementX, movementZ;
    float gravity = -20f;
    Vector3 velocity;

    [SerializeField] bool enableMovement = true;
    [SerializeField] bool crouch;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        LockCursor();
        Disable();
    }

    void FixedUpdate()
    {
        if(enableMovement)
        {
            GroundMovement();
            Turn();
        }
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    void GroundMovement()
    {
        if(IsGrounded() && velocity.y < 0) velocity.y = -2f;

        Vector3 move = transform.right * movementX + transform.forward * movementZ;
        controller.Move(move * moveSpeed * Time.fixedDeltaTime * sprintMultiplier);
        
        velocity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    void Turn()
    {
        Quaternion transform_Rot = Quaternion.Euler(transform.eulerAngles.x, mouseY, transform.eulerAngles.z);
        Quaternion camera_Rot = Quaternion.Euler(mouseX, cameraParent.transform.localEulerAngles.y, cameraParent.transform.localEulerAngles.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, transform_Rot, rotationLerp);
        cameraParent.transform.localRotation = Quaternion.Lerp(cameraParent.transform.localRotation, camera_Rot, rotationLerp);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void TryJump()
    {
        if(!IsGrounded() || crouch) return;
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    
    public void SprintBegin()
    {
        sprintMultiplier = 1.5f;
        playerCamera.DOFieldOfView(70f, 0.2f);
    }

    public void SprintEnd()
    {
        sprintMultiplier = 1f;
        playerCamera.DOFieldOfView(60f, 0.2f);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
    }

    public void SetInput(Vector2 moveInput, Vector2 lookInput)
    {
        if(!enableMovement) return;

        movementX = moveInput.x;
        movementZ = moveInput.y;

        mouseX -= lookInput.x * mouseSensitivity * Time.fixedDeltaTime;
        mouseY += lookInput.y * mouseSensitivity * Time.fixedDeltaTime;
        mouseX = Mathf.Clamp(mouseX, -70, 80);
    }

    public void SetPositionAndRotation(Vector3 pos, Vector2 rot)
    {
        Disable();
        controller.enabled = false;

        mouseX = rot.x;
        mouseY = rot.y;

        transform.position = pos;
        transform.rotation = Quaternion.Euler(0f, rot.y, 0);
        cameraParent.transform.localRotation = Quaternion.Euler(rot.x, 0f, 0f);

        Enable();
        controller.enabled = true;
    }

    public void SetInitial()
    {
        float cameraAngle = (cameraParent.eulerAngles.x > 180f) ? cameraParent.eulerAngles.x - 360f : cameraParent.eulerAngles.x;
        SetPositionAndRotation(transform.position, new Vector2(cameraAngle, transform.eulerAngles.y));
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Enable()
    {
        enableMovement = true;
    }

    public void Disable()
    {
        enableMovement = false;
    }
}
