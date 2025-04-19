using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day7MissionManager : MonoBehaviour
{
    public MonoBehaviour[] missionBehaviours; // �� ���� �̼� ������Ʈ �迭
    private IMission[] missions; // ���������� IMission �������̽��� ��ȯ
    //private bool missionCompleted = false;
    public RegenStorageMissionTimer timer;
    public Dialog dialog;

    void Start()
    {
        missions = new IMission[missionBehaviours.Length];
        for (int i = 0; i < missionBehaviours.Length; i++)
        {
            missions[i] = missionBehaviours[i] as IMission;
            //Debug.Log(missions[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�̼� �ϳ� �����Ҷ����� ������Ʈ�ѷ����� �� ȣ��
    public void CheckAllMission()
    {
        foreach (IMission mission in missions)
        {
            if (!mission.CheckCompletion())
            {
                Debug.Log(mission+"�̼� ��� ������");
                return;
            }
        }

        Debug.Log("��ü ȹ�� �̼� ����");
        timer.CompleteMission();
        GameManager.Instance.GetCurrentDayController().CompleteTask("AllMissionClear");
        DialogManager.Instance.PlayerMessageDialog(dialog);
        //missionCompleted = true;

    }
}
