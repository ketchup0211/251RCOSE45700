using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    public WaterDroplet dropletInstance;  // 직접 연결
    public float spawnInterval = 1f;
    public bool autoStart = false;

    private float timer = 0f;
    private bool isSpawning = false;

    void Start()
    {
        if (autoStart)
            isSpawning = true;
    }

    void Update()
    {
        if (!isSpawning || dropletInstance == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            ResetDroplet();
            timer = 0f;
        }
    }

    void ResetDroplet()
    {
        dropletInstance.transform.position = transform.position;
        dropletInstance.spawnPoint = transform;

        var rb = dropletInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }
}
