using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GridUpdate : MonoBehaviour
{

    public GameObject[] player1;
    public GameObject[] player2;
    public TextMeshProUGUI[] p1text;
    public TextMeshProUGUI[] p2text;

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
                    p1text[i].text = curr.GetComponent<Board>().currVal.ToString();
                }
                else if (curr.GetComponent<Board>().currOwner == Board.Owner.Blue)
                {
                    player1[i].SetActive(false);
                    player2[i].SetActive(true);
                    p2text[i].text = curr.GetComponent<Board>().currVal.ToString();
                }
                else 
                {
                    player1[i].SetActive(false);
                    player2[i].SetActive(false);
                }
            }
        }
    }

}
