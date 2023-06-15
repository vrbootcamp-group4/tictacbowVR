using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] targetBoardArray;

    public TextMeshProUGUI currentPlayerText;
    public bool player1Active;
    public bool player2Active;
    public Material handleMat;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player1Active = true;
        player2Active = false;
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
    }
}
