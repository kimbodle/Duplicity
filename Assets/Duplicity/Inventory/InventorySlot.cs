using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public Image icon;
    public bool isSelected = false;
    public bool hasBeenSelected = false; // ���ο� �÷��� �߰�
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
        icon.color = originalColor; // �ʱ�ȭ �� ���� ���� ����

        // �������� ��ġ�� ���� ��ġ�� �ǵ���
        rectTransform.anchoredPosition = originalPosition;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        icon.color = originalColor; // ���� ���� ���·� ����
        hasBeenSelected = false; // ���� ���� �� ���� ���� �ʱ�ȭ
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
                hasBeenSelected = true; // ó�� ���õǾ����� ���
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
            icon.color = selectedColor; // ���� ������ �� ȸ������ ����
        }
        else
        {
            icon.color = originalColor; // ���� ���� �� ���� �������� ����
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �������� ���õǾ��� ������ �� �� �̻� ���õ� ���¿����� �巡�� ����
        if (item != null && isSelected && hasBeenSelected)
        {
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            eventData.pointerDrag = null; // �巡�׸� ���
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && isSelected && hasBeenSelected)
        {
            rectTransform.position = Input.mousePosition; // �巡�� �� ���콺�� ���󰡵��� ����
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null && isSelected)
        {
            canvasGroup.blocksRaycasts = true;

            // ����� �������� �� ���� ��ġ�� ����
            if (!eventData.pointerEnter || eventData.pointerEnter.GetComponent<DropZone>() == null)
            {
                rectTransform.anchoredPosition = originalPosition; // �������� ���� ������ ��ġ�� �ǵ���
                DeselectSlot();
            }
        }
    }
}
