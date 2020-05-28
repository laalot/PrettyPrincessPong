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
        if (transform.position.y < ball.position.y)
        {
            movementHandler.setMovementDirection(1.0f);
        }
        else if (transform.position.y > ball.position.y)
        {
            movementHandler.setMovementDirection(-1.0f);
        }
        else
        {
            movementHandler.setMovementDirection(0.0f);
        }
    }
}
