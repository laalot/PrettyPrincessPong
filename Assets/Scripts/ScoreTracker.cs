using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public int playerScore = 0;
    public int opponentScore = 0;

    private Text text;

    void Awake()
    {
        text = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void Update()
    {
        text.text = (playerScore + " | " + opponentScore).ToString();
    }

    public void incrementPlayerScore()
    {
        playerScore++;
    }

    public void incrementOpponentScore()
    {
        opponentScore++;
    }
}
