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
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 디테일 패널을 초기 상태에서 비활성화
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
        //AddItemToInventory(item);
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
                Debug.Log($"{item.itemName}을 인벤토리에 추가함.");
                return;
            }
        }
        Debug.Log("인벤토리 꽉 참.");
    }

    // 모든 슬롯 비우기
    public void ClearAllItemSlot()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot(); // 슬롯 비우기
        }
        Debug.Log("인벤토리 비워짐.");
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

    // 특정 아이템을 갖고 있는지 확인
    public bool HasItem(Item targetItem)
    {
        foreach (var slot in slots)
        {
            if (slot.item == targetItem) // 대상 아이템과 동일한 아이템이 있다면
            {
                return true;
            }
        }
        return false;
    }

    // 특정 아이템을 삭제하고 새로운 아이템 추가
    public bool ReplaceItem(Item targetItem, Item newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.item == targetItem) // 대상 아이템 발견
            {
                slot.ClearSlot(); // 기존 아이템 삭제
                AddItemToInventory(newItem); // 새로운 아이템 추가
                Debug.Log($"{targetItem.itemName}아이템을 {newItem.itemName}로 변경");
                return true;
            }
        }
        Debug.Log($"{targetItem.itemName}이 인벤토리에 존재하지 않음.");
        return false;
    }
}
