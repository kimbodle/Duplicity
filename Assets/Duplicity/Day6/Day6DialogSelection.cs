using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Day6DialogSelection : MonoBehaviour
{
    public TMP_Text dialogueText; // ��� �ؽ�Ʈ
    public Button choiceButton1; // ������ 1 ��ư
    public Button choiceButton2; // ������ 2 ��ư
    [Space(10)]
    public Dialog BadendingDialog;
    public Dialog OpenRegenDialog;
    // Start is called before the first frame update
    void Start()
    {
        // ������ ��ư�� Ŭ�� �̺�Ʈ ����
        choiceButton1.onClick.AddListener(() => OnChoiceClick(1)); // ������ 1 Ŭ�� ��
        choiceButton2.onClick.AddListener(() => OnChoiceClick(2)); // ������ 2 Ŭ�� ��

        // ������ ��ư �ʱ� ��Ȱ��ȭ
        choiceButton1.gameObject.SetActive(false);
        choiceButton2.gameObject.SetActive(false);
    }

    // �ܺο��� ȣ���Ͽ� ������ ��ư ǥ��
    public void ShowChoices()
    {
        SetChoices("�ֺ��� ���ƺ��� ���� �� �߸��̾�.", "�׷��� �� ���� ������. �̰� ���� ��� �� �� �߸��̾�?");
    }

    private void SetChoices(string choice1, string choice2)
    {
        choiceButton1.GetComponentInChildren<TMP_Text>().text = choice1;
        choiceButton2.GetComponentInChildren<TMP_Text>().text = choice2;
        choiceButton1.gameObject.SetActive(true); // ������ 1 Ȱ��ȭ
        choiceButton2.gameObject.SetActive(true); // ������ 2 Ȱ��ȭ
    }

    // ������ ��ư Ŭ�� ó��
    private void OnChoiceClick(int choice)
    {
        if (choice == 1)
        {
            Debug.Log("����");
            DialogManager.Instance.PlayerMessageDialog(BadendingDialog);
            //���� �Ŵ��� 

        }
        else
        {
            Debug.Log("����� ����");
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
}
