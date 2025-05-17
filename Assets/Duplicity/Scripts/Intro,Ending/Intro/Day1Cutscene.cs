using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day1Cutscene : MonoBehaviour
{
    public Image sceneImage;
    public TMP_Text dialogueText;
    public Button nextButton;
    public Button choiceButton1;
    public Button choiceButton2;
    public AudioClipData[] Effect;

    public Sprite[] cutsceneImages;
    public float typingSpeed = 0.05f;

    private bool isTyping = false;
    private bool isChoiceProcessing = false; // 선택지 처리 중 여부
    private int dialogueIndex = 0;
    private bool isFirstChoice = true;

    private Day1Controller day1Controller;

    private string[] dialogues = {
        ".... ",
        "쿵 -",
        "?… 이게 무슨 소리지?\n연구소 밖으로 나가봐야 하는 걸까..?",
        "??????????.....",
        "내가 보고 있는 게 진짜야..?\n지금 전쟁 중이라고…?",
        "어떡하지, 어떡해.. 내가 뭘 해야 하지?\n아니, 내가 뭘 할 수 있는 거야?!",
    };

    void Start()
    {
        day1Controller = FindObjectOfType<Day1Controller>();
        ShowNextDialogue();
        nextButton.onClick.AddListener(OnNextButtonClick);
        choiceButton1.onClick.AddListener(() => OnChoiceClick(1));
        choiceButton2.onClick.AddListener(() => OnChoiceClick(2));
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
    }

    void Update()
    {
        // 안드로이드가 아닐 때만 키 입력 허용
        if (Application.platform != RuntimePlatform.Android)
        {
            // 대사 진행 중이 아니고, 선택지 처리 중도 아니면
            if (Input.GetKeyDown(KeyCode.Space) && !isTyping && !isChoiceProcessing)
            {
                OnNextButtonClick();
                AudioManager.Instance.PlayUIButton();
            }
        }
    }

    public void OnNextButtonClick()
    {
        if (!isTyping)
        {
            ShowNextDialogue();
        }
    }

    public void ShowNextDialogue()
    {
        if (dialogueIndex < dialogues.Length)
        {
            switch (dialogueIndex)
            {
                case 0:
                    sceneImage.sprite = cutsceneImages[0];
                    StartCoroutine(PlaySoundWithDelay(Effect[0].clip, 2f));
                    StartCoroutine(PlaySoundWithDelay(Effect[1].clip, 2f));
                    StartCoroutine(PlaySoundWithDelay(Effect[2].clip, 2f));
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    break;

                case 1:
                    sceneImage.sprite = cutsceneImages[1];
                    AudioManager.Instance.PlaySFX(Effect[4].clip);
                    StartCoroutine(PlaySoundWithDelay(Effect[3].clip, 0.5f));
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    break;

                case 2:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    SetChoices("나간다", "나가지 않는다");
                    break;

                case 3:
                    AudioManager.Instance.PlaySFX(Effect[5].clip);
                    AudioManager.Instance.PlaySFX(Effect[6].clip);
                    StartCoroutine(PlaySoundWithDelay(Effect[7].clip, 1f));
                    sceneImage.sprite = cutsceneImages[2];
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    break;

                case 4:
                    AudioManager.Instance.PlaySFX(Effect[8].clip);
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    break;

                case 5:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    SetChoices("맞서 싸운다", "연구소로 돌아간다");
                    break;
            }

            dialogueIndex++;
        }
    }
    private IEnumerator PlaySoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay); // 지연 시간 대기
        AudioManager.Instance.PlaySFX(clip);    // 효과음 재생
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        isTyping = true;
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void SetChoices(string choice1, string choice2)
    {
        nextButton.gameObject.SetActive(false);
        choiceButton1.GetComponentInChildren<TMP_Text>().text = choice1;
        choiceButton2.GetComponentInChildren<TMP_Text>().text = choice2;
        choiceButton1.gameObject.SetActive(true);
        choiceButton2.gameObject.SetActive(true);
        isChoiceProcessing = true; // 선택지 처리 활성화
    }

    private void HideChoiceButton()
    {
        nextButton.gameObject.SetActive(true);
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        isChoiceProcessing = false; // 선택지 처리 종료
    }

    public void OnChoiceClick(int choice)
    {
        if (isTyping || !isChoiceProcessing) return; // 선택지 처리 중 아니면 무시

        if (isFirstChoice)
        {
            if (choice == 1)
            {
                dialogueText.text = "";
                HideChoiceButton();
                ShowNextDialogue();
                isFirstChoice = false;
            }
            else
            {
                FadeManager.Instance.StartFadeOut(() =>
                {
                    EndingManager.Instance.LoadEnding("GameOver", "나가지 않는다", 0);
                }, true, 3f);
            }
        }
        else
        {
            if (choice == 1)
            {
                FadeManager.Instance.StartFadeOut(() =>
                {
                    EndingManager.Instance.LoadEnding("GameOver", "맞서 싸운다", 1);
                }, true, 3f);
            }
            else
            {
                dialogueText.text = "";
                HideChoiceButton();
                StartCoroutine(CompleteTaskWithDialogue("그래 일단 연구소로 돌아가서\n중요한 물품들을 챙기고 얼른 여기를 떠나자"));
            }
        }
    }

    private IEnumerator CompleteTaskWithDialogue(string dialogue)
    {
        yield return StartCoroutine(TypeDialogue(dialogue));
        yield return new WaitForSeconds(2f);
        day1Controller.CompleteTask("Day1CutScene");
        GameManager.Instance.CompleteTask("LaboratoryScene");
    }
}
