using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poster : MonoBehaviour, IInteractable
{
    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    public Dialog dialog;

    public string interactionMessage = "포스터가 부착되어 있다.";

    private void Start()
    {

    }
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        bigPanelDisplay.ShowPanel(assignedImage);
        if(dialog != null)
        {
            DialogManager.Instance.PlayerMessageDialog(dialog);
        }
        else
        {
            return;
        }
        
    }

    public void ResetTask()
    {

    }

    public void HandleTask(string taskKey)
    {

    }
}
