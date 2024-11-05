using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Image icon;
    public bool isSelected = false;
    public bool hasBeenSelected = false; // 새로운 플래그 추가
    public Color selectedColor = Color.gray;
    private Color originalColor;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition;

    private void Start()
    {
        originalColor = icon.color;
        canvas = GetComponentInParent<Canvas>();
        rectTransform = icon.GetComponent<RectTransform>();
        canvasGroup = icon.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = icon.gameObject.AddComponent<CanvasGroup>();
        }

        originalPosition = rectTransform.anchoredPosition;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        icon.color = originalColor; // 초기화 시 원래 색상 적용

        // 아이콘의 위치를 원래 위치로 되돌림
        rectTransform.anchoredPosition = originalPosition;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        icon.color = originalColor; // 선택 해제 상태로 복귀
        hasBeenSelected = false; // 슬롯 비우기 시 선택 상태 초기화
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
                else
                {
                    DeselectSlot();
                }
            }
            else
            {
                InventoryManager.Instance.DeselectAllSlots();
                SelectSlot();
                hasBeenSelected = true; // 처음 선택되었음을 기록
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
        if (isSelected)
        {
            icon.color = selectedColor; // 선택 상태일 때 회색으로 변경
        }
        else
        {
            icon.color = originalColor; // 선택 해제 시 원래 색상으로 복원
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 아이템이 선택되었고 이전에 한 번 이상 선택된 상태에서만 드래그 가능
        if (item != null && isSelected && hasBeenSelected)
        {
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            eventData.pointerDrag = null; // 드래그를 취소
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && isSelected && hasBeenSelected)
        {
            rectTransform.position = Input.mousePosition; // 드래그 시 마우스를 따라가도록 수정
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null && isSelected)
        {
            canvasGroup.blocksRaycasts = true;

            // 드롭이 실패했을 때 원래 위치로 복구
            if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<DropZone>() == null)
            {
                rectTransform.anchoredPosition = originalPosition; // 아이템을 원래 슬롯의 위치로 되돌림
                DeselectSlot();
            }
        }
    }
}
