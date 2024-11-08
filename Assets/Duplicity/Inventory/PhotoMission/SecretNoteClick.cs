using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretNoteClick : MonoBehaviour
{
    public GameObject[] gamePlayeObject;
    public GameObject ending;

    public void DisplayDay5ending()
    {
        ending.SetActive(true);
        foreach (GameObject canvas in gamePlayeObject)
        {
            canvas.SetActive(false);
        }

    }
}
