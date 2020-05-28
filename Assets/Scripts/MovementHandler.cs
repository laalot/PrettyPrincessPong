using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    //Script references
    [SerializeField]
    internal CollisionHandler collisionHandler;

    //Component references
    private Rigidbody2D rb2d;

    private float movementDirection;

    public float movementSpeed = 15.0f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovementForce();
    }

    public void setMovementDirection(float movementDirection)
    {
        this.movementDirection = movementDirection;
    }

    private void ApplyMovementForce()
    {
        if (movementDirection == 1.0f && collisionHandler.canMoveUp)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * movementSpeed) * Time.fixedDeltaTime);
        }
        if (movementDirection == -1.0f && collisionHandler.canMoveDown)
        {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, movementDirection * movementSpeed) * Time.fixedDeltaTime);
        }
    }
}
