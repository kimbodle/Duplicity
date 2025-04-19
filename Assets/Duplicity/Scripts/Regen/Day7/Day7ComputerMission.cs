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
    public Dialog printDialog;

    private const string correctPassword = "0916";

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
        printButton.SetActive(true);

        /*
        //���� ���ϸ� ����Ʈ �� �� ����
        if (documentIndex == 1)
        {
            printButton.SetActive(true);
        }
        else
        {
            printButton.SetActive(false);
        }*/
        // Assign the current document index for printing
        int indexForPrint = documentIndex;
        printButton.GetComponent<Button>().onClick.RemoveAllListeners(); // Clear previous listeners
        printButton.GetComponent<Button>().onClick.AddListener(() => OnClickPrintButton(indexForPrint));
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

    public void OnClickPrintButton(int documentIndex)
    {
        if (printedDocument != null)
        {
            printedDocument.SetActive(true);
            // ���� �ε����� �´� �̹��� Ȱ��ȭ
            Transform documentImage = printedDocument.transform.GetChild(documentIndex);
            if (documentImage != null)
            {
                documentImage.gameObject.SetActive(true);
                if (documentIndex == 1)
                {
                    GameManager.Instance.SaveEnding("EndingItem", 0);
                }
            }
            else
            {
                Debug.LogError($"�ε��� {documentIndex}�� �ش��ϴ� printedDocument �̹����� �����ϴ�.");
            }
            DialogManager.Instance.PlayerMessageDialog(printDialog);
            // ����Ʈ ��ư�� �� ���� ��� �����ϵ��� ����
            Destroy(printButton);
        }
    }
}
