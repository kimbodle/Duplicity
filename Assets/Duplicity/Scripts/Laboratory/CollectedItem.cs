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
    /*
    private void OnMouseDown()
    {
        CompletCollectItem();
    }*/
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
            InventoryManager.Instance.AddItemToInventory(item);
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

    }
  
}
