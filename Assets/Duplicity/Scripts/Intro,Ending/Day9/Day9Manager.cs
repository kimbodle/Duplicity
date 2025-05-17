using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day9Manager : MonoBehaviour
{
    public Item[] items;
    public GameObject shakeButton;

    void Start()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item item in items)
            {
                InventoryManager.Instance.AddItemToInventory(item);
            }
        }
        // �����(Android, iOS)�� �ƴϸ� ��ư�� Ȱ��ȭ
        if (Application.platform != RuntimePlatform.Android)
        {
            if (shakeButton != null)
                shakeButton.SetActive(true);
        }
        else
        {
            if (shakeButton != null)
                shakeButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
