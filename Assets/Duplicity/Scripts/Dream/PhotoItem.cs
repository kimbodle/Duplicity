using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoItem : MonoBehaviour, IInteractable
{
    public string interactionMessage = "����";

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
            // ȯû ���̾�α� ���� �� ���� �̺�Ʈ ����
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
        // ȯû ���̾�α� ���� �� �÷��̾� ���̾�α� �ٽ� ǥ��
        if (playerDialog != null)
        {
            DialogManager.Instance.PlayerMessageDialog(playerDialog);
        }

        // �̺�Ʈ ���� ����
        hallucination.OnHallucinationDialogEnd -= OnHallucinationDialogEnd;
    }

    public void HandleTask(string taskKey)
    {
        
    }

    public void ResetTask()
    {

    }
}
