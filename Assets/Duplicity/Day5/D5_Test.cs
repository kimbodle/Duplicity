using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D5_Test : MonoBehaviour
{
    public Dialog dialog;
    private HallucinationDialogManager hallucinationDialogManager;
    void Start()
    {
        hallucinationDialogManager = FindFirstObjectByType<HallucinationDialogManager>();
    }

    public void OnClickButton()
    {
        hallucinationDialogManager.StartHallucinationDialog(dialog);

    }
}
