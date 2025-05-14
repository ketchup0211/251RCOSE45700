using UnityEngine;
using UnityEngine.UI; // Text 사용 시 필요
using TMPro; // TextMeshProUGUI 사용 시 필요

public class BlinkingText : MonoBehaviour
{
    public float blinkSpeed = 1.0f;
    private Text uiText;
    private TextMeshProUGUI tmpText;
    private bool useTMP = false;

    void Start()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();
        useTMP = tmpText != null;
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, 1f);

        if (useTMP)
        {
            var color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;
        }
        else if (uiText != null)
        {
            var color = uiText.color;
            color.a = alpha;
            uiText.color = color;
        }
    }
}
