using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonalCabinet : MonoBehaviour, IMission, IInteractable
{
    public string interactionMessage = "�������� ���� ĳ���";
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
        //�ʱ�ȭx
    }
    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void OnClickKey()
    {
        //���� ������ ȹ�� ��� �ڵ� ����
        IsMissionCompleted = true;
    }


    public void OnInteract()
    {
        //ĳ��� ���� �ǳ� true
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
            //��ȣ ��ġ
            //���� Ȱ��ȭ
            key.SetActive(true);
            //DialogManager.Instance.PlayerMessageDialog(dialog);
        }
        else
        {
            Debug.Log("�߸��� ��й�ȣ�Դϴ�.");
            passwordInputField.text = "";
            //Ʋ�� �˸���
        }
    }
    public void HandleTask(string taskKey)
    {

    }

    public void ResetTask()
    {

    }
}
