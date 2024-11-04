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
            isStart=false;
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

            EndingManager.Instance.LoadEnding("BadEnding", "���� �ý��� ��ŷ ����");
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
        // ��ü �̼� ���� ó��
    }

    public void OnCloseButton()
    {
        securitySystemPanel.SetActive(false);
    }
}
