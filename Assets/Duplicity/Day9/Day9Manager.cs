using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day9Manager : MonoBehaviour
{
    public Item[] items;
    // Start is called before the first frame update
    void Start()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item item in items)
            {
                InventoryManager.Instance.AddItemToInventory(item);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
