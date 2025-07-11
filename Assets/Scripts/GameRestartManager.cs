using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameRestartManager : MonoBehaviour
{
    public Transform car;
    public Image crashImage;
    public float fallYThreshold = 50f;
    //public Image crashImage;
    private bool gameEnded = false;
    public float crashScreenDuration = 4f;
    public float timeLimitPerCheckpoint = 50f;
    private float checkpointTimer;
    private bool timerRunning = true;
    //public float nextCheckpointZ = 378.7297f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI checkpointTimeText;

    private bool hasCheckpoint = false;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        // Check if a checkpoint exists (we check for one coordinate)
        hasCheckpoint = PlayerPrefs.HasKey("CheckpointZ");
        if (crashImage != null)
            crashImage.gameObject.SetActive(false);
        checkpointTimer = timeLimitPerCheckpoint;
        timerRunning = true;
        Checkpoint.firstCheckpointPassed = false;
        //Checkpoint.firstCheckpointPassed = false;
    }

    void Update()
    {
        if (!gameEnded && car != null)
        {
            // Check fall off cliff
            if (car.position.y < fallYThreshold)
            {
                ShowCrashImageAndRestart();
            }

           

            // Timer logic
            if (timerRunning)
            {
                checkpointTimer -= Time.deltaTime;

                // Check if player reached checkpoint Z before time ran out
               

                if (timerText != null)
                {
                    int displayTime = Mathf.CeilToInt(checkpointTimer);
                    timerText.text = "Time Left: " + displayTime.ToString() + "s";
                    //timerText.color = (checkpointTimer <= 10f) ? Color.red : Color.white;
                    if (checkpointTimer <= 10f)
                        timerText.color = Color.red;
                    else
                        timerText.color = Color.white;
                }

                if (checkpointTimer <= 0f)
                {
                 
                    ShowCrashImageAndRestart();
                }
            }
        }
    }
    public void ResetCheckpointTimer()
    {
        checkpointTimer = timeLimitPerCheckpoint;
        timerRunning = true;
    }

    public void ShowCrashImageAndRestart()
    {
        gameEnded = true;

        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; // freeze movement during crash screen
        }


        if (crashImage != null)
            crashImage.gameObject.SetActive(true);
        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (checkpointTimeText != null)
            checkpointTimeText.gameObject.SetActive(false);

        if (hasCheckpoint)
            
        Invoke(nameof(RestartFromCheckpoint), crashScreenDuration);
        else
            
        Invoke(nameof(RestartGame), crashScreenDuration);
        Invoke(nameof(ShowUIAfterCrash), crashScreenDuration);
    }
    private void ShowUIAfterCrash()
    {
        if (timerText != null)
            timerText.gameObject.SetActive(true);

        if (checkpointTimeText != null)
            checkpointTimeText.gameObject.SetActive(false); // only re-enable if needed manually later
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    public void RestartFromCheckpoint()
    {
        float x = PlayerPrefs.GetFloat("CheckpointX", car.position.x);
        float y = PlayerPrefs.GetFloat("CheckpointY", car.position.y + 3f); // slight lift
        float z = PlayerPrefs.GetFloat("CheckpointZ", car.position.z);

        // Move car to saved checkpoint position
        car.position = new Vector3(x, y, z);

        // Optional: Reset car rotation and motion
        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.rotation = Quaternion.identity;
            rb.isKinematic = false;
        }

        if (crashImage != null)
            crashImage.gameObject.SetActive(false);

        gameEnded = false;
    }
}
