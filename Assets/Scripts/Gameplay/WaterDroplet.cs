using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
    public Transform spawnPoint;  // 처음 위치 (Spawer가 할당해줌)
    public float resetDelay = 0.5f; // 다시 떨어지기까지 시간

    private Rigidbody2D rb;
    private Collider2D coll;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 물리 및 충돌 일시 비활성화
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            coll.enabled = false;

            // 잠시 후 초기 위치로 되돌림
            Invoke(nameof(ResetDroplet), resetDelay);
        }
    }

    void ResetDroplet()
    {
        transform.position = spawnPoint.position;
        rb.isKinematic = false;
        coll.enabled = true;
    }
}
