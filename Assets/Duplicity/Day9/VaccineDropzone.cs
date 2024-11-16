using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VaccineDropzone : MonoBehaviour, IDropHandler
{
    public string missionName; // �� ������� �̼� �̸�
    public string expectedItemName; // ������� �ʿ��� ������ �̸�
    public Sprite itemDroppedSprite; // ��� �� ������ �̹���
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
                Debug.Log($"{slot.item.itemName} ��� �Ϸ�! {missionName} �̼� ����!");
                slot.ClearSlot();

                // �̹��� ����
                if (dropZoneImage != null && itemDroppedSprite != null)
                {
                    dropZoneImage.sprite = itemDroppedSprite;
                }

                // �̼� �Ϸ� ó��
                VaccineMission.Instance.CompleteMission(missionName);
            }
            else
            {
                Debug.Log($"�߸��� ������: {slot.item.itemName}. �� ��������� {expectedItemName}��(��) �ʿ��մϴ�.");
            }
        }
        else
        {
            Debug.Log("�������� �����ϴ�!");
        }
    }
}
