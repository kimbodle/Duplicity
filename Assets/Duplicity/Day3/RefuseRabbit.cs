using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RefuseRabbit : MonoBehaviour
{
    public Dialog[] dialog; // �ش� ĳ������ ���̾�α�
    public Sprite characterSprite; // �ش� ĳ������ �̹���

    private bool isTalk= false;

    private int dialogCount = 0;
    private DialogManager dialogManager;
    private DayController dayController;
    private Button interactionButton; // UI Button�� ����

    private void Start()
    {
        dialogManager = DialogManager.Instance;
        dialogCount = FindObjectOfType<ShelterController>().dialogCount;
        dayController = FindObjectOfType<DayController>();
        
        interactionButton = GetComponent<Button>();
        if (interactionButton != null)
        {
            // �ڵ�� Button Ŭ�� �̺�Ʈ�� �޼��� ���
            interactionButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("interactionButton�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void OnButtonClick()
    {
        if (dialogManager != null)
        {
            dialogManager.StartDialog(dialog[dialogCount], characterSprite);
            if (isTalk == false)
            {
                dayController.talkRabbitCount++;
                isTalk = true;
                dayController.CompleteTask("TallWithAllRabbit");
            }
        }
        else
        {
            Debug.Log("dialogManager is null");
        }
    }
}
