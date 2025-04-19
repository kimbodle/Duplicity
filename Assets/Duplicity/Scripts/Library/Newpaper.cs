using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour, IInteractable
{
    //Test �ڵ�
    public string interactionMessage = "Press [E] to look.";

    Renderer newpaper;

    private void OnEnable()
    {
        newpaper = GetComponent<Renderer>();
    }
    public void OnInteract()
    {
        newpaper.material.color = Color.red;
        //ȣ���� ����
        GameManager.Instance.GetCurrentDayController().CompleteTask("FindItem");
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
    public void HandleTask(string taskKey)
    {
        Debug.Log("Newspaper �ļ� �۾� ó��");
        if (taskKey == "FindItem")
        {
            newpaper.material.color = Color.red;
            Debug.Log("Newspaper �ļ� �۾� ó��");
        }
    }

    void IInteractable.ResetTask()
    {
        Debug.Log(gameObject.name+"����");
    }
}
