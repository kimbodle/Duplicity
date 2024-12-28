using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonalCabinet : MonoBehaviour, IInteractable
{
    public string interactionMessage = "누군가의 개인 캐비넷";
    public GameObject CabinetPasswordPanel;
    [Space(10)]
    public Button closeButton;
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
    }

}
