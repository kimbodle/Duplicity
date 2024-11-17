using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldMission : MonoBehaviour, IMission
{
    public TMP_InputField[] inputFields; // �� �׸� ĭ�� InputField �迭
    public Button checkButton; // ���� ��ư
    public string correctWord = "NIGHT"; // ���� �ܾ�
    public GameObject missionStatusText; // ���� �̼� ��� ���¸� ǥ���� Text (�ɼ�)

    public bool IsMissionCompleted { get; private set; }

    public void Initialize()
    {
        IsMissionCompleted = false;
        missionStatusText.SetActive(false);
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
            /*
            if (missionStatusText != null)
            {
                //missionStatusText.text = "Clear!";
                missionStatusText.SetActive(true);
            }*/
            // �� InputField�� �ؽ�Ʈ�� ���ļ� ����� �Է� Ȯ��
            int i = 0;
            foreach (TMP_InputField inputField in inputFields)
            {
                if(i == 0)
                {
                    inputField.text = "N";
                    i++;
                }
                else if(i == 1)
                {
                    inputField.text = "I";
                    i++;
                }
                else if (i == 2)
                {
                    inputField.text = "G";
                    i++;
                }
                else if (i == 3)
                {
                    inputField.text = "H";
                    i++;
                }
                else if (i == 4)
                {
                    inputField.text = "T";
                }

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
