using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonManager : MonoBehaviour
{
    private static SingletonManager _instance;

    public static SingletonManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("SingletonManager");
                _instance = obj.AddComponent<SingletonManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    private List<GameObject> singletonObjects = new List<GameObject>();

    // �̱��� ��ü�� ���
    public void RegisterSingleton(GameObject singleton)
    {
        if (!singletonObjects.Contains(singleton))
        {
            singletonObjects.Add(singleton);
        }
    }

    // �̱��� ��ü �ʱ�ȭ
    public void LogoutAndDestroySingletons()
    {
        foreach (var obj in singletonObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // ����Ʈ ����
        singletonObjects.Clear();

        // ���� ������ �̵�
        SceneManager.LoadScene("MainScene");
    }
}
