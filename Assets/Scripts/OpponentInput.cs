using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInput : MonoBehaviour
{
    [SerializeField]
    internal MovementHandler movementHandler;

    [SerializeField]
    internal Transform ball;

    // Update is called once per frame
    void Update()
    {
        SeekBall();
    }

    private void SeekBall()
    {
        if (ball.position.y > (transform.position.y + 2f))
        {
            movementHandler.setMovementDirection(1.0f);
        }
        else if (ball.position.y < (transform.position.y - 2f))
        {
            movementHandler.setMovementDirection(-1.0f);
        }
        else
        {
            movementHandler.setMovementDirection(0.0f);
        }
    }
}
