using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoItem : MonoBehaviour, IInteractable
{
    public string interactionMessage = "사진";

    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    public Dialog playerDialog;
    public Dialog halluciantionDialog;
    public HallucinationDialogManager hallucination;
    [Space(10)]
    public Item photoItem;

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        bigPanelDisplay.ShowPhotoPanel(assignedImage, this);
        if(InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItemToInventory(photoItem);
            // 환청 다이얼로그 시작 후 종료 이벤트 구독
            if (hallucination != null && halluciantionDialog != null)
            {
                hallucination.OnHallucinationDialogEnd += OnHallucinationDialogEnd;
                hallucination.StartHallucinationDialog(halluciantionDialog);
            }
            else
            {
                return;
            }
        }
    }
    private void OnHallucinationDialogEnd()
    {
        // 환청 다이얼로그 종료 후 플레이어 다이얼로그 다시 표시
        if (playerDialog != null)
        {
            DialogManager.Instance.PlayerMessageDialog(playerDialog);
        }

        // 이벤트 구독 해제
        hallucination.OnHallucinationDialogEnd -= OnHallucinationDialogEnd;
    }

    public void HandleTask(string taskKey)
    {
        
    }

    public void ResetTask()
    {

    }
}
