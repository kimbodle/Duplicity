using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClick : MonoBehaviour
{
    public Item item; // ȹ���� ������ ����
    // ���콺 Ŭ���� �������� �� ȣ��Ǵ� �޼���
    private void OnMouseDown()
    {
        // �κ��丮�� ������ �߰�
        InventoryManager.Instance.AddItemToInventory(item);
        Debug.Log($"{item.itemName}��(��) ȹ���߽��ϴ�!");
        

        // ������ ������Ʈ�� ��Ȱ��ȭ�ϰų� ����
        //Destroy(gameObject);
    }
}
