using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenStoreHint : MonoBehaviour, IInteractable
{
    public string interactionMessage = "정보가 적혀있다.";
    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    [SerializeField]private Dialog dialog;
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
    public void OnInteract()
    {
        bigPanelDisplay.ShowPanel(assignedImage);
        if (dialog != null)
        {
            DialogManager.Instance.PlayerMessageDialog(dialog);
        }
    }

    public void HandleTask(string taskKey)
    {

    }

    public void ResetTask()
    {

    }
}
