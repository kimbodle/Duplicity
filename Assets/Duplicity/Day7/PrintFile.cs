using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintFile : MonoBehaviour, IMission
{
    public Item printDocument;
    public GameObject printItem;

    public bool IsMissionCompleted { get; private set; }

    private void Start()
    {
        IsMissionCompleted = false;
        printItem.GetComponentInChildren<Button>().onClick.AddListener(GetPrint);
    }
    public void Initialize()
    {
        //√ ±‚»≠x
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
            GameManager.Instance.GetCurrentDayController().CompleteTask("GetDocument");
            Destroy(printItem);
        }
        else
        {
            Debug.Log("πÆº≠ »πµÊx");
        }
    } 
}
