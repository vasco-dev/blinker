using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsRunning { get; private set; } = true;

    [SerializeField]
    private LevelData[] AllLevelData = { };

    public LevelData CurrentLevelData { get; private set; }
    public int CurrentLevelIndex { get; private set; }

    private float _currentTime = 0;

    public int CurrentScore { get; private set; } = 0;
    public float RecordScore { get; private set; } = 0;

    public static GameManager Instance { get; private set; }


    private int _frames = 0;
    private float _frameRate = 0;
    [SerializeField ] private TextMeshProUGUI _fpsText;

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

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > CurrentLevelData.EndTime && CurrentLevelIndex < AllLevelData.Length - 1)
        {
            ++CurrentLevelIndex;
            CurrentLevelData = AllLevelData[CurrentLevelIndex];

            CollectibleManager.Instance.UpdateLevelData();
        }

        //++_frames;
        //_frameRate = Mathf.RoundToInt(_frames / _currentTime);
        //_fpsText.text= _frameRate.ToString();



    }
    public void AddScore(int scoreToAdd)
    {
        CurrentScore+= scoreToAdd;
    }

    public void RestartLevel()
    {
        CollectibleManager.Instance.DestroyAllCollectibles();
        CurrentLevelData = AllLevelData[0];
        CurrentScore = 0;
        _currentTime = 0;
    }
}
