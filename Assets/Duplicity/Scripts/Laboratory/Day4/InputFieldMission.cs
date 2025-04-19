using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldMission : MonoBehaviour, IMission
{
    public TMP_InputField[] inputFields; // 각 네모 칸의 InputField 배열
    public Button checkButton; // 빨간 버튼
    public string correctWord = "NIGHT"; // 정답 단어
    public GameObject missionStatusText; // 서브 미션 통과 상태를 표시할 Text (옵션)

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

        // 각 InputField의 텍스트를 합쳐서 사용자 입력 확인
        foreach (TMP_InputField inputField in inputFields)
        {
            userInput += inputField.text;
            inputField.text = "";
        }

        // 입력된 텍스트가 정답 단어와 같은지 확인
        if (userInput.Equals(correctWord, System.StringComparison.OrdinalIgnoreCase))
        {
            IsMissionCompleted = true;
            Debug.Log("입력 필드 서브 미션 통과!");
            /*
            if (missionStatusText != null)
            {
                //missionStatusText.text = "Clear!";
                missionStatusText.SetActive(true);
            }*/
            // 각 InputField의 텍스트를 합쳐서 사용자 입력 확인
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
            Debug.Log("입력 필드 서브 미션 실패. 다시 시도하세요.");
            if (missionStatusText != null)
            {
               // missionStatusText.text = "서브 미션 실패. 다시 시도하세요.";
            }
        }
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }
}
