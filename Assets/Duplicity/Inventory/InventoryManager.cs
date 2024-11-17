using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // 싱글톤 인스턴스
    public List<InventorySlot> slots; // 모든 슬롯 리스트
    public GameObject detailPanel; // 디테일 패널
    public Image detailImage; // 디테일 이미지
    public TMP_Text detailDescription; // 디테일 설명 텍스트
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
        // 디테일 패널을 초기 상태에서 비활성화
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
        AddItemToInventory(item);
    }

    // 아이템을 슬롯에 추가
    public void AddItemToInventory(Item item)
    {
        foreach (var slot in slots)
        {
            // 슬롯이 비어 있는 경우
            if (slot.item == null)
            {
                slot.AddItem(item); // 슬롯에 아이템 추가
                Debug.Log($"Added {item.itemName} to inventory slot.");
                return;
            }
        }
        Debug.Log("Inventory is full!");
    }

    // 모든 슬롯 비우기
    public void ClearAllItemSlot()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot(); // 슬롯 비우기
        }
        Debug.Log("Inventory cleared!");
    }

    // 디테일 패널 표시
    public void ShowDetailPanel(Item item)
    {
        if (detailPanel != null && item != null && item.canViewDetails)
        {
            detailImage.sprite = item.itemIcon; // 아이템 이미지 표시
            detailDescription.text = item.itemDescription; // 아이템 설명 표시
            detailPanel.SetActive(true); // 디테일 패널 활성화
        }
    }

    // 디테일 패널 숨기기
    public void HideDetailPanel()
    {
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
    }
}
