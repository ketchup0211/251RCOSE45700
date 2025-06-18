using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HiddenObstacleRevealer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool revealed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // 시작 시 숨기기
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!revealed && other.CompareTag("Player"))
        {
            spriteRenderer.enabled = true; // 닿으면 보이게
            revealed = true;
        }
    }
}
