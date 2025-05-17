using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

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

    private bool isStartingFromQueue = false;


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

    private void Update()
    {
        if (!isDialogActive) return;

        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��� �Է� ����
        {
            if (isTyping)
            {
                SkipDialog(); // Ÿ���� ���̸� ��� ���
            }
            else
            {
                DisplayNextSentence(); // Ÿ������ �������� ���� ��������
                AudioManager.Instance.PlayUIButton();
            }
        }
    }

    public void StartDialog(Dialog dialog, Sprite characterSprite)
    {
        if ((isDialogActive || isTyping) && !isStartingFromQueue)
        {
            dialogQueue.Enqueue((dialog, characterSprite));
            return;
        }

        isStartingFromQueue = false; // ť���� ������ ��쿡�� �ߺ� ť ����

        if (!dialogHistory.Contains(dialog))
        {
            dialogHistory.Add(dialog);
        }

        isDialogActive = true;
        dialogPanel.SetActive(true);
        currentCharacterSprite = characterSprite;
        characterImage.sprite = characterSprite;
        sentenceQueue.Clear();

        for (int i = 0; i < dialog.sentences.Length; i++)
        {
            bool isPlayerSpeaking = i < dialog.isPlayerSpeaking.Length && dialog.isPlayerSpeaking[i];
            sentenceQueue.Enqueue((dialog.sentences[i], isPlayerSpeaking));
        }

        nextButton.gameObject.SetActive(false);
        DisplayNextSentence();
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
        nextButton.gameObject.SetActive(false);
        isTyping = false;
        isDialogActive = false;

        OnDialogEnd?.Invoke();

        if (dialogQueue.Count > 0)
        {
            (Dialog nextDialog, Sprite nextSprite) = dialogQueue.Dequeue();
            isStartingFromQueue = true;
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
        }
    }
}
