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
            if (playerDialog != null)
            {
                DialogManager.Instance.PlayerMessageDialog(playerDialog);
                hallucination.StartHallucinationDialog(halluciantionDialog);
            }
            else
            {
                return;
            }
        }
    }

    public void HandleTask(string taskKey)
    {
        
    }

    public void ResetTask()
    {

    }

    /*
    public void OnInteract()
    {
        bigPanelDisplay.ShowPanel(assignedImage);

        if (playerDialog != null)
        {
            // 플레이어 다이얼로그 시작
            DialogManager.Instance.PlayerMessageDialog(playerDialog);

            // 플레이어 다이얼로그가 끝났을 때 환청 다이얼로그를 실행하도록 이벤트에 등록
            DialogManager.Instance.OnDialogEnd += TriggerHallucinationDialog;

            InventoryManager.Instance.AddItemToInventory(photoItem);
        }
    }

    private void TriggerHallucinationDialog()
    {
        // 환청 다이얼로그 실행
        hallucination.StartHallucinationDialog(halluciantionDialog);

        // 이벤트 핸들러를 제거하여 여러 번 실행되지 않도록 설정
        DialogManager.Instance.OnDialogEnd -= TriggerHallucinationDialog;
    }
    */
}
