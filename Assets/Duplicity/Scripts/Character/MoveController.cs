using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    private Vector2 movement;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;

    // 모바일에서만 사용될 조이스틱
    [SerializeField] private GameObject variableJoystickUI; // UI 오브젝트
    private VariableJoystick variableJoystick; // 조이스틱 컴포넌트
    private bool isMobile;
    [SerializeField] private bool forceMobileInput = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        // 플랫폼 확인
#if UNITY_ANDROID || UNITY_IOS
    isMobile = true;
#else
        isMobile = forceMobileInput; // 에디터에서도 강제 가능
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
            // 모바일: 조이스틱 입력
            movement.x = variableJoystick.Horizontal;
            movement.y = variableJoystick.Vertical;
        }
        else
        {
            // PC: 키보드 입력
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        movement.Normalize(); // 대각선 이동 속도 보정
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
