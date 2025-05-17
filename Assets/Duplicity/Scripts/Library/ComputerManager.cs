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
    public GameObject passwordPanel; // 비밀번호 입력창
    public TMP_InputField passwordInputField;
    public Button confirmButton;
    [Space(10)]
    public GameObject folderOpenImage; // 폴더 내용 표시 패널
    public Button documentIconButton;
    public GameObject documentOpenPanel; // 문서 내용 표시 패널
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
            //틀린 알림음
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
