using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsRunning { get; private set; } = true;

    [SerializeField]
    private LevelData[] AllLevelData = { };

    public LevelData CurrentLevelData { get; private set; }
    public int CurrentLevelIndex { get; private set; }

    private float _currentTime = 0;




    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        //siingleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (AllLevelData.Length > 0)
        {
            CurrentLevelData = AllLevelData[0];
            CurrentLevelIndex = 0;
        }
        else
        {
            Debug.LogError("NO LEVEL DATA!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;

        if(_currentTime > CurrentLevelData.EndTime && CurrentLevelIndex < AllLevelData.Length)
        {
            ++CurrentLevelIndex;
            CurrentLevelData = AllLevelData[CurrentLevelIndex];   

            CollectibleManager.Instance.UpdateLevelData();
        }

    }
}
