using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [Header("Rock Settings")]
    public GameObject rockPrefab;

    [Header("Spawn Area")]
    public Vector3 spawnAreaCenter = new Vector3(152.6f, 70f, 130f); // Adjust to middle of your road
    public float spawnWidth = 6f;    // Side-to-side (X-axis)
    public float spawnLength = 150f; // Forward-back (Z-axis)

    [Header("Spawn Timing")]
    public float spawnInterval = 1.5f;
    public int rocksPerSpawn = 3; // Number of rocks per wave

    [Header("Rock Size")]
    public float minScale = 2.5f;
    public float maxScale = 4f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);
    }

    // Update is called once per frame
    void SpawnRock()
    {
        for (int i = 0; i < rocksPerSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(
                spawnAreaCenter.x + Random.Range(-spawnWidth / 2f, spawnWidth / 2f),
                spawnAreaCenter.y,
                spawnAreaCenter.z + Random.Range(-spawnLength / 2f, spawnLength / 2f)
            );

            GameObject rock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);

            float scale = Random.Range(minScale, maxScale);
            rock.transform.localScale = Vector3.one * scale;

            Rigidbody rb = rock.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.mass = scale * 2f;
            }

            Destroy(rock, 15f); // Auto-destroy to prevent clutter
        }
    }

    // 🔍 Visualize spawn area in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnAreaCenter, new Vector3(spawnWidth, 1f, spawnLength));
    }
}

