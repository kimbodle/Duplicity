using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClick : MonoBehaviour
{
    public Item item; // 획득할 아이템 정보
    // 마우스 클릭을 감지했을 때 호출되는 메서드
    private void OnMouseDown()
    {
        // 인벤토리에 아이템 추가
        InventoryManager.Instance.AddItemToInventory(item);
        Debug.Log($"{item.itemName}을(를) 획득했습니다!");
        

        // 아이템 오브젝트를 비활성화하거나 삭제
        //Destroy(gameObject);
    }
}
