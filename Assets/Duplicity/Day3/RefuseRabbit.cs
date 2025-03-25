using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RefuseRabbit : MonoBehaviour
{
    public Dialog dialog; // 해당 캐릭터의 다이얼로그
    //public Sprite characterSprite; // 해당 캐릭터의 이미지

    public bool isTalk= false;

    public AudioClip touchSound;

    private DialogManager dialogManager;
    private DayController dayController;
    private Button interactionButton; // UI Button을 참조

    private void Start()
    {
        dialogManager = DialogManager.Instance;
        dayController = FindObjectOfType<DayController>();
        
        interactionButton = GetComponent<Button>();
        if (interactionButton != null)
        {
            // 코드로 Button 클릭 이벤트에 메서드 등록
            interactionButton.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("interactionButton이 설정되지 않았습니다.");
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
