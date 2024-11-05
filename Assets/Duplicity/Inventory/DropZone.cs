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
            frame.SetPhoto(slot.item);
            slot.ClearSlot(); // ������ ��� �� ���� ���
        }
    }
}
