using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    //should be always 0
    private float _currentTime = 0f;
    //needs to be lower than _currentTime to be set in the code
    private float _currentLevelEnd = -1f;

    [SerializeField] private TextMeshProUGUI _timerText;
    public static Timer Instance { get; private set; }

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

        if (_timerText == null)
        {
            _timerText = GetComponent<TextMeshProUGUI>();
        }

    }
    private void Start()
    {
        StartCoroutine(TimerFunction());
    }


    public void RestartGame()
    {
        _currentTime = 0f;
        _currentLevelEnd = -1f;
    }

    private IEnumerator TimerFunction()
    {
        while (GameManager.Instance.IsRunning)
        {
            if (_currentLevelEnd <= 0f)
            {
                _currentTime = 0f;
                _currentLevelEnd = GameManager.Instance.GetCurrentLevelEndTime();
            }

            _currentTime += Time.deltaTime;

            if (_currentTime >= _currentLevelEnd)
            {                
                GameManager.Instance.UpdateCurrentLevelData();
                _currentLevelEnd = GameManager.Instance.GetCurrentLevelEndTime();
            }

            _timerText.text = ((int)_currentTime).ToString();

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
