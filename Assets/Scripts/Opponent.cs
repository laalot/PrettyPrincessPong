using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    internal Transform ball;

    //Attributes
    [SerializeField]
    public float paddleSpeed;

    private float movementDirection;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SeekBall();
        ApplyMovement();
    }

    private void SeekBall()
    {
        if (ball.position.y > (transform.position.y + 2f))
        {
            movementDirection = 1.0f;
        }
        else if (ball.position.y < (transform.position.y - 2f))
        {
            movementDirection = -1.0f;
        }
        else
        {
            movementDirection = 0.0f;
        }
    }

    private void ApplyMovement()
    {
        rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * paddleSpeed) * Time.fixedDeltaTime);
    }
}
