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

    private void Start()
    {
        regionButton.onClick.AddListener(OnRegionClicked);
        //UpdateRegionStatus(); // 초기 상태 업데이트
    }

    // 지역 활성화 상태 업데이트
    public void UpdateRegionStatus()
    {
        if (GameManager.Instance.GetCurrentDay() >= unlockDay) // 현재 날짜에 따라 활성화
        {
            regionButton.interactable = true; // 버튼 활성화
            regionButton.GetComponent<Image>().color = Color.white; // 색상 변경
        }
        else
        {
            regionButton.interactable = false; // 버튼 비활성화
            regionButton.GetComponent<Image>().color = Color.gray; // 색상 변경
        }
    }

    // 지역 클릭 시
    // 지역 이름으로 해야할까 씬 이름으로 해야할까...
    private void OnRegionClicked()
    {
        if (regionButton.interactable)
        {
            UIManager.Instance.ToggleMapUI();
            //SceneManager.LoadScene(sceneName);
            FindObjectOfType<DayController>().MapIconClick(regionName);
        }
    }

    // 지역 활성화 메서드
    public void Unlock()
    {
        regionButton.interactable = true;
        regionButton.GetComponent<Image>().color = Color.white; // 색상 변경
    }

    // 지역 비활성화 메서드
    public void Lock()
    {
        regionButton.interactable = false;
        regionButton.GetComponent<Image>().color = Color.gray; // 색상 변경
    }
}
