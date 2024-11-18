using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day6DialogSelection : MonoBehaviour
{
    public GameObject ChoicePanel;
    public Button choiceButton1; // 선택지 1 버튼
    public Button choiceButton2; // 선택지 2 버튼
    [Space(10)]
    public Dialog BadendingDialog;
    public Dialog OpenRegenDialog;

    private bool isChoiceMade = false; // 선택이 이미 이루어졌는지 확인하는 플래그

    void Start()
    {
        if (GameManager.Instance.GetCurrentDay() == 6)
        {
            // 선택지 버튼에 클릭 이벤트 연결
            choiceButton1.onClick.AddListener(() => OnChoiceClick(1)); // 선택지 1 클릭 시
            choiceButton2.onClick.AddListener(() => OnChoiceClick(2)); // 선택지 2 클릭 시

            // 선택지 버튼 초기 비활성화
            ChoicePanel.SetActive(false);
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
        }
    }

    // 외부에서 호출하여 선택지 버튼 표시
    public void ShowChoices()
    {
        if (isChoiceMade) return; // 이미 선택이 이루어졌으면 반환하여 중복 클릭 방지

        ChoicePanel.SetActive(true);
        SetChoices("그런데 난 정말 몰랐어.\n이게 정말 모두 다 내 잘못이야?", "주변을 돌아보지 못한 내 잘못이야.");
    }

    private void SetChoices(string choice1, string choice2)
    {
        choiceButton1.GetComponent<TMP_Text>().text = choice1;
        choiceButton2.GetComponent<TMP_Text>().text = choice2;
        choiceButton1.gameObject.SetActive(true); // 선택지 1 활성화
        choiceButton2.gameObject.SetActive(true); // 선택지 2 활성화
    }

    // 선택지 버튼 클릭 처리
    private void OnChoiceClick(int choice)
    {
        if (isChoiceMade) return; // 이미 선택을 했다면 함수 종료 (중복 클릭 방지)

        isChoiceMade = true; // 선택지가 클릭되었음을 표시
        ChoicePanel.SetActive(false); // 선택지 패널 숨기기

        if (choice == 1)
        {
            Debug.Log("엔딩");
            // 다이얼로그 종료 시 Debug.Log 호출을 위한 콜백 등록
            DialogManager.Instance.OnDialogEnd += OnDialogEnd;

            DialogManager.Instance.PlayerMessageDialog(BadendingDialog);
        }
        else
        {
            Debug.Log("저장소 오픈");
            MapManager.Instance.UnlockRegion("RegenScene");
            DialogManager.Instance.PlayerMessageDialog(OpenRegenDialog);
            GameManager.Instance.GetCurrentDayController().CompleteTask("OpenRegen");
        }

        HideChoiceButtons();
    }

    // 선택지 버튼 숨기기
    private void HideChoiceButtons()
    {
        choiceButton1.gameObject.SetActive(false); // 선택지 1 비활성화
        choiceButton2.gameObject.SetActive(false); // 선택지 2 비활성화
    }

    private void OnDialogEnd()
    {
        // 콜백을 한 번만 실행하도록 등록 해제
        DialogManager.Instance.OnDialogEnd -= OnDialogEnd;

        EndingManager.Instance.LoadEnding("Ending", "배드엔딩", 0);
    }
}
