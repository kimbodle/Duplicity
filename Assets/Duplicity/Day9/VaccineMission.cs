using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VaccineMission : MonoBehaviour
{
    public static VaccineMission Instance;

    private List<string> correctOrder = new List<string>
    {
        "InsertCard",      // 1. 출입증 넣기
        "WearLabCoat",     // 2. 실험복 입기
        "AddChemicals",    // 3. 시약 넣기
        "AddRegen",        // 4. 리젠 넣기
        "ShakeFlask"       // 5. 흔들기
    };

    private List<string> completedMissions = new List<string>(); // 플레이어가 완료한 미션 순서
    private HashSet<string> completedMissionSet = new HashSet<string>(); // 중복 방지용

    private List<string> enteredChemicalSequence = new List<string>(); // 플레이어가 입력한 시약 순서
    private string correctChemicalSequence = "YRRYGRGR";//"노빨빨노초빨초빨"; // 정답 시약 순서

    public GameObject flask; // 삼각 플라스크
    public GameObject blueFlask; // 성공 시 플라스크 이미지
    public GameObject grayFlask; // 실패 시 플라스크 이미지
    public TMP_Text chemicalSequenceDisplay; // 시약 순서 표시 UI

    private int shakeCount = 0; // 핸드폰 흔든 횟수


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 미션 완료 처리
    public void CompleteMission(string missionName)
    {
        if (!completedMissionSet.Contains(missionName))
        {
            completedMissions.Add(missionName);
            completedMissionSet.Add(missionName);
            Debug.Log($"{missionName} 미션 완료!");
        }
        else
        {
            Debug.Log($"{missionName} 이미 완료됨.");
        }
    }

    // 시약 추가
    public void AddChemical(string color)
    {
        if (enteredChemicalSequence.Count <= 8)
        {
            CompleteMission("AddChemicals");
            enteredChemicalSequence.Add(color);
            UpdateChemicalSequenceDisplay(); // UI 업데이트

            Debug.Log($"현재 시약 순서: {string.Join("", enteredChemicalSequence)}");

            if (string.Join("", enteredChemicalSequence) != correctChemicalSequence)
            {
                Debug.Log("잘못된 시약 순서입니다. 미션 실패!");
            }
            else
            {
                Debug.Log("올바른 시약 순서입니다.");
            }
        }
        else
        {
            return;
        }
    }

    // 리젠 추가
    public void AddRegen()
    {
        if (completedMissionSet.Contains("AddRegen"))
        {
            Debug.Log("리젠은 이미 추가되었습니다.");
            return;
        }

        CompleteMission("AddRegen");
        Debug.Log("리젠 추가 완료!");
    }
    private void UpdateChemicalSequenceDisplay()
    {
        if (chemicalSequenceDisplay != null)
        {
            chemicalSequenceDisplay.text = string.Join("-", enteredChemicalSequence);
        }
    }

    // 플라스크 흔들기
    public void ShakeFlask()
    {
        if (completedMissionSet.Contains("ShakeFlask"))
        {
            Debug.Log("이미 플라스크를 흔들었습니다.");
            return;
        }

        shakeCount++;
        Debug.Log($"플라스크를 {shakeCount}번 흔들었습니다.");

        if (shakeCount == 7)
        {
            CompleteMission("ShakeFlask");
            //인벤토리 비활성화 추가
            EvaluateResult(); // 7번 흔들기 완료 후 결과 평가
        }
    }

    // 결과 평가
    public void EvaluateResult()
    {
        if (completedMissions.Count < correctOrder.Count)
        {
            Debug.Log("모든 미션을 완료하지 않았습니다. 회색 플라스크로 변경.");
            SetFlaskSprite(false); // 회색 플라스크
            return;
        }

        // 올바른 순서인지 확인
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (i >= completedMissions.Count || completedMissions[i] != correctOrder[i])
            {
                Debug.Log("미션 순서가 올바르지 않습니다. 회색 플라스크로 변경.");
                SetFlaskSprite(false); // 회색 플라스크
                return;
            }
        }

        // 시약 순서 확인
        if (string.Join("", enteredChemicalSequence) != correctChemicalSequence)
        {
            Debug.Log("시약 순서가 올바르지 않습니다. 회색 플라스크로 변경.");
            SetFlaskSprite(false); // 회색 플라스크
            return;
        }

        Debug.Log("모든 미션을 올바른 순서로 완료했습니다. 파란 플라스크로 변경.");
        GameManager.Instance.GetCurrentDayController().CompleteTask("TheEnd");
        SetFlaskSprite(true); // 파란 플라스크
    }

    // 플라스크 색상 변경
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

        // 2초 후 FinalEnding 호출
        StartCoroutine(DelayedFinalEnding(isSuccess));
    }
    private IEnumerator DelayedFinalEnding(bool isSuccess)
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        FinalEnding(isSuccess);
    }

    private void FinalEnding(bool isSuccess)
    {
        Debug.Log("Final Ending 실행!");
        if (isSuccess)
        {
            EndingManager.Instance.LoadEnding("Ending", "Yell: 외치다", 2);
        }
        else
        {
            EndingManager.Instance.LoadEnding("GameOver", "실험 틀림", 9);
        }
    }

}
