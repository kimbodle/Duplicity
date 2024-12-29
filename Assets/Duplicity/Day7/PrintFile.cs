using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintFile : MonoBehaviour, IMission
{
    public List<Item> printDocuments; // �ε����� ���� ���
    public GameObject printItem;
    public Day7MissionManager missionManager;

    public bool IsMissionCompleted { get; private set; }

    private void Start()
    {
        IsMissionCompleted = false;
        Button button = printItem.GetComponentInChildren<Button>(true); // ��Ȱ��ȭ�� ���¿����� �˻�
        /*
        if (button != null)
        {
            button.onClick.AddListener(() => GetPrint(0));
        }
        else
        {
            Debug.LogError("��Ȱ��ȭ�� ���¿����� Button�� ã�� �� �����ϴ�.");
        }*/
    }
    public void Initialize()
    {
        //�ʱ�ȭx
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void GetPrint(int documentIndex)
    {
        if (InventoryManager.Instance != null && documentIndex >= 0 && documentIndex < printDocuments.Count)
        {
            IsMissionCompleted = true;
            InventoryManager.Instance.AddItemToInventory(printDocuments[documentIndex]); // �ε����� �´� ���� �߰�
            Debug.Log($"���� {documentIndex} �߰���.");

            // ����Ʈ ������ ����
            Destroy(printItem);

            // Day7MissionManager�� ���� ���� ��û
            missionManager.CheckAllMission();
        }
        else
        {
            Debug.LogError("���� �߰� ���� �Ǵ� �߸��� �ε���.");
        }
    }
}
