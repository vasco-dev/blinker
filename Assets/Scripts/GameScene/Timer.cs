using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float __tempoAtualF;
    private int __tempoAtualInt;
    private int __tempoLimite;
    private int _easydifficulty_time = 10;
    private int _mediumdifficulty_time = 35;

    [SerializeField] private TextMeshProUGUI _timerText;
    private int __difficulty;

    public bool __hasSceneLoaded;

    
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
        Debug.Log("Nível Atual: " + __difficulty);
    }

    private void TimerFunction()
    {
        //Debug.Log(__hasSceneLoaded);
        if (__hasSceneLoaded == true)
        {
            //__tempoAtualF += Time.deltaTime;
            __tempoAtualInt = Mathf.RoundToInt(__tempoAtualF);

            if (__tempoAtualInt > __tempoLimite)
            {
                //__gameover = true;
            }
            else
            {
                int __tempoRestante = __tempoLimite - __tempoAtualInt;
                _timerText.text = __tempoRestante.ToString();
                /*if (__gameover == false)
                {
                    _timerText.text = __tempoRestante.ToString();
                }*/
                if (__tempoLimite - __tempoRestante <= _easydifficulty_time)
                {
                    __difficulty = 1;
                }
                else if (__tempoLimite - __tempoRestante <= (_mediumdifficulty_time + _easydifficulty_time)) __difficulty = 2;
                else __difficulty = 3;
            }
        }
        
    }
    
    public int GetCurrentLevel()
    {
        return __difficulty;
    }
}
