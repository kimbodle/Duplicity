using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0IntroPlay : MonoBehaviour
{
    public Dialog dialogIntro;

    Day0Controller day0controller;
    DialogManager dialogManager;

    private void Start()
    {
        day0controller = FindObjectOfType<Day0Controller>();

        dialogManager = DialogManager.Instance;

        // 다이얼로그 종료 이벤트 구독
        dialogManager.OnDialogEnd += HandleDialogEnd;
        DialogManager.Instance.PlayerMessageDialog(dialogIntro);
    }

    private void HandleDialogEnd()
    {
        day0controller.CompleteTask("Intro");
        GameManager.Instance.CompleteTask("Day1Scene");
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
