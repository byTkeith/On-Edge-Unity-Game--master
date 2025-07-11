using UnityEngine;
using System.Collections;

public class DropRockOnProximity : MonoBehaviour
{
    public Transform car;
    public float triggerDistance = 30f;
    public float minDelay = 0.1f;
    public float maxDelay = 0.5f;
    public float resetDelay = 15f;

    private Rigidbody rb;
    private bool hasFallen = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        if (car == null)
        {
            GameObject foundCar = GameObject.FindWithTag("Player");
            if (foundCar != null)
            {
                car = foundCar.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFallen && car != null)
        {
            float distance = Vector3.Distance(transform.position, car.position);
            if (distance <= triggerDistance)
            {
                hasFallen = true;
                float delay = Random.Range(minDelay, maxDelay);
                Invoke(nameof(DropRock), delay);
            }
        }
    }

    void DropRock()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        StartCoroutine(ResetRockAfterDelay());
    }
    IEnumerator ResetRockAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        // Reset position and physics
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        hasFallen = false; // Allow the rock to fall again
    }
}
