using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float ballSpeed = 2.5f;
    public float maxBallSpeed = 100.0f;

    public float batImpulseMagnitude = 10.0f;

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

    private void Update()
    {
        if (rb2d.velocity.magnitude > maxBallSpeed)
        {
            rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxBallSpeed);
        }

        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -18.5f, 18.5f);
        pos.y = Mathf.Clamp(pos.y, -10.5f, 10.5f);
        transform.position = pos;
    }

    private void ResetBall()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.MovePosition(new Vector2(0.0f, 0.0f));

        int serveDirection = Random.Range(0, 4);

        if (serveDirection == 0)
        {
            movementVector = new Vector2(1.0f, 1.0f);
        }
        else if (serveDirection == 1)
        {
            movementVector = new Vector2(-1.0f, 1.0f);
        }
        else if (serveDirection == 2)
        {
            movementVector = new Vector2(1.0f, -1.0f);
        }
        else if (serveDirection == 3)
        {
            movementVector = new Vector2(-1.0f, -1.0f);
        }

        rb2d.AddForce(movementVector * ballSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            var collisionPoint = collision.GetContact(0).point;
            var distanceFromPaddleCentre = collisionPoint.y - collision.collider.attachedRigidbody.position.y; // positive or negative
            var collisionPointPortion = distanceFromPaddleCentre / collision.collider.bounds.size.y;
            var angle = collisionPointPortion * Mathf.PI;
            var impulseX = Mathf.Cos(angle);
            var impulseY = Mathf.Sin(angle);
            rb2d.AddForce(new Vector2(impulseX, impulseY) * batImpulseMagnitude, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerGoal"))
        {
            scoreTracker.incrementOpponentScore();
            ResetBall();
        }
        if (collision.gameObject.CompareTag("OpponentGoal"))
        {
            scoreTracker.incrementPlayerScore();
            ResetBall();
        }
    }
}
