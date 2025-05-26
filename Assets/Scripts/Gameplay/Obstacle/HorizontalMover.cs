using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public float distance = 3f;
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + new Vector3(offset, 0f, 0f);
    }
}
