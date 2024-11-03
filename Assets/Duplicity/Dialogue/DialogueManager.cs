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

    private Queue<(Dialog dialog, Sprite characterSprite)> dialogQueue; // ��� ���� ���̾�α� ť
    private Queue<(string sentence, bool isPlayerSpeaking)> sentenceQueue; // ȭ�� ������ ������ ���� ť
    private bool isTyping = false; // �ڷ�ƾ ���� ���θ� ��Ÿ���� ����
    private bool isDialogActive = false; // ���̾�α� Ȱ�� ���¸� �����ϴ� ����
    private Sprite currentCharacterSprite; // ���� ��ȭ ���� ĳ���� �̹���

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
        dialogQueue = new Queue<(Dialog, Sprite)>();
        sentenceQueue = new Queue<(string, bool)>();
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence); // ��ư Ŭ�� �̺�Ʈ �߰�
        nextButton.gameObject.SetActive(false); // ��ư�� ó������ ��Ȱ��ȭ
    }

    public void StartDialog(Dialog dialog, Sprite characterSprite)
    {
        // ���̾�αװ� ���� ���̸� ť�� ����
        if (isDialogActive || isTyping)
        {
            dialogQueue.Enqueue((dialog, characterSprite));
            return;
        }

        // ���̾�α� ����
        isDialogActive = true;
        dialogPanel.SetActive(true);
        currentCharacterSprite = characterSprite; // ���� ĳ���� �̹����� ����
        characterImage.sprite = characterSprite; // ���̾�α� ���� �� ĳ���� �̹��� ����
        sentenceQueue.Clear();

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
        characterImage.sprite = isPlayerSpeaking ? playerImage : currentCharacterSprite;

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
        isDialogActive = false; // ���̾�αװ� ����Ǹ� ��Ȱ�� ���·� ����

        // ���̾�α� ���� �̺�Ʈ ȣ��
        OnDialogEnd?.Invoke();

        // ��� ���� ���̾�αװ� ������ �ڵ����� ����
        if (dialogQueue.Count > 0)
        {
            (Dialog nextDialog, Sprite nextSprite) = dialogQueue.Dequeue();
            StartDialog(nextDialog, nextSprite);
        }
    }

    public void PlayerMessageDialog(Dialog dialog)
    {
        StartDialog(dialog, playerImage);
    }

    public void AdviseMessageDialog(int adviseMessageDialogNumber)
    {
        if (adviseMessageDialogNumber < advissMessages.Length)
        {
            StartDialog(advissMessages[adviseMessageDialogNumber], playerImage);
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ���� �޽��� �ε����Դϴ�.");
        }
    }
}
