using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Image icon;
    public Image dragIcon; // 드래그할 때 사용되는 아이콘
    public bool isSelected = false;
    public bool hasBeenSelected = false;
    public Color selectedColor = Color.gray;
    private Color originalColor;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition;

    private void Start()
    {
        originalColor = icon.color;
        rectTransform = icon.GetComponent<RectTransform>();
        canvasGroup = icon.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = icon.gameObject.AddComponent<CanvasGroup>();
        }

        originalPosition = rectTransform.anchoredPosition;

        // 초기에는 드래그 아이콘을 비활성화합니다.
        if (dragIcon != null)
        {
            dragIcon.gameObject.SetActive(false);
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        icon.color = originalColor;

        // 아이콘의 위치를 원래 위치로 되돌림
        rectTransform.anchoredPosition = originalPosition;
        DeselectSlot();
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        icon.color = originalColor;
        DeselectSlot();
        hasBeenSelected = false;
    }

    public void OnSlotClicked()
    {
        if (item != null)
        {
            if (isSelected)
            {
                if (item.canViewDetails)
                {
                    ShowItemDetails();
                }
                DeselectSlot();
            }
            else
            {
                InventoryManager.Instance.DeselectAllSlots();
                SelectSlot();
                hasBeenSelected = true;
            }
        }
    }

    public void ShowItemDetails()
    {
        InventoryManager.Instance.ShowDetailPanel(item);
    }

    public void SelectSlot()
    {
        isSelected = true;
        UpdateIconColor();
    }

    public void DeselectSlot()
    {
        isSelected = false;
        UpdateIconColor();
    }

    private void UpdateIconColor()
    {
        icon.color = isSelected ? selectedColor : originalColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && isSelected && hasBeenSelected)
        {
            canvasGroup.blocksRaycasts = false; // 드래그 중 클릭 비활성화
            // 드래그 아이콘 활성화 및 초기 설정
            if (dragIcon != null)
            {
                dragIcon.sprite = icon.sprite;
                dragIcon.color = icon.color;
                dragIcon.gameObject.SetActive(true);
                dragIcon.rectTransform.position = Input.mousePosition; // 드래그 시작 위치
            }
        }
        else
        {
            eventData.pointerDrag = null; // 드래그를 취소
            DeselectSlot();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && isSelected && hasBeenSelected && dragIcon != null)
        {
            dragIcon.rectTransform.position = Input.mousePosition; // 드래그 아이콘을 마우스 위치로 따라가게 함
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null && isSelected)
        {
            canvasGroup.blocksRaycasts = true;

            // Raycast로 다른 캔버스의 드롭존 탐색
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            bool dropSucceeded = false;

            foreach (var result in results)
            {
                DropZone dropZone = result.gameObject.GetComponent<DropZone>();
                if (dropZone != null)
                {
                    dropZone.OnDrop(eventData);
                    dropSucceeded = true;
                    break;
                }
            }

            // 드롭이 실패했을 때 드래그 아이콘을 비활성화하고 슬롯 아이콘 복구
            if (!dropSucceeded)
            {
                rectTransform.anchoredPosition = originalPosition;
                DeselectSlot();
            }

            // 드래그 종료 후 드래그 아이콘 비활성화
            if (dragIcon != null)
            {
                dragIcon.gameObject.SetActive(false);
            }

            isSelected = false;
            hasBeenSelected = false;
        }
    }
}
