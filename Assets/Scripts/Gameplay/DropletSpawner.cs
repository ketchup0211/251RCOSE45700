using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    public GameObject dropletPrefab;
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
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnDroplet();
            timer = 0f;
        }
    }

    void SpawnDroplet()
    {
        GameObject droplet = Instantiate(dropletPrefab, transform.position, Quaternion.identity);

        var script = droplet.GetComponent<WaterDroplet>();
        if (script != null)
        {
            script.spawnPoint = transform;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }
}
