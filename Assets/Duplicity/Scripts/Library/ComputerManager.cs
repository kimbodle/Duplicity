using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
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
    public Button documentIconButton;
    public GameObject documentOpenPanel; // ���� ���� ǥ�� �г�
    [Space(10)]
    public Dialog dialog;

    private const string correctPassword = "0511";

    private void Start()
    {
        ComputerPanel.SetActive(false);
        passwordPanel.SetActive(false);
        folderOpenImage.SetActive(false);
        documentOpenPanel.SetActive(false);

        closeButton.onClick.AddListener(CloseDocumentContent);
        folderIconButton.onClick.AddListener(OnFolderClicked);
        confirmButton.onClick.AddListener(CheckPassword);
        documentIconButton.onClick.AddListener(OnDocumentClicked);
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
        }
        else
        {
            passwordInputField.text = "";
            //Ʋ�� �˸���
        }
    }

    public void OnDocumentClicked()
    {
        documentOpenPanel.SetActive(true);
        DialogManager.Instance.PlayerMessageDialog(dialog);
        GameManager.Instance.GetCurrentDayController().CompleteTask("Day3ComputerUnlock");
    }

    public void CloseDocumentContent()
    {
        ComputerPanel.SetActive(false);
    }
}
