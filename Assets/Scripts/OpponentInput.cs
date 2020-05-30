using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInput : MonoBehaviour
{
    [SerializeField]
    internal MovementHandler movementHandler;

    [SerializeField]
    internal Transform ball;

    private void Update()
    {
        SeekBall();
    }

    private void SeekBall()
    {
        var movementDirection = 0.0f;
        var movementMagnitude = 0.0f;

        if (ball.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            if (transform.position.y < ball.position.y)
            {
                movementDirection = 1.0f;
                movementMagnitude = ball.position.y - transform.position.y;
            }
            else if (transform.position.y > ball.position.y)
            {
                movementDirection = -1.0f;
                movementMagnitude = transform.position.y - ball.position.y;
            }
        }

        movementHandler.SetMovement(movementDirection, movementMagnitude);
    }
}
