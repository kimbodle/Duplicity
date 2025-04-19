using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDialogOrder : MonoBehaviour
{
    public List<RefuseRabbit> requiredDialogs; // ���� ��ȭ ĳ���͵� (���ϸ�, Ŭ����)
    public RefuseRabbit targetDialog; // ���� ��ȭ ĳ���� (���)

    private DayController dayController;
    private bool isAllRequiredDialogsCompleted;

    private void Start()
    {
        dayController = FindObjectOfType<DayController>();
        isAllRequiredDialogsCompleted = false;

        // ó���� targetDialog ��ư ��Ȱ��ȭ
        if (targetDialog != null)
        {
            targetDialog.GetComponent<Button>().interactable = false;
        }

        // ���� ��ȭ ĳ���� �̺�Ʈ ���
        foreach (var dialog in requiredDialogs)
        {
            dialog.GetComponent<Button>().onClick.AddListener(CheckRequiredDialogs);
        }
    }

    private void CheckRequiredDialogs()
    {
        // ��� ���� ��ȭ ĳ���Ϳ� ��ȭ�� �Ϸ��ߴ��� Ȯ��
        isAllRequiredDialogsCompleted = true;
        foreach (var dialog in requiredDialogs)
        {
            if (!dialog.isTalk) // `isTalk`�� false�� ��� ��ȭ �̿Ϸ�
            {
                isAllRequiredDialogsCompleted = false;
                break;
            }
        }

        // ��� ���� ��ȭ�� �Ϸ�Ǹ� ��� ��ư Ȱ��ȭ
        if (isAllRequiredDialogsCompleted && targetDialog != null)
        {
            targetDialog.GetComponent<Button>().interactable = true;
        }
    }
}
