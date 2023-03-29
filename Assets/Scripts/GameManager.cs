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
    public int CurrentScore { get; private set; } = 0;
    public int HighScore { get; private set; } = 0;


    //[SerializeField ] private TextMeshProUGUI _fpsText;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        //Singleton
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

    private void OnEnable()
    {
        IsRunning = true;
    }
    public void UpdateCurrentLevelData()
    {
        if (CurrentLevelIndex < AllLevelData.Length - 1)
        {
            ++CurrentLevelIndex;
            CurrentLevelData = AllLevelData[CurrentLevelIndex];

            CollectibleManager.Instance.UpdateLevelData();
        }
        else
        {
            EndGame();
        }
    }
    public float GetCurrentLevelEndTime()
    {
        return CurrentLevelData.EndTime;
    }

    public void AddScore(int scoreToAdd)
    {
        CurrentScore+= scoreToAdd;
        ScoreManager.Instance.UpdateScoreText(CurrentScore);

        if(CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            ScoreManager.Instance.UpdateHighScore(HighScore);

        }

    }

    public void RestartGame()
    {
        MenuManager.Instance.EndGame();

        CurrentScore = 0;
        ScoreManager.Instance.UpdateScoreText(CurrentScore);    

        CollectibleManager.Instance.DestroyAllCollectibles();

        CurrentLevelData = AllLevelData[0];

        CurrentScore = 0;

        Timer.Instance.RestartGame();
    }
    public void EndGame()
    {
        IsRunning = false;
        MenuManager.Instance.EndGame();
    }
}
