using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuseRabbit : MonoBehaviour
{
    public Dialog dialog; // �ش� ĳ������ ���̾�α�
    public Sprite characterSprite; // �ش� ĳ������ �̹���

    private DialogManager dialogManager;

    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
    }

    private void OnMouseDown()
    {
        if (dialogManager != null)
        {
            dialogManager.StartDialog(dialog, characterSprite);
            // �� DayController�� MarkTaskComplete ȣ��
            DayController dayController = FindObjectOfType<DayController>();
            dayController.CompleteTask("DialogWithCharacter"); // ����
        }
    }
}
