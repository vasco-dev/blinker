using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashMenuManager : MonoBehaviour
{
    public int _gameStartScene;
    private Timer _Timer;

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(_gameStartScene);
        _Timer.__hasSceneLoaded = true;
    }
}
