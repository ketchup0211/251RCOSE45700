using UnityEngine;
using Platformer.Mechanics;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel;
    public PlayerController player;

    private bool gameStarted = false;

    void Start()
    {
        // 플레이어 조작 비활성화
        if (player != null)
            player.controlEnabled = false;
    }

    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            gameStarted = true;

            // 인트로 UI 제거
            if (introPanel != null)
                introPanel.SetActive(false);

            // 플레이어 조작 활성화
            if (player != null)
                player.controlEnabled = true;
        }
    }
}
