using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBookMission : MonoBehaviour, IInteractable, IMission
{
    public string interactionMessage = "중요해 보이는 문서가 떨어져있다.";

    [Space(10)]
    public Day7MissionManager missionManager;
    [Space(10)]
    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    public Item secretBookItem;
    private bool isFirst = true;

    public bool IsMissionCompleted { get; private set; }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        bigPanelDisplay.ShowPanel(assignedImage);
        if(isFirst)
        {
            if(InventoryManager.Instance != null && GameManager.Instance != null)
            {
                InventoryManager.Instance.AddItemToInventory(secretBookItem);
                //GameManager.Instance.GetCurrentDayController().CompleteTask("GetSecretBook");

                missionManager.CheckAllMission();
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
