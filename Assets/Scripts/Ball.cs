using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float ballSpeed = 20f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.MovePosition(new Vector2(0.0f, 0.0f));
        rb2d.AddForce(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * ballSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGoal"))
        {
            ScoreTracker.incrementPlayerScore();
        }
        if (collision.gameObject.CompareTag("OpponentGoal"))
        {
            ScoreTracker.incrementOpponentScore();
        }
        ResetBall();
    }
}
