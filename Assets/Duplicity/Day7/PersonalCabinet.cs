using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonalCabinet : MonoBehaviour, IMission, IInteractable
{
    public string interactionMessage = "누군가의 개인 캐비넷";
    public GameObject CabinetPasswordPanel;
    public GameObject key;
    public TMP_InputField passwordInputField;
    public Button confirmButton;
    public Dialog dialog;

    private const string correctPassword = "0428";
    public bool IsMissionCompleted { get; private set; }

    void Start()
    {
        CabinetPasswordPanel.SetActive(false);
        key.SetActive(false);

        key.GetComponent<Button>().onClick.AddListener(OnClickKey);
        confirmButton.onClick.AddListener(CheckPassword);
    }

    public void Initialize()
    {
        //초기화x
    }
    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void OnClickKey()
    {
        //열쇠 아이템 획득 기능 코드 구현
        IsMissionCompleted = true;
    }


    public void OnInteract()
    {
        //캐비넷 오픈 판넬 true
        CabinetPasswordPanel.SetActive(true);
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }

    private void CheckPassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            //번호 일치
            //열쇠 활성화
            key.SetActive(true);
            //DialogManager.Instance.PlayerMessageDialog(dialog);
        }
        else
        {
            Debug.Log("잘못된 비밀번호입니다.");
            passwordInputField.text = "";
            //틀린 알림음
        }
    }
    public void HandleTask(string taskKey)
    {

    }

    public void ResetTask()
    {

    }
}
