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

    [Space(10)]
    public Sprite playerImage;

    private Queue<string> sentences; // ���̾�α� ���� ť

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
        sentences = new Queue<string>();
        dialogPanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence); // ��ư Ŭ�� �̺�Ʈ �߰�
        nextButton.gameObject.SetActive(false); // ��ư�� ó������ ��Ȱ��ȭ
    }

    public void StartDialog(Dialog dialog, Sprite characterSprite)
    {
        dialogPanel.SetActive(true);
        sentences.Clear();
        characterImage.sprite = characterSprite; // ĳ���� �̹��� ����

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        nextButton.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
        DisplayNextSentence(); // ù ���� ǥ��
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed); // Ÿ���� �ӵ��� ���� ���
        }

        nextButton.gameObject.SetActive(true); // ������ ������ ��ư Ȱ��ȭ
    }

    private void EndDialog()
    {
        dialogPanel.SetActive(false);
        nextButton.gameObject.SetActive(false); // ���̾�α� ���� �� ��ư ��Ȱ��ȭ

        // ���̾�α� ���� �̺�Ʈ ȣ��
        OnDialogEnd?.Invoke();
    }

    //Player ȥ�㸻
    public void PlayerMessageDialog(Dialog dialog)
    {
        StartDialog(dialog, playerImage);
    }

    //DayController���� ���ѿ� ���� �޼��� ȣ��
    public void AdviseMessageDialog(int adviseMessageDialogNumber)
    {
        if(adviseMessageDialogNumber == 0)
        {
            StartDialog(new Dialog { sentences = new[] { "�ǳ������ ���� �� ��ȭ�� ��������." } }, playerImage);
        }
        else
        {
            StartDialog(new Dialog { sentences = new[] { "���� ���⼭ �ؾ� �� ���� �� ���Ҿ�! �������� �Ĳ��� �����غ���." } }, playerImage);
        }
    }
}
