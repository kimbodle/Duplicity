using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day7ComputerManager : MonoBehaviour
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
    [Space(10)]
    public Dialog dialog;

    private const string correctPassword = "0916";

    private void Start()
    {
        ComputerPanel.SetActive(false);
        passwordPanel.SetActive(false);
        folderOpenImage.SetActive(false);
        documentOpenPanel.SetActive(false);

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
        //GameManager.Instance.GetCurrentDayController().CompleteTask($"Document{documentIndex + 1}");
    }

    public void CloseComputerPanel()
    {
        ComputerPanel.SetActive(false);
    }

    public void CloseDocumentContent()
    {
        documentOpenPanel.SetActive(false);
    }

    //Ŭ���� ���� ȹ�� �޼ҵ� ����
}
