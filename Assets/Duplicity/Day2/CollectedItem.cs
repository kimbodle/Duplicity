using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItem : MonoBehaviour, IInteractable
{
    public ItemCollector collector;
    public string interactionMessage = "Press [E] to look.";
    [SerializeField] private int currentDay;
    [SerializeField] Item item;

    void Start()
    {
        if(GameManager.Instance != null)
        {
            currentDay = GameManager.Instance.GetCurrentDay();
        }
    }

    // 디버깅용
    private void OnMouseDown()
    {
        CompletCollectItem();
    }
    private void CompletCollectItem()
    {
        if (collector != null)
        {
            collector.CollectItem();
            if (currentDay == 8)
            {
                InventoryManager.Instance.AddItemToInventory(item);
            }
            Destroy(gameObject); // 아이템을 수집하고 파괴
        }
        else
        {
            Debug.Log("플레이어 못찾음");
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
        Debug.Log("아이템 리셋");
    }
    /*
     * private void CompletCollectItem()
    {
        GameObject player = GameObject.FindWithTag("Player"); // 플레이어 찾기
        if (player != null && player.GetComponent<ItemCollector>() != null)
        {
            player.GetComponent<ItemCollector>().CollectItem();
            if(currentDay == 7)
            {
                InventoryManager.Instance.AddItemToInventory(item);
            }
            Destroy(gameObject); // 아이템을 수집하고 파괴
        }
        else
        {
            Debug.Log("플레이어 못찾음");
        }
    }
    */
}
