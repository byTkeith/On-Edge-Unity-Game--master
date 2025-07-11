using UnityEngine;
using UnityEngine.UI;

public class SpeedometerNeedle : MonoBehaviour
{
    public Rigidbody carRb;
    public RectTransform needleTransform;

    public float maxSpeed = 200f;
    public float minAngle = 45f;     // Needle angle at 0 speed
    public float maxAngle = -220f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = carRb.linearVelocity.magnitude * 3.6f; // m/s to km/h
        speed = Mathf.Clamp(speed, 0f, maxSpeed);

        float angle = Mathf.Lerp(minAngle, maxAngle, speed / maxSpeed);
        needleTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
