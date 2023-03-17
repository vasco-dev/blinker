using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsRunning { get; private set; } = true;
    public static GameManager Instance { get; private set; }
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
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
