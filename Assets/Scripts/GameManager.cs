using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    public GameObject easyBoard;
    public GameObject hardBoard;
    public GameObject extremeBoard;
    public GameObject[] easyBoardArray;
    public GameObject[] hardBoardArray;
    public GameObject[] extremeBoardArray;
    public GameObject[] targetBoardArray;

    public TextMeshProUGUI currentPlayerText;
    public bool player1Active;
    public bool player1Wins;
    public bool player2Active;
    public bool player2Wins;
    public Material handleMat;
    public bool arrowIsReleased;
    public float currTime;

    private string selectedBoardName;
    public GameObject selectedBoard;

    private string[] board;
    private bool gameFinished; // Flag to indicate if the game has finished

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
        board = new string[9];
        gameFinished = false;
        UpdateGameState(GameState.SelectMode);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.SelectMode:
                ActivateEasyBoard();
                break;
            case GameState.Player1Turn:
                SetPlayer1Turn();
                break;
            case GameState.Player2Turn:
                SetPlayer2Turn();
                break;
            case GameState.Calculate:
                //CalculateWinState();
                break;
            case GameState.Restart:
                break;
            case GameState.GameOver:
                break;
            case GameState.Reset:
                SceneManager.LoadScene("0");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void ActivateEasyBoard()
    {
        easyBoard.SetActive(true);
        hardBoard.SetActive(false);
        extremeBoard.SetActive(false);
        SetCurrentBoardArray();
    }

    public void ActivateHardBoard()
    {
        easyBoard.SetActive(false);
        hardBoard.SetActive(true);
        extremeBoard.SetActive(false);
        SetCurrentBoardArray();
    }

    public void ActivateExtremeBoard()
    {
        easyBoard.SetActive(false);
        hardBoard.SetActive(false);
        extremeBoard.SetActive(true);
        SetCurrentBoardArray();
    }

    public void SetCurrentBoardArray()
    {
        if (easyBoard.activeSelf)
        {
            for (int i = 0; i < easyBoardArray.Length; i++)
            {
                targetBoardArray[i] = easyBoardArray[i];
            }
        }

        if (hardBoard.activeSelf)
        {
            for (int i = 0; i < hardBoardArray.Length; i++)
            {
                targetBoardArray[i] = hardBoardArray[i];
            }
        }

        if (extremeBoard.activeSelf)
        {
            for (int i = 0; i < extremeBoardArray.Length; i++)
            {
                targetBoardArray[i] = extremeBoardArray[i];
            }
        }
    }

    public void StartGame()
    {
        UpdateGameState(GameState.Player1Turn);
    }

    private void SetPlayer1Turn()
    {
        player1Active = true;
        player2Active = false;
        if (!player1Wins && !player2Wins)
        {
            currentPlayerText.text = "Player 1";
        }
    }

    private void SetPlayer2Turn()
    {
        player2Active = true;
        player1Active = false;
        if (!player1Wins && !player2Wins)
        {
            currentPlayerText.text = "Player 2";
        }
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
                if (player1Active)
                {
                    UpdateGameState(GameState.Player2Turn);
                    player1Active = false;
                    player2Active = true;
                }
                else if (player2Active)
                {
                    UpdateGameState(GameState.Player1Turn);
                    player1Active = true;
                    player2Active = false;
                }
            }
        }  
    }

    public void SetSelectedBoard(string name)
    {
        selectedBoardName = name;
        CalculateWinState();
    }

    private void CalculateWinState()
    {
        for (int i = 0; i < targetBoardArray.Length; i++)
        {
            if (targetBoardArray[i].name == selectedBoardName)
            {
                Debug.Log("Why");
                selectedBoard = targetBoardArray[i];
                if (selectedBoard.GetComponent<Board>().currOwner == Board.Owner.Red)
                {
                    board[i] = "1";
                    if (CheckWinCondition("1"))
                    {
                        //winText.text = "Player " + currentPlayer + " wins!";
                        //winText.gameObject.SetActive(true);
                        gameFinished = true;
                        player1Wins = true;
                        currentPlayerText.text = "Player 1 wins!";
                        UpdateGameState(GameState.GameOver);
                    }
                }
                if (selectedBoard.GetComponent<Board>().currOwner == Board.Owner.Blue)
                {
                    board[i] = "2";
                    if (CheckWinCondition("2"))
                    {
                        //winText.text = "Player " + currentPlayer + " wins!";
                        //winText.gameObject.SetActive(true);
                        gameFinished = true;
                        player2Wins = true;
                        currentPlayerText.text = "Player 2 wins!";
                        UpdateGameState(GameState.GameOver);
                    }
                }

                /*else
                {
                    if (player1Active)
                    {
                        UpdateGameState(GameState.Player2Turn);
                        Debug.Log("Switch2");
                    }

                    else if (player2Active)
                    {
                        UpdateGameState(GameState.Player1Turn);
                        Debug.Log("Switch1");
                    }
                }*/
            }

            /*else
            {
                if (player1Active)
                {
                    UpdateGameState(GameState.Player2Turn);
                    Debug.Log("Switch2Out");
                }

                else if (player2Active)
                {
                    UpdateGameState(GameState.Player1Turn);
                    Debug.Log("Switch1Out");
                }
            }*/
        }

        selectedBoard = null;
        selectedBoardName = null;
    }

    bool CheckWinCondition(string player)
    {
        // Check rows
        for (int i = 0; i < 9; i += 3)
        {
            if (board[i] == player && board[i + 1] == player && board[i + 2] == player)
                return true;
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (board[i] == player && board[i + 3] == player && board[i + 6] == player)
                return true;
        }

        // Check diagonals
        if ((board[0] == player && board[4] == player && board[8] == player) ||
            (board[2] == player && board[4] == player && board[6] == player))
            return true;

        return false;
    }

    bool IsBoardFull()
    {
        foreach (string value in board)
        {
            if (string.IsNullOrEmpty(value))
                return false;
        }
        return true;
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
        Reset,
    }
}
