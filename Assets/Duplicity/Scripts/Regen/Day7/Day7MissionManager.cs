using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day7MissionManager : MonoBehaviour
{
    public MonoBehaviour[] missionBehaviours; // 각 서브 미션 컴포넌트 배열
    private IMission[] missions; // 내부적으로 IMission 인터페이스로 변환
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

    //미션 하나 수행할때마다 데이컨트롤러에서 얘 호출
    public void CheckAllMission()
    {
        foreach (IMission mission in missions)
        {
            if (!mission.CheckCompletion())
            {
                Debug.Log(mission+"미션 요건 미충족");
                return;
            }
        }

        Debug.Log("전체 획득 미션 성공");
        timer.CompleteMission();
        GameManager.Instance.GetCurrentDayController().CompleteTask("AllMissionClear");
        DialogManager.Instance.PlayerMessageDialog(dialog);
        //missionCompleted = true;

    }
}
