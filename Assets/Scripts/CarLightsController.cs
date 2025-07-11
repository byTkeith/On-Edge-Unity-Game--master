using UnityEngine;

public class CarLightsController : MonoBehaviour
{
    public Light headlightLeft, headlightRight;
    public Light tailLightLeft, tailLightRight;
    public Light brakeLightLeft, brakeLightRight;
    public Light reverseLightLeft, reverseLightRight;

    public KeyCode headlightToggleKey = KeyCode.L;
    public Rigidbody carRigidbody;
    public float brakeThreshold = -1f;

    private bool headlightsOn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(headlightToggleKey))
        {
            headlightsOn = !headlightsOn;
            headlightLeft.enabled = headlightsOn;
            headlightRight.enabled = headlightsOn;
            tailLightLeft.enabled = headlightsOn;
            tailLightRight.enabled = headlightsOn;
        }

        // Brake lights
        float brakeInput = Input.GetAxis("Vertical");
        bool isBraking = brakeInput < brakeThreshold;

        brakeLightLeft.enabled = isBraking;
        brakeLightRight.enabled = isBraking;

        // Reverse lights
        bool isReversing = carRigidbody.linearVelocity.z < -0.1f;
        reverseLightLeft.enabled = isReversing;
        reverseLightRight.enabled = isReversing;

    }
}
