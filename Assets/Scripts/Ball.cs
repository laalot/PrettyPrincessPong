using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float ballSpeed = 75.0f;

    private Vector2 movementVector;

    private ScoreTracker scoreTracker;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movementVector.Normalize();
        scoreTracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.MovePosition(new Vector2(0.0f, 0.0f));
        movementVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        rb2d.AddForce(movementVector * ballSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGoal"))
        {
            scoreTracker.incrementOpponentScore();
        }
        if (collision.gameObject.CompareTag("OpponentGoal"))
        {
            scoreTracker.incrementPlayerScore();
        }
        ResetBall();
    }
}
