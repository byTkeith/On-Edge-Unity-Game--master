using UnityEngine;

public class CarMobileController : MonoBehaviour
{
    public float turnSpeed = 100f;
    public float moveForce = 15f;
    public Rigidbody rb;

    private float moveInput = 0f;
    private float turnInput = 0f;
    private bool handbrakeInput = false;

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime));
        }
    }

    // Button event methods for UI
    public void TurnLeftDown() => turnInput = -1f;
    public void TurnRightDown() => turnInput = 1f;
    public void TurnUp() => turnInput = 0f;
    public void AccelerateDown() => moveInput = moveForce;
    public void BrakeDown() => moveInput = -moveForce / 2f;
    public void StopMoving() => moveInput = 0f;
    public void HandbrakeDown() => handbrakeInput = true;
    public void HandbrakeUp() => handbrakeInput = false;

    // Methods required by ArcadeCar script
    public float GetMobileVerticalInput()
    {
        // Convert moveInput to a normalized value (-1 to 1)
        // Positive values for forward, negative for reverse
        if (moveInput > 0)
            return 1f;  // Forward
        else if (moveInput < 0)
            return -1f; // Reverse
        else
            return 0f;  // No input
    }

    public float GetMobileHorizontalInput()
    {
        // Return the current turn input (-1 for left, 1 for right, 0 for straight)
        return turnInput;
    }

    public bool GetMobileHandbrake()
    {
        // Return the current handbrake state
        return handbrakeInput;
    }
}