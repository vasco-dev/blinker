using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public int _gameStartScene = 1;
    private int _gameEndScene = 2;
    //public Timer _timer;
    public bool __gameover { get; set; }

    private void Update()
    {
        CheckEndGame();
    }

    public void QuitGame()
    {
        Application.Quit(); 
        //print("O botao funciona");
    }
    public void StartGame()
    {
        SceneManager.LoadScene(_gameStartScene);
        Timer _timer = GetComponent<Timer>();
        //Debug.Log(_timer);
        _timer.__hasSceneLoaded = true;
        __gameover = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(_gameStartScene);
        //print("O botao funciona");
    }

    public void CheckEndGame()
    {
        if (__gameover == true)
        {
            //_gameover = true;
            SceneManager.LoadScene(_gameEndScene);
        }
    }
}
