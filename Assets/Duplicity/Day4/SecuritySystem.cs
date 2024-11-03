using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecuritySystem : MonoBehaviour
{
    public MonoBehaviour[] missionBehaviours; // 각 서브 미션 컴포넌트 배열
    private IMission[] missions; // 내부적으로 IMission 인터페이스로 변환
    public Button finalButton;
    public Text timerText;
    private float timeLimit = 77f;
    private bool missionCompleted = false;

    void Start()
    {
        // missionBehaviours 배열의 각 요소를 IMission으로 캐스팅
        missions = new IMission[missionBehaviours.Length];
        for (int i = 0; i < missionBehaviours.Length; i++)
        {
            missions[i] = missionBehaviours[i] as IMission;
            missions[i].Initialize();
        }

        finalButton.onClick.AddListener(CheckFinalMission);
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        float timeRemaining = timeLimit;
        while (timeRemaining > 0 && !missionCompleted)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "남은 시간: " + Mathf.Ceil(timeRemaining).ToString() + "초";
            yield return null;
        }

        if (!missionCompleted)
        {
            Debug.Log("시간 초과! 미션 실패");
            // 실패 처리 로직
        }
    }

    private void CheckFinalMission()
    {
        foreach (IMission mission in missions)
        {
            if (!mission.CheckCompletion())
            {
                Debug.Log("미션 요건 미충족");
                return;
            }
        }

        Debug.Log("전체 보안 시스템 해킹 미션 성공!");
        missionCompleted = true;
        // 전체 미션 성공 처리
    }
}
