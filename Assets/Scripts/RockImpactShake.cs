using UnityEngine;

public class RockImpactShake : MonoBehaviour
{
    public float shakeTriggerDistance = 50f;
    public float shakeDuration = 0.4f;
    public float shakeMagnitude = 0.1f;
    private AudioSource audioSource;

    private bool hasLanded = false;
    //private bool hasLanded = false;
    public Transform car;
    public CameraShake cameraShake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (car == null)
            car = GameObject.FindWithTag("Player")?.transform;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasLanded) return;

        hasLanded = true;
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
        if (car != null && cameraShake != null)
        {
            float distance = Vector3.Distance(transform.position, car.position);

            if (distance <= shakeTriggerDistance)
            {
                cameraShake.TriggerShake(shakeDuration, shakeMagnitude);
            }
        }
    }

    
}
