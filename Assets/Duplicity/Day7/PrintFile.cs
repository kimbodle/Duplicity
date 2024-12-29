using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintFile : MonoBehaviour, IMission
{
    public List<Item> printDocuments; // 인덱스별 문서 목록
    public GameObject printItem;
    public Day7MissionManager missionManager;

    public bool IsMissionCompleted { get; private set; }

    private void Start()
    {
        IsMissionCompleted = false;
        Button button = printItem.GetComponentInChildren<Button>(true); // 비활성화된 상태에서도 검색
        /*
        if (button != null)
        {
            button.onClick.AddListener(() => GetPrint(0));
        }
        else
        {
            Debug.LogError("비활성화된 상태에서도 Button을 찾을 수 없습니다.");
        }*/
    }
    public void Initialize()
    {
        //초기화x
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void GetPrint(int documentIndex)
    {
        if (InventoryManager.Instance != null && documentIndex >= 0 && documentIndex < printDocuments.Count)
        {
            IsMissionCompleted = true;
            InventoryManager.Instance.AddItemToInventory(printDocuments[documentIndex]); // 인덱스에 맞는 문서 추가
            Debug.Log($"문서 {documentIndex} 추가됨.");

            // 프린트 아이템 제거
            Destroy(printItem);

            // Day7MissionManager에 상태 갱신 요청
            missionManager.CheckAllMission();
        }
        else
        {
            Debug.LogError("문서 추가 실패 또는 잘못된 인덱스.");
        }
    }
}
