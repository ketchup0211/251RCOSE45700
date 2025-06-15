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
        sparrowFly = GetComponent<SparrowFly>(); // 🔥 SparrowFly 컴포넌트도 가져옴
    }

    void Update()
    {
        if (!hasFlown && Mathf.Abs(player.position.x - transform.position.x) < triggerDistance)
        {
            // ✅ 애니메이션 & 이동 트리거 동시에 실행
            if (sparrowFly != null)
            {
                sparrowFly.TriggerFly();
            }
            else
            {
                animator.SetTrigger("FlyTrigger"); // 백업: 애니메이터만 있는 경우
            }

            hasFlown = true;
        }
    }
}
