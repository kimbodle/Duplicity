using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintFile : MonoBehaviour, IMission
{
    public Item printDocument;
    public GameObject printItem;
    public Day7MissionManager missionManager;

    public bool IsMissionCompleted { get; private set; }

    private void Start()
    {
        IsMissionCompleted = false;
        printItem.GetComponentInChildren<Button>().onClick.AddListener(GetPrint);
    }
    public void Initialize()
    {
        //�ʱ�ȭx
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    public void GetPrint()
    {
        if(InventoryManager.Instance != null) {
            IsMissionCompleted = true;
            InventoryManager.Instance.AddItemToInventory(printDocument);
            Destroy(printItem);

            // Day7MissionManager�� ���� ���� ��û
            missionManager.CheckAllMission();
        }
        else
        {
            Debug.Log("���� ȹ��x");
        }
    } 
}
