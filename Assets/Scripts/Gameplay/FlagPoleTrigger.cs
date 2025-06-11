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

        // 플레이어 위치: FlagPole X 위치 + 슬라이드 시작 Y
        var poleX = transform.position.x;
        Vector3 playerStart = new Vector3(poleX, slideStartPoint.position.y, player.transform.position.z);
        player.transform.position = playerStart;

        // 깃발 위치: 현재 X 고정 + 슬라이드 시작 Y
        Vector3 flagStart = new Vector3(flagObject.transform.position.x, slideStartPoint.position.y, flagObject.transform.position.z);
        flagObject.transform.position = flagStart;

        // 슬라이딩 내려오기
        while (player.transform.position.y > playerEndPoint.position.y || flagObject.transform.position.y > flagEndPoint.position.y)
        {
            if (player.transform.position.y > playerEndPoint.position.y)
            {
                player.transform.position += Vector3.down * slideSpeed * Time.deltaTime;
            }

            if (flagObject.transform.position.y > flagEndPoint.position.y)
            {
                Vector3 flagPos = flagObject.transform.position;
                flagPos.y -= slideSpeed * Time.deltaTime;
                flagObject.transform.position = flagPos;
            }

            yield return null;
        }

        // 일정 시간 걷기
        float walkTime = 0f;
        while (walkTime < walkDuration)
        {
            player.transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            walkTime += Time.deltaTime;
            yield return null;
        }

        // TODO: 스테이지 클리어 처리
    }
}
