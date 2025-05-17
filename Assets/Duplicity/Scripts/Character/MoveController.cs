using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    private Vector2 movement;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;

    // ����Ͽ����� ���� ���̽�ƽ
    [SerializeField] private GameObject variableJoystickUI; // UI ������Ʈ
    private VariableJoystick variableJoystick; // ���̽�ƽ ������Ʈ
    private bool isMobile;
    [SerializeField] private bool forceMobileInput = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        // �÷��� Ȯ��
#if UNITY_ANDROID || UNITY_IOS
    isMobile = true;
#else
        isMobile = forceMobileInput; // �����Ϳ����� ���� ����
#endif

        if (isMobile)
        {
            if (variableJoystickUI != null)
                variableJoystick = variableJoystickUI.GetComponent<VariableJoystick>();
        }
        else
        {
            if (variableJoystickUI != null)
                variableJoystickUI.SetActive(false);
        }
    }

    void Update()
    {
        UpdateInput();
        UpdateMoveState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void UpdateInput()
    {
        if (isMobile && variableJoystick != null)
        {
            // �����: ���̽�ƽ �Է�
            movement.x = variableJoystick.Horizontal;
            movement.y = variableJoystick.Vertical;
        }
        else
        {
            // PC: Ű���� �Է�
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        movement.Normalize(); // �밢�� �̵� �ӵ� ����
    }

    void MoveCharacter()
    {
        rigidbody2D.velocity = movement * movementSpeed;
    }

    private void UpdateMoveState()
    {
        bool isMoving = !Mathf.Approximately(movement.x, 0) || !Mathf.Approximately(movement.y, 0);

        animator.SetBool("isMove", isMoving);
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
}
