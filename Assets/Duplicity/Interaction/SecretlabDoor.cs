using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretlabDoor : MonoBehaviour, IInteractable
{
    public string interactionMessage = "수상해 보이는 곳이다.";
    public SecuritySystem securitySystem; // 보안 시스템 스크립트를 연결하기 위한 변수
    [SerializeField] private int clickCount = 0;
    [SerializeField] private int requiredClicks = 7; // missionstart 메서드를 실행할 클릭 수
    private Renderer objectRenderer;
    private float colorIncrementStep = 0.1f; // 클릭당 색상 변화 정도

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer가 이 오브젝트에 없습니다. 색상 변경을 위해 Renderer가 필요합니다.");
        }
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        Debug.Log("미션시작");
        ///HandleClick();
    }

    public void ResetTask()
    {
    }

    public void HandleTask(string taskKey)
    {
    }

    void HandleClick()
    {
        //왜 안돼
        clickCount++;
        // 클릭할 때마다 빨간색 성분을 증가시킴
        if (objectRenderer != null)
        {
            Color currentColor = objectRenderer.material.color;
            float newRedValue = Mathf.Clamp(currentColor.r + colorIncrementStep, 0, 1);
            objectRenderer.material.color = new Color(newRedValue, currentColor.g, currentColor.b);
        }

        if (clickCount == requiredClicks)
        {
            // 클릭 횟수가 7번이면 보안 시스템의 missionstart 메서드 실행
            if (securitySystem != null)
            {
                //securitySystem.missionstart();
                Debug.Log("미션시작");
            }
        }
    }
}
