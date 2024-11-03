using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
    public string interactionMessage = "컴퓨터가 있다.";
    public GameObject ComputerPanel;
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        ComputerPanel.SetActive(true);
    }

    public void HandleTask(string taskKey)
    {
    }

   

    public void ResetTask()
    {
    }
}
