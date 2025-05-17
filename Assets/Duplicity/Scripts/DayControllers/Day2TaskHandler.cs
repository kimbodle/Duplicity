using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2TaskHandler : TaskHandler
{
    protected override void InitializeTaskMappings()
    {
        taskMappings = new Dictionary<string, IInteractable>();
        var newspaper = FindObjectOfType<Newspaper>();

        if (newspaper != null)
        {
            taskMappings.Add("FindItem", newspaper);
        }
        else
        {

        }

        
    }
}

