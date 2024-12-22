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

    // ΩÃ±€≈Ê ∞¥√º∏¶ µÓ∑œ
    public void RegisterSingleton(GameObject singleton)
    {
        if (!singletonObjects.Contains(singleton))
        {
            singletonObjects.Add(singleton);
        }
    }

    // ΩÃ±€≈Ê ∞¥√º √ ±‚»≠
    public void LogoutAndDestroySingletons()
    {
        foreach (var obj in singletonObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        // ∏ÆΩ∫∆Æ ∫ÒøÏ±‚
        singletonObjects.Clear();

        // ∏ﬁ¿Œ æ¿¿∏∑Œ ¿Ãµø
        SceneManager.LoadScene("MainScene");
    }
}
