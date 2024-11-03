using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItem : MonoBehaviour, IInteractable
{
    public string interactionMessage = "Press [E] to look.";

    // 디버깅용
    private void OnMouseDown()
    {
        CompletCollectItem();
    }

    private void CompletCollectItem()
    {
        GameObject player = GameObject.FindWithTag("Player"); // 플레이어 찾기
        if (player != null && player.GetComponent<ItemCollector>() != null)
        {
            player.GetComponent<ItemCollector>().CollectItem();
            Destroy(gameObject); // 아이템을 수집하고 파괴
        }
    }

    public void OnInteract()
    {
        CompletCollectItem();
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    public void HandleTask(string taskKey)
    {

    }

    void IInteractable.ResetTask()
    {
        Debug.Log("아이템들 업뎃됐져욤");
    }
}
