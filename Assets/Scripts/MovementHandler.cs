using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal Paddle paddle;

    //Component references
    private Rigidbody2D rb2d;

    private float movementDirection;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        ApplyMovementForce();
    }

    public void setMovementDirection(float movementDirection)
    {
        this.movementDirection = movementDirection;
    }

    private void ApplyMovementForce()
    {
        if (movementDirection == 1.0f && paddle.collisionHandler.canMoveUp)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * paddle.paddleSpeed) * Time.fixedDeltaTime);
        }
        if (movementDirection == -1.0f && paddle.collisionHandler.canMoveDown)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * paddle.paddleSpeed) * Time.fixedDeltaTime);
        }
    }
}
