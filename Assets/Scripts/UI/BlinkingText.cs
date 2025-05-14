using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public float blinkSpeed = 1.5f; // 깜빡이는 속도 (1.5 정도 추천)

    private TextMeshProUGUI tmpText;
    private Color originalColor;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        originalColor = tmpText.color;
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }
}
