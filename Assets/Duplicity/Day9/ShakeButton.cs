using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        VaccineMission.Instance.CompleteMission("ShakeFlask");
        //VaccineMission.Instance.EvaluateResult(); // ���� �� ��� ��
    }
}
