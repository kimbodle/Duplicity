using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapRegion : MonoBehaviour
{
    [Header("각 지역마다 설정")]
    public string regionName; // 지역 이름
    public string sceneName; // 해당 지역의 씬 이름
    public Button regionButton; // UI 버튼
    public int unlockDay; // 활성화되는 날.
    public Dialog[] dialogIfIncomplete;

    private void Start()
    {
        regionButton.onClick.AddListener(OnRegionClicked);
        //Debug.Log("맵리전 스타트???????????");
        //어떤 이유에서인지, Start임에도 불구하고 씬전환하고 mapIcon을 클릭했을때 이게 뜬다 왜지
        //UpdateRegionStatus(); // 초기 상태 업데이트
    }

    // 지역 활성화 상태 업데이트
    public void UpdateRegionStatus()
    {
        regionButton.interactable = true; // 버튼 활성화
        regionButton.GetComponent<Image>().color = Color.gray; // 색상 변경
    }

    // 지역 클릭 시
    // 지역 이름으로 해야할까 씬 이름으로 해야할까...
    private void OnRegionClicked()
    {
        //맵 UI 닫기
        UIManager.Instance.ToggleMapUI();
        FindObjectOfType<DayController>().MapIconClick(regionName);
    }

    // 지역 활성화 메서드
    public void Unlock()
    {
        Debug.Log(regionName + "언락");
        regionButton.interactable = true;
        regionButton.GetComponent<Image>().color = Color.white; // 색상 변경
    }

    // 지역 비활성화 메서드
    public void Lock()
    {
        Debug.Log(regionName + "Lock");
        regionButton.interactable = false;
        regionButton.GetComponent<Image>().color = Color.gray; // 색상 변경
    }
}
