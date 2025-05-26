using UnityEngine;
using Platformer.Mechanics;
using Platformer.Core;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            // 플레이어 사망 이벤트 트리거
            Simulation.Schedule<Platformer.Gameplay.PlayerDeath>();
        }
    }
}
