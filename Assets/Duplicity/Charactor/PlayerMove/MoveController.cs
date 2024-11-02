using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    private Vector2 movement; // Vector2�� �̵� ������ ����
    private new Rigidbody2D rigidbody2D; // Rigidbody2D ����
    private Animator animator; // Animator ����
    private VariableJoystick variableJoystick; // VariableJoystick ����

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        variableJoystick = FindObjectOfType<VariableJoystick>(); // VariableJoystick ã��
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
        // VariableJoystick�� ����Ͽ� �Է� ó��
        movement.x = variableJoystick.Horizontal; // ���� �Է�
        movement.y = variableJoystick.Vertical; // ���� �Է�

        movement.Normalize(); // �밢�� �̵� �ӵ� ����

        rigidbody2D.velocity = movement * movementSpeed; // Rigidbody2D �ӵ� ����
    }

    private void UpdateMoveState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isMove", false); // �̵� ���� �ƴ� ��
        }
        else
        {
            animator.SetBool("isMove", true); // �̵� ���� ��
        }
        animator.SetFloat("xDir", movement.x); // x ���� �ִϸ��̼� �� ����
        animator.SetFloat("yDir", movement.y); // y ���� �ִϸ��̼� �� ����
    }
}
