using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CabinetMission : MonoBehaviour,IMission
{
    public Day7MissionManager missionManager;
    [Space(10)]
    public TMP_InputField[] inputFields; // �� �׸� ĭ�� InputField �迭
    public Button checkButton;
    public GameObject cabinetOpenPanel;
    public GameObject key;
    public Item keyItem;
    //public TMP_InputField passwordInputField;
    //public Button confirmButton;
    public Dialog dialog;

    [Space(10)]
    public Item secretBook;
    public Item openSecretBook;
    [Space(10)]
    public const string correctPassword = "0428"; // ���� �ܾ�
    public bool IsMissionCompleted { get; private set; }

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
        }
    }
    //Key -> Onclick �̺�Ʈ ����
    public void OnClickKey()
    {
        //���� ������ ȹ�� ��� �ڵ� ����
        IsMissionCompleted = true;
        if (InventoryManager.Instance != null && GameManager.Instance != null)
        {
            InventoryManager.Instance.AddItemToInventory(keyItem);
            //GameManager.Instance.GetCurrentDayController().CompleteTask("GetKey");
            missionManager.CheckAllMission();
            if (dialog != null)
            {
                DialogManager.Instance.PlayerMessageDialog(dialog);
                ReplaceItemExample();
            }
        }

        Destroy(key);
    }


    void ReplaceItemExample()
    {
        Item targetItem = secretBook; // ��ü�� ��� ������
        Item newItem = openSecretBook; // ���� �߰��� ������

        if (InventoryManager.Instance.ReplaceItem(targetItem, newItem))
        {
            Debug.Log($"������ ��ü ���� {targetItem.itemName} -> {newItem.itemName}.");
        }
        else
        {
            Debug.Log($"{targetItem.itemName}�������� �κ��丮�� �������� ����");
        }
    }
}
