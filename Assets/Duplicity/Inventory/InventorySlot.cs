using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item; // 슬롯에 포함된 아이템
    public Image icon; // 슬롯 아이콘 이미지
    public Image dragIcon; // 드래그 중 보여줄 아이콘

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition; // 드래그 실패 시 원래 위치로 복구

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
            dragIcon.gameObject.SetActive(false); // 초기에는 드래그 아이콘을 숨김
        }

        if (icon != null)
        {
            icon.enabled = true; // 아이콘 표시
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true; // 아이콘 표시
        icon.color = Color.white;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false; // 아이콘 숨김
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            //InventoryManager.Instance.DeselectAllSlots(); // 모든 슬롯 선택 해제
            InventoryManager.Instance.ShowDetailPanel(item); // 디테일 패널 표시
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null) // 슬롯에 아이템이 있는지 확인
        {
            canvasGroup.blocksRaycasts = false; // 드래그 중 클릭 비활성화
            originalPosition = rectTransform.anchoredPosition; // 원래 위치 저장

            if (dragIcon != null)
            {
                dragIcon.sprite = icon.sprite;
                dragIcon.color = icon.color;
                dragIcon.gameObject.SetActive(true); // 드래그 아이콘 활성화
                dragIcon.rectTransform.position = Input.mousePosition; // 드래그 시작 위치
            }
        }
        else
        {
            eventData.pointerDrag = null; // 드래그 취소
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null && dragIcon != null)
        {
            dragIcon.rectTransform.position = Input.mousePosition; // 드래그 아이콘을 마우스 위치로 따라가게 함
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            canvasGroup.blocksRaycasts = true;

            // Raycast로 드롭존 확인
            List<RaycastResult> results = GetRaycastResults(eventData);

            bool dropSucceeded = false;

            foreach (var result in results)
            {
                if(currentDay == 5)
                {
                    DropZone dropZone = result.gameObject.GetComponent<DropZone>();
                    if (dropZone != null)
                    {
                        dropZone.OnDrop(eventData); // 드롭존 처리
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
                        dropZone.OnDrop(eventData); // 드롭존 처리
                        dropSucceeded = true;
                        break;
                    }
                }

            }

            // 드롭 실패 시 원래 위치로 복구
            if (!dropSucceeded)
            {
                rectTransform.anchoredPosition = originalPosition;
            }

            if (dragIcon != null)
            {
                dragIcon.gameObject.SetActive(false); // 드래그 아이콘 숨김
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
