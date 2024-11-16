using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CabinetMission : MonoBehaviour,IMission
{
    public TMP_InputField[] inputFields; // �� �׸� ĭ�� InputField �迭
    public Button checkButton;
    public GameObject cabinetOpenPanel;
    public GameObject key;
    public Item keyItem;
    //public TMP_InputField passwordInputField;
    //public Button confirmButton;
    public Dialog dialog;
    public bool IsMissionCompleted { get; private set; }
    public const string correctPassword = "0428"; // ���� �ܾ�

    void Start()
    {
        cabinetOpenPanel.SetActive(false);
        checkButton.onClick.AddListener(CheckMission);
        key.GetComponent<Button>().onClick.AddListener(OnClickKey);
        //confirmButton.onClick.AddListener(CheckPassword);
    }

    public void Initialize()
    {
        //�ʱ�ȭx
    }
    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    //Key -> Onclick �̺�Ʈ ����
    public void OnClickKey()
    {
        //���� ������ ȹ�� ��� �ڵ� ����
        IsMissionCompleted = true;
        if(InventoryManager.Instance != null && GameManager.Instance != null)
        {
            InventoryManager.Instance.AddItemToInventory(keyItem);
            GameManager.Instance.GetCurrentDayController().CompleteTask("GetKey");
        }
        
        Destroy(key);
    }

    public void CheckMission()
    {
        string userInput = "";

        // �� InputField�� �ؽ�Ʈ�� ���ļ� ����� �Է� Ȯ��
        foreach (TMP_InputField inputField in inputFields)
        {
            userInput += inputField.text;
            inputField.text = "";
        }

        // �Էµ� �ؽ�Ʈ�� ���� �ܾ�� ������ Ȯ��
        if (userInput.Equals(correctPassword, System.StringComparison.OrdinalIgnoreCase))
        {
            cabinetOpenPanel.SetActive(true);
        }
        else
        {
            Debug.Log("�Է� �ʵ� ���� �̼� ����. �ٽ� �õ��ϼ���.");
        }
    }
}
