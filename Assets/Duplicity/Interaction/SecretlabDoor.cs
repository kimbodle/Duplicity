using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretlabDoor : MonoBehaviour, IInteractable
{
    public string interactionMessage = "������ ���̴� ���̴�.";
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        GameObject player = GameObject.FindWithTag("Player"); // �÷��̾� ã��
        //���� �ý��� �ڵ� ����
        /*
        if (player != null && player.GetComponent<..>() != null)
        {
            player.GetComponent<..>()SecuritySystemStart();
            
        }*/
    }

    public void ResetTask()
    {
    }

    public void HandleTask(string taskKey)
    {
    }

}
