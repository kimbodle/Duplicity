using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogOrder : MonoBehaviour
{
    public List<RefuseRabbit> requiredDialogs; // 선행 대화 캐릭터들 (에일리, 클로이)
    public RefuseRabbit targetDialog; // 후행 대화 캐릭터 (토미)

    private DayController dayController;
    private bool isAllRequiredDialogsCompleted;

    private void Start()
    {
        dayController = FindObjectOfType<DayController>();
        isAllRequiredDialogsCompleted = false;

        // 처음에 targetDialog 버튼 비활성화
        if (targetDialog != null)
        {
            targetDialog.GetComponent<Button>().interactable = false;
        }

        // 선행 대화 캐릭터 이벤트 등록
        foreach (var dialog in requiredDialogs)
        {
            dialog.GetComponent<Button>().onClick.AddListener(CheckRequiredDialogs);
        }
    }

    private void CheckRequiredDialogs()
    {
        // 모든 선행 대화 캐릭터와 대화를 완료했는지 확인
        isAllRequiredDialogsCompleted = true;
        foreach (var dialog in requiredDialogs)
        {
            if (!dialog.isTalk) // `isTalk`이 false인 경우 대화 미완료
            {
                isAllRequiredDialogsCompleted = false;
                break;
            }
        }

        // 모든 선행 대화가 완료되면 토미 버튼 활성화
        if (isAllRequiredDialogsCompleted && targetDialog != null)
        {
            targetDialog.GetComponent<Button>().interactable = true;
        }
    }
}
