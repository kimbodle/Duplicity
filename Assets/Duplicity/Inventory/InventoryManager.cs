using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // �̱��� �ν��Ͻ�
    public List<InventorySlot> slots; // ��� ���� ����Ʈ
    public GameObject detailPanel; // ������ �г�
    public Image detailImage; // ������ �̹���
    public TMP_Text detailDescription; // ������ ���� �ؽ�Ʈ
    public Item item;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // ������ �г��� �ʱ� ���¿��� ��Ȱ��ȭ
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
        AddItemToInventory(item);
    }

    // �������� ���Կ� �߰�
    public void AddItemToInventory(Item item)
    {
        foreach (var slot in slots)
        {
            // ������ ��� �ִ� ���
            if (slot.item == null)
            {
                slot.AddItem(item); // ���Կ� ������ �߰�
                Debug.Log($"Added {item.itemName} to inventory slot.");
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }

    // ��� ���� ����
    public void ClearAllItemSlot()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot(); // ���� ����
        }
        Debug.Log("Inventory cleared!");
    }

    // ������ �г� ǥ��
    public void ShowDetailPanel(Item item)
    {
        if (detailPanel != null && item != null && item.canViewDetails)
        {
            detailImage.sprite = item.itemIcon; // ������ �̹��� ǥ��
            detailDescription.text = item.itemDescription; // ������ ���� ǥ��
            detailPanel.SetActive(true); // ������ �г� Ȱ��ȭ
        }
    }

    // ������ �г� �����
    public void HideDetailPanel()
    {
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
    }
}
