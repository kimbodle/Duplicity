using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poster : MonoBehaviour, IInteractable
{
    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    public Dialog dialog;

    public string interactionMessage = "�����Ͱ� �����Ǿ� �ִ�.";

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
