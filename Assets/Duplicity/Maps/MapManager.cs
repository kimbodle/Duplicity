using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    public MapRegion[] mapRegions; // 모든 지역 아이콘 배열

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

    private void Start()
    {
        //UpdateMapRegions();
    }

    // 모든 지역 아이콘 상태 업데이트
    public void UpdateMapRegions()
    {
        foreach (var region in mapRegions)
        {
            region.UpdateRegionStatus();
        }
    }

    public void InitializeMapRegions()
    {
        foreach (var region in mapRegions)
        {
            region.Lock();
        }
    }

    // 특정 지역 활성화
    public void UnlockRegion(string regionName)
    {
        foreach (var region in mapRegions)
        {
            if (region.regionName == regionName)
            {
                region.Unlock();
                break;
            }
        }
    }

    // 특정 지역 비활성화
    public void LockRegion(string regionName)
    {
        foreach (var region in mapRegions)
        {
            if (region.regionName == regionName)
            {
                region.Lock();
                break;
            }
        }
    }
    public void UnlockAllRegion()
    {
        foreach (var region in mapRegions)
        {
            region.Unlock();
        }
    }
}