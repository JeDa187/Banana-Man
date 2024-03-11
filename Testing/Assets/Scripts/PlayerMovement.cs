using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    public Transform cameraTransform; // Assign the camera's transform here in the inspector

    private Vector3 movement;
    private Vector3 desiredDirection;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    void Update()
    {
        // Input from the keyboard
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Determine the direction the camera is looking
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0; // Don't affect the y (upward) direction
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Determine movement direction based on camera's perspective
        desiredDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // Smoothly rotate the player to face the direction of movement
        if (desiredDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(desiredDirection.x, desiredDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    void FixedUpdate()
    {
        // Movement based on physics
        rb.MovePosition(rb.position + desiredDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
