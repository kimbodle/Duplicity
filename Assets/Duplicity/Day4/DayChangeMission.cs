using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeMission : MonoBehaviour, IMission
{
    public Button dayButton;
    public TMP_Text dayText;
    private string[] days = { "월요일", "화요일", "수요일", "목요일", "금요일" };
    private int currentDayIndex = 0;

    public bool IsMissionCompleted { get; private set; }

    public void Initialize()
    {
        dayButton.onClick.AddListener(ChangeDay);
        dayText.text = days[currentDayIndex];
        IsMissionCompleted = false;
    }

    private void ChangeDay()
    {
        if (currentDayIndex < days.Length - 1)
        {
            currentDayIndex++;
            dayText.text = days[currentDayIndex];
            if (currentDayIndex == days.Length - 1)
            {
                IsMissionCompleted = true;
                Debug.Log("요일 변경 서브 미션 통과!");
            }
        }
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }
}
