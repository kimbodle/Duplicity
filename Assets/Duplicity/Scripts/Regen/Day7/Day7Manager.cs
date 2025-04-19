using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day7Manager : MonoBehaviour
{
    public Item secretBook;
    void Start()
    {
        if(InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItemToInventory(secretBook);
        }
    }
}
