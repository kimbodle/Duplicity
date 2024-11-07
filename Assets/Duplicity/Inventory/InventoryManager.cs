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
        Debug.Log("Inventory 전부 비움!");
    }

    public void ShowDetailPanel(Item item)
    {
        if (detailPanel != null && item.canViewDetails)
        {
            detailImage.sprite = item.itemIcon; // 아이템의 확대 이미지 표시
            detailDescription.text = item.itemDescription; // 아이템 설명 표시
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
        // 슬롯이 아닌 곳을 클릭했을 때 모든 슬롯의 선택 상태를 해제
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DeselectAllSlots();
            }
        }
    }
}
