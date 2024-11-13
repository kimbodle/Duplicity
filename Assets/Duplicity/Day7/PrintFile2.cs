using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintFile2 : MonoBehaviour
{
    public Item printDocument;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GetPrint);
    }

    public void GetPrint()
    {
        if(InventoryManager.Instance != null) {
            InventoryManager.Instance.AddItemToInventory(printDocument);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("¹®¼­ È¹µæx");
        }
    }
}
