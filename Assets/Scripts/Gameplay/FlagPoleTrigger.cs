using UnityEngine;
using Platformer.Mechanics;
using Platformer.Core;

public class FlagPoleTrigger : MonoBehaviour
{
    public Transform slideStartPoint;
    public Transform slideEndPoint;
    public Transform flagObject;
    public float slideSpeed = 2f;
    public float walkSpeed = 2f;
    public float walkDuration = 2f;

    private bool sliding = false;
    private bool walking = false;
    private float walkTimer = 0f;

    private PlayerController player;
    private Vector3 flagStartPos;
    private Vector3 flagEndPos;

    void Start()
    {
        if (flagObject != null)
        {
            flagStartPos = flagObject.position;
            flagEndPos = new Vector3(flagStartPos.x, slideEndPoint.position.y, flagStartPos.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.controlEnabled = false;
                player.velocity = Vector2.zero;
                player.transform.position = slideStartPoint.position;
                sliding = true;
            }
        }
    }

    void Update()
    {
        if (sliding && player != null)
        {
            // 플레이어와 깃발을 같이 슬라이드
            player.transform.position = Vector3.MoveTowards(
                player.transform.position,
                slideEndPoint.position,
                slideSpeed * Time.deltaTime
            );

            if (flagObject != null)
            {
                flagObject.position = Vector3.MoveTowards(
                    flagObject.position,
                    flagEndPos,
                    slideSpeed * Time.deltaTime
                );
            }

            if (Vector3.Distance(player.transform.position, slideEndPoint.position) < 0.01f)
            {
                sliding = false;
                walking = true;
                walkTimer = 0f;
            }
        }

        if (walking && player != null)
        {
            walkTimer += Time.deltaTime;
            player.transform.position += Vector3.right * walkSpeed * Time.deltaTime;

            if (walkTimer >= walkDuration)
            {
                walking = false;
                player.controlEnabled = true; // 혹은 씬 전환 트리거
            }
        }
    }
}
