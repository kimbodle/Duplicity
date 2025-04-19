using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour, IInteractable
{
    //Test 코드
    public string interactionMessage = "Press [E] to look.";

    Renderer newpaper;

    private void OnEnable()
    {
        newpaper = GetComponent<Renderer>();
    }
    public void OnInteract()
    {
        newpaper.material.color = Color.red;
        //호출방식 변경
        GameManager.Instance.GetCurrentDayController().CompleteTask("FindItem");
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
    public void HandleTask(string taskKey)
    {
        Debug.Log("Newspaper 후속 작업 처리");
        if (taskKey == "FindItem")
        {
            newpaper.material.color = Color.red;
            Debug.Log("Newspaper 후속 작업 처리");
        }
    }

    void IInteractable.ResetTask()
    {
        Debug.Log(gameObject.name+"리셋");
    }
}
