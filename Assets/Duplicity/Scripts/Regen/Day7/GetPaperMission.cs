using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPaperMission : MonoBehaviour, IInteractable, IMission
{
    public string interactionMessage = "�߿��� ���̴� ������ �������ִ�.";

    [Space(10)]
    public Day7MissionManager missionManager;
    [Space(10)]
    public Item paper;
    public bool IsMissionCompleted { get; private set; }

    private bool isFirst = true;
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider2D;

    private void Start()
    {
        // SpriteRenderer�� Collider2D�� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        //bigPanelDisplay.ShowPanel(assignedImage);
        if(isFirst)
        {
            if(InventoryManager.Instance != null && GameManager.Instance != null)
            {
                InventoryManager.Instance.AddItemToInventory(paper);

                missionManager.CheckAllMission();
            }
            // �̹��� ����ȭ ó��
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0; // ���İ��� 0���� ���� (����)
                spriteRenderer.color = color;
            }

            // �ݶ��̴� ��Ȱ��ȭ
            if (collider2D != null)
            {
                collider2D.enabled = false;
            }

            IsMissionCompleted = true;
            isFirst = false;
        }
        
    }
    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }
    public void HandleTask(string taskKey)
    {

    }
    
    public void ResetTask()
    {

    }

    public void Initialize()
    {

    }

}
