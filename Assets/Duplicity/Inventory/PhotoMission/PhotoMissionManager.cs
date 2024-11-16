using System.Collections;
using UnityEngine;

public class PhotoMissionManager : MonoBehaviour
{
    public Frame[] frames;
    public Item[] correctOrder;
    [Space(10)]
    public GameObject PhotoMissionPanel;
    public GameObject WallCrackPanel;
    public GameObject SecretNotePanel;
    [Space(10)]
    private PhotoMissionTimer timer;

    private void Start()
    {
        timer = GetComponent<PhotoMissionTimer>();
        WallCrackPanel.SetActive(false);
        SecretNotePanel.SetActive(false);
    }

    // 모든 프레임에 사진이 배치되었는지 확인
    public void OnPhotoPlaced()
    {
        if (AreAllFramesFilled())
        {
            StartCoroutine(DelayedCheckFrames());
        }
    }

    // 모든 프레임에 사진이 배치되었는지 여부를 반환
    private bool AreAllFramesFilled()
    {
        foreach (var frame in frames)
        {
            if (!frame.HasPhoto())
            {
                return false;
            }
        }
        return true;
    }

    // 일정 시간 지연 후 CheckFrames 호출
    private IEnumerator DelayedCheckFrames()
    {
        yield return new WaitForSeconds(0.1f); // 약간의 지연 추가
        CheckFrames();
    }

    public void CheckFrames()
    {
        bool isCorrect = true;
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].GetCurrentItem() != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("Photos are in the correct order!");
            timer.CompleteMission();
            WallCrackPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Photos are not in the correct order. Returning to inventory.");

            // 모든 슬롯의 선택 상태 해제
            //InventoryManager.Instance.DeselectAllSlots();

            // 인벤토리를 먼저 비운다
            InventoryManager.Instance.ClearAllItemSlot();

            // 모든 프레임의 아이템을 인벤토리에 추가
            foreach (var frame in frames)
            {
                if (frame.HasPhoto())
                {
                    InventoryManager.Instance.AddItemToInventory(frame.GetCurrentItem());
                }
            }

            // 모든 프레임 초기화
            foreach (var frame in frames)
            {
                frame.ResetFrame();
            }
        }
    }

    //OnClick 이벤트 연결
    public void OnSecretNoteClick()
    {
        SecretNotePanel.SetActive(true);
    }
    //OnClick 이벤트 연결
    public void OnCloseButton()
    {
        PhotoMissionPanel.SetActive(false);
    }
}
