using UnityEngine;
using TMPro;
using Platformer.Mechanics;

public class IntroManager : MonoBehaviour
{
    public GameObject introPanel;
    public PlayerController player;

    private bool introCleared = false;
    private bool controlEnabled = false;

    void Start()
    {
        if (player != null)
            player.controlEnabled = false;
    }

    void Update()
    {
        if (!introCleared && Input.anyKeyDown)
        {
            // 첫 키 입력 → UI 제거, 조작은 아직 비활성화
            introPanel.SetActive(false);
            introCleared = true;
        }
        else if (introCleared && !controlEnabled && Input.anyKeyDown)
        {
            // 두 번째 키 입력 → 조작 가능
            if (player != null)
                player.controlEnabled = true;

            controlEnabled = true;
        }
    }
}
