using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Board : MonoBehaviour
{

    public enum BoardType { Easy, Hard, Extreme }

    public enum Owner { Red, Blue, Empty }

    public BoardType type;
    public Owner currOwner;
    public Owner currPlayer;
    public TextMeshProUGUI display;
    public int id;
    public int currVal = 0;

    // Start is called before the first frame update
    /*
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Player1Active)
        {
            currPlayer = Owner.Red;
        }

        if (state == GameManager.GameState.Player2Active)
        {
            currPlayer = Owner.Blue;
        }
    }
    
    */
    public void UpdateOwner(int newValue)
    {
        if (newValue > currVal) 
        {
            currVal = newValue;
            currOwner = currPlayer;
            // Update the owner status by sending a command to the gameManager
            display.text = currVal.ToString();
        }

    }
}
