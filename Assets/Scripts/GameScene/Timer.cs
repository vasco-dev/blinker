using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float __tempoAtualF;
    private int __tempoAtualInt;
    private int __tempoLimite;
    private int __difficulty;
    public int __tempoRestante;
    private int _easydifficulty_time = 10;
    private int _mediumdifficulty_time = 35;
    public bool __hasSceneLoaded {get; set;}

    [SerializeField] private TextMeshProUGUI _timerText;
    
    private MenuManager _menuManager;

    private void Awake()
    {
        Time.timeScale = 1f;
        __tempoLimite = 60;
        __hasSceneLoaded = false;
    }
    // quando o objeto se tornar ativo significa que a cena carregou
    private void OnEnable()
    {
        __hasSceneLoaded = true;        
    }

    private void Update()
    {
        __tempoAtualF = __tempoAtualF + Time.deltaTime;
        TimerFunction();
        //Debug.Log("Nível Atual: " + __difficulty);
    }

    private void TimerFunction()
    {
        //Debug.Log(__hasSceneLoaded);
        if (__hasSceneLoaded == true)
        {
            __tempoAtualInt = Mathf.RoundToInt(__tempoAtualF);
            //MenuManager _menuManager = GetComponent<MenuManager>();

            if (__tempoAtualInt > __tempoLimite)
            {
                MenuManager _menuManager = GetComponent<MenuManager>();
                _menuManager.__gameover = true;
            }
            else
            {
                __tempoRestante = __tempoLimite - __tempoAtualInt;
                _timerText.text = __tempoRestante.ToString();

                if (__tempoLimite - __tempoRestante <= _easydifficulty_time)
                {
                    __difficulty = 0;
                }
                else if (__tempoLimite - __tempoRestante <= (_mediumdifficulty_time + _easydifficulty_time)) __difficulty = 1;
                else __difficulty = 2;
            }
        }
        
    }
    
    public int GetCurrentLevel()
    {
        return __difficulty;
    }
}
