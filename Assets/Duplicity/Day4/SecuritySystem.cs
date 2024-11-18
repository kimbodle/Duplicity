using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SecuritySystem : MonoBehaviour
{
    public MonoBehaviour[] missionBehaviours; // �� ���� �̼� ������Ʈ �迭
    private IMission[] missions; // ���������� IMission �������̽��� ��ȯ
    [Space(10)]
    public GameObject securitySystemPanel;
    public Button finalButton;
    public TMP_Text timerText;
    public Button closeButton;
    [Space(10)]
    public Dialog dialog;
    public SecretLab secretLab; // SecretLab Ŭ���� ����

    private float timeLimit = 77f;
    private bool missionCompleted = false;
    private bool isStart = true;

    void Start()
    {
        // missionBehaviours �迭�� �� ��Ҹ� IMission���� ĳ����
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
            timerText.text = "���� �ð�: " + Mathf.Ceil(timeRemaining).ToString() + "��";
            yield return null;
        }

        if (!missionCompleted)
        {
            Debug.Log("�ð� �ʰ�! �̼� ����");
            EndingManager.Instance.LoadEnding("GameOver", "���� �ý��� ��ŷ ����", 3);
        }
    }

    private void CheckFinalMission()
    {
        foreach (IMission mission in missions)
        {
            if (!mission.CheckCompletion())
            {
                Debug.Log("�̼� ��� ������");
                return;
            }
        }

        Debug.Log("��ü ���� �ý��� ��ŷ �̼� ����!");
        missionCompleted = true;
        OnCloseButton();
        FadeManager.Instance.StartFadeOut(() =>
        {
            Debug.Log("SecretLabDisplay ����");
            secretLab.SecretLabDisplay();
        }, true, 1.5f);
    }

    public void OnCloseButton()
    {
        securitySystemPanel.SetActive(false);
    }

    public void Debugging()
    {
        Debug.Log("��ü ���� �ý��� ��ŷ �̼� ����!");
        missionCompleted = true;
        OnCloseButton();
        FadeManager.Instance.StartFadeOut(() =>
        {
            Debug.Log("SecretLabDisplay ����");
            secretLab.SecretLabDisplay();
        }, true); // autoFadeIn�� true�� ����
    }
}
