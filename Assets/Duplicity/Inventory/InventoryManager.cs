using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // �̱��� �ν��Ͻ�
    public List<InventorySlot> slots; // ��� ���� ����Ʈ
    public GameObject detailPanel; // ������ �г�
    public Image detailImage; // ������ �̹���
    public TMP_Text detailDescription; // ������ ���� �ؽ�Ʈ
    public Item item;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ������ �г��� �ʱ� ���¿��� ��Ȱ��ȭ
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
        //AddItemToInventory(item);
    }

    // �������� ���Կ� �߰�
    public void AddItemToInventory(Item item)
    {
        foreach (var slot in slots)
        {
            // ������ ��� �ִ� ���
            if (slot.item == null)
            {
                slot.AddItem(item); // ���Կ� ������ �߰�
                Debug.Log($"{item.itemName}�� �κ��丮�� �߰���.");
                return;
            }
        }
        Debug.Log("�κ��丮 �� ��.");
    }

    // ��� ���� ����
    public void ClearAllItemSlot()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot(); // ���� ����
        }
        Debug.Log("�κ��丮 �����.");
    }

    // ������ �г� ǥ��
    public void ShowDetailPanel(Item item)
    {
        if (detailPanel != null && item != null && item.canViewDetails)
        {
            detailImage.sprite = item.itemIcon; // ������ �̹��� ǥ��
            detailDescription.text = item.itemDescription; // ������ ���� ǥ��
            detailPanel.SetActive(true); // ������ �г� Ȱ��ȭ
        }
    }

    // ������ �г� �����
    public void HideDetailPanel()
    {
        if (detailPanel != null)
        {
            detailPanel.SetActive(false);
        }
    }

    // Ư�� �������� ���� �ִ��� Ȯ��
    public bool HasItem(Item targetItem)
    {
        foreach (var slot in slots)
        {
            if (slot.item == targetItem) // ��� �����۰� ������ �������� �ִٸ�
            {
                return true;
            }
        }
        return false;
    }

    // Ư�� �������� �����ϰ� ���ο� ������ �߰�
    public bool ReplaceItem(Item targetItem, Item newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.item == targetItem) // ��� ������ �߰�
            {
                slot.ClearSlot(); // ���� ������ ����
                AddItemToInventory(newItem); // ���ο� ������ �߰�
                Debug.Log($"{targetItem.itemName}�������� {newItem.itemName}�� ����");
                return true;
            }
        }
        Debug.Log($"{targetItem.itemName}�� �κ��丮�� �������� ����.");
        return false;
    }
}
