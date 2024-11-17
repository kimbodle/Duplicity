using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonalCabinet : MonoBehaviour, IInteractable
{
    public string interactionMessage = "누군가의 개인 캐비넷";
    public GameObject CabinetPasswordPanel;
    [SerializeField] Dialog dialog;
    [Space(10)]
    public Button closeButton;
    [Space(10)]
    public Item secretBook;
    public Item openSecretBook;
    // Start is called before the first frame update
    void Start()
    {
        CabinetPasswordPanel.SetActive(false);
        closeButton.onClick.AddListener(OnClickCloseButton);
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

    public void HandleTask(string taskKey)
    {
    }

    public void ResetTask()
    {
    }

    public void OnClickCloseButton()
    {
        //캐비넷 오픈 판넬 true
        CabinetPasswordPanel.SetActive(false);
        if(dialog  != null)
        {
            DialogManager.Instance.PlayerMessageDialog(dialog);
            ReplaceItemExample();
        }
    }

    void ReplaceItemExample()
    {
        Item targetItem = secretBook; // 교체할 대상 아이템
        Item newItem = openSecretBook; // 새로 추가할 아이템

        if (InventoryManager.Instance.ReplaceItem(targetItem, newItem))
        {
            Debug.Log($"아이템 교체 성공 {targetItem.itemName} -> {newItem.itemName}.");
        }
        else
        {
            Debug.Log($"{targetItem.itemName}아이템이 인벤토리에 존재하지 않음");
        }
    }

}
