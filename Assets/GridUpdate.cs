using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GridUpdate : MonoBehaviour
{

    public GameObject[] slots;
    public GameObject[] player1;
    public GameObject[] player2;
    public Board[] tictactoe;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Calculate)
        {
            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 3; j++) 
                {
                    if ()
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
