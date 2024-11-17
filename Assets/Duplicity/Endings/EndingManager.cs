using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance { get; private set; }
    public int endingIndex; // ���� �ε���
    public string endingMessage; // ���� �޽���

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadEnding(string endingName, string message, int endingIndex = 0)
    {
        //���� ���� �ʱ�ȭ
        GameManager.Instance.isInitializingGameState = true;
        UIManager.Instance.EndingUI();
        if(InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ClearAllItemSlot();
        }

        this.endingIndex = endingIndex;
        this.endingMessage = message;

        switch (endingName)
        {
            case "Ending":
                SceneManager.LoadScene("EndingScene");
                break;
            case "GameOver":
                SceneManager.LoadScene("GameOverScene");
                break;
            default:
                Debug.LogError("Unknown ending: " + endingName);
                break;
        }
    }
}
