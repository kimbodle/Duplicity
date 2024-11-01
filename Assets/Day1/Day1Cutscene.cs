using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day1Cutscene : MonoBehaviour
{
    public Image sceneImage; // ���� ǥ�õ� �̹���
    public TMP_Text dialogueText; // ��� �ؽ�Ʈ
    public Button nextButton; // ���� ���� ��ư
    public Button choiceButton1; // ������ 1 ��ư
    public Button choiceButton2; // ������ 2 ��ư
    public AudioSource soundEffect; // ȿ����

    private int dialogueIndex = 0;
    private bool isFirstChoice = true; // ù ��° ������ ���� Ȯ��

    private Day1Controller day1Controller;

    // ��� �迭
    private string[] dialogues = {
        "�������� �ζ�", //0
        "�� -", // 1
        "?�� �̰� ���� ������? ������ ������ �������� �ϴ� �ɱ�..?", // 2
        "??????????.....", // 3
        "���� ���� ���� �ִ� �� ��¥�ߡ�? ���� ���̶��?", // 4
        "����� ��� ���� ���� �� �ؾ� �� ���� �� �� �� �ִ� �ž�",  // 5 
    };

    // �ƾ� �̹��� �迭
    public Sprite[] cutsceneImages;

    public float typingSpeed = 0.05f; // Ÿ���� �ӵ�
    private bool isTyping = false; // Ÿ���� ���� ���� Ȯ�ο� ����

    void Start()
    {
        day1Controller = FindObjectOfType<Day1Controller>();
        // ù �ƾ� ����
        ShowNextDialogue();
        nextButton.onClick.AddListener(OnNextButtonClick);
        choiceButton1.onClick.AddListener(() => OnChoiceClick(1)); // ������ 1
        choiceButton2.onClick.AddListener(() => OnChoiceClick(2)); // ������ 2
        choiceButton1.gameObject.SetActive(false); // �ʱ⿡�� ������ ��ư ��Ȱ��ȭ
        choiceButton2.gameObject.SetActive(false);
    }

    public void OnNextButtonClick()
    {
        if (!isTyping)
        {
            ShowNextDialogue(); // Ÿ������ ������ ���� ���� ���� �̵�
        }
    }

    public void ShowNextDialogue()
    {
        if (dialogueIndex <= dialogues.Length)
        {
            switch (dialogueIndex)
            {
                case 0:
                    sceneImage.sprite = cutsceneImages[0]; // �̹��� ����
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // Ÿ���� ȿ�� ����
                    break;

                case 1:
                    sceneImage.sprite = cutsceneImages[1];
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    break;

                case 2:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                    SetChoices("������", "������ �ʴ´�");
                    break;

                case 3:
                    sceneImage.sprite = cutsceneImages[2]; // �̹��� ����
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // Ÿ���� ȿ�� ����
                    break;

                case 4:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // Ÿ���� ȿ�� ����
                    break;

                case 5:
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex])); // Ÿ���� ȿ�� ����
                    SetChoices("�¼� �ο��", "�����ҷ� ���ư���");
                    break;
            }

            dialogueIndex++; // �������� �ε��� ����
            Debug.Log(dialogueIndex);
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ
        isTyping = true; // Ÿ���� ������ ǥ��
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // �� ���ھ� Ÿ����
        }
        isTyping = false; // Ÿ������ ������ ǥ��
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
        nextButton.gameObject.SetActive(true); // ���� ��ư Ȱ��ȭ
        choiceButton1.gameObject.SetActive(false); // ������1 ��Ȱ��ȭ
        choiceButton2.gameObject.SetActive(false); // ������2 ��Ȱ��ȭ
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
                    Debug.Log("Game Over: ������ �ʴ´� ����");
                    EndingManager.Instance.LoadEnding("BadEnding", "������ �ʴ´�");
                }
            }
            else
            {
                // �� ��° ������ ó��
                if (choice == 1)
                {
                    Debug.Log("Game Over: �¼� �ο�� ����");
                    EndingManager.Instance.LoadEnding("BadEnding", "�¼� �ο��");
                }
                else
                {
                    dialogueText.text = "";
                    StartCoroutine(CompleteTaskWithDialogue("�׷� �ϴ� �����ҷ� ���ư��� �߿��� ��ǰ���� ì��� �� ���⸦ ������"));
                }
            }
        }
    }
    private IEnumerator CompleteTaskWithDialogue(string dialogue)
    {
        //yield return StartCoroutine(TypeDialogue(dialogue)); // Ÿ���� �Ϸ�� ������ ���
        yield return StartCoroutine(TypeDialogue(dialogue));
        yield return new WaitForSeconds(2f);
        day1Controller.CompleteTask("Day1CutScene");
        GameManager.Instance.CompleteTask("Day2Scene");
    }
}
