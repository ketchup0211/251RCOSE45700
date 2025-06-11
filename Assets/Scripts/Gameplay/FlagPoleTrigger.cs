using UnityEngine;
using Platformer.Mechanics;
using Platformer.Core;
using System.Collections;

public class FlagPoleTrigger : MonoBehaviour
{
    public Transform slideStartPoint;
    public Transform flagEndPoint;      // 깃발이 멈출 위치
    public Transform playerEndPoint;    // 플레이어가 내려갈 위치
    public GameObject flagObject;

    public float slideSpeed = 2f;
    public float walkSpeed = 2f;
    public float walkDuration = 2f;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            triggered = true;
            StartCoroutine(SlideSequence(player));
        }
    }

    IEnumerator SlideSequence(PlayerController player)
    {
        player.controlEnabled = false;

        // 위치 세팅
        var poleX = transform.position.x;
        Vector3 playerStart = new Vector3(poleX, slideStartPoint.position.y, player.transform.position.z);
        player.transform.position = playerStart;

        Vector3 flagStart = new Vector3(flagObject.transform.position.x, slideStartPoint.position.y, flagObject.transform.position.z);
        flagObject.transform.position = flagStart;

        // 슬라이딩 내려오기
        while (player.transform.position.y > playerEndPoint.position.y || flagObject.transform.position.y > flagEndPoint.position.y)
        {
            if (player.transform.position.y > playerEndPoint.position.y)
                player.transform.position += Vector3.down * slideSpeed * Time.deltaTime;

            if (flagObject.transform.position.y > flagEndPoint.position.y)
            {
                Vector3 flagPos = flagObject.transform.position;
                flagPos.y -= slideSpeed * Time.deltaTime;
                flagObject.transform.position = flagPos;
            }

            yield return null;
        }

        // ➤ 걷기 애니메이션 적용
        if (player.animator != null)
            player.animator.SetFloat("velocityX", 1f);

        float walkTime = 0f;
        while (walkTime < walkDuration)
        {
            player.transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            walkTime += Time.deltaTime;

            // ➤ 걷는 동안에도 애니메이션 유지
            if (player.animator != null)
                player.animator.SetFloat("velocityX", 1f);
                
            yield return null;
        }

        // ➤ 걷기 종료 후 애니메이션 멈춤 처리
        if (player.animator != null)
            player.animator.SetFloat("velocityX", 0f);

        // TODO: 스테이지 클리어 처리
    }
}
