using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeBoxMission : MonoBehaviour, IMission
{
    public Button[] numberButtons; // 숫자 버튼 배열 (9개의 흰색 버튼)
    public TMP_Text inputDisplay; // 빨간 부분에 입력된 숫자를 표시하는 텍스트
    public Button submitButton; // 노란색 확인 버튼
    private string currentInput = ""; // 현재 입력된 숫자 문자열
    private string correctAnswer = "12760"; // 정답
    [Space(10)]
    public GameObject boxOpenImage;
    public Button regen;
    public Item regeneratium;
    [Space(10)]
    public Button boxCloseButton;
    public bool IsMissionCompleted { get; private set; }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void Initialize()
    {
        //x
    }

    void Start()
    {
        // 숫자 버튼에 클릭 이벤트 추가
        foreach (Button button in numberButtons)
        {
            int number = int.Parse(button.GetComponentInChildren<TMP_Text>().text); // 버튼의 숫자를 가져옴
            button.onClick.AddListener(() => OnNumberButtonClick(number));
        }

        // 제출 버튼에 클릭 이벤트 추가
        submitButton.onClick.AddListener(CheckAnswer);
        regen.onClick.AddListener(GetRegen);

        // 초기화
        inputDisplay.text = "";
    }

    // 숫자 버튼 클릭 시 호출되는 함수
    private void OnNumberButtonClick(int number)
    {
        if (currentInput.Length < 10) // 입력 최대 길이 제한
        {
            currentInput += number.ToString();
            inputDisplay.text = currentInput;
        }
    }

    // 정답 확인 함수
    private void CheckAnswer()
    {
        if (currentInput == correctAnswer)
        {
            Debug.Log("정답");
            CompleteMission();
        }
        else
        {
            Debug.Log("틀림");
            ResetInput();
        }
    }

    // 미션 성공 처리
    private void CompleteMission()
    {
        boxOpenImage.SetActive(true);
    }

    // 입력 초기화 함수
    private void ResetInput()
    {
        currentInput = "";
        inputDisplay.text = "";
    }

    private void GetRegen()
    {
        Debug.Log("미션 클리어!");
        InventoryManager.Instance.AddItemToInventory(regeneratium);
        GameManager.Instance.GetCurrentDayController().CompleteTask("GetRegen");
        IsMissionCompleted = true;
        Destroy(regen.gameObject);
    }

    //Onclick 이벤트 연결
    public void OnclickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
