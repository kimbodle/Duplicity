using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefuseRabbit : MonoBehaviour
{
    public Dialog dialog; // 해당 캐릭터의 다이얼로그
    public Sprite characterSprite; // 해당 캐릭터의 이미지

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
            // 각 DayController의 MarkTaskComplete 호출
            DayController dayController = FindObjectOfType<DayController>();
            dayController.CompleteTask("DialogWithCharacter"); // 예시
        }
    }
}
