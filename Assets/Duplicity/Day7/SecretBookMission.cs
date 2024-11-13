using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBookMission : MonoBehaviour, IInteractable, IMission
{
    public string interactionMessage = "중요해 보이는 문서가 떨어져있다.";

    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    public Item secretBookItem;

    public bool IsMissionCompleted { get; private set; }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        bigPanelDisplay.ShowPanel(assignedImage);
        InventoryManager.Instance.AddItemToInventory(secretBookItem);
        IsMissionCompleted = true;
        GameManager.Instance.GetCurrentDayController().CompleteTask("GetSecretBook");
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
