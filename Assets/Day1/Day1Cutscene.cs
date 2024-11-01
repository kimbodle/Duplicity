using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day1Cutscene : MonoBehaviour
{
    public Image sceneImage; // 씬에 표시될 이미지
    public TMP_Text dialogueText; // 대사 텍스트
    public Button nextButton; // 다음 문장 버튼
    public Button choiceButton1; // 선택지 1 버튼
    public Button choiceButton2; // 선택지 2 버튼
    public AudioSource soundEffect; // 효과음

    private int dialogueIndex = 0;
    private bool isFirstChoice = true; // 첫 번째 선택지 여부 확인

    private Day1Controller day1Controller;

    // 대사 배열
    private string[] dialogues = {
        "연구중인 로라", //0
        "쿵 -", // 1
        "?… 이게 무슨 일이지? 연구소 밖으로 나가봐야 하는 걸까..?", // 2
        "??????????.....", // 3
        "내가 지금 보고 있는 게 진짜야…? 전쟁 중이라고…?", // 4
        "어떡하지 어떡해 내가 지금 뭘 해야 해 내가 뭘 할 수 있는 거야",  // 5 
    };

    // 컷씬 이미지 배열
    public Sprite[] cutsceneImages;

    public float typingSpeed = 0.05f; // 타이핑 속도
    private bool isTyping = false; // 타이핑 진행 여부 확인용 변수

    void Start()
    {
        day1Controller = FindObjectOfType<Day1Controller>();
        // 첫 컷씬 시작
        ShowNextDialogue();
        nextButton.onClick.AddListener(OnNextButtonClick);
        choiceButton1.onClick.AddListener(() => OnChoiceClick(1)); // 선택지 1
        choiceButton2.onClick.AddListener(() => OnChoiceClick(2)); // 선택지 2
        choiceButton1.gameObject.SetActive(false); // 초기에는 선택지 버튼 비활성화
        choiceButton2.gameObject.SetActive(false);
    }

    public void OnNextButtonClick()
    {
        if (!isTyping)
        {
            ShowNextDialogue(); // 타이핑이 끝났을 때만 다음 대사로 이동
        }
    }

    public void ShowNextDialogue()
    {
        if (dialogueIndex <= dialogues.Length)
        {
            switch (dialogueIndex)
            {
                case 0:
                    sceneImage.sprite = cutsceneImages[0]; // 이미지 변경
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 효과 적용
                    break;

                case 1:
                    sceneImage.sprite = cutsceneImages[1];
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    break;

                case 2:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    SetChoices("나간다", "나가지 않는다");
                    break;

                case 3:
                    sceneImage.sprite = cutsceneImages[2]; // 이미지 변경
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 효과 적용
                    break;

                case 4:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 효과 적용
                    break;

                case 5:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // 타이핑 효과 적용
                    SetChoices("맞서 싸운다", "연구소로 돌아간다");
                    break;
            }

            dialogueIndex++; // 마지막에 인덱스 증가
            Debug.Log(dialogueIndex);
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // 기존 텍스트 초기화
        isTyping = true; // 타이핑 중임을 표시
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // 한 글자씩 타이핑
        }
        isTyping = false; // 타이핑이 끝나면 표시
    }

    private void SetChoices(string choice1, string choice2)
    {
        nextButton.gameObject.SetActive(false);
        choiceButton1.GetComponentInChildren<TMP_Text>().text = choice1;
        choiceButton2.GetComponentInChildren<TMP_Text>().text = choice2;
        choiceButton1.gameObject.SetActive(true);
        choiceButton2.gameObject.SetActive(true);
    }


    private void HideChoiceButton()
    {
        nextButton.gameObject.SetActive(true); // 다음 버튼 활성화
        choiceButton1.gameObject.SetActive(false); // 선택지1 비활성화
        choiceButton2.gameObject.SetActive(false); // 선택지2 비활성화
    }
    public void OnChoiceClick(int choice)
    {
        if (!isTyping)
        {
            if (isFirstChoice)
            {
                if (choice == 1)
                {
                    dialogueText.text = "";
                    ShowNextDialogue();
                    HideChoiceButton();
                    isFirstChoice = false;
                }
                else
                {
                    Debug.Log("Game Over: 나가지 않는다 선택");
                    EndingManager.Instance.LoadEnding("BadEnding", "나가지 않는다");
                }
            }
            else
            {
                // 두 번째 선택지 처리
                if (choice == 1)
                {
                    Debug.Log("Game Over: 맞서 싸운다 선택");
                    EndingManager.Instance.LoadEnding("BadEnding", "맞서 싸운다");
                }
                else
                {
                    dialogueText.text = "";
                    StartCoroutine(CompleteTaskWithDialogue("그래 일단 연구소로 돌아가서 중요한 물품들을 챙기고 얼른 여기를 떠나자"));
                }
            }
        }
    }
    private IEnumerator CompleteTaskWithDialogue(string dialogue)
    {
        //yield return StartCoroutine(TypeDialogue(dialogue)); // 타이핑 완료될 때까지 대기
        yield return StartCoroutine(TypeDialogue(dialogue));
        yield return new WaitForSeconds(2f);
        day1Controller.CompleteTask("Day1CutScene");
        GameManager.Instance.CompleteTask("Day2Scene");
    }
}
