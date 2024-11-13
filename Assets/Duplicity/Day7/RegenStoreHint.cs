using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenStoreHint : MonoBehaviour, IInteractable
{
    public string interactionMessage = "정보가 적혀있다.";
    public BigPanelDisplay bigPanelDisplay;
    public Sprite assignedImage;
    public string GetInteractionMessage()
    {
        return interactionMessage;
    }
    public void OnInteract()
    {
        bigPanelDisplay.ShowPanel(assignedImage);
    }

    public void HandleTask(string taskKey)
    {

    }

    public void ResetTask()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
