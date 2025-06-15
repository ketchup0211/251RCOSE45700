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

        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // 플레이어 사망
            Simulation.Schedule<Platformer.Gameplay.PlayerDeath>();
        }

        // 바닥 또는 플레이어와 충돌 시 리셋 시작
        StartCoroutine(ResetDroplet());
    }

    System.Collections.IEnumerator ResetDroplet()
    {
        isResetting = true;

        // 약간의 시간 지연 후 위치 초기화
        yield return new WaitForSeconds(resetDelay);

        // 위치 초기화 및 속도 리셋
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
