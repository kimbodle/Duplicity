using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day0IntroPlay : MonoBehaviour
{
    public Sprite mainCharacterSprite; // 주인공 이미지
    public Dialog dialogIntro; // 에디터에서 설정할 Dialog 객체
    //Dialog dialogIntro;

    Day0Controller day0controller;
    DialogManager dialogManager;

    private void Start()
    {
        //dialogIntro = GetComponent<Dialog>();
        day0controller = FindObjectOfType<Day0Controller>();

        dialogManager = DialogManager.Instance;
        dialogManager.characterImage.sprite = mainCharacterSprite;

        // 다이얼로그 종료 이벤트 구독
        dialogManager.OnDialogEnd += HandleDialogEnd;

        DialogManager.Instance.StartDialog(dialogIntro, mainCharacterSprite);
    }

    public void OnClickIntroTestButton()
    {
        DialogManager.Instance.StartDialog(dialogIntro, mainCharacterSprite);
    }

    private void HandleDialogEnd()
    {
        // 다이얼로그가 끝났을 때 실행할 코드
        Debug.Log("다이얼로그가 종료되었습니다.");

        day0controller.CompleteTask("Intro");
        GameManager.Instance.CompleteTask();
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
