using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance; // �̱��� �ν��Ͻ�

    public GameObject dialogPanel; // ���̾�α� �г�
    public TMP_Text dialogText; // ���̾�α� �ؽ�Ʈ
    public Image characterImage; // ĳ���� �̹���
    public Button nextButton; // ���� ���� ��ư
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ� ���� ����
    public Dialog[] advissMessages;

    [Space(10)]
    public Sprite playerImage; // �÷��̾� �̹���

    private Queue<(string sentence, bool isPlayerSpeaking)> sentenceQueue; // ȭ�� ������ ������ ���� ť
    private bool isTyping = false; // �ڷ�ƾ ���� ���θ� ��Ÿ���� ����

    // ���̾�α� ���� �̺�Ʈ
    public event Action OnDialogEnd;

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ��� �ı�
        }
    }

    private void Start()
    {
        sentenceQueue = new Queue<(string, bool)>();
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence); // ��ư Ŭ�� �̺�Ʈ �߰�
        nextButton.gameObject.SetActive(false); // ��ư�� ó������ ��Ȱ��ȭ
    }

    public void StartDialog(Dialog dialog, Sprite characterSprite)
    {
        dialogPanel.SetActive(true);
        sentenceQueue.Clear();
        characterImage.sprite = characterSprite; // �⺻ ĳ���� �̹��� ����

        for (int i = 0; i < dialog.sentences.Length; i++)
        {
            bool isPlayerSpeaking = i < dialog.isPlayerSpeaking.Length && dialog.isPlayerSpeaking[i];
            sentenceQueue.Enqueue((dialog.sentences[i], isPlayerSpeaking));
        }

        nextButton.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
        DisplayNextSentence(); // ù ���� ǥ��
    }

    public void DisplayNextSentence()
    {
        // �ڷ�ƾ�� ���� ���̸� Ŭ���� ���õ�
        if (isTyping)
        {
            return;
        }

        if (sentenceQueue.Count == 0)
        {
            EndDialog();
            return;
        }

        (string sentence, bool isPlayerSpeaking) = sentenceQueue.Dequeue();

        // ȭ�ڿ� ���� ĳ���� �̹����� ����
        characterImage.sprite = isPlayerSpeaking ? playerImage : characterImage.sprite;

        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // �ڷ�ƾ ���� �� true�� ����
        dialogText.text = "";
        nextButton.gameObject.SetActive(false); // Ÿ���� �߿��� ��ư ��Ȱ��ȭ

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Ÿ���� �ӵ��� ���� ���
        }

        nextButton.gameObject.SetActive(true); // ������ ������ ��ư Ȱ��ȭ
        isTyping = false; // �ڷ�ƾ ���� �� false�� ����
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        nextButton.gameObject.SetActive(false); // ���̾�α� ���� �� ��ư ��Ȱ��ȭ
        isTyping = false; // ���̾�α� ���� �ÿ��� false�� ����

        // ���̾�α� ���� �̺�Ʈ ȣ��
        OnDialogEnd?.Invoke();
    }

    public void PlayerMessageDialog(Dialog dialog)
    {
        StartDialog(dialog, playerImage);
    }

    public void AdviseMessageDialog(int adviseMessageDialogNumber)
    {
        if (adviseMessageDialogNumber == 0)
        {
            StartDialog(advissMessages[0], playerImage);
        }
        else if (adviseMessageDialogNumber == 1)
        {
            StartDialog(advissMessages[1], playerImage);

        }
    }
}
