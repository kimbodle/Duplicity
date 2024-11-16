using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day7ComputerMission : MonoBehaviour
{
    public GameObject ComputerPanel;
    public Button closeButton;
    [Space(10)]
    public Button folderIconButton;
    public GameObject passwordPanel; // ��й�ȣ �Է�â
    public TMP_InputField passwordInputField;
    public Button confirmButton;
    [Space(10)]
    public GameObject folderOpenImage; // ���� ���� ǥ�� �г�
    [Header("files")]
    public List<Button> documentIconButtons; // ���� ������ ��ư ����Ʈ
    public GameObject documentOpenPanel; // ���� ���� ǥ�� �г�
    public Image documentDisplayImage; // documentOpenPanel�� �ִ� Image ������Ʈ
    public List<Sprite> documentSprites; // ���� �̹��� ����Ʈ
    [Header("prints")]
    public GameObject printButton;
    public GameObject printedDocument;
    [Space(10)]
    public Dialog dialog;

    private const string correctPassword = "0916";
    private bool isFirst = true;

    private void Start()
    {
        ComputerPanel.SetActive(false);
        passwordPanel.SetActive(false);
        folderOpenImage.SetActive(false);
        documentOpenPanel.SetActive(false);
        printButton.SetActive(false);

        closeButton.onClick.AddListener(CloseComputerPanel);
        folderIconButton.onClick.AddListener(OnFolderClicked);
        confirmButton.onClick.AddListener(CheckPassword);
        printButton.GetComponent<Button>().onClick.AddListener(OnClickPrintButton);

        // �� ���� ��ư�� �̺�Ʈ�� �������� ����
        for (int i = 0; i < documentIconButtons.Count; i++)
        {
            int index = i; // ���� ������ ĸ���Ͽ� ��ư���� �ٸ� ���� ����
            documentIconButtons[i].onClick.AddListener(() => OnDocumentClicked(index));
        }
    }
    public void OnFolderClicked()
    {
        passwordPanel.SetActive(true);
    }

    private void CheckPassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            //passwordPanel.SetActive(false);
            folderOpenImage.SetActive(true);
            DialogManager.Instance.PlayerMessageDialog(dialog);
        }
        else
        {
            Debug.Log("�߸��� ��й�ȣ�Դϴ�.");
            passwordInputField.text = "";
            //Ʋ�� �˸���
        }
    }

    public void OnDocumentClicked(int documentIndex)
    {
        // documentIndex�� �ش��ϴ� ��������Ʈ�� �̹��� ����
        documentDisplayImage.sprite = documentSprites[documentIndex];

        documentOpenPanel.SetActive(true);
        if (documentIndex == 1)
        {
            printButton.SetActive(true);
        }
        else
        {
            printButton.SetActive(false);
        }

        if (isFirst)
        {
            isFirst = false;
            return;
        }
        else
        {
            
        }
        
        //GameManager.Instance.GetCurrentDayController().CompleteTask($"Document{documentIndex + 1}");
    }

    public void CloseComputerPanel()
    {
        ComputerPanel.SetActive(false);
    }

    //Onclick �̺�Ʈ ����
    public void CloseDocumentContent()
    {
        documentOpenPanel.SetActive(false);
    }

    //���� ����Ʈ
    public void OnClickPrintButton()
    {
        if(printedDocument != null)
        {
            printedDocument.SetActive(true);
            //����Ʈ ��ư ���� ���� �߰�
            // ���� 2�� ����Ʈ ������ ���� �ٹ� save ���� �߰�
        }
    }
}
