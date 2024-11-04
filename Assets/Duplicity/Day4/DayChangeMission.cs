using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeMission : MonoBehaviour, IMission
{
    public Button dayButton;
    public TMP_Text dayText;
    private string[] days = { "������", "ȭ����", "������", "�����", "�ݿ���" };
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
                Debug.Log("���� ���� ���� �̼� ���!");
            }
        }
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }
}
