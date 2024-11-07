using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventorySlot> slots;
    public GameObject detailPanel;
    public Image detailImage;
    public TMP_Text detailDescription;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItemToInventory(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.AddItem(item);
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }

    public void ClearAllItemSlot()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
        Debug.Log("Inventory ���� ���!");
    }

    public void ShowDetailPanel(Item item)
    {
        if (detailPanel != null && item.canViewDetails)
        {
            detailImage.sprite = item.itemIcon; // �������� Ȯ�� �̹��� ǥ��
            detailDescription.text = item.itemDescription; // ������ ���� ǥ��
            detailPanel.SetActive(true);
        }
    }

    public void HideDetailPanel()
    {
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in slots)
        {
            if (slot.isSelected)
            {
                slot.DeselectSlot();
            }
        }
    }

    private void Update()
    {
        // ������ �ƴ� ���� Ŭ������ �� ��� ������ ���� ���¸� ����
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DeselectAllSlots();
            }
        }
    }
}
