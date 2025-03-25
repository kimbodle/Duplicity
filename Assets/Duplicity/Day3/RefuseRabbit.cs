using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RefuseRabbit : MonoBehaviour
{
    public Dialog dialog; // �ش� ĳ������ ���̾�α�
    //public Sprite characterSprite; // �ش� ĳ������ �̹���

    public bool isTalk= false;

    public AudioClip touchSound;

    private DialogManager dialogManager;
    private DayController dayController;
    private Button interactionButton; // UI Button�� ����

    private void Start()
    {
        dialogManager = DialogManager.Instance;
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
            AudioManager.Instance.PlaySFX(touchSound);
            dialogManager.StartDialog(dialog, dialog.characterSprite);
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
