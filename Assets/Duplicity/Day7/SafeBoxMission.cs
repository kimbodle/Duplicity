using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeBoxMission : MonoBehaviour, IMission
{
    public Button[] numberButtons; // ���� ��ư �迭 (9���� ��� ��ư)
    public TMP_Text inputDisplay; // ���� �κп� �Էµ� ���ڸ� ǥ���ϴ� �ؽ�Ʈ
    public Button submitButton; // ����� Ȯ�� ��ư
    private string currentInput = ""; // ���� �Էµ� ���� ���ڿ�
    private string correctAnswer = "12760"; // ����
    [Space(10)]
    public GameObject boxOpenImage;
    public Button regen;
    public Item regeneratium;
    [Space(10)]
    public Button boxCloseButton;
    public bool IsMissionCompleted { get; private set; }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void Initialize()
    {
        //x
    }

    void Start()
    {
        // ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        foreach (Button button in numberButtons)
        {
            int number = int.Parse(button.GetComponentInChildren<TMP_Text>().text); // ��ư�� ���ڸ� ������
            button.onClick.AddListener(() => OnNumberButtonClick(number));
        }

        // ���� ��ư�� Ŭ�� �̺�Ʈ �߰�
        submitButton.onClick.AddListener(CheckAnswer);
        regen.onClick.AddListener(GetRegen);

        // �ʱ�ȭ
        inputDisplay.text = "";
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    private void OnNumberButtonClick(int number)
    {
        if (currentInput.Length < 10) // �Է� �ִ� ���� ����
        {
            currentInput += number.ToString();
            inputDisplay.text = currentInput;
        }
    }

    // ���� Ȯ�� �Լ�
    private void CheckAnswer()
    {
        if (currentInput == correctAnswer)
        {
            Debug.Log("����");
            CompleteMission();
        }
        else
        {
            Debug.Log("Ʋ��");
            ResetInput();
        }
    }

    // �̼� ���� ó��
    private void CompleteMission()
    {
        boxOpenImage.SetActive(true);
    }

    // �Է� �ʱ�ȭ �Լ�
    private void ResetInput()
    {
        currentInput = "";
        inputDisplay.text = "";
    }

    private void GetRegen()
    {
        Debug.Log("�̼� Ŭ����!");
        InventoryManager.Instance.AddItemToInventory(regeneratium);
        GameManager.Instance.GetCurrentDayController().CompleteTask("GetRegen");
        IsMissionCompleted = true;
        Destroy(regen.gameObject);
    }

    //Onclick �̺�Ʈ ����
    public void OnclickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
