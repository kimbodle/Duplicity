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

        // ���̾�α� ���� �� Debug.Log ȣ���� ���� �ݹ� ���
        DialogManager.Instance.OnDialogEnd += OnDialogEnd;

        GameManager.Instance.GetCurrentDayController().CompleteTask("WakeUp");
    }

    private void OnDialogEnd()
    {
        // �ݹ��� �� ���� �����ϵ��� ��� ����
        DialogManager.Instance.OnDialogEnd -= OnDialogEnd;

        GameManager.Instance.CompleteTask("ShelterScene");
    }
}
