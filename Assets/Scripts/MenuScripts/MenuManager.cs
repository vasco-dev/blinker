using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private int _gameStartScene = 1;

    [SerializeField]
    private int _gameEndScene = 2;
    public static MenuManager Instance { get; private set; }

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
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void StartGame()
    {
        SceneManager.LoadScene(_gameStartScene);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(_gameEndScene);
    }
}
