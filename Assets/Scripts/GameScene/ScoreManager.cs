using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else 
        {
            Instance = this; 
        }

        if(_scoreText == null)
        {
            TryGetComponent(out TextMeshProUGUI textComponent);
            _scoreText = textComponent; 
        }
    }

    public void UpdateScoreText(int ScoreIn)
    {
        _scoreText.text = ScoreIn.ToString();
    }
    public void UpdateHighScore(int HighScoreIn)
    {
        _highScoreText.text = HighScoreIn.ToString();
    }
}