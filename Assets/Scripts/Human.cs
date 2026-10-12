using UnityEngine;
using UnityEngine.InputSystem;


public class Human : MonoBehaviour
{
    [Header("Movement Stat")]
    [SerializeField] float speed;
    [SerializeField] float maxSpeed = 7f;
    [SerializeField] float sprintMulti = 0.15f;
    [SerializeField] float jump = 2f;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.1f;
    [SerializeField] float dashCooldown = 1.5f;
    [SerializeField] float dashCooldownTimer;
    [SerializeField] float dashCounts;
    [SerializeField] float maxDashCounts = 3;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSens = 0.1f;

    public float xRot = 0f;
    public float yRot = 0f;

    CharacterController controller;
    Vector3 playerVelocity;
    Vector3 moveInp;
    float dashTimer = 0f;
    Vector3 dashDirection = Vector3.zero;
    public bool isDashing = false;
     

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        dashCounts = maxDashCounts; 
        speed = maxSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSet();

        playerVelocity.y += gravity * Time.deltaTime;//gravity
        controller.Move(playerVelocity * Time.deltaTime);

        if (isDashing)
        {
            
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
            }
        }

        if (dashCounts < maxDashCounts)
        {
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer >= dashCooldown)
            {
                dashCounts = Mathf.Min(dashCounts + 1f, maxDashCounts);
                dashCooldownTimer = 0f;
            }
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        Vector2 lookInput = context.ReadValue<Vector2>();
        float deltaTime = context.control.device is Mouse ? 1f : Time.deltaTime;
        float mousex = lookInput.x * mouseSens * deltaTime;
        float mousey = lookInput.y * mouseSens * deltaTime;

        xRot -= mousey;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        yRot += mousex;

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
        }
    }

    public void MoveSet()
    {
        if (isDashing)
        {
            return;
        }

        Vector3 move = GetCameraRelativeMoveDirection();
        controller.Move(move * speed * Time.deltaTime);
    }

    Vector3 GetCameraRelativeMoveDirection()
    {
        Vector3 forward = cameraTransform != null ? cameraTransform.forward : transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forward);
        Vector3 move = right * moveInp.x + forward * moveInp.y;
        return Vector3.ClampMagnitude(move, 1f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInp = context.ReadValue<Vector2>();
        Debug.Log($"MoveInp {moveInp}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jumped");
        if(context.performed && controller.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jump * -2f *gravity);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!context.performed || isDashing || dashCounts <= 0f)
        {
            return;
        }

        dashDirection = GetCameraRelativeMoveDirection();
        if (dashDirection.sqrMagnitude <= 0.0001f)
        {
            dashDirection = cameraTransform != null ? cameraTransform.forward : transform.forward;
        }

        dashDirection.y = 0f;
        dashDirection.Normalize();
        dashCounts -= 1f;
        dashTimer = 0f;
        isDashing = true;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed += speed * sprintMulti;
        }
        else
        {
            speed = maxSpeed;
        }
    }
}
