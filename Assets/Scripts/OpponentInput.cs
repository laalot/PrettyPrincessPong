﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInput : MonoBehaviour
{
    //Component references
    private Animator animator;

    [SerializeField]
    internal MovementHandler movementHandler;

    [SerializeField]
    internal Transform ball;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SeekBall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            animator.SetBool("paddleHit", true);
        }
    }

    public void AnimationFinished()
    {
        animator.SetBool("paddleHit", false);
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
