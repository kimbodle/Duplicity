using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldMission : MonoBehaviour, IMission
{
    public TMP_InputField[] inputFields; // �� �׸� ĭ�� InputField �迭
    public Button checkButton; // ���� ��ư
    public string correctWord = "NIGHT"; // ���� �ܾ�
    public TMP_Text missionStatusText; // ���� �̼� ��� ���¸� ǥ���� Text (�ɼ�)

    public bool IsMissionCompleted { get; private set; }

    public void Initialize()
    {
        IsMissionCompleted = false;
        checkButton.onClick.AddListener(CheckMission);
    }

    public void CheckMission()
    {
        string userInput = "";

        // �� InputField�� �ؽ�Ʈ�� ���ļ� ����� �Է� Ȯ��
        foreach (TMP_InputField inputField in inputFields)
        {
            userInput += inputField.text;
            inputField.text = "";
        }

        // �Էµ� �ؽ�Ʈ�� ���� �ܾ�� ������ Ȯ��
        if (userInput.Equals(correctWord, System.StringComparison.OrdinalIgnoreCase))
        {
            IsMissionCompleted = true;
            Debug.Log("�Է� �ʵ� ���� �̼� ���!");
            if (missionStatusText != null)
            {
                missionStatusText.text = "Clear!";
            }
        }
        else
        {
            Debug.Log("�Է� �ʵ� ���� �̼� ����. �ٽ� �õ��ϼ���.");
            if (missionStatusText != null)
            {
               // missionStatusText.text = "���� �̼� ����. �ٽ� �õ��ϼ���.";
            }
        }
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }
}
