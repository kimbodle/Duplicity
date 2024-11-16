using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CabinetMission : MonoBehaviour,IMission
{
    public TMP_InputField[] inputFields; // 각 네모 칸의 InputField 배열
    public Button checkButton;
    public GameObject cabinetOpenPanel;
    public GameObject key;
    public Item keyItem;
    //public TMP_InputField passwordInputField;
    //public Button confirmButton;
    public Dialog dialog;
    public bool IsMissionCompleted { get; private set; }
    public const string correctPassword = "0428"; // 정답 단어

    void Start()
    {
        cabinetOpenPanel.SetActive(false);
        checkButton.onClick.AddListener(CheckMission);
        key.GetComponent<Button>().onClick.AddListener(OnClickKey);
        //confirmButton.onClick.AddListener(CheckPassword);
    }

    public void Initialize()
    {
        //초기화x
    }
    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }

    //Key -> Onclick 이벤트 연결
    public void OnClickKey()
    {
        //열쇠 아이템 획득 기능 코드 구현
        IsMissionCompleted = true;
        if(InventoryManager.Instance != null && GameManager.Instance != null)
        {
            InventoryManager.Instance.AddItemToInventory(keyItem);
            GameManager.Instance.GetCurrentDayController().CompleteTask("GetKey");
        }
        
        Destroy(key);
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
        if (userInput.Equals(correctPassword, System.StringComparison.OrdinalIgnoreCase))
        {
            cabinetOpenPanel.SetActive(true);
        }
        else
        {
            Debug.Log("입력 필드 서브 미션 실패. 다시 시도하세요.");
        }
    }
}
