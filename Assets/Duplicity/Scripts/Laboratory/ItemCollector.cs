using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public int itemsToCollect = 3; // 수집해야 할 아이템 수
    private int collectedItems = 0;

    public void CollectItem()
    {
        collectedItems++;
        Debug.Log("Item Collected: " + collectedItems + "/" + itemsToCollect);

        if (collectedItems >= itemsToCollect)
        {
            FindObjectOfType<MissionTimer>().CompleteMission();
        }
    }
}
