using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : MonoBehaviour
{
    [Space(10)]
    public float patrolDistance = 5f; // 경비병 이동 거리
    [Space(10)]
    public float patrolSpeed = 2f;
    public float detectionRange = 5f;
    public float captureRange = 1f; // 플레이어와의 캡처 거리
    public LayerMask playerLayer;

    [Space(10)]
    public Transform detectionArea;
    public GameObject detectionVisual;

    public bool isHorizontal = true;

    private bool movingRight = true;
    private Transform player;
    private bool isStunned = false;
    private bool isChasing = false;
    private Vector3 initialPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isStunned) return;

        // 경비병의 감지
        DetectPlayer();

        // 감지 상태에 따라 패트롤 또는 플레이어 추적
        if (isChasing)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (isHorizontal)
        {
            if (movingRight)
            {
                transform.position += Vector3.right * patrolSpeed * Time.deltaTime;
                if (transform.position.x >= initialPosition.x + patrolDistance)
                {
                    movingRight = false;
                    FlipDirection();
                }
            }
            else
            {
                transform.position += Vector3.left * patrolSpeed * Time.deltaTime;
                if (transform.position.x <= initialPosition.x - patrolDistance)
                {
                    movingRight = true;
                    FlipDirection();
                }
            }
        }
        else
        {
            if (movingRight)
            {
                transform.position += Vector3.up * patrolSpeed * Time.deltaTime;
                if (transform.position.y >= initialPosition.y + patrolDistance)
                {
                    movingRight = false;
                    FlipDirection();
                }
            }
            else
            {
                transform.position += Vector3.down * patrolSpeed * Time.deltaTime;
                if (transform.position.y <= initialPosition.y - patrolDistance)
                {
                    movingRight = true;
                    FlipDirection();
                }
            }
        }
    }

    private void DetectPlayer()
    {
        Collider2D playerInRange = Physics2D.OverlapCircle(detectionArea.position, detectionRange, playerLayer);

        if (playerInRange)
        {
            detectionVisual.SetActive(true);
            isChasing = true;
            FacePlayer(); // 플레이어의 방향을 향하도록 전환
        }
        else
        {
            detectionVisual.SetActive(false);
            isChasing = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        //Vector3 direction = (player.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, player.position, patrolSpeed * 2 * Time.deltaTime);

        // 플레이어의 방향을 바라보도록 전환
        FacePlayer();

        // 플레이어와의 거리가 캡처 범위 이내에 들어오면 엔딩 처리
        if (Vector3.Distance(transform.position, player.position) < captureRange)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("게임 오버");
        if(EndingManager.Instance != null)
        {
            FadeManager.Instance.StartFadeOut(() =>
            {
                EndingManager.Instance.LoadEnding("GameOver", "잠입 실패", 5);
            }, true, 3f);
        }
    }

    public void StunGuard()
    {
        isStunned = true;
        detectionVisual.SetActive(false);
        Invoke("ResumePatrol", 3f); // 3초 후에 다시 활성화
    }

    private void ResumePatrol()
    {
        isStunned = false;
    }

    private void FlipDirection()
    {
        // 가드와 감지 비주얼의 방향을 전환
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;

        //Vector3 visualScale = detectionVisual.transform.localScale;
        //visualScale.x = movingRight ? Mathf.Abs(visualScale.x) : -Mathf.Abs(visualScale.x);
        //detectionVisual.transform.localScale = visualScale;
    }

    private void FacePlayer()
    {
        // 플레이어의 위치에 따라 경비병과 감지 비주얼 방향을 전환
        if (player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            //detectionVisual.transform.localScale = new Vector3(Mathf.Abs(detectionVisual.transform.localScale.x), detectionVisual.transform.localScale.y, detectionVisual.transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            //detectionVisual.transform.localScale = new Vector3(-Mathf.Abs(detectionVisual.transform.localScale.x), detectionVisual.transform.localScale.y, detectionVisual.transform.localScale.z);
        }
    }
}