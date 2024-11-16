using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // ���Կ� ���Ե� ������
    public Image icon; // ���� ������ �̹���
    public Image dragIcon; // �巡�� �� ������ ������

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition; // �巡�� ���� �� ���� ��ġ�� ����

    private int currentDay = 0;

    private void Start()
    {
        rectTransform = icon.GetComponent<RectTransform>();
        canvasGroup = icon.GetComponent<CanvasGroup>();
        currentDay = GameManager.Instance.GetCurrentDay();

        if (canvasGroup == null)
        {
            canvasGroup = icon.gameObject.AddComponent<CanvasGroup>();
        }

        if (dragIcon != null)
        {
            dragIcon.gameObject.SetActive(false); // �ʱ⿡�� �巡�� �������� ����
        }

        if (icon != null)
        {
            icon.enabled = true; // ������ ǥ��
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true; // ������ ǥ��
        icon.color = Color.white;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false; // ������ ����
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            //InventoryManager.Instance.DeselectAllSlots(); // ��� ���� ���� ����
            InventoryManager.Instance.ShowDetailPanel(item); // ������ �г� ǥ��
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null) // ���Կ� �������� �ִ��� Ȯ��
        {
            canvasGroup.blocksRaycasts = false; // �巡�� �� Ŭ�� ��Ȱ��ȭ
            originalPosition = rectTransform.anchoredPosition; // ���� ��ġ ����

            if (dragIcon != null)
            {
                dragIcon.sprite = icon.sprite;
                dragIcon.color = icon.color;
                dragIcon.gameObject.SetActive(true); // �巡�� ������ Ȱ��ȭ
                dragIcon.rectTransform.position = Input.mousePosition; // �巡�� ���� ��ġ
            }
        }
        else
        {
            eventData.pointerDrag = null; // �巡�� ���
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && dragIcon != null)
        {
            dragIcon.rectTransform.position = Input.mousePosition; // �巡�� �������� ���콺 ��ġ�� ���󰡰� ��
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            canvasGroup.blocksRaycasts = true;

            // Raycast�� ����� Ȯ��
            List<RaycastResult> results = GetRaycastResults(eventData);

            bool dropSucceeded = false;

            foreach (var result in results)
            {
                if(currentDay == 5)
                {
                    DropZone dropZone = result.gameObject.GetComponent<DropZone>();
                    if (dropZone != null)
                    {
                        dropZone.OnDrop(eventData); // ����� ó��
                        dropSucceeded = true;
                        break;
                    }
                }
                else if (currentDay == 9)
                {

                }
                {
                    VaccineDropzone dropZone = result.gameObject.GetComponent<VaccineDropzone>();
                    if (dropZone != null)
                    {
                        dropZone.OnDrop(eventData); // ����� ó��
                        dropSucceeded = true;
                        break;
                    }
                }

            }

            // ��� ���� �� ���� ��ġ�� ����
            if (!dropSucceeded)
            {
                rectTransform.anchoredPosition = originalPosition;
            }

            if (dragIcon != null)
            {
                dragIcon.gameObject.SetActive(false); // �巡�� ������ ����
            }
        }
    }

    private List<RaycastResult> GetRaycastResults(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }
}
