using UnityEngine;

public class SparrowFly : MonoBehaviour
{
    public Animator animator;
    public float flySpeed = 2f;
    public Vector2 flyDirection = new Vector2(-1f, 1f); // 좌상단 방향
    public float flyDuration = 2f;
    public DropletSpawner dropletSpawner; // ✅ DropletSpawner 연결

    private bool isFlying = false;
    private float flyTimer = 0f;

    void Update()
    {
        if (isFlying)
        {
            flyTimer += Time.deltaTime;
            transform.position += (Vector3)(flyDirection.normalized * flySpeed * Time.deltaTime);

            if (flyTimer > flyDuration)
            {
                Destroy(gameObject); // 날아간 후 제거
            }
        }
    }

    public void TriggerFly()
    {
        if (!isFlying)
        {
            isFlying = true;
            animator.SetTrigger("FlyTrigger");

            // ✅ 날면서 물방울 떨어뜨리기 시작
            if (dropletSpawner != null)
            {
                dropletSpawner.StartSpawning();
            }
        }
    }
}
