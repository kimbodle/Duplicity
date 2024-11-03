using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretlabDoor : MonoBehaviour, IInteractable
{
    public string interactionMessage = "수상해 보이는 곳이다.";
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void OnInteract()
    {
        GameObject player = GameObject.FindWithTag("Player"); // 플레이어 찾기
        //보안 시스템 코드 실행
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
