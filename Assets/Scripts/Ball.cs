using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float ballSpeed = 2.5f;
    public float maxBallSpeed = 100.0f;

    public float angleWideness = 1f;

    public AudioClip wallHit;
    public AudioClip paddleHit;

    public Material trailMaterial;
    public Material rainbowTrailMaterial;

    private Vector2 movementVector;

    private ScoreTracker scoreTracker;

    private AudioSource audio;

    private TrailRenderer trail;

    private int numberOfBounces;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movementVector.Normalize();
        scoreTracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
        audio = GetComponent<AudioSource>();
        trail = GetComponent<TrailRenderer>();
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

        UpdateBallAppearance();
    }

    private void ResetBall()
    {
        trail.emitting = false;

        numberOfBounces = 0;

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

        trail.emitting = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            audio.PlayOneShot(wallHit, 1);
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

        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (numberOfBounces < 8)
            {
                numberOfBounces++;
            }

            audio.PlayOneShot(paddleHit, 1);

            var collisionPoint = collision.ClosestPoint(rb2d.position);
            var distanceFromPaddleCentre = collisionPoint.y - collision.attachedRigidbody.position.y; // positive or negative
            var collisionPointPortion = distanceFromPaddleCentre / collision.bounds.size.y; // value between -0.5 and 0.5

            var newVelocityXSign = collision.transform.position.x < rb2d.position.x ? 1 : -1;

            var velocityAngle = collisionPointPortion * Mathf.PI * angleWideness;
            var newVelocityX = Mathf.Cos(velocityAngle);
            var newVelocityY = Mathf.Sin(velocityAngle);
            var currentSpeed = rb2d.velocity.magnitude;
            rb2d.velocity = new Vector2(newVelocityX * newVelocityXSign, newVelocityY) * currentSpeed;
        }
    }

    private void UpdateBallAppearance()
    {
        switch (numberOfBounces)
        {
            case 0:
                trail.material = trailMaterial;
                trail.startColor = Color.white;
                trail.endColor = Color.white;
                break;
            case 1:
                trail.startColor = ConvertRGBToFloat(189, 31, 63);
                trail.endColor = ConvertRGBToFloat(189, 31, 63);
                break;
            case 2:
                trail.startColor = ConvertRGBToFloat(236, 97, 74);
                trail.endColor = ConvertRGBToFloat(236, 97, 74);
                break;
            case 3:
                trail.startColor = ConvertRGBToFloat(244, 176, 60);
                trail.endColor = ConvertRGBToFloat(244, 176, 60);
                break;
            case 4:
                trail.startColor = ConvertRGBToFloat(70, 198, 87);
                trail.endColor = ConvertRGBToFloat(70, 198, 87);
                break;
            case 5:
                trail.startColor = ConvertRGBToFloat(57, 83, 192);
                trail.endColor = ConvertRGBToFloat(57, 83, 192);
                break;
            case 6:
                trail.startColor = ConvertRGBToFloat(102, 59, 147);
                trail.endColor = ConvertRGBToFloat(102, 59, 147);
                break;
            case 7:
                trail.startColor = ConvertRGBToFloat(195, 75, 145);
                trail.endColor = ConvertRGBToFloat(195, 75, 145);
                break;
            case 8:
                trail.material = rainbowTrailMaterial;
                trail.startColor = Color.white;
                trail.endColor = Color.white;
                break;
        }
    }

    private Color ConvertRGBToFloat(int r, int g, int b)
    {
        float rFloat, gFloat, bFloat = 0.0f;
        rFloat = (1 / 255f) * r;
        gFloat = (1 / 255f) * g;
        bFloat = (1 / 255f) * b;

        return new Color(rFloat, gFloat, bFloat);
    }
}
