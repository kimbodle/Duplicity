using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HallucinationDialogManager : MonoBehaviour
{
    public GameObject hallucinationPanel; // ȯû ���̾�α� �г�
    public TMP_Text hallucinationText; // ȯû ���̾�α� �ؽ�Ʈ
    [SerializeField] float typingSpeed = 0.05f; // Ÿ���� �ӵ� ���� ����
    [SerializeField] private float displayDuration = 1f; // �۾��� ���� �� �� �����Ǵ� �ð�

    private bool isTyping = false; // ���� Ÿ���� ������ ����

    public event Action OnHallucinationDialogEnd; // ȯû ���� �� �߻��ϴ� �̺�Ʈ

    private void Start()
    {
        hallucinationPanel.SetActive(false);
    }

    // ȯû ���̾�α� ���� �޼���
    public void StartHallucinationDialog(Dialog hallucinationDialog)
    {
        if (isTyping) return;

        hallucinationPanel.SetActive(true);
        StartCoroutine(TypeSentence(hallucinationDialog.sentences[0]));
    }

    // ������ �� ���ھ� ǥ���� ��, typingSpeed �ڿ� �г��� ��Ȱ��ȭ
    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        hallucinationText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            hallucinationText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        EndHallucinationDialog();
        // ��� �۾��� �� �� �� ��� ����
        yield return new WaitForSeconds(displayDuration);
    }

    // ȯû ���̾�α� ���� �޼���
    public void EndHallucinationDialog()
    {
        hallucinationPanel.SetActive(false);
        hallucinationText.text = "";

        // ���� �̺�Ʈ �߻�
        OnHallucinationDialogEnd?.Invoke();
    }
}
