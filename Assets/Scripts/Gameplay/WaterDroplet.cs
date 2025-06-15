using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
    public Transform spawnPoint;       // ← 여기에 추가!
    public float resetDelay = 0.5f;

    private Rigidbody2D rb;
    private bool hasLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasLanded && collision.collider.CompareTag("Ground"))
        {
            hasLanded = true;
            Invoke(nameof(ResetDroplet), resetDelay);
        }
    }

    void ResetDroplet()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            rb.linearVelocity = Vector2.zero;
            hasLanded = false;
        }
        else
        {
            Debug.LogWarning("Spawn Point not assigned to WaterDroplet.");
        }
    }
}
