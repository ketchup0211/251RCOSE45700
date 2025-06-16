using UnityEngine;
using Platformer.Mechanics;
using Platformer.Core;

[RequireComponent(typeof(Collider2D))]
public class WaterDroplet : MonoBehaviour
{
    public Transform spawnPoint;
    public float resetDelay = 0.5f;

    private bool isResetting = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isResetting) return;

        // ✅ Ground에 닿은 경우
        if (other.CompareTag("Ground"))
        {
            StartCoroutine(ResetDroplet());
            return;
        }

        // ✅ 플레이어에 닿은 경우
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Simulation.Schedule<Platformer.Gameplay.PlayerDeath>();
            StartCoroutine(ResetDroplet()); // 사망 후에도 droplet reset
        }
    }

    System.Collections.IEnumerator ResetDroplet()
    {
        isResetting = true;

        yield return new WaitForSeconds(resetDelay);

        transform.position = spawnPoint.position;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        isResetting = false;
    }
}
