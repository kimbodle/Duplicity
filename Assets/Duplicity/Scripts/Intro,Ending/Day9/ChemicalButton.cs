using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChemicalButton : MonoBehaviour
{
    public string color; // �þ� ���� (��, ��, �� ��)
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickColorButton);
    }

    public void OnClickColorButton()
    {
        if (button != null)
        {
            VaccineMission.Instance.AddChemical(color);
            AudioManager.Instance.PlayUIButton();
        }
    }
}
