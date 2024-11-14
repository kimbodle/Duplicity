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
            // �÷��̾� ���̾�α� ����
            DialogManager.Instance.PlayerMessageDialog(playerDialog);

            // �÷��̾� ���̾�αװ� ������ �� ȯû ���̾�α׸� �����ϵ��� �̺�Ʈ�� ���
            DialogManager.Instance.OnDialogEnd += TriggerHallucinationDialog;

            InventoryManager.Instance.AddItemToInventory(photoItem);
        }
    }

    private void TriggerHallucinationDialog()
    {
        // ȯû ���̾�α� ����
        hallucination.StartHallucinationDialog(halluciantionDialog);

        // �̺�Ʈ �ڵ鷯�� �����Ͽ� ���� �� ������� �ʵ��� ����
        DialogManager.Instance.OnDialogEnd -= TriggerHallucinationDialog;
    }
    */
}
