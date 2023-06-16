using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GridUpdate : MonoBehaviour
{

    public GameObject[] player1;
    public GameObject[] player2;
    void Awake()
    {
        GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Calculate)
        {
            for (int i = 0; i < 9; i++) 
            {
                GameObject curr = GameManager.Instance.targetBoardArray[i];
                if (curr.GetComponent<Board>().currOwner == Board.Owner.Red) 
                {
                    player1[i].SetActive(true);
                    player2[i].SetActive(false);
                }
                else if (curr.GetComponent<Board>().currOwner == Board.Owner.Blue)
                {
                    player1[i].SetActive(false);
                    player2[i].SetActive(true);
                }
                else 
                {
                    player1[i].SetActive(false);
                    player2[i].SetActive(false);
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
