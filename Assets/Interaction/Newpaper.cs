using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : MonoBehaviour, IInteractable
{
    //Test 内靛
    public string interactionMessage = "Press [E] to look.";
    //Day1Controller day1Controller;

    Renderer newpaper;

    private void OnEnable()
    {
        newpaper = GetComponent<Renderer>();
        //day1Controller = FindObjectOfType<Day1Controller>();
    }
    public void OnInteract()
    {
        newpaper.material.color = Color.red;
        //day1Controller.CompleteTask("FindItem");
    }

    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
    public void HandleTask(string taskKey)
    {
        Debug.Log("Newspaper 饶加 累诀 贸府");
        /*if (taskKey == "FindItem")
        {
            newpaper.material.color = Color.red;
            Debug.Log("Newspaper 饶加 累诀 贸府");
        }*/
    }
}
