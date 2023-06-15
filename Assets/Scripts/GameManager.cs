using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public GameObject[] targetBoardArray;

    public TextMeshProUGUI currentPlayerText;
    public bool player1Active;
    public bool player2Active;
    public Material handleMat;
    public bool arrowIsReleased;
    public float currTime;

    private string selectedBoardName;
    public GameObject selectedBoard;

    private char[] board;

    private void OnCollisionEnter(Collision collision)
    {

    }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.SelectMode);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SelectMode:
                break;
            case GameState.Player1Turn:
                SetPlayer1Turn();
                break;
            case GameState.Player2Turn:
                SetPlayer2Turn();
                break;
            case GameState.Calculate:
                break;
            case GameState.Restart:
                break;
            case GameState.GameOver:
                break;
            case GameState.EndGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void SetSelectedBoard(string name)
    {
        selectedBoardName = name;
        SetSelectedBoardObject();
    }

    private void SetSelectedBoardObject()
    {
        for (int i = 0; i < targetBoardArray.Length; i++)
        {
            if (targetBoardArray[i].name == selectedBoardName)
            {
                selectedBoard = targetBoardArray[i];
                CheckBoardOwner();
            }
        }
    }

    private void CheckBoardOwner()
    {
        if (selectedBoard.GetComponent<Board>().currOwner == Board.Owner.Red)
        {

        }
    }

    private bool CheckWinCondition()
    {
        // Check rows
        for (int i = 0; i < 3; i++)
        {
            if (board[i * 3] != ' ' && board[i * 3] == board[i * 3 + 1] && board[i * 3] == board[i * 3 + 2])
            {
                return true;
            }
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (board[i] != ' ' && board[i] == board[i + 3] && board[i] == board[i + 6])
            {
                return true;
            }
        }

        // Check diagonals
        if (board[0] != ' ' && board[0] == board[4] && board[0] == board[8])
        {
            return true;
        }

        if (board[2] != ' ' && board[2] == board[4] && board[2] == board[6])
        {
            return true;
        }

        return false;
    }

    private void SetPlayer1Turn()
    {
        player1Active = true;
        player2Active = false;
    }

    private void SetPlayer2Turn()
    {
        player2Active = true;
        player1Active = false;
    }

    public void SetArrowReleased(bool isReleased) 
    {
        arrowIsReleased = isReleased;
    } 

    // Update is called once per frame
    void Update()
    {
        if (player1Active)
        {
            handleMat.color = Color.red;
        }

        if (player2Active)
        {
            handleMat.color = Color.blue;
        }
        // After arrow released, begin countdown
        if (arrowIsReleased) 
        {
            currTime -= Time.deltaTime * 1;
            // Check if time is up
            if (currTime <= 0) 
            {
                resetTimer();
                arrowIsReleased = false;
                // Switch turn
                UpdateGameState(GameState.Calculate);
            }
        }  
    }

    public void resetTimer() 
    {
        currTime = 10;
    }

    public enum GameState
    {
        SelectMode,
        Player1Turn,
        Player2Turn,
        Calculate,
        Restart,
        GameOver, 
        EndGame,
    }
}
