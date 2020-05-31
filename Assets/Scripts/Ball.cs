using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float ballSpeed = 2.5f;
    public float rainbowRushAddedSpeed = 4f;
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

    private Color red;
    private Color orange;
    private Color yellow;
    private Color green;
    private Color blue;
    private Color purple;
    private Color violet;

    Gradient whiteGradient = new Gradient();
    Gradient redGradient = new Gradient();
    Gradient orangeGradient = new Gradient();
    Gradient yellowGradient = new Gradient();
    Gradient greenGradient = new Gradient();
    Gradient blueGradient = new Gradient();
    Gradient purpleGradient = new Gradient();
    Gradient violetGradient = new Gradient();

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movementVector.Normalize();
        scoreTracker = GameObject.Find("ScoreTracker").GetComponent<ScoreTracker>();
        audio = GetComponent<AudioSource>();
        trail = GetComponent<TrailRenderer>();

        SetUpColours();
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

        chargeRainbowBall();
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
            numberOfBounces++;

            var speedToAdd = 0f;
            if (numberOfBounces == 8)
            {
                speedToAdd = rainbowRushAddedSpeed;
            }

            audio.PlayOneShot(paddleHit, 1);

            var collisionPoint = collision.ClosestPoint(rb2d.position);
            var distanceFromPaddleCentre = collisionPoint.y - collision.attachedRigidbody.position.y; // positive or negative
            var collisionPointPortion = distanceFromPaddleCentre / collision.bounds.size.y; // value between -0.5 and 0.5

            var newVelocityXSign = collision.transform.position.x < rb2d.position.x ? 1 : -1;

            var velocityAngle = collisionPointPortion * Mathf.PI * angleWideness;
            var newVelocityX = Mathf.Cos(velocityAngle);
            var newVelocityY = Mathf.Sin(velocityAngle);
            var newSpeed = rb2d.velocity.magnitude + speedToAdd;
            rb2d.velocity = new Vector2(newVelocityX * newVelocityXSign, newVelocityY) * newSpeed;
        }
    }

    private void SetUpColours()
    {
        red = ConvertRGBToFloat(189, 31, 63);
        orange = ConvertRGBToFloat(236, 97, 74);
        yellow = ConvertRGBToFloat(244, 176, 60);
        green = ConvertRGBToFloat(70, 198, 87);
        blue = ConvertRGBToFloat(57, 83, 192);
        purple = ConvertRGBToFloat(102, 59, 147);
        violet = ConvertRGBToFloat(195, 75, 145);

        whiteGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        redGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(red, 0.0f), new GradientColorKey(red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        orangeGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(orange, 0.0f), new GradientColorKey(orange, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        yellowGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(yellow, 0.0f), new GradientColorKey(yellow, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        greenGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(green, 0.0f), new GradientColorKey(green, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        blueGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(blue, 0.0f), new GradientColorKey(blue, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        purpleGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(purple, 0.0f), new GradientColorKey(purple, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        violetGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(violet, 0.0f), new GradientColorKey(violet, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
    }

    private void chargeRainbowBall()
    {
        switch (numberOfBounces)
        {
            case 0:
                trail.material = trailMaterial;
                trail.colorGradient = whiteGradient;
                audio.pitch = 2.0f;
                break;
            case 1:
                trail.colorGradient = redGradient;
                break;
            case 2:
                trail.colorGradient = orangeGradient;
                break;
            case 3:
                trail.colorGradient = yellowGradient;
                break;
            case 4:
                trail.colorGradient = greenGradient;
                break;
            case 5:
                trail.colorGradient = blueGradient;
                break;
            case 6:
                trail.colorGradient = purpleGradient;
                break;
            case 7:
                trail.colorGradient = violetGradient;
                break;
            case 8:
                trail.material = rainbowTrailMaterial;
                trail.colorGradient = whiteGradient;
                audio.pitch = 2.5f;
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
