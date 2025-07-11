using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    private GameRestartManager restartManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartManager = FindObjectOfType<GameRestartManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Rigidbody rockRb = collision.rigidbody;
            if (rockRb != null)
            {
                // Check the vertical (Y) speed of the rock
                float fallingSpeed = rockRb.linearVelocity.y;

                Debug.Log("Rock Y velocity: " + fallingSpeed);

                // Only restart if the rock is falling fast enough
                if (fallingSpeed <= -9.8f)
                {
                    restartManager.ShowCrashImageAndRestart();
                }
            }
            //restartManager.ShowCrashImageAndRestart();
        }
    }
    // Update is called once per frame
    
}
