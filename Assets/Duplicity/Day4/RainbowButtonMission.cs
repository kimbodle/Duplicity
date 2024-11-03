using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class RainbowButtonMission : MonoBehaviour, IMission
{
    public Button[] rainbowButtons;
    private List<Button> clickedButtons = new List<Button>();
    private int clickCount = 0;

    public bool IsMissionCompleted { get; private set; }

    public void Initialize()
    {
        foreach (Button button in rainbowButtons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
        IsMissionCompleted = false;
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (clickedButtons.Contains(clickedButton)) return;

        clickedButtons.Add(clickedButton);
        clickCount++;
        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            buttonText.text = clickCount.ToString();
        }

        if (clickedButtons.Count == rainbowButtons.Length)
        {
            CheckOrder();
        }
    }

    private void CheckOrder()
    {
        IsMissionCompleted = true;
        for (int i = 0; i < rainbowButtons.Length; i++)
        {
            if (clickedButtons[i] != rainbowButtons[i])
            {
                IsMissionCompleted = false;
                break;
            }
        }

        if (IsMissionCompleted)
        {
            Debug.Log("무지개 버튼 서브 미션 통과!");
        }
        else
        {
            Debug.Log("무지개 버튼 서브 미션 실패. 다시 시도하세요.");
            ResetMission();
        }
    }

    private void ResetMission()
    {
        foreach (Button button in rainbowButtons)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = "";
            }
        }
        clickedButtons.Clear();
        clickCount = 0;
    }

    public bool CheckCompletion()
    {
        return IsMissionCompleted;
    }
}
