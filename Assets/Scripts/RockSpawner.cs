using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public float spawnInterval = 1.5f;
    public float spawnWidth = 10f; // How wide the rocks should be spread
    public float spawnLength = 100f; // How far along the road rocks should fall
    public Vector3 spawnAreaCenter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRock", 1f, spawnInterval);
    }

    // Update is called once per frame
    void SpawnRock()
    {
        Vector3 randomPosition = new Vector3(
            spawnAreaCenter.x + Random.Range(-spawnWidth / 2, spawnWidth / 2),
            spawnAreaCenter.y,
            spawnAreaCenter.z + Random.Range(-spawnLength / 2, spawnLength / 2)
        );

        GameObject rock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);

        // Optional: Vary rock size
        float scale = Random.Range(0.5f, 1.5f);
        rock.transform.localScale = Vector3.one * scale;
    }
}
