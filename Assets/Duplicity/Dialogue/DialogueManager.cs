using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance; // �̱��� �ν��Ͻ�

    [Header("UI Components")]
    public GameObject dialogPanel;            // ��ȭ �г�
    public TMP_Text dialogText;               // ���� ��� �ؽ�Ʈ
    public Image characterImage;              // ĳ���� �̹���
    public Button nextButton;                 // ���� ��ư
    public float typingSpeed = 0.05f; // Ÿ���� �ӵ� ���� ����
    public Dialog[] advissMessages;
    [Header("History UI")]
    public GameObject historyPanel;           // �����丮 �г�
    public Transform contentParent;           // ScrollView Content
    public GameObject dialogEntryPrefab;      // ������
    [Header("Assets")]
    public Sprite lauraImage;

    private Queue<(Dialog dialog, Sprite characterSprite)> dialogQueue; // ��� ���� ���̾�α� ť
    private Queue<(string sentence, bool isPlayerSpeaking)> sentenceQueue; // ȭ�� ������ ������ ���� ť
    private List<Dialog> dialogHistory;       // �����丮�� ����Ǵ� Dialog
    private bool isTyping = false; // �ڷ�ƾ ���� ���θ� ��Ÿ���� ����
    private bool isDialogActive = false; // ���̾�α� Ȱ�� ���¸� �����ϴ� ����
    private Sprite currentCharacterSprite; // ���� ��ȭ ���� ĳ���� �̹���
    private string currentSentence; // ���� ����
    private bool currentIsPlayerSpeaking; // ���� ȭ�� ����


    // ���̾�α� ���� �̺�Ʈ
    public event Action OnDialogEnd;
    //��ŵ ��� �¿���
    //public bool allowSkip = true;

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
        dialogHistory = new List<Dialog>();
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

        // �����丮�� Dialog ���� (�ߺ� ����)
        if (!dialogHistory.Contains(dialog))
        {
            dialogHistory.Add(dialog);
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

        // ���� ������ ť���� �������� ���� ���� ������ ����
        (currentSentence, currentIsPlayerSpeaking) = sentenceQueue.Dequeue();

        // ȭ�ڿ� ���� ĳ���� �̹��� ����
        characterImage.sprite = currentIsPlayerSpeaking ? lauraImage : currentCharacterSprite;

        // ���� Ÿ���� ����
        StartCoroutine(TypeSentence(currentSentence));
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
        isTyping = false; // �ڷ�ƾ ���� �� false�� ����
        nextButton.gameObject.SetActive(true); // ������ ������ ��ư Ȱ��ȭ
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

    public void UpdateHistoryUI()
    {
        // ���� UI ����
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // �����丮 UI �߰�
        foreach (var dialog in dialogHistory)
        {
            for (int i = 0; i < dialog.sentences.Length; i++)
            {
                GameObject dialogEntry = Instantiate(dialogEntryPrefab, contentParent);

                Image characterImage = dialogEntry.transform.Find("CharactorImage").GetComponent<Image>();
                TMP_Text dialogText = dialogEntry.transform.Find("DialogHistoryText").GetComponent<TMP_Text>();

                // �÷��̾� �Ǵ� NPC �̹��� ����
                bool isPlayerSpeaking = dialog.isPlayerSpeaking[i];
                characterImage.sprite = isPlayerSpeaking ? lauraImage : dialog.characterSprite;

                /*
                // �ؽ�Ʈ ����
                dialogText.text = isPlayerSpeaking
                    ? $"<color=blue>Player:</color> {dialog.sentences[i]}"
                    : $"<color=green>NPC:</color> {dialog.sentences[i]}";
                */
                dialogText.text = dialog.sentences[i];
            }
        }
    }

    public void ToggleHistoryUI()
    {
        historyPanel.SetActive(!historyPanel.activeSelf);

        if (historyPanel.activeSelf)
        {
            UpdateHistoryUI();
        }
    }

    public void ClearHistory()
    {
        // �����丮 ����Ʈ ����
        dialogHistory.Clear();

        // �����丮 UI �ʱ�ȭ
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("��ȭ �����丮�� �ʱ�ȭ");
    }

    public void SkipDialog()
    {
        if (isTyping)
        {
            // �ڷ�ƾ �ߴ�
            StopAllCoroutines();

            // ���� ������ ��� �ϼ�
            dialogText.text = currentSentence;

            // ���� �ʱ�ȭ
            isTyping = false; // Ÿ���� ���� ����
            nextButton.gameObject.SetActive(true); // ���� ��ư Ȱ��ȭ
        }
    }



    /*
    public void SkipDialog()
    {
        //if (!allowSkip) return;
        if (isTyping)
        {
            // Ÿ���� ��� �Ϸ�
            StopAllCoroutines();
            dialogText.text = sentenceQueue.Peek().sentence; // ���� ���� �ϼ�
            isTyping = false;
            nextButton.gameObject.SetActive(true); // ��ư Ȱ��ȭ
        }
        /*
        else if (sentenceQueue.Count > 0)
        {
            // ���� �������� �̵�
            DisplayNextSentence();
        }
        else
        {
            // ��ȭ ����
            EndDialog();
        }
    }
*/


    public void PlayerMessageDialog(Dialog dialog)
    {
        StartDialog(dialog, lauraImage);
    }

    public void AdviseMessageDialog(int adviseMessageDialogNumber)
    {
        if (adviseMessageDialogNumber < advissMessages.Length)
        {
            StartDialog(advissMessages[adviseMessageDialogNumber], lauraImage);
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ���� �޽��� �ε���");
        }
    }
}
