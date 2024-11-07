using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoMissionWall : MonoBehaviour, IInteractable
{
    public string interactionMessage = "»çÁø girl 2";

    public GameObject PhotoMissionPanel;

    private void Start()
    {
        PhotoMissionPanel.SetActive(false);
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        PhotoMissionPanel.SetActive(true);
    }

    public void HandleTask(string taskKey)
    {
        
    }
    
    public void ResetTask()
    {
  
    }
}
