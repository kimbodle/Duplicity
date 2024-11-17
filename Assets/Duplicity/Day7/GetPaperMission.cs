using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPaperMission : MonoBehaviour, IInteractable, IMission
{
    public string interactionMessage = "중요해 보이는 문서가 떨어져있다.";

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
        // SpriteRenderer와 Collider2D를 참조
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
            // 이미지 투명화 처리
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = 0; // 알파값을 0으로 설정 (투명)
                spriteRenderer.color = color;
            }

            // 콜라이더 비활성화
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
