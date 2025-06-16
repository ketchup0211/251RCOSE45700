using UnityEngine;

public class SparrowTrigger : MonoBehaviour
{
    public Transform player;             // 플레이어 트랜스폼
    public float triggerDistance = 3f;   // 반응 거리

    private Animator animator;
    private SparrowFly sparrowFly;
    private bool hasFlown = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        sparrowFly = GetComponent<SparrowFly>();
    }

    void Update()
    {
        if (!hasFlown && Mathf.Abs(player.position.x - transform.position.x) < triggerDistance)
        {
            // ✅ 애니메이션 & 물방울 트리거를 모두 포함한 함수
            if (sparrowFly != null)
            {
                sparrowFly.TriggerFly();
            }
            else
            {
                animator.SetTrigger("FlyTrigger"); // 백업
            }

            hasFlown = true;
        }
    }
}
