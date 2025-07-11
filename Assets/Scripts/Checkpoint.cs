using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    public Transform car;
    public Rigidbody carRb;
    //public Image checkpointImage;
    public float checkpointZ;// = 378.7297f;
    public TextMeshProUGUI bestTimeText;
    public int checkpointID;
    private bool checkpointTriggered = false;
    private float lastCheckpointTime;
    public TextMeshProUGUI checkpointTimeText;
    public static bool firstCheckpointPassed = false;

    public GameRestartManager gameRestartManager;
    void Start()
    {
        if (checkpointTimeText != null)
            checkpointTimeText.gameObject.SetActive(false);

        //lastCheckpointTime = PlayerPrefs.GetFloat("LastCheckpointTime", 0f);
    }

    void Update()
    {
        if (!checkpointTriggered && Mathf.Abs(car.position.z - checkpointZ) < 5f)
        {
            if (checkpointID == 1 || firstCheckpointPassed)
            {
                checkpointTriggered = true;

                if (checkpointID == 1)
                    firstCheckpointPassed = true;

                //float currentTime = Time.time;
                //float timeTaken = currentTime - lastCheckpointTime;
                float timeTaken = Time.timeSinceLevelLoad;

                string bestKey = $"BestTime_CP{checkpointID}";
                float bestTime = PlayerPrefs.GetFloat(bestKey, float.MaxValue);
                bool hasBest = PlayerPrefs.HasKey(bestKey);
                bool isNewBest = timeTaken < bestTime || !hasBest;

                if (isNewBest)
                    PlayerPrefs.SetFloat(bestKey, timeTaken);

                bool isFirstCheckpoint = checkpointID == 1;
                StartCoroutine(ShowCheckpointTimeText(timeTaken, isFirstCheckpoint, isNewBest, bestTime));

                if (gameRestartManager != null)
                    gameRestartManager.ResetCheckpointTimer();
            }
        }
    }

    IEnumerator ShowCheckpointTimeText(float timeTaken, bool isFirstCheckpoint, bool isNewBest, float bestTime)
    {
        if (checkpointTimeText != null)
        {
            if (isFirstCheckpoint)
            {
                checkpointTimeText.text = $"Time Taken: {timeTaken:F1}s";
            }
            else if (isNewBest)
            {
                checkpointTimeText.text = $"Best Time Yet! {timeTaken:F1}s";
            }
            else
            {
                checkpointTimeText.text = $"Best Time: {bestTime:F1}s | Time Taken: {timeTaken:F1}s";
            }

            checkpointTimeText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            checkpointTimeText.gameObject.SetActive(false);
        }
    }





    void SaveCheckpointPosition()
    {
        PlayerPrefs.SetFloat("CheckpointX", car.position.x);
        PlayerPrefs.SetFloat("CheckpointY", car.position.y);
        PlayerPrefs.SetFloat("CheckpointZ", car.position.z);
        
    }



}
