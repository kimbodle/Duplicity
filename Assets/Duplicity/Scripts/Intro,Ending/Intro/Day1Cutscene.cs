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
    private bool isChoiceProcessing = false; // ������ ó�� �� ����
    private int dialogueIndex = 0;
    private bool isFirstChoice = true;

    private Day1Controller day1Controller;

    private string[] dialogues = {
        ".... ",
        "�� -",
        "?�� �̰� ���� �Ҹ���?\n������ ������ �������� �ϴ� �ɱ�..?",
        "??????????.....",
        "���� ���� �ִ� �� ��¥��..?\n���� ���� ���̶��?",
        "�����, ���.. ���� �� �ؾ� ����?\n�ƴ�, ���� �� �� �� �ִ� �ž�?!",
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
        // �ȵ���̵尡 �ƴ� ���� Ű �Է� ���
        if (Application.platform != RuntimePlatform.Android)
        {
            // ��� ���� ���� �ƴϰ�, ������ ó�� �ߵ� �ƴϸ�
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
                    SetChoices("������", "������ �ʴ´�");
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
                    SetChoices("�¼� �ο��", "�����ҷ� ���ư���");
                    break;
            }

            dialogueIndex++;
        }
    }
    private IEnumerator PlaySoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay); // ���� �ð� ���
        AudioManager.Instance.PlaySFX(clip);    // ȿ���� ���
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
        isChoiceProcessing = true; // ������ ó�� Ȱ��ȭ
    }

    private void HideChoiceButton()
    {
        nextButton.gameObject.SetActive(true);
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
        isChoiceProcessing = false; // ������ ó�� ����
    }

    public void OnChoiceClick(int choice)
    {
        if (isTyping || !isChoiceProcessing) return; // ������ ó�� �� �ƴϸ� ����

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
                    EndingManager.Instance.LoadEnding("GameOver", "������ �ʴ´�", 0);
                }, true, 3f);
            }
        }
        else
        {
            if (choice == 1)
            {
                FadeManager.Instance.StartFadeOut(() =>
                {
                    EndingManager.Instance.LoadEnding("GameOver", "�¼� �ο��", 1);
                }, true, 3f);
            }
            else
            {
                dialogueText.text = "";
                HideChoiceButton();
                StartCoroutine(CompleteTaskWithDialogue("�׷� �ϴ� �����ҷ� ���ư���\n�߿��� ��ǰ���� ì��� �� ���⸦ ������"));
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
