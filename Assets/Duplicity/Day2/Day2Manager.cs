using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2Manager : MonoBehaviour
{
    public Dialog[] dialog;
    public MissionTimer missionTimer;

    private int dialogIndex = 0;

    private InteractionManager interactionManager;
    private DialogManager dialogManager;

    void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
        if(DialogManager.Instance != null )
        {
            dialogManager = DialogManager.Instance;
            //다이얼로그 0
            dialogManager.PlayerMessageDialog(dialog[dialogIndex]);
            // 다이얼로그 종료 이벤트 구독
            dialogManager.OnDialogEnd += HandleDialogEnd;
        }
        
    }
    private void HandleDialogEnd()
    {

        Debug.Log("다이얼로그가 종료되었습니다.");

        //다이얼로그 종료 후 아이템 수집 미션 활성화
        if(dialogIndex == 0)
        {
            interactionManager.isInteraction = true;
            missionTimer.isMissionActive = true;
            missionTimer.gameObject.SetActive(true);
        }
        //미션 수집 종료 후 활성화
        if (dialogIndex == 1)
        {
            //지도 활성화
            Debug.Log("지도 활성화");
            UIManager.Instance.ActiveMapIcon();
            MapManager.Instance.UnlockRegion("ShelterScene");
        }

    }
    public void CompleteItemCollected()
    {
        dialogIndex++;
        dialogManager.PlayerMessageDialog(dialog[dialogIndex]);
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
