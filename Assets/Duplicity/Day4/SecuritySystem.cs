using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SecuritySystem : MonoBehaviour
{
    public MonoBehaviour[] missionBehaviours; // 각 서브 미션 컴포넌트 배열
    private IMission[] missions; // 내부적으로 IMission 인터페이스로 변환
    [Space(10)]
    public GameObject securitySystemPanel;
    public Button finalButton;
    public TMP_Text timerText;
    public Button closeButton;
    [Space(10)]
    public Dialog dialog;
    public SecretLab secretLab; // SecretLab 클래스 참조

    private float timeLimit = 77f;
    private bool missionCompleted = false;
    private bool isStart = true;

    void Start()
    {
        // missionBehaviours 배열의 각 요소를 IMission으로 캐스팅
        missions = new IMission[missionBehaviours.Length];
        for (int i = 0; i < missionBehaviours.Length; i++)
        {
            missions[i] = missionBehaviours[i] as IMission;
            missions[i].Initialize();
        }
        securitySystemPanel.SetActive(false);
        finalButton.onClick.AddListener(CheckFinalMission);
    }

    public void SecuritySystemMissionStart()
    {
        if (isStart)
        {
            securitySystemPanel.SetActive(true);
            StartCoroutine(StartTimer());
            if(DialogManager.Instance != null)
            {
                DialogManager.Instance.PlayerMessageDialog(dialog);
            }
            isStart = false;
        }
        else
        {
            securitySystemPanel.SetActive(true);
        }
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
            FadeManager.Instance.StartFadeOut(() =>
            {
                EndingManager.Instance.LoadEnding("GameOver", "보안 시스템 해킹 실패", 3);
            }, true, 3f);
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
        OnCloseButton();
        FadeManager.Instance.StartFadeOut(() =>
        {
            Debug.Log("SecretLabDisplay 실행");
            secretLab.SecretLabDisplay();
        }, true, 1.5f);
    }

    public void OnCloseButton()
    {
        securitySystemPanel.SetActive(false);
    }

    public void Debugging()
    {
        Debug.Log("전체 보안 시스템 해킹 미션 성공!");
        missionCompleted = true;
        OnCloseButton();
        FadeManager.Instance.StartFadeOut(() =>
        {
            Debug.Log("SecretLabDisplay 실행");
            secretLab.SecretLabDisplay();
        }, true); // autoFadeIn을 true로 설정
    }
}
