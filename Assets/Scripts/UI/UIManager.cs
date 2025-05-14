using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI tokenText;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI timeText;

    private int tokenCount = 0;
    private int deathCount = 0;
    private float playTime = 0f;

    void Update()
    {
        playTime += Time.deltaTime;
        UpdateUI();
    }

    void UpdateUI()
    {
        tokenText.text = $"Token: {tokenCount}";
        deathText.text = $"Death: {deathCount}";
        timeText.text = $"Time: {playTime:F0}s";
    }

    public void AddToken()
    {
        tokenCount++;
    }

    public void AddDeath()
    {
        deathCount++;
    }
}
