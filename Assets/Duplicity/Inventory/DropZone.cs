using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Frame frame;

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot slot = eventData.pointerDrag.GetComponent<InventorySlot>();

        if (slot != null && slot.item != null)
        {
            bool success = frame.SetPhoto(slot.item);

            if (success)
            {
                slot.ClearSlot(); // 사진이 걸린 경우에만 슬롯 비움
                AudioManager.Instance.PlayUIButton();
            }
        }
    }

}
