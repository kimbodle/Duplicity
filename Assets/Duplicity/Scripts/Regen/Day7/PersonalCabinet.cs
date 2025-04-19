using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonalCabinet : MonoBehaviour, IInteractable
{
    public string interactionMessage = "�������� ���� ĳ���";
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
        //ĳ��� ���� �ǳ� true
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
        //ĳ��� ���� �ǳ� true
        CabinetPasswordPanel.SetActive(false);
    }

}
