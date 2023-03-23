using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private HighScoreDisplay _showHighScore;
    private HighScoreEntry _newHighScore;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}