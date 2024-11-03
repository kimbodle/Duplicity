using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecuritySystem : MonoBehaviour
{
    public MonoBehaviour[] missionBehaviours; // �� ���� �̼� ������Ʈ �迭
    private IMission[] missions; // ���������� IMission �������̽��� ��ȯ
    public Button finalButton;
    public Text timerText;
    private float timeLimit = 77f;
    private bool missionCompleted = false;

    void Start()
    {
        // missionBehaviours �迭�� �� ��Ҹ� IMission���� ĳ����
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
            timerText.text = "���� �ð�: " + Mathf.Ceil(timeRemaining).ToString() + "��";
            yield return null;
        }

        if (!missionCompleted)
        {
            Debug.Log("�ð� �ʰ�! �̼� ����");
            // ���� ó�� ����
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
}
