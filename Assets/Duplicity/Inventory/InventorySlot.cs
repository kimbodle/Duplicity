using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Image icon;
    public Image dragIcon; // �巡���� �� ���Ǵ� ������
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

        // �ʱ⿡�� �巡�� �������� ��Ȱ��ȭ�մϴ�.
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

        // �������� ��ġ�� ���� ��ġ�� �ǵ���
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
            canvasGroup.blocksRaycasts = false; // �巡�� �� Ŭ�� ��Ȱ��ȭ
            // �巡�� ������ Ȱ��ȭ �� �ʱ� ����
            if (dragIcon != null)
            {
                dragIcon.sprite = icon.sprite;
                dragIcon.color = icon.color;
                dragIcon.gameObject.SetActive(true);
                dragIcon.rectTransform.position = Input.mousePosition; // �巡�� ���� ��ġ
            }
        }
        else
        {
            eventData.pointerDrag = null; // �巡�׸� ���
            DeselectSlot();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && isSelected && hasBeenSelected && dragIcon != null)
        {
            dragIcon.rectTransform.position = Input.mousePosition; // �巡�� �������� ���콺 ��ġ�� ���󰡰� ��
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null && isSelected)
        {
            canvasGroup.blocksRaycasts = true;

            // Raycast�� �ٸ� ĵ������ ����� Ž��
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

            // ����� �������� �� �巡�� �������� ��Ȱ��ȭ�ϰ� ���� ������ ����
            if (!dropSucceeded)
            {
                rectTransform.anchoredPosition = originalPosition;
                DeselectSlot();
            }

            // �巡�� ���� �� �巡�� ������ ��Ȱ��ȭ
            if (dragIcon != null)
            {
                dragIcon.gameObject.SetActive(false);
            }

            isSelected = false;
            hasBeenSelected = false;
        }
    }
}
