using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretlabDoor : MonoBehaviour, IInteractable
{
    public string interactionMessage = "������ ���̴� ���̴�.";
    public SecuritySystem securitySystem; // ���� �ý��� ��ũ��Ʈ�� �����ϱ� ���� ����
    [SerializeField] private int clickCount = 0;
    [SerializeField] private int requiredClicks = 7; // missionstart �޼��带 ������ Ŭ�� ��
    private Renderer objectRenderer;
    private float colorIncrementStep = 0.1f; // Ŭ���� ���� ��ȭ ����

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer�� �� ������Ʈ�� �����ϴ�. ���� ������ ���� Renderer�� �ʿ��մϴ�.");
        }
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        Debug.Log("�̼ǽ���");
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
        //�� �ȵ�
        clickCount++;
        // Ŭ���� ������ ������ ������ ������Ŵ
        if (objectRenderer != null)
        {
            Color currentColor = objectRenderer.material.color;
            float newRedValue = Mathf.Clamp(currentColor.r + colorIncrementStep, 0, 1);
            objectRenderer.material.color = new Color(newRedValue, currentColor.g, currentColor.b);
        }

        if (clickCount == requiredClicks)
        {
            // Ŭ�� Ƚ���� 7���̸� ���� �ý����� missionstart �޼��� ����
            if (securitySystem != null)
            {
                //securitySystem.missionstart();
                Debug.Log("�̼ǽ���");
            }
        }
    }
}
