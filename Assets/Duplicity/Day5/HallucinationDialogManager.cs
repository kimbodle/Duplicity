using System.Collections;
using TMPro;
using UnityEngine;

public class HallucinationDialogManager : MonoBehaviour
{
    public GameObject hallucinationPanel; // ȯû ���̾�α� �г�
    public TMP_Text hallucinationText; // ȯû ���̾�α� �ؽ�Ʈ
    [SerializeField] float typingSpeed = 0.05f; // Ÿ���� �ӵ� ���� ����
    [SerializeField] private float displayDuration = 2f; // �۾��� ���� �� �� �����Ǵ� �ð� (2��)

    private bool isTyping = false; // ���� Ÿ���� ������ ����

    private void Start()
    {
        hallucinationPanel.SetActive(false); // �ʱ⿡�� ȯû �г��� ��Ȱ��ȭ
    }

    // ȯû ���̾�α� ���� �޼���
    public void StartHallucinationDialog(Dialog hallucinationDialog)
    {
        if (isTyping) return;

        hallucinationPanel.SetActive(true);
        StartCoroutine(TypeSentence(hallucinationDialog.sentences[0]));
    }

    // ������ �� ���ھ� ǥ���� ��, 2�� �ڿ� �г��� ��Ȱ��ȭ�ϴ� �ڷ�ƾ
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

        // ��� �۾��� �� �� �� ��縦 2�ʰ� ����
        yield return new WaitForSeconds(displayDuration);
        EndHallucinationDialog();
    }

    // ȯû ���̾�α� ���� �޼���
    public void EndHallucinationDialog()
    {
        hallucinationPanel.SetActive(false);
        hallucinationText.text = "";
    }
}
