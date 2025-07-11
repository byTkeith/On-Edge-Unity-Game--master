using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
    public AudioSource engineAudio;
    public Rigidbody carRb;

    public float minPitch = 0.7f;
    public float maxPitch = 2.0f;
    public float pitchLerpSpeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (engineAudio != null)
            engineAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (engineAudio == null || carRb == null) return;

        float speed = carRb.linearVelocity.magnitude;

        // Normalize speed to pitch range
        float targetPitch = Mathf.Lerp(minPitch, maxPitch, speed / 50f); // 50f = max speed estimate
        engineAudio.pitch = Mathf.Lerp(engineAudio.pitch, targetPitch, Time.deltaTime * pitchLerpSpeed);
        engineAudio.volume = speed < 1f ? 1f : 0.6f;
    }
}
