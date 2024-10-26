using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Manager : MonoBehaviour
{
    public Dialog[] dialog;
    public Sprite characterSprite;
    private int dialogIndex = 0;

    InteractionManager interactionManager;
    MissionTimer missionTimer;
    DialogManager dialogManager;

    void Start()
    {
        dialogManager = DialogManager.Instance;
        interactionManager= FindObjectOfType<InteractionManager>();
        missionTimer = FindObjectOfType<MissionTimer>();

        //다이얼로그 0
        dialogManager.StartDialog(dialog[dialogIndex], characterSprite);
        // 다이얼로그 종료 이벤트 구독
        dialogManager.OnDialogEnd += HandleDialogEnd;
    }
    private void HandleDialogEnd()
    {

        Debug.Log("다이얼로그가 종료되었습니다.");
        //day2Controller 에서 아이템 수집 미션 활성화 하기
        if(dialogIndex == 0)
        {
            interactionManager.isInteraction = true;
            missionTimer.isMissionActive = true;
            dialogIndex++;
        }

        if (dialogIndex == 1)
        {
            //지도 활성화
            Debug.Log("지도 활성화");
        }

    }
    public void CompleteItemCollected()
    {
        dialogManager.StartDialog(dialog[dialogIndex], characterSprite);
    }
    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd -= HandleDialogEnd;
        }
    }
}
