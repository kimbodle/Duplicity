using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day6DialogSelection : MonoBehaviour
{
    public GameObject ChoicePanel;
    public Button choiceButton1; // ������ 1 ��ư
    public Button choiceButton2; // ������ 2 ��ư
    [Space(10)]
    public Dialog BadendingDialog;
    public Dialog OpenRegenDialog;

    private bool isChoiceMade = false; // ������ �̹� �̷�������� Ȯ���ϴ� �÷���

    void Start()
    {
        if (GameManager.Instance.GetCurrentDay() == 6)
        {
            // ������ ��ư�� Ŭ�� �̺�Ʈ ����
            choiceButton1.onClick.AddListener(() => OnChoiceClick(1)); // ������ 1 Ŭ�� ��
            choiceButton2.onClick.AddListener(() => OnChoiceClick(2)); // ������ 2 Ŭ�� ��

            // ������ ��ư �ʱ� ��Ȱ��ȭ
            ChoicePanel.SetActive(false);
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
        }
    }

    // �ܺο��� ȣ���Ͽ� ������ ��ư ǥ��
    public void ShowChoices()
    {
        if (isChoiceMade) return; // �̹� ������ �̷�������� ��ȯ�Ͽ� �ߺ� Ŭ�� ����

        ChoicePanel.SetActive(true);
        SetChoices("�׷��� �� ���� ������.\n�̰� ���� ��� �� �� �߸��̾�?", "�ֺ��� ���ƺ��� ���� �� �߸��̾�.");
    }

    private void SetChoices(string choice1, string choice2)
    {
        choiceButton1.GetComponent<TMP_Text>().text = choice1;
        choiceButton2.GetComponent<TMP_Text>().text = choice2;
        choiceButton1.gameObject.SetActive(true); // ������ 1 Ȱ��ȭ
        choiceButton2.gameObject.SetActive(true); // ������ 2 Ȱ��ȭ
    }

    // ������ ��ư Ŭ�� ó��
    private void OnChoiceClick(int choice)
    {
        if (isChoiceMade) return; // �̹� ������ �ߴٸ� �Լ� ���� (�ߺ� Ŭ�� ����)

        isChoiceMade = true; // �������� Ŭ���Ǿ����� ǥ��
        ChoicePanel.SetActive(false); // ������ �г� �����

        if (choice == 1)
        {
            Debug.Log("����");
            // ���̾�α� ���� �� Debug.Log ȣ���� ���� �ݹ� ���
            DialogManager.Instance.OnDialogEnd += OnDialogEnd;

            DialogManager.Instance.PlayerMessageDialog(BadendingDialog);
        }
        else
        {
            Debug.Log("����� ����");
            MapManager.Instance.UnlockRegion("RegenScene");
            DialogManager.Instance.PlayerMessageDialog(OpenRegenDialog);
            GameManager.Instance.GetCurrentDayController().CompleteTask("OpenRegen");
        }

        HideChoiceButtons();
    }

    // ������ ��ư �����
    private void HideChoiceButtons()
    {
        choiceButton1.gameObject.SetActive(false); // ������ 1 ��Ȱ��ȭ
        choiceButton2.gameObject.SetActive(false); // ������ 2 ��Ȱ��ȭ
    }

    private void OnDialogEnd()
    {
        // �ݹ��� �� ���� �����ϵ��� ��� ����
        DialogManager.Instance.OnDialogEnd -= OnDialogEnd;

        EndingManager.Instance.LoadEnding("Ending", "��忣��", 0);
    }
}
