using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poster : MonoBehaviour, IInteractable
{
    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage; // �� �����Ϳ� �Ҵ�� �̹���

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
    }

    public void ResetTask()
    {

    }

    public void HandleTask(string taskKey)
    {

    }
}
