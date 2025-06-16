using UnityEngine;

public class DropletSpawner : MonoBehaviour
{
    public WaterDroplet dropletInstance;  // 드래그해서 연결
    public float spawnInterval = 1f;
    public bool autoStart = false;

    private float timer = 0f;
    private bool isSpawning = false;

    void Start()
    {
        if (autoStart)
        {
            isSpawning = true;
            dropletInstance.gameObject.SetActive(true); // 자동 시작이면 보이게
        }
        else
        {
            dropletInstance.gameObject.SetActive(false); // 시작 시 안 보이게
        }
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
        dropletInstance.gameObject.SetActive(true); // 여기서도 다시 보이게
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

        if (dropletInstance != null)
        {
            dropletInstance.gameObject.SetActive(true); // ✅ 참새가 날기 시작했을 때 보이게
        }
    }
}
