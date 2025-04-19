using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    private Vector2 movement; // Vector2로 이동 방향을 저장
    private new Rigidbody2D rigidbody2D; // Rigidbody2D 참조
    private Animator animator; // Animator 참조
    private VariableJoystick variableJoystick; // VariableJoystick 참조

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        variableJoystick = FindObjectOfType<VariableJoystick>(); // VariableJoystick 찾기
    }

    void Update()
    {
        UpdateMoveState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // VariableJoystick을 사용하여 입력 처리
        movement.x = variableJoystick.Horizontal; // 수평 입력
        movement.y = variableJoystick.Vertical; // 수직 입력

        movement.Normalize(); // 대각선 이동 속도 유지

        rigidbody2D.velocity = movement * movementSpeed; // Rigidbody2D 속도 설정
    }

    private void UpdateMoveState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isMove", false); // 이동 중이 아닐 때
        }
        else
        {
            animator.SetBool("isMove", true); // 이동 중일 때
        }
        animator.SetFloat("xDir", movement.x); // x 방향 애니메이션 값 설정
        animator.SetFloat("yDir", movement.y); // y 방향 애니메이션 값 설정
    }
}
