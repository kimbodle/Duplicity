using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VaccineDropzone : MonoBehaviour, IDropHandler
{
    public string missionName; // 이 드롭존의 미션 이름
    public string expectedItemName; // 드롭존에 필요한 아이템 이름
    public Sprite itemDroppedSprite; // 드롭 후 변경할 이미지
    private Image dropZoneImage;

    private void Start()
    {
        dropZoneImage = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();

        if (slot != null && slot.item != null)
        {
            if (slot.item.itemName == expectedItemName)
            {
                Debug.Log($"{slot.item.itemName} 드롭 완료! {missionName} 미션 수행!");
                slot.ClearSlot();

                // 이미지 변경
                if (dropZoneImage != null && itemDroppedSprite != null)
                {
                    dropZoneImage.sprite = itemDroppedSprite;
                }

                // 미션 완료 처리
                VaccineMission.Instance.CompleteMission(missionName);
            }
            else
            {
                Debug.Log($"잘못된 아이템: {slot.item.itemName}. 이 드롭존에는 {expectedItemName}이(가) 필요합니다.");
            }
        }
        else
        {
            Debug.Log("아이템이 없습니다!");
        }
    }
}
