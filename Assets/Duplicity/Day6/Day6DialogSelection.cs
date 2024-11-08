using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day6DialogSelection : MonoBehaviour
{
    public TMP_Text dialogueText; // 대사 텍스트
    public Button choiceButton1; // 선택지 1 버튼
    public Button choiceButton2; // 선택지 2 버튼
    [Space(10)]
    public Dialog BadendingDialog;
    public Dialog OpenRegenDialog;
    // Start is called before the first frame update
    void Start()
    {
        // 선택지 버튼에 클릭 이벤트 연결
        choiceButton1.onClick.AddListener(() => OnChoiceClick(1)); // 선택지 1 클릭 시
        choiceButton2.onClick.AddListener(() => OnChoiceClick(2)); // 선택지 2 클릭 시

        // 선택지 버튼 초기 비활성화
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
    }

    // 외부에서 호출하여 선택지 버튼 표시
    public void ShowChoices()
    {
        SetChoices("주변을 돌아보지 못한 내 잘못이야.", "그런데 난 정말 몰랐어. 이게 정말 모두 다 내 잘못이야?");
    }

    private void SetChoices(string choice1, string choice2)
    {
        choiceButton1.GetComponentInChildren<TMP_Text>().text = choice1;
        choiceButton2.GetComponentInChildren<TMP_Text>().text = choice2;
        choiceButton1.gameObject.SetActive(true); // 선택지 1 활성화
        choiceButton2.gameObject.SetActive(true); // 선택지 2 활성화
    }

    // 선택지 버튼 클릭 처리
    private void OnChoiceClick(int choice)
    {
        if (choice == 1)
        {
            Debug.Log("엔딩");
            DialogManager.Instance.PlayerMessageDialog(BadendingDialog);
            //엔딩 매니저 

        }
        else
        {
            Debug.Log("저장소 오픈");
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
}
