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

        // ���̾�α� ���� �̺�Ʈ ����
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
        // �̺�Ʈ ���� ����
        if (dialogManager != null)
        {
            dialogManager.OnDialogEnd -= HandleDialogEnd;
        }
    }
}
