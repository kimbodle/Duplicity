using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day8Manager : MonoBehaviour
{
    public Dialog dialog;
    public SignMisisonTimer missionTimer;
    public MissionTimer missionTimer2;
    public Item[] items;
    public Item correctRecipe;

    // Start is called before the first frame update
    void Start()
    {
        if (dialog != null)
        {
            if (DialogManager.Instance != null)
            {
                if(missionTimer != null)
                {
                    UIManager.Instance.EndingUI();
                }
                DialogManager.Instance.OnDialogEnd += HandleDialogEnd;
                DialogManager.Instance.PlayerMessageDialog(dialog);
            }
        }
    }
    public void AddDay8Items()
    {
        if (InventoryManager.Instance != null)
        {
            foreach (Item item in items)
            {
                InventoryManager.Instance.AddItemToInventory(item);
            }
            bool hasRecipe = GameManager.Instance.HasSeenEnding("EndingItem", 0);
            if (hasRecipe)
            {
                InventoryManager.Instance.AddItemToInventory(correctRecipe);
            }
        }
    }
    private void HandleDialogEnd()
    {
        Debug.Log("다이얼로그가 종료됨.");
        if(missionTimer != null)
        {
            missionTimer.gameObject.SetActive(true);
            missionTimer.isMissionActive = true;
        }
        if (missionTimer2 != null)
        {
            missionTimer2.gameObject.SetActive(true);
            missionTimer2.isMissionActive = true;
        }
    }
}
