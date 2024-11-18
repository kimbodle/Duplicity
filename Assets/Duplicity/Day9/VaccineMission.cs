using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VaccineMission : MonoBehaviour
{
    public static VaccineMission Instance;

    private List<string> correctOrder = new List<string>
    {
        "InsertCard",      // 1. ������ �ֱ�
        "WearLabCoat",     // 2. ���躹 �Ա�
        "AddChemicals",    // 3. �þ� �ֱ�
        "AddRegen",        // 4. ���� �ֱ�
        "ShakeFlask"       // 5. ����
    };

    private List<string> completedMissions = new List<string>(); // �÷��̾ �Ϸ��� �̼� ����
    private HashSet<string> completedMissionSet = new HashSet<string>(); // �ߺ� ������

    private List<string> enteredChemicalSequence = new List<string>(); // �÷��̾ �Է��� �þ� ����
    private string correctChemicalSequence = "YRRYGRGR";//"�뻡�����ʻ��ʻ�"; // ���� �þ� ����

    public GameObject flask; // �ﰢ �ö�ũ
    public GameObject blueFlask; // ���� �� �ö�ũ �̹���
    public GameObject grayFlask; // ���� �� �ö�ũ �̹���
    public TMP_Text chemicalSequenceDisplay; // �þ� ���� ǥ�� UI

    private int shakeCount = 0; // �ڵ��� ��� Ƚ��


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // �̼� �Ϸ� ó��
    public void CompleteMission(string missionName)
    {
        if (!completedMissionSet.Contains(missionName))
        {
            completedMissions.Add(missionName);
            completedMissionSet.Add(missionName);
            Debug.Log($"{missionName} �̼� �Ϸ�!");
        }
        else
        {
            Debug.Log($"{missionName} �̹� �Ϸ��.");
        }
    }

    // �þ� �߰�
    public void AddChemical(string color)
    {
        if (enteredChemicalSequence.Count <= 8)
        {
            CompleteMission("AddChemicals");
            enteredChemicalSequence.Add(color);
            UpdateChemicalSequenceDisplay(); // UI ������Ʈ

            Debug.Log($"���� �þ� ����: {string.Join("", enteredChemicalSequence)}");

            if (string.Join("", enteredChemicalSequence) != correctChemicalSequence)
            {
                Debug.Log("�߸��� �þ� �����Դϴ�. �̼� ����!");
            }
            else
            {
                Debug.Log("�ùٸ� �þ� �����Դϴ�.");
            }
        }
        else
        {
            return;
        }
    }

    // ���� �߰�
    public void AddRegen()
    {
        if (completedMissionSet.Contains("AddRegen"))
        {
            Debug.Log("������ �̹� �߰��Ǿ����ϴ�.");
            return;
        }

        CompleteMission("AddRegen");
        Debug.Log("���� �߰� �Ϸ�!");
    }
    private void UpdateChemicalSequenceDisplay()
    {
        if (chemicalSequenceDisplay != null)
        {
            chemicalSequenceDisplay.text = string.Join("-", enteredChemicalSequence);
        }
    }

    // �ö�ũ ����
    public void ShakeFlask()
    {
        if (completedMissionSet.Contains("ShakeFlask"))
        {
            Debug.Log("�̹� �ö�ũ�� �������ϴ�.");
            return;
        }

        shakeCount++;
        Debug.Log($"�ö�ũ�� {shakeCount}�� �������ϴ�.");

        if (shakeCount == 7)
        {
            CompleteMission("ShakeFlask");
            //�κ��丮 ��Ȱ��ȭ �߰�
            EvaluateResult(); // 7�� ���� �Ϸ� �� ��� ��
        }
    }

    // ��� ��
    public void EvaluateResult()
    {
        if (completedMissions.Count < correctOrder.Count)
        {
            Debug.Log("��� �̼��� �Ϸ����� �ʾҽ��ϴ�. ȸ�� �ö�ũ�� ����.");
            SetFlaskSprite(false); // ȸ�� �ö�ũ
            return;
        }

        // �ùٸ� �������� Ȯ��
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (i >= completedMissions.Count || completedMissions[i] != correctOrder[i])
            {
                Debug.Log("�̼� ������ �ùٸ��� �ʽ��ϴ�. ȸ�� �ö�ũ�� ����.");
                SetFlaskSprite(false); // ȸ�� �ö�ũ
                return;
            }
        }

        // �þ� ���� Ȯ��
        if (string.Join("", enteredChemicalSequence) != correctChemicalSequence)
        {
            Debug.Log("�þ� ������ �ùٸ��� �ʽ��ϴ�. ȸ�� �ö�ũ�� ����.");
            SetFlaskSprite(false); // ȸ�� �ö�ũ
            return;
        }

        Debug.Log("��� �̼��� �ùٸ� ������ �Ϸ��߽��ϴ�. �Ķ� �ö�ũ�� ����.");
        GameManager.Instance.GetCurrentDayController().CompleteTask("TheEnd");
        SetFlaskSprite(true); // �Ķ� �ö�ũ
    }

    // �ö�ũ ���� ����
    private void SetFlaskSprite(bool isSuccess)
    {
        if(UIManager.Instance != null)
        {
            UIManager.Instance.EndingUI();
        }

        flask.SetActive(false);

        if (isSuccess)
        {
            blueFlask.SetActive(true);
        }
        else
        {
            grayFlask.SetActive(true);
        }

        // 2�� �� FinalEnding ȣ��
        StartCoroutine(DelayedFinalEnding(isSuccess));
    }
    private IEnumerator DelayedFinalEnding(bool isSuccess)
    {
        yield return new WaitForSeconds(2f); // 2�� ���
        FinalEnding(isSuccess);
    }

    private void FinalEnding(bool isSuccess)
    {
        Debug.Log("Final Ending ����!");
        if (isSuccess)
        {
            EndingManager.Instance.LoadEnding("Ending", "Yell: ��ġ��", 2);
        }
        else
        {
            EndingManager.Instance.LoadEnding("GameOver", "���� Ʋ��", 9);
        }
    }

}
