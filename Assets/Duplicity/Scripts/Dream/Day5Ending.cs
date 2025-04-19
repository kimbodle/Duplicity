using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day5Ending : MonoBehaviour
{
    public Sprite wakeupImage;
    public Dialog dialog;

    public void StartDay5EndingDialog()
    {
        if(UIManager.Instance == null && DialogManager.Instance == null) { return; }
        UIManager.Instance.TogglInventoryUI();
        DialogManager.Instance.StartDialog(dialog, wakeupImage);

        // 다이얼로그 종료 시 Debug.Log 호출을 위한 콜백 등록
        DialogManager.Instance.OnDialogEnd += OnDialogEnd;

        GameManager.Instance.GetCurrentDayController().CompleteTask("WakeUp");
    }

    private void OnDialogEnd()
    {
        // 콜백을 한 번만 실행하도록 등록 해제
        DialogManager.Instance.OnDialogEnd -= OnDialogEnd;

        GameManager.Instance.CompleteTask("ShelterScene");
    }
}
