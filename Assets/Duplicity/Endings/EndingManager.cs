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
        GameManager.Instance.isInitializingGameState = true;
        this.endingIndex = endingIndex;
        this.endingMessage = message;

        switch (endingName)
        {
            case "GoodEnding":
                SceneManager.LoadScene("GoodEndingScene");
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