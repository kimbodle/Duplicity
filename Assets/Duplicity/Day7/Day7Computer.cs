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
    public GameObject passwordPanel; // 비밀번호 입력창
    public TMP_InputField passwordInputField;
    public Button confirmButton;
    [Space(10)]
    public GameObject folderOpenImage; // 폴더 내용 표시 패널
    [Header("files")]
    public List<Button> documentIconButtons; // 문서 아이콘 버튼 리스트
    public GameObject documentOpenPanel; // 문서 내용 표시 패널
    public Image documentDisplayImage; // documentOpenPanel에 있는 Image 컴포넌트
    public List<Sprite> documentSprites; // 문서 이미지 리스트
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

        // 각 문서 버튼에 이벤트를 동적으로 연결
        for (int i = 0; i < documentIconButtons.Count; i++)
        {
            int index = i; // 로컬 변수로 캡쳐하여 버튼마다 다른 문서 열기
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
            Debug.Log("잘못된 비밀번호입니다.");
            passwordInputField.text = "";
            //틀린 알림음
        }
    }

    public void OnDocumentClicked(int documentIndex)
    {
        // documentIndex에 해당하는 스프라이트로 이미지 변경
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

    //클릭시 문서 획득 메소드 구현
}
