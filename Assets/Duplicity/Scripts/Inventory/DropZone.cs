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
                slot.ClearSlot(); // ������ �ɸ� ��쿡�� ���� ���
                AudioManager.Instance.PlayUIButton();
            }
        }
    }

}
