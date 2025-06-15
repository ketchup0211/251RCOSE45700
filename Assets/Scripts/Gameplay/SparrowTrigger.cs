using UnityEngine;

public class SparrowTrigger : MonoBehaviour
{
    public Transform player;             // 플레이어 트랜스폼
    public float triggerDistance = 3f;   // 반응 거리

    private Animator animator;
    private bool hasFlown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!hasFlown && Mathf.Abs(player.position.x - transform.position.x) < triggerDistance)
        {
            animator.SetTrigger("FlyTrigger");
            hasFlown = true;
        }

    }
}
